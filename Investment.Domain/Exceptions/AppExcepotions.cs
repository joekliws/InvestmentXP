using System;

namespace Investment.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)  { }
    }

    public class InvalidPropertyException : Exception
    {
        public InvalidPropertyException(string message) : base(message) { }
    }

    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException(string message) : base(message) { }
    }
}
