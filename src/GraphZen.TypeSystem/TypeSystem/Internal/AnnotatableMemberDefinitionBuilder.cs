// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public abstract class AnnotatableMemberDefinitionBuilder<TDefinition> : MemberDefinitionBuilder<TDefinition>
        where TDefinition : AnnotatableMemberDefinition
    {
        protected AnnotatableMemberDefinitionBuilder(TDefinition definition,
            InternalSchemaBuilder schemaBuilder) : base(
            definition, schemaBuilder)
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

        public void DirectiveAnnotation(object value, ConfigurationSource configurationSource)
        {
            if (value is DirectiveSyntax node)
            {
                if (TryGetDeprecatedAttribute(node, out var attr))
                {
                    DirectiveAnnotation("deprecated", attr, configurationSource);
                    return;
                }

                var existingDirective = Schema.FindDirective(node.Name.Value);
                if (existingDirective != null)
                {
                }
                else
                {
                    DirectiveAnnotation(node, configurationSource);
                }
            }
        }

        private void DirectiveAnnotation(DirectiveSyntax directive, ConfigurationSource configurationSource)
        {
            Definition.AddDirectiveAnnotation(directive.Name.Value, directive);
        }


        public void DirectiveAnnotation(string name, object? value, ConfigurationSource configurationSource)
        {
            var existing = Definition.FindDirectiveAnnotation(name);
            if (existing == null)
            {
                var directive = Schema.FindDirective(name);
                var displayVal = Definition.DirectiveLocation.GetDisplayValue();
                var directiveDescription =
                    Definition is INamed namedDef ? $"{displayVal} '{namedDef}'" : displayVal;
                if (directive == null)
                    throw new InvalidOperationException(
                        $"Unknown directive: cannot add '{name}' directive to the {directiveDescription}. Ensure the '{name}' directive is defined in the schema before it is used.");

                if (!directive.Locations.Contains(Definition.DirectiveLocation))
                {
                    var validLocations = directive.Locations.Any()
                        ? $" The '{name}' directive is only valid on a {directive.Locations.Select(_ => _.GetDisplayValue()).OrList()}."
                        : "";

                    throw new InvalidOperationException(
                        $"Invalid directive location: The '{name}' directive cannot be annotated on the {directiveDescription}.{validLocations}");
                }

                Definition.AddDirectiveAnnotation(name, value);
            }
            else
            {
                Definition.UpdateDirectiveAnnotation(name, value);
            }
        }

        public void RemoveDirectiveAnnotation(string name)
        {
            Definition.RemoveDirectiveAnnotation(name);
        }
    }
}