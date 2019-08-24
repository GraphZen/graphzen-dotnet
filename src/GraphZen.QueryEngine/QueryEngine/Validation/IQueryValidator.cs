// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem;

namespace GraphZen.QueryEngine.Validation
{
    public interface IQueryValidator
    {
        
        
        IReadOnlyCollection<GraphQLError> Validate(Schema schema, DocumentSyntax query);
    }
}