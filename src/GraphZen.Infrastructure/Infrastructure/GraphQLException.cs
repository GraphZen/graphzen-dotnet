using System;
using System.Collections.Generic;
using System.Text;

namespace GraphZen.Infrastructure
{
    public abstract class GraphQLException : Exception
    {
        protected GraphQLException(string message) : base(message)
        {

        }

        protected GraphQLException(string message, Exception? innerException) : base(message, innerException)
        {

        }
    }
    public class DuplicateNameException : GraphQLException
    {
        public DuplicateNameException(string message) : base(message)
        {
        }
    }
}
