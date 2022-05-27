using System;

namespace GPSlibrary.exception
{
    public class NullMonthException : Exception
    {
        public NullMonthException()
        {
        }

        public NullMonthException(string message)
            : base(message)
        {
        }

        public NullMonthException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
