#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem.Internal
{
    public class ResolveInfo
    {
        internal ResolveInfo(string fieldName, IReadOnlyList<FieldSyntax> fieldNodes, IGraphQLType returnType,
            IFieldsContainer parentType, ResponsePath path, Schema schema,
            IReadOnlyDictionary<string, FragmentDefinitionSyntax> fragments, OperationDefinitionSyntax operation,
            IReadOnlyDictionary<string, object> variableValues, object rootValue)
        {
            FieldName = Check.NotNull(fieldName, nameof(fieldName));
            FieldNodes = Check.NotNull(fieldNodes, nameof(fieldNodes));
            ReturnType = Check.NotNull(returnType, nameof(returnType));
            ParentType = Check.NotNull(parentType, nameof(parentType));
            Path = Check.NotNull(path, nameof(path));
            Schema = Check.NotNull(schema, nameof(schema));
            Fragments = Check.NotNull(fragments, nameof(fragments));
            Operation = operation;
            VariableValues = variableValues;
            RootValue = rootValue;
        }

        [NotNull]
        public string FieldName { get; }

        [NotNull]
        public IReadOnlyList<FieldSyntax> FieldNodes { get; }

        [NotNull]
        public IGraphQLType ReturnType { get; }

        [NotNull]
        public IFieldsContainer ParentType { get; }

        [NotNull]
        public ResponsePath Path { get; }

        public object RootValue { get; }

        [NotNull]
        public Schema Schema { get; }

        [NotNull]
        public IReadOnlyDictionary<string, FragmentDefinitionSyntax> Fragments { get; }

        public OperationDefinitionSyntax Operation { get; }
        public IReadOnlyDictionary<string, object> VariableValues { get; }
    }
}