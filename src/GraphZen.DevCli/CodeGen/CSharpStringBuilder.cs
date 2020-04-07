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