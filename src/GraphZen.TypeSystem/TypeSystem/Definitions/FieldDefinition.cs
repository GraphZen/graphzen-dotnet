// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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
    public partial class FieldDefinition : AnnotatableMemberDefinition, IMutableFieldDefinition
    {
        private string? _deprecationReason;
        private bool _isDeprecated;
        private ConfigurationSource _nameConfigurationSource;

        private readonly ArgumentsDefinition _args;

        public override SchemaDefinition Schema { get; }
        public IEnumerable<IMemberDefinition> Children() => GetArguments();

        public FieldDefinition(string name, ConfigurationSource nameConfigurationSource,
            TypeIdentity fieldTypeIdentity,
            TypeSyntax fieldTypeSyntax,
            SchemaDefinition schema,
            FieldsDefinition declaringType,
            ConfigurationSource configurationSource, MemberInfo? clrInfo) : base(configurationSource)
        {
            Schema = schema;
            ClrInfo = clrInfo;
            _nameConfigurationSource = nameConfigurationSource;
            DeclaringType = Check.NotNull(declaringType, nameof(declaringType));
            InternalBuilder = new InternalFieldBuilder(this);
            Name = name;
            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(
                    TypeSystemExceptions.InvalidNameException.CannotCreateField(name, this));
            }

            TypeReference = new FieldTypeReference(fieldTypeIdentity, fieldTypeSyntax, this);
            _args = new ArgumentsDefinition(this);
        }


        private string DebuggerDisplay => $"field {Name}";


        internal new InternalFieldBuilder InternalBuilder { get; }
        protected override MemberDefinitionBuilder GetInternalBuilder() => InternalBuilder;


        public FieldsDefinition DeclaringType { get; }

        public bool SetFieldType(TypeIdentity identity, TypeSyntax syntax, ConfigurationSource configurationSource) =>
            TypeReference.Update(identity, syntax, configurationSource);

        public bool SetFieldType(string type, ConfigurationSource configurationSource) =>
            TypeReference.Update(type, configurationSource);

        public MemberInfo? ClrInfo { get; }

        public bool RenameArgument(ArgumentDefinition argument, string name, ConfigurationSource configurationSource) =>
            _args.RenameArgument(argument, name, configurationSource);


        //public bool RemoveArgument(ArgumentDefinition argument, ConfigurationSource configurationSource) =>
        //    _args.RemoveArgument(argument, configurationSource);


        public IEnumerable<ArgumentDefinition> GetArguments() => _args.GetArguments();

        public TypeReference FieldType => TypeReference;
        IGraphQLTypeReference IFieldDefinition.FieldType => FieldType;
        public Resolver<object, object?>? Resolver { get; set; }

        IFieldsDefinition IFieldDefinition.DeclaringType => DeclaringType;

        public string Name { get; private set; }

        public bool IsDeprecated
        {
            get => _isDeprecated || DeprecationReason != null;
            set
            {
                _isDeprecated = value;
                if (!_isDeprecated)
                {
                    DeprecationReason = null;
                }
            }
        }

        public string? DeprecationReason
        {
            get => _deprecationReason;
            set
            {
                _deprecationReason = value;
                if (_deprecationReason != null)
                {
                    IsDeprecated = true;
                }
            }
        }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.FieldDefinition;

        [GenDictionaryAccessors(nameof(ArgumentDefinition.Name), "Argument")]
        public IReadOnlyDictionary<string, ArgumentDefinition> Arguments => _args.Arguments;

        public ArgumentDefinition?
            GetOrAddArgument(string name, Type clrType, ConfigurationSource configurationSource) =>
            _args.GetOrAddArgument(name, clrType, configurationSource);

        public ArgumentDefinition?
            GetOrAddArgument(string name, string type, ConfigurationSource configurationSource) =>
            _args.GetOrAddArgument(name, type, configurationSource);

        IMutableFieldsDefinition IMutableFieldDefinition.DeclaringType => DeclaringType;

        public bool SetName(string name, ConfigurationSource configurationSource)
        {
            Check.NotNull(name, nameof(name));
            if (!name.IsValidGraphQLName())
            {
                throw InvalidNameException.ForRename(this, name);
            }

            if (!configurationSource.Overrides(_nameConfigurationSource))
            {
                return false;
            }

            if (Name != name)
            {
                DeclaringType.RenameField(this, name, configurationSource);
            }

            Name = name;
            _nameConfigurationSource = configurationSource;
            return true;
        }

        public ConfigurationSource GetNameConfigurationSource() => _nameConfigurationSource;

        IEnumerable<IArgumentDefinition> IArgumentsDefinition.GetArguments() => GetArguments();

        object? IClrInfo.ClrInfo => ClrInfo;

        public bool MarkAsDeprecated(string reason, ConfigurationSource configurationSource) =>
            throw new NotImplementedException();

        public bool RemoveDeprecation(ConfigurationSource configurationSource) => throw new NotImplementedException();

        public ConfigurationSource? FindIgnoredArgumentConfigurationSource(string name)
            => _args.FindIgnoredArgumentConfigurationSource(name);

        public bool IgnoreArgument(string name, ConfigurationSource configurationSource) =>
            _args.IgnoreArgument(name, configurationSource);


        public ArgumentDefinition? FindArgument(ParameterInfo member)
        {
            // ReSharper disable once PossibleNullReferenceException
            var memberMatch = GetArguments().SingleOrDefault(_ => _.ClrInfo == member);
            if (memberMatch != null)
            {
                return memberMatch;
            }

            var (argumentName, _) = member.GetGraphQLArgumentName();
            return FindArgument(argumentName);
        }

        public bool RemoveArgument(ArgumentDefinition argument) => _args.RemoveArgument(argument);

        public void UnignoreArgument(string name) => _args.UnignoreArgument(name);


        public ArgumentDefinition AddArgument(ParameterInfo parameter,
            ConfigurationSource configurationSource)
        {
            var (argName, nameConfigurationSource) = parameter.GetGraphQLArgumentName();

            if (!parameter.TryGetGraphQLTypeInfo(out var typeSyntax, out var innerClrType))
            {
                throw new Exception($"Unable to get type info from parameter {parameter}");
            }

            var typeIdentity = Schema.GetOrAddInputTypeIdentity(innerClrType);
            var argument = new ArgumentDefinition(argName, nameConfigurationSource, typeIdentity, typeSyntax,
                configurationSource, this, parameter);
            var ab = argument.InternalBuilder;
            ab.DefaultValue(parameter, configurationSource);
            if (parameter.TryGetDescriptionFromDataAnnotation(out var description))
            {
                ab.Description(description, ConfigurationSource.DataAnnotation);
            }

            AddArgument(argument);
            return argument;
        }


        public bool AddArgument(ArgumentDefinition argument) => _args.AddArgument(argument);


        public override string ToString() => $"{DeclaringType.GetTypeDisplayName()} field {DeclaringType.Name}.{Name}";

        public ConfigurationSource GetTypeReferenceConfigurationSource() =>
            TypeReference.GetTypeReferenceConfigurationSource();

        public TypeReference TypeReference { get; }

        public bool SetTypeReference(TypeIdentity identity, TypeSyntax syntax,
            ConfigurationSource configurationSource) =>
            TypeReference.Update(identity, syntax, configurationSource);

        public bool SetTypeReference(string type, ConfigurationSource configurationSource) =>
            TypeReference.Update(type, configurationSource);

        IGraphQLTypeReference ITypeReferenceDefinition.TypeReference => TypeReference;
    }
}