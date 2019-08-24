// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System;
using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel.Validation
{
    public class DocumentValidationContext : ValidationContext
    {
        public DocumentValidationContext( DocumentSyntax schema, DocumentSyntax initialSchema,
            Lazy<GraphQLSyntaxWalker> parentVisitor) : base(schema,
            Check.NotNull(parentVisitor, nameof(parentVisitor))
        )
        {
            InitialSchema = initialSchema;
        }

        
        public DocumentSyntax InitialSchema { get; }

        
        public DocumentSyntax Schema => AST;
    }
}