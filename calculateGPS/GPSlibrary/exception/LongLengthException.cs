using System;

namespace GPSlibrary.exception
{
    public class LongLengthException : Exception
    {
        public LongLengthException()
        {
        }

        public LongLengthException(string message)
            : base(message)
        {
        }

        public LongLengthException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
