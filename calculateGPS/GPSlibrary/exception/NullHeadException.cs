using System;

namespace GPSlibrary.exception
{
    public class NullHeadException : Exception
    {
        public NullHeadException()
        {
        }

        public NullHeadException(string message)
            : base(message)
        {
        }

        public NullHeadException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
