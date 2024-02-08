using blog__dotnet_clean_architecture.Domain.Exceptions;

namespace blog__dotnet_clean_architecture.Domain.Entities;

public struct Description
{
    private readonly string _value;

    public Description(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new DomainException("The description cannot be empty");
        }
        _value = value;
    }

    public override string ToString() => _value;

    public static implicit operator string(Description description) => description.ToString();

    public static implicit operator Description(string value) => new Description(value);
}