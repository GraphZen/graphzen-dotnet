// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Validation;
using GraphZen.TypeSystem;

namespace GraphZen.QueryEngine.Validation
{
    public class QueryValidator : IQueryValidator
    {
        public QueryValidator(IReadOnlyCollection<ValidationRule> rules = null)
        {
            Rules = rules ?? QueryValidationRules.SpecifiedQueryRules;
        }

        [NotNull]
        [ItemNotNull]
        private IReadOnlyCollection<ValidationRule> Rules { get; }

        public IReadOnlyCollection<GraphQLError> Validate(Schema schema, DocumentSyntax query)
        {
            GraphQLSyntaxWalker validationVisitor = null;
            var validationContext = new QueryValidationContext(schema, query, new TypeInfo(schema),
                // ReSharper disable once AccessToModifiedClosure
                new Lazy<GraphQLSyntaxWalker>(() => validationVisitor));
            var ruleVisitors = Rules.Select(rule => rule(validationContext)).ToArray();
            validationVisitor = new ParallelValidationVisitor(validationContext, ruleVisitors);
            validationVisitor.Visit(query);
            return validationContext.GetErrors();
        }
    }
}