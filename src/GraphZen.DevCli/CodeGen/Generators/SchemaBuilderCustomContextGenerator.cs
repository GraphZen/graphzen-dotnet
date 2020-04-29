// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Text;
using GraphZen.CodeGen.CodeGenFx.Generators;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public class SchemaBuilderCustomContextGenerator : PartialTypeGenerator
    {
        public SchemaBuilderCustomContextGenerator() : base(typeof(SchemaBuilder<>))
        {
        }

        public override void Apply(StringBuilder csharp)
        {
            csharp.AppendLine($"// hello {TargetType} ");
        }
    }
}