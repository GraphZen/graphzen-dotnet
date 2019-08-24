#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Validation;

namespace GraphZen
{
    public class DocumentValidator : IDocumentValidator
    {
        public DocumentValidator([CanBeNull] IReadOnlyCollection<ValidationRule> rules)
        {
            Rules = rules ?? DocumentValidationRules.SpecifiedSchemaRules;
        }

        [NotNull]
        [ItemNotNull]
        private IReadOnlyCollection<ValidationRule> Rules { get; }

        public IEnumerable<GraphQLError> Validate(DocumentSyntax schemaDocument,
            DocumentSyntax initialSchemaDocument = null)
        {
            Check.NotNull(schemaDocument, nameof(schemaDocument));
            schemaDocument = schemaDocument.WithSpecDefinitions();
            GraphQLSyntaxWalker validationVisitor = null;
            var validationContext = new DocumentValidationContext(schemaDocument, initialSchemaDocument,
                // ReSharper disable once AccessToModifiedClosure
                new Lazy<GraphQLSyntaxWalker>(() => validationVisitor));
            var ruleVisitors = Rules.Select(rule => rule(validationContext)).ToArray();
            validationVisitor = new ParallelValidationVisitor(validationContext, ruleVisitors);
            validationVisitor.Visit(schemaDocument);
            return validationContext.GetErrors();
        }
    }
}