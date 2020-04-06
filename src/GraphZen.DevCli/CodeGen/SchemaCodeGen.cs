// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Text;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
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


    public static class SchemaCodeGen
    {
        public static void AddEnum(this StringBuilder cs, EnumType @enum)
        {
            cs.AppendLine($"public enum {@enum.Name} {{");
            foreach (var (name, value) in @enum.Values)
            {
                if (value.Description != null) cs.AppendLine($"/// <summary>{value.Description}</summary>");

                cs.AppendLine($"{name},");
            }
            cs.Append("}");
        }
        public static void PrintEnum(EnumType @enum, string @namespace, string path)
        {
            var csharp = CSharpStringBuilder.Create();
            csharp.Namespace(@namespace, cs =>
            {

            });
        }
    }
}