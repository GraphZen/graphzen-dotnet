using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public static class CSharpStringBuilderExtensions
    {
        public static void WriteToFile(this StringBuilder csharp, string projectName, string fileName)
        {
            CodeGenHelpers.WriteFile($"./src/Linked/{projectName}/{fileName}.Generated.cs", csharp.ToString());
        }


        public static void AddCommonUsings(this StringBuilder csharp) =>
            csharp.AppendLine(CodeGenConstants.CommonUsings);

        public static void Namespace(this StringBuilder csharp, string name, Action<StringBuilder> @namespace)
        {
            csharp.AppendLine($"namespace {name} {{");
            @namespace(csharp);
            csharp.AppendLine("}");
        }


        public static void Region(this StringBuilder csharp, string name, Action<StringBuilder> region)
        {
            csharp.AppendLine($"namespace {name} {{");
            region(csharp);
            csharp.AppendLine("}");
        }


        public static void Class(this StringBuilder csharp, string name, Action<StringBuilder> @class)
        {
            csharp.AppendLine($"public class {name} {{");
            @class(csharp);
            csharp.AppendLine("}");
        }

        public static void StaticClass(this StringBuilder csharp, string name, Action<StringBuilder> @class)
        {
            csharp.AppendLine($"public static class {name} {{");
            @class(csharp);
            csharp.AppendLine("}");
        }

        public static void AbstractPartialClass(this StringBuilder csharp, string name, Action<StringBuilder> @class)
        {
            csharp.AppendLine($"public abstract partial class {name} {{");
            @class(csharp);
            csharp.AppendLine("}");
        }

        public static void PartialClass(this StringBuilder csharp, string name, Action<StringBuilder> @class)
        {
            csharp.AppendLine($"public partial class {name} {{");
            @class(csharp);
            csharp.AppendLine("}");
        }
    }
}