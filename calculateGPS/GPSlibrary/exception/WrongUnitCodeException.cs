using System;

namespace GPSlibrary.exception
{
    public class WrongUnitCodeException : Exception
    {
        public WrongUnitCodeException()
        {
        }

        public WrongUnitCodeException(string message)
            : base(message)
        {
        }

        public WrongUnitCodeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
