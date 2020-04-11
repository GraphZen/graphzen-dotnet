// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public static class CodeGenStringBuilderExtensions
    {
        public static void AddCommonUsings(this StringBuilder csharp) =>
            csharp.AppendLine(CodeGenConstants.CommonUsings);

        public static void Namespace(this StringBuilder csharp, string name, Action<StringBuilder> @namespace) =>
            csharp.Block($"namespace {name} {{", "}", @namespace);

        private static void Class(this StringBuilder csharp, string qualifiers, string name,
            Action<StringBuilder> @class) => csharp.Block($"public {qualifiers} class {name} {{", "}", @class);

        public static void AbstractPartialClass(this StringBuilder csharp, string name, Action<StringBuilder> @class) =>
            csharp.Class("abstract partial", name, @class);

        public static void PartialClass(this StringBuilder csharp, string name, Action<StringBuilder> @class) =>
            csharp.Class("partial", name, @class);


        public static void StaticClass(this StringBuilder csharp, string name, Action<StringBuilder> @class) =>
            csharp.Class("static", name, @class);

        public static void Region(this StringBuilder csharp, string name, Action<StringBuilder> region) =>
            csharp.Block($"#region {name}", "#endregion", region);

        public static void Block(this StringBuilder csharp, string open, string close,
            Action<StringBuilder> content)
        {
            csharp.AppendLine(open);
            content(csharp);
            csharp.AppendLine(close);
        }


        public static void WriteToFile(this StringBuilder csharp, string path) =>
            CodeGenHelpers.WriteFile(path, csharp.ToString());

        public static void WriteToFile(this StringBuilder csharp, string project, string name) =>
            CodeGenHelpers.WriteFile($"./src/Linked/{project}/{name}.Generated.cs", csharp.ToString());
    }
}