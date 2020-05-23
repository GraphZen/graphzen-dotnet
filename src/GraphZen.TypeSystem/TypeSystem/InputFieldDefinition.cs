// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class InputFieldDefinition : InputValueDefinition, IMutableInputFieldDefinition
    {
        public InputFieldDefinition(string name,
            ConfigurationSource nameConfigurationSource,
            SchemaDefinition schema, ConfigurationSource configurationSource, PropertyInfo? clrInfo,
            IInputObjectTypeDefinition declaringMember) :
            base(Check.NotNull(name, nameof(name)), nameConfigurationSource,
                Check.NotNull(schema, nameof(schema)), configurationSource, clrInfo, declaringMember)
        {
            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(TypeSystemExceptionMessages.InvalidNameException.CannotCreateInputFieldWithInvalidName(this, name));
            }
        }

        private string DebuggerDisplay => ToString();

        public override bool SetName(string name, ConfigurationSource configurationSource)
        {
            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(
                    TypeSystemExceptionMessages.InvalidNameException.CannotRename(name, this, DeclaringType));
            }

            if (!configurationSource.Overrides(GetNameConfigurationSource()))
            {
                return false;
            }

            if (Name != name)
            {
                DeclaringType.RenameField(this, name, configurationSource);
            }

            Name = name;
            NameConfigurationSource = configurationSource;
            return true;
        }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.InputFieldDefinition;
        public new PropertyInfo? ClrInfo => base.ClrInfo as PropertyInfo;
        IGraphQLTypeReference IInputFieldDefinition.FieldType => FieldType;

        public TypeReference FieldType => InputType;
        IInputObjectTypeDefinition IInputFieldDefinition.DeclaringType => DeclaringType;

        public InputObjectTypeDefinition DeclaringType => (InputObjectTypeDefinition)DeclaringMember;

        public override string ToString() => $"input field {Name}";
    }
}