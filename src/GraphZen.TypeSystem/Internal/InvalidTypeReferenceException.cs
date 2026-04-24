// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace GraphZen.Internal;

public class InvalidTypeReferenceException : Exception
{
    public InvalidTypeReferenceException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
