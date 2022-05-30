using System;

namespace GPSlibrary.exception
{
    public class TooMuchHighException : Exception
    {
        public TooMuchHighException()
        {
        }

        public TooMuchHighException(string message)
            : base(message)
        {
        }

        public TooMuchHighException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
