// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Language;
using GraphZen.Language.Internal;
using JetBrains.Annotations;
using static GraphZen.Language.SyntaxFactory;


namespace GraphZen.Types
{
    [GraphQLName("__Directive")]
    [Description("A Directive provides a way to describe alternate runtime execution and " +
                 "type validation behavior in a GraphQL document." +
                 "\n\nIn some cases, you need to provide options to alter GraphQL's " +
                 "execution behavior in ways field arguments will not suffice, such as " +
                 "conditionally including or skipping a field. Directives provide this by " +
                 "describing additional information to the executor.")]
    public class Directive : Member, IDirective
    {
        [NotNull] [ItemNotNull] private readonly Lazy<DirectiveDefinitionSyntax> _syntax;

        public Directive(string name, string description, IReadOnlyList<DirectiveLocation> locations,
            IEnumerable<IArgumentDefinition> arguments, TypeResolver typeResolver)
        {
            Name = Check.NotNull(name, nameof(name));
            Description = description;
            Locations = Check.NotNull(locations, nameof(locations));

            arguments = arguments ?? Enumerable.Empty<IArgumentDefinition>();
            // ReSharper disable once PossibleNullReferenceException
            Arguments = new ReadOnlyDictionary<string, Argument>(arguments.ToDictionary(_ => _.Name,
                _ => Argument.From(_, this, typeResolver)));
            _syntax = new Lazy<DirectiveDefinitionSyntax>(() =>
            {
                return new DirectiveDefinitionSyntax(Name(Name),
                    Locations.Select(DirectiveLocationHelper.ToStringValue).Select(_ => Name(_)).ToArray(),
                    SyntaxHelpers.Description(Description),
                    Arguments.Values.ToSyntaxNodes<InputValueDefinitionSyntax>().ToList());
            });
        }

        [GraphQLIgnore]
        public IReadOnlyDictionary<string, Argument> Arguments { get; }

        public string Name { get; }

        public override string Description { get; }

        public IReadOnlyList<DirectiveLocation> Locations { get; }

        public override SyntaxNode ToSyntaxNode() => _syntax.Value;

        [GraphQLName("args")]
        public IEnumerable<Argument> GetArguments() => Arguments.Values;

        [GraphQLIgnore]
        IEnumerable<IArgumentDefinition> IArgumentsContainerDefinition.GetArguments() => GetArguments();


        [NotNull]
        [GraphQLIgnore]
        public static Directive From(IDirectiveDefinition definition, TypeResolver typeResolver)
        {
            Check.NotNull(definition, nameof(definition));
            return new Directive(definition.Name, definition.Description, definition.Locations,
                definition.GetArguments(), typeResolver);
        }
    }
}