// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public abstract class
        InternalAnnotatableMemberDefinitionBuilder<TDefinition> : InternalAnnotatableMemberDefinitionBuilder
        where TDefinition : AnnotatableMemberDefinition
    {
        protected InternalAnnotatableMemberDefinitionBuilder(AnnotatableMemberDefinition definition) : base(definition)
        {
        }

        public new TDefinition Definition => (TDefinition)base.Definition;
    }

    public abstract class
        InternalAnnotatableMemberDefinitionBuilder : InternalMemberDefinitionBuilder<AnnotatableMemberDefinition>
    {
        protected InternalAnnotatableMemberDefinitionBuilder(AnnotatableMemberDefinition definition) : base(definition)
        {
        }

        public void RemoveDirectiveAnnotations(string name, ConfigurationSource configurationSource)
        {
            foreach (var annotation in Definition.FindDirectiveAnnotations(name).ToArray())
            {
                Definition.RemoveDirectiveAnnotation(annotation, configurationSource);
            }
        }

        public void ClearDirectiveAnnotations(ConfigurationSource configurationSource)
        {
            foreach (var annotation in Definition.DirectiveAnnotations.ToArray())
            {
                Definition.RemoveDirectiveAnnotation(annotation, configurationSource);
            }
        }

        public void AddDirectiveAnnotation(string name, object? value, ConfigurationSource configurationSource)
        {
            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(
                    $"Cannot annotate {Definition} with directive: \"{name}\" is not a valid directive name.");
            }

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