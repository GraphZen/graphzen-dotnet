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
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    [DisplayName("field")]
    public class MutableInputField : MutableInputValue, IMutableInputField
    {
        public MutableInputField(string name,
            TypeIdentity typeIdentity, TypeSyntax typeSyntax,
            ConfigurationSource nameConfigurationSource,
            MutableSchema schema, ConfigurationSource configurationSource, PropertyInfo? clrInfo,
            IInputObjectType declaringMember) :
            base(name, nameConfigurationSource,
                typeIdentity, typeSyntax,
                schema, configurationSource, clrInfo, declaringMember)
        {
            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(TypeSystemExceptions.InvalidNameException
                    .CannotCreateInputFieldWithInvalidName(this, name));
            }

            InternalBuilder = new InternalInputFieldBuilder(this);
        }

        internal new InternalInputFieldBuilder InternalBuilder { get; }
        protected override MemberDefinitionBuilder GetInternalBuilder() => InternalBuilder;

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
        IGraphQLTypeReference IInputField.FieldType => FieldType;
        public TypeReference FieldType => TypeReference;
        IInputObjectType IInputField.DeclaringType => DeclaringType;

        public bool SetFieldType(TypeIdentity identity, TypeSyntax syntax, ConfigurationSource configurationSource) =>
            TypeReference.Update(identity, syntax, configurationSource);

        public bool SetFieldType(string type, ConfigurationSource configurationSource) =>
            TypeReference.Update(type, configurationSource);

        public MutableInputObjectType DeclaringType => (MutableInputObjectType)DeclaringMember;
    }
}