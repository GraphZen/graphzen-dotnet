// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System.Collections.Generic;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    public class InputField : InputValue, IInputField
    {
        public InputField(
             string name,
             string description,
             IGraphQLTypeReference type,
             object defaultValue,
            bool hasDefaultValue,
             IReadOnlyList<IDirectiveAnnotation> directives,
            TypeResolver typeResolver, PropertyInfo clrInfo,  InputObjectType inputObject) :
            base(name, description, type,
                defaultValue, hasDefaultValue,
                Check.NotNull(directives, nameof(directives)),
                Check.NotNull(typeResolver, nameof(typeResolver)), clrInfo, inputObject)
        {
        }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.InputFieldDefinition;

        public new PropertyInfo ClrInfo => base.ClrInfo as PropertyInfo;
        IInputObjectTypeDefinition IInputFieldDefinition.DeclaringMember => DeclaringMember;

        public new InputObjectType DeclaringMember => (InputObjectType)base.DeclaringMember;

        
        public static InputField From(IInputFieldDefinition definition, TypeResolver typeResolver,
             InputObjectType declaringType)
        {
            Check.NotNull(definition, nameof(definition));
            Check.NotNull(typeResolver, nameof(typeResolver));
            return new InputField(definition.Name, definition.Description, definition.InputType,
                definition.DefaultValue, definition.HasDefaultValue, definition.DirectiveAnnotations, typeResolver,
                definition.ClrInfo, declaringType);
        }
    }
}