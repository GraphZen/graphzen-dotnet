using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public class DuplicateNameException : GraphQLException
    {
        public DuplicateNameException(string message) : base(message)
        {
        }
    }
}