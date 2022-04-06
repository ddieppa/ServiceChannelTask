namespace ServiceChannel.Test.Domain.Exceptions;

public class InvalidLocationException : Exception
{
    public InvalidLocationException(string message) : base(message)
    {
    }
}

