// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.QueryEngine.Validation
{
    public interface IQueryValidator
    {
        IReadOnlyCollection<GraphQLServerError> Validate(Schema schema, DocumentSyntax query);
    }
}