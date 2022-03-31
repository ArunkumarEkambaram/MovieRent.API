using System;

namespace MovieRent.API.CustomExceptions
{
    public class MovieException : ApplicationException
    {
        public MovieException() : base()
        {

        }
        public MovieException(string message) : base(message) { }
    }
}
