// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;


namespace GraphZen.Validation
{
    public class DocumentValidationContext : ValidationContext
    {
        public DocumentValidationContext([NotNull] DocumentSyntax schema, DocumentSyntax initialSchema,
            Lazy<GraphQLSyntaxWalker> parentVisitor) : base(schema,
            Check.NotNull(parentVisitor, nameof(parentVisitor))
        )
        {
            InitialSchema = initialSchema;
        }

        [CanBeNull]
        public DocumentSyntax InitialSchema { get; }

        [NotNull]
        public DocumentSyntax Schema => AST;
    }
}