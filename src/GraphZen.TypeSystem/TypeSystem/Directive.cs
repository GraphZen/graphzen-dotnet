// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLName("__Directive")]
    [Description("A Directive provides a way to describe alternate runtime execution and " +
                 "type validation behavior in a GraphQL document." +
                 "\n\nIn some cases, you need to provide options to alter GraphQL's " +
                 "execution behavior in ways field arguments will not suffice, such as " +
                 "conditionally including or skipping a field. Directives provide this by " +
                 "describing additional information to the executor.")]
    public partial class Directive : Member, IDirective
    {
        private readonly Lazy<DirectiveDefinitionSyntax> _syntax;

        public Directive(string name, string? description, IEnumerable<IArgumentDefinition>? arguments, bool repeatable,
            IReadOnlyCollection<DirectiveLocation> locations, Type? clrType, Schema schema) : base(schema)
        {
            Name = name;
            IsSpec = name.IsSpecDirective();
            Description = description;
            ClrType = clrType;
            Locations = locations;
            IsRepeatable = repeatable;

            // arguments = arguments != null ? Enumerable.Empty<IArgumentDefinition>();
            // ReSharper disable once PossibleNullReferenceException
            Arguments = new ReadOnlyDictionary<string, Argument>(arguments.ToDictionary(_ => _.Name,
                _ => Argument.From(_, this)));
            _syntax = new Lazy<DirectiveDefinitionSyntax>(() =>
            {
                return new DirectiveDefinitionSyntax(SyntaxFactory.Name(Name),
                    Locations.Select(DirectiveLocationHelper.ToStringValue).Select(_ => SyntaxFactory.Name(_))
                        .ToArray(),
                    Arguments.Values.ToSyntaxNodes<InputValueDefinitionSyntax>().ToList(),
                    SyntaxHelpers.Description(Description)
                );
            });
        }

        [GraphQLIgnore]
        [GenDictionaryAccessors(nameof(Argument.Name), nameof(Argument))]
        public IReadOnlyDictionary<string, Argument> Arguments { get; }

        public string Name { get; }


        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        [GraphQLName("args")]
        public IEnumerable<Argument> GetArguments() => Arguments.Values;

        [GraphQLIgnore]
        IEnumerable<IArgumentDefinition> IArgumentsDefinition.GetArguments() => GetArguments();


        [GraphQLIgnore]
        public static Directive From(IDirectiveDefinition definition, Schema schema)
        {
            Check.NotNull(definition, nameof(definition));
            return new Directive(definition.Name, definition.Description, definition.GetArguments(),
                definition.IsRepeatable, definition.Locations, definition.ClrType, schema);
        }

        public IReadOnlyCollection<DirectiveLocation> Locations { get; }

        [GraphQLIgnore]
        public bool HasLocation(DirectiveLocation location) => Locations.Contains(location);

        [GraphQLIgnore] public Type? ClrType { get; }

        [GraphQLCanBeNull] public string? Description { get; }

        public override string ToString() => ClrType != null && ClrType.Name != Name
            ? $"directive {Name} (CLR {ClrType.GetClrTypeKind()}: {ClrType.Name})"
            : $"directive {Name}";

        IEnumerable<IMemberDefinition> IMemberParentDefinition.Children() => Children();

        public IEnumerable<IMember> Children() => GetArguments();

        [GraphQLIgnore] public bool IsSpec { get; }
        public bool IsRepeatable { get; }
    }
}