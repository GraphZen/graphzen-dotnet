// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public abstract class AnnotatableMemberDefinitionBuilder<TDefinition> : MemberDefinitionBuilder<TDefinition>
        where TDefinition : AnnotatableMemberDefinition
    {
        protected AnnotatableMemberDefinitionBuilder(TDefinition definition) : base(definition)
        {
        }

        private static bool TryGetDeprecatedAttribute(DirectiveSyntax node,
            [NotNullWhen(true)] out GraphQLDeprecatedAttribute? attribute)
        {
            if (node.Name.Value != "deprecated")
            {
                attribute = null;
                return false;
            }

            var reason =
                node.Arguments.SingleOrDefault(_ => _.Name.Value == "reason")?.Value is StringValueSyntax strValue
                    ? strValue.Value
                    : null;

            attribute = new GraphQLDeprecatedAttribute(reason);
            return true;
        }


        public void AddDirectiveAnnotation(string name, object? value, ConfigurationSource configurationSource)
        {
            var directive = Schema.FindDirective(name);

            if (directive == null)
            {
                throw new InvalidOperationException(
                    $"Cannot annotate {Definition} with directive {name}: Directive {name} has not been defined yet.");
            }

            if (!directive.HasLocation(Definition.DirectiveLocation))
            {
                var reason = directive.Locations.Any()
                    ? $"is only valid on {directive.Locations.Select(_ => _.GetPluralizedDisplayValue()).OrList()}."
                    : "does not have any locations defined.";

                throw new InvalidOperationException(
                    $"Cannot annotate {Definition} with {directive}: Directive {name} cannot be annotated on {Definition.DirectiveLocation.GetPluralizedDisplayValue()} because it {reason}");
            }

            Definition.AddDirectiveAnnotation(new DirectiveAnnotation(directive, value), configurationSource);
        }
    }
}