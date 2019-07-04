// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Infrastructure.Extensions;
using GraphZen.Language;
using GraphZen.TypeSystem;
using GraphZen.Utilities;
using JetBrains.Annotations;

namespace GraphZen.Validation
{
    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    public class QueryValidationContext : ValidationContext
    {
        [NotNull] [ItemNotNull] private readonly Lazy<IReadOnlyDictionary<string, FragmentDefinitionSyntax>> _fragments;


        public QueryValidationContext(Schema schema, DocumentSyntax ast, [NotNull] TypeInfo typeInfo,
            Lazy<GraphQLSyntaxWalker> parentVisitor) : base(
            Check.NotNull(ast, nameof(ast)), Check.NotNull(parentVisitor, nameof(parentVisitor))
        )
        {
            Schema = Check.NotNull(schema, nameof(schema));
            TypeInfo = Check.NotNull(typeInfo, nameof(typeInfo));
            _fragments = new Lazy<IReadOnlyDictionary<string, FragmentDefinitionSyntax>>(() =>
                ast.Definitions.OfType<FragmentDefinitionSyntax>()
                    // ReSharper disable once PossibleNullReferenceException
                    .ToReadOnlyDictionaryIgnoringDuplicates(_ => _.Name.Value));
        }

        [NotNull]
        public Schema Schema { get; }


        [NotNull]
        public IReadOnlyDictionary<string, FragmentDefinitionSyntax> Fragments => _fragments.Value;

        [NotNull]
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