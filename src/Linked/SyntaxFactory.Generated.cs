

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;


#nullable enable

namespace GraphZen.LanguageModel
{
    public static partial class SyntaxFactory
    {
        public static ArgumentSyntax Argument(GraphZen.LanguageModel.NameSyntax name, GraphZen.LanguageModel.StringValueSyntax? description, GraphZen.LanguageModel.ValueSyntax value, GraphZen.LanguageModel.SyntaxLocation? location = null) => new ArgumentSyntax(name, description, value, location);
        public static OperationTypeDefinitionSyntax OperationTypeDefinition(GraphZen.LanguageModel.OperationType operationType, GraphZen.LanguageModel.NamedTypeSyntax type, GraphZen.LanguageModel.SyntaxLocation? location = null) => new OperationTypeDefinitionSyntax(operationType, type, location);
        public static SchemaDefinitionSyntax SchemaDefinition(System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.OperationTypeDefinitionSyntax>? operationTypes = null, System.Collections.Generic.IReadOnlyList<GraphZen.LanguageModel.DirectiveSyntax>? directives = null, GraphZen.LanguageModel.SyntaxLocation? location = null) => new SchemaDefinitionSyntax(operationTypes, directives, location);
    }
}
