// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Validation;
using GraphZen.TypeSystem;
using GraphZen.Utilities;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.QueryEngine.Validation
{
    public class QueryValidator : IQueryValidator
    {
        public QueryValidator(IReadOnlyCollection<ValidationRule> rules = null)
        {
            Rules = rules ?? QueryValidationRules.SpecifiedQueryRules;
        }


        private IReadOnlyCollection<ValidationRule> Rules { get; }

        public IReadOnlyCollection<GraphQLServerError> Validate(Schema schema, DocumentSyntax query)
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