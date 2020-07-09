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
    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    public class QueryValidationContext : ValidationContext
    {
        private readonly Lazy<IReadOnlyDictionary<string, FragmentDefinitionSyntax>> _fragments;


        public QueryValidationContext(Schema schema, DocumentSyntax ast, TypeInfo typeInfo,
            Lazy<GraphQLSyntaxWalker> parentVisitor) : base(
            Check.NotNull(ast, nameof(ast)), Check.NotNull(parentVisitor, nameof(parentVisitor))
        )
        {
            Schema = Check.NotNull(schema, nameof(schema));
            TypeInfo = Check.NotNull(typeInfo, nameof(typeInfo));
            _fragments = new Lazy<IReadOnlyDictionary<string, FragmentDefinitionSyntax>>(() =>
                ast.Definitions.OfType<FragmentDefinitionSyntax>()
                    .ToReadOnlyDictionaryIgnoringDuplicates(_ => _.Name.Value));
        }


        public Schema Schema { get; }


        public IReadOnlyDictionary<string, FragmentDefinitionSyntax> Fragments => _fragments.Value;


        public TypeInfo TypeInfo { get; }

        public Directive Directive => TypeInfo.Directive;

        public Argument Argument => TypeInfo.Argument;


        public override void Enter(SyntaxNode node)
        {
            TypeInfo.Enter(node);
        }

        public override void Leave(SyntaxNode node)
        {
            TypeInfo.Leave(node);
        }

        public ICompositeType GetParentType() => TypeInfo.GetParentType();

        public Field GetFieldDef() => TypeInfo.GetField();

        public IGraphQLType OutputType() => TypeInfo.GetOutputType();

        public IGraphQLType GetInputType() => TypeInfo.GetInputType();

        public IGraphQLType GetParentInputType() => TypeInfo.GetParentInputType();
    }
}