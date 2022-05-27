using System;

namespace GPSlibrary.exception
{
    public class WrongCRCException : Exception
    {
        public WrongCRCException()
        {
        }

        public WrongCRCException(string message)
            : base(message)
        {
        }

        public WrongCRCException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
