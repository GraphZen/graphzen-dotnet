// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.LanguageModel;

namespace GraphZen;

public interface IDocumentValidator
{
    IEnumerable<GraphQLServerError> Validate(DocumentSyntax schemaDocument,
        DocumentSyntax? initialSchemaDocument = null);
}
