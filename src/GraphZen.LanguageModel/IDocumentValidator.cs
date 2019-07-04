// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;


namespace GraphZen.Validation
{
    public interface IDocumentValidator
    {
        [NotNull]
        [ItemNotNull]
        IEnumerable<GraphQLError> Validate(DocumentSyntax schemaDocument, DocumentSyntax initialSchemaDocument = null);
    }
}