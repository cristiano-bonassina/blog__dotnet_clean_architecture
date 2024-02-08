using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using blog__dotnet_clean_architecture.Application;
using blog__dotnet_clean_architecture.Application.Data.Repositories;
using blog__dotnet_clean_architecture.Domain;
using blog__dotnet_clean_architecture.Domain.Entities;
using blog__dotnet_clean_architecture.Infrastructure.Data;
using blog__dotnet_clean_architecture.Infrastructure.Data.Repositories;
using blog__dotnet_clean_architecture.Infrastructure.Data.Schema;
using blog__dotnet_clean_architecture.Infrastructure.Data.Serialization;
using blog__dotnet_clean_architecture.Resources;
using FluentValidation;
using LogicArt.Common.FluentValidation;
using LogicArt.Common.Localization.Extensions;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mapper = blog__dotnet_clean_architecture.Application.Data.Mapping.Mapper;

namespace blog__dotnet_clean_architecture.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureApiServices(this IServiceCollection services)
    {
        services.AddHostedService<DatabaseMigrationService>();

        return services
            .ConfigureLocalization(typeof(ResourcesAnchor).Assembly)
            .ConfigureDatabase()
            .ConfigureJsonSerializer()
            .ConfigureMappings()
            .ConfigureMediator()
            .ConfigureRepositories()
            .ConfigureValidation();
    }

    private static IServiceCollection ConfigureDatabase(this IServiceCollection services)
    {
        return services.AddDbContextPool<AppDbContext>((provider, options) =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString("Default") ?? throw new Exception("Invalid database connection string");
            options.UseSqlite(connectionString)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
        });
    }

    private static IServiceCollection ConfigureMappings(this IServiceCollection services)
    {
        services.AddSingleton<TypeAdapterConfig>(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, ServiceMapper>();
        services.AddScoped<LogicArt.Common.IMapper, Mapper>();
        TypeAdapterConfig.GlobalSettings.Scan(typeof(ApplicationAnchor).Assembly);

        return services;
    }

    private static IServiceCollection ConfigureMediator(this IServiceCollection services)
    {
        return services.AddMediatR(x =>
        {
            x.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            x.RegisterServicesFromAssemblyContaining<ApplicationAnchor>();
        });
    }

    private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        var domainAssembly = typeof(DomainAnchor).Assembly;
        var entityTypes = domainAssembly.GetTypes()
            .Where(type => type is { IsAbstract: false, IsGenericTypeDefinition: false })
            .Where(type => typeof(IAggregateRoot).IsAssignableFrom(type));
        foreach (var entityType in entityTypes)
        {
            var typeArguments = new[] { entityType };

            var repositoryServiceType = typeof(IRepository<>).MakeGenericType(typeArguments);
            var repositoryImplementationType = typeof(Repository<>).MakeGenericType(typeArguments);
            services.Add(new ServiceDescriptor(
                serviceType: repositoryServiceType,
                implementationType: repositoryImplementationType,
                lifetime: ServiceLifetime.Scoped));

            var readonlyRepositoryServiceType = typeof(IReadOnlyRepository<>).MakeGenericType(typeArguments);
            var readonlyRepositoryImplementationType = typeof(ReadOnlyRepository<>).MakeGenericType(typeArguments);
            services.Add(new ServiceDescriptor(
                serviceType: readonlyRepositoryServiceType,
                implementationType: readonlyRepositoryImplementationType,
                lifetime: ServiceLifetime.Scoped));
        }

        return services;
    }

    private static IServiceCollection ConfigureJsonSerializer(this IServiceCollection services)
    {
        return services.Configure<JsonSerializerOptions>(options =>
        {
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.Converters.Add(new JsonStringEnumConverter());
            options.Converters.Add(new IdJsonConverter());
        });
    }

    private static IServiceCollection ConfigureValidation(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ApplicationAnchor).Assembly;
        var validationTypes = applicationAssembly.GetTypes()
            .Where(type => type is { IsAbstract: false, IsGenericTypeDefinition: false })
            .Where(type => typeof(IValidator).IsAssignableFrom(type))
            .ToList();
        validationTypes.ForEach(validationType => services.AddScoped(validationType));

        return services;
    }
}
