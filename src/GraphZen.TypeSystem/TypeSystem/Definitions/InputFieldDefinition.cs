// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
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
    [DisplayName("field")]
    public class InputFieldDefinition : InputValueDefinition, IMutableInputFieldDefinition
    {
        public InputFieldDefinition(string name,
            TypeIdentity typeIdentity, TypeSyntax typeSyntax,
            ConfigurationSource nameConfigurationSource,
            SchemaDefinition schema, ConfigurationSource configurationSource, PropertyInfo? clrInfo,
            IInputObjectTypeDefinition declaringMember) :
            base(name, nameConfigurationSource,
                typeIdentity, typeSyntax,
                schema, configurationSource, clrInfo, declaringMember)
        {
            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(TypeSystemExceptions.InvalidNameException
                    .CannotCreateInputFieldWithInvalidName(this, name));
            }

            Builder = new InternalInputFieldBuilder(this);
        }

        internal new InternalInputFieldBuilder Builder { get; }
        protected override MemberDefinitionBuilder GetBuilder() => Builder;

        private string DebuggerDisplay => ToString();

        public override bool SetName(string name, ConfigurationSource configurationSource)
        {
            if (!name.IsValidGraphQLName())
            {
                throw InvalidNameException.ForRename(this, name);
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
        public TypeReference FieldType => TypeReference;
        IInputObjectTypeDefinition IInputFieldDefinition.DeclaringType => DeclaringType;

        public bool SetFieldType(TypeIdentity identity, TypeSyntax syntax, ConfigurationSource configurationSource) =>
            TypeReference.Update(identity, syntax, configurationSource);

        public bool SetFieldType(string type, ConfigurationSource configurationSource) =>
            TypeReference.Update(type, configurationSource);

        public InputObjectTypeDefinition DeclaringType => (InputObjectTypeDefinition)DeclaringMember;
    }
}