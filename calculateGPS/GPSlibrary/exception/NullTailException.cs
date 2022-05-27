using System;

namespace GPSlibrary.exception
{
    public class NullTailException : Exception
    {
        public NullTailException()
        {
        }

        public NullTailException(string message)
            : base(message)
        {
        }

        public NullTailException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
