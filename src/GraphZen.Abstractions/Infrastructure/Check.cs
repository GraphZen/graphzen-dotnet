// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    [DebuggerStepThrough]
    public static class Check
    {
        [ContractAnnotation("value:null => halt")]
        [return: NotNull]
        public static T NotNull<T>([NotNull] [NoEnumeration] T value, [InvokerParameterName] string parameterName)
        {
            if (value is null) throw new ArgumentNullException(parameterName);
            return value;
        }
    }
}