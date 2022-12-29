namespace SequelFilter
{
    using System;

    public class ParseException : Exception
    {
        public ParseException(string errorMessage)
            : base(errorMessage)
        { }
    }
}
