// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Text;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public static class CSharpStringBuilder
    {
        public static StringBuilder Create()
        {
            var sb = new StringBuilder();
            sb.AppendLine("#nullable enable");
            sb.AddCommonUsings();
            return sb;
        }
    }
}