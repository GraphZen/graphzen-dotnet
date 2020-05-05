using System;
using System.Collections.Generic;
using System.Text;
using GraphZen.TypeSystem;

namespace GraphZen.Infrastructure
{
    public static class TypeSystemExceptionMessages
    {
        public static class InvalidNameException
        {
            public static string InvalidNameAnnotation(Type clrType, string name, TypeKind kind)
            {
                return "";
            }
        }

    }
}
