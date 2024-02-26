namespace Cf.Dotnet.Architecture.Domain.Exceptions;

public class OrderingDomainException : ApplicationException
{
    public OrderingDomainException()
    {
    }

    public OrderingDomainException(string message)
        : base(message)
    {
    }

    public OrderingDomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}