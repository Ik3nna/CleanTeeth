namespace CleanTeeth.Application.Common.Exceptions;

public class MediatorException : Exception
{
    public MediatorException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
