// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics;
using GraphZen.Infrastructure;

namespace GraphZen.Infrastructure;

[DebuggerStepThrough]
internal static class Check
{
    [ContractAnnotation("value:null => halt")]
    [return: NotNull]
    public static T NotNull<T>([NotNull] [NoEnumeration] T value, [InvokerParameterName] string parameterName)
    {
        if (value is null) throw new ArgumentNullException(parameterName);
        return value;
    }
}