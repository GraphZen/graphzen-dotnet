// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class InputField : InputValue, IInputField
    {
        public InputField(
            string name, string? description, IGraphQLTypeReference type, object? defaultValue, bool hasDefaultValue,
            IEnumerable<IDirectiveAnnotation> directives, PropertyInfo? clrInfo, InputObjectType inputObject) :
            base(name, description, type, defaultValue, hasDefaultValue, directives, clrInfo, inputObject)
        {
        }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.InputFieldDefinition;

        public new PropertyInfo? ClrInfo => base.ClrInfo as PropertyInfo;
        public IGraphQLType FieldType => InputType;
        IGraphQLTypeReference IInputFieldDefinition.FieldType => FieldType;

        IInputObjectTypeDefinition IInputFieldDefinition.DeclaringType => DeclaringType;

        public InputObjectType DeclaringType => (InputObjectType)DeclaringMember;


        public static InputField From(IInputFieldDefinition definition,
            InputObjectType declaringType)
        {
            Check.NotNull(definition, nameof(definition));
            return new InputField(definition.Name, definition.Description, definition.FieldType,
                definition.DefaultValue, definition.HasDefaultValue, definition.GetDirectiveAnnotations(),
                definition.ClrInfo, declaringType);
        }
    }
}