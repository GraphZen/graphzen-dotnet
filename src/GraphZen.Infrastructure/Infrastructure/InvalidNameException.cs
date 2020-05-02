using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public class InvalidNameException : GraphQLException
    {
        public InvalidNameException(string message) : base(message)
        {
        }
    }
}