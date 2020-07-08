// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public abstract class Member : ISyntaxConvertable, IMember
    {
        protected Member(Schema schema)
        {
            Schema = schema;
        }

        [GraphQLIgnore]
        public abstract SyntaxNode ToSyntaxNode();

        ISchemaDefinition IMemberDefinition.Schema => Schema;

        [GraphQLIgnore] public Schema Schema { get; }
    }
}