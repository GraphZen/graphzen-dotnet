// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.Language;
using JetBrains.Annotations;

namespace GraphZen.Validation
{
    public interface ISchemaValidator
    {
        [NotNull]
        [ItemNotNull]
        IEnumerable<GraphQLError> Validate(DocumentSyntax schemaDocument, DocumentSyntax initialSchemaDocument = null);
    }
}