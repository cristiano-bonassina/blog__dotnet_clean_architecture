using System;

namespace blog__dotnet_clean_architecture.Domain.Exceptions;

public class DomainException(string message) : Exception(message)
{
}
