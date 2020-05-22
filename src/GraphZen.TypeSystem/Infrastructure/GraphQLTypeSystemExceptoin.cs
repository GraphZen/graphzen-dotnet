using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public class InvalidTypeException : GraphQLException
    {
        public InvalidTypeException([NotNull] string message) : base(message)
        {
        }

        public InvalidTypeException([NotNull] string message, [CanBeNull] Exception? innerException) : base(message, innerException)
        {
        }
    }
}
