// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
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
    public partial class FieldDefinition : AnnotatableMemberDefinition, IMutableFieldDefinition
    {
        private readonly Dictionary<string, ArgumentDefinition> _arguments =
            new Dictionary<string, ArgumentDefinition>();


        private readonly Dictionary<string, ConfigurationSource> _ignoredArguments =
            new Dictionary<string, ConfigurationSource>();

        private string? _deprecationReason;
        private bool _isDeprecated;
        private ConfigurationSource _nameConfigurationSource;

        protected override SchemaDefinition Schema { get; }


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
            Builder = new InternalFieldBuilder(this, schema.Builder);
            FieldType = new TypeReference(fieldTypeIdentity, fieldTypeSyntax, this);
            Name = name;
            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(TypeSystemExceptionMessages.InvalidNameException.CannotCreateField(name, this));
            }
        }


        private string DebuggerDisplay => $"field {Name}";


        public InternalFieldBuilder Builder { get; }


        public FieldsDefinition DeclaringType { get; }

        public MemberInfo? ClrInfo { get; }

        public bool RenameArgument(ArgumentDefinition argument, string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(argument.GetNameConfigurationSource()))
            {
                return false;
            }

            if (TryGetArgument(name, out var existing) && existing != argument)
            {
                throw new DuplicateItemException(
                    TypeSystemExceptionMessages.DuplicateItemException.CannotRenameArgument(argument, name));
            }

            _arguments.Remove(argument.Name);
            _arguments[name] = argument;
            return true;
        }


        public bool RemoveArgument(ArgumentDefinition argument, ConfigurationSource configurationSource) => throw new NotImplementedException();

        public ArgumentDefinition AddArgument(string name, string type, ConfigurationSource configurationSource)
        {
            var typeSyntax = Schema.Builder.Parser.ParseType(type);
            var typeName = typeSyntax.GetNamedType().Name.Value;
            var typeIdentity = Schema.GetOrAddInputTypeIdentity(typeName);
            var argument = new ArgumentDefinition(name, configurationSource, typeIdentity, typeSyntax, Schema, configurationSource, this, null);
            AddArgument(argument);
            return argument;
        }

        public ArgumentDefinition AddArgument(string name, Type clrType, ConfigurationSource configurationSource)
        {
            if (!clrType.TryGetGraphQLTypeInfo(out var typeSyntax, out var innerClrType))
            {
                throw new InvalidOperationException($"Unable to get argument type info from  {clrType}");

            }
            var typeIdentity = Schema.GetOrAddInputTypeIdentity(innerClrType);
            var argument = new ArgumentDefinition(name, configurationSource, typeIdentity, typeSyntax, Schema, configurationSource, this, null);
            AddArgument(argument);
            return argument;
        }

        public IEnumerable<ArgumentDefinition> GetArguments() => _arguments.Values;

        public TypeReference FieldType { get; set; }
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
        public IReadOnlyDictionary<string, ArgumentDefinition> Arguments => _arguments;

        IMutableFieldsDefinition IMutableFieldDefinition.DeclaringType => DeclaringType;

        public bool SetName(string name, ConfigurationSource configurationSource)
        {
            Check.NotNull(name, nameof(name));
            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(
                    TypeSystemExceptionMessages.InvalidNameException.CannotRename(name, this, DeclaringType));
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
        {
            if (_ignoredArguments.TryGetValue(name, out var cs))
            {
                return cs;
            }

            return null;
        }

        public bool IgnoreArgument(string name, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = FindIgnoredArgumentConfigurationSource(name);
            if (ignoredConfigurationSource.HasValue &&
                ignoredConfigurationSource.Overrides(configurationSource))
            {
                return true;
            }

            if (ignoredConfigurationSource != null)
            {
                configurationSource = configurationSource.Max(ignoredConfigurationSource);
            }

            _ignoredArguments[name] = configurationSource;
            var existing = FindArgument(name);

            if (existing != null)
            {
                return IgnoreArgument(existing, configurationSource);
            }

            return true;
        }

        private bool IgnoreArgument(ArgumentDefinition argument, ConfigurationSource configurationSource)
        {
            if (configurationSource.Overrides(argument.GetConfigurationSource()))
            {
                _arguments.Remove(argument.Name);
                return true;
            }

            return false;
        }

        public ArgumentDefinition? FindArgument(ParameterInfo member)
        {
            // ReSharper disable once PossibleNullReferenceException
            var memberMatch = _arguments.Values.SingleOrDefault(_ => _.ClrInfo == member);
            if (memberMatch != null)
            {
                return memberMatch;
            }

            var (argumentName, _) = member.GetGraphQLArgumentName();
            return FindArgument(argumentName);
        }

        public bool RemoveArgument(ArgumentDefinition argument)
        {
            if (_arguments.Remove(argument.Name, out var removed))
            {
                if (!removed.Equals(argument))
                {
                    throw new Exception("Did not remove expected argument");
                }
                return true;
            }
            return false;
        }

        public void UnignoreArgument(string name)
        {
            _ignoredArguments.Remove(name);
        }

        public ArgumentDefinition AddArgument(ParameterInfo parameter,
            ConfigurationSource configurationSource)
        {
            var (argName, nameConfigurationSource) = parameter.GetGraphQLArgumentName();

            if (!parameter.TryGetGraphQLTypeInfo(out var typeSyntax, out var innerClrType))
            {
                throw new Exception($"Unable to get type info from parameter {parameter}");
            }

            var typeIdentity = Schema.GetOrAddInputTypeIdentity(innerClrType);
            var argument = new ArgumentDefinition(argName, nameConfigurationSource, typeIdentity, typeSyntax, Schema, configurationSource, this, parameter);
            var ab = argument.Builder;
            ab.DefaultValue(parameter, configurationSource);
            if (parameter.TryGetDescriptionFromDataAnnotation(out var description))
            {
                ab.Description(description, ConfigurationSource.DataAnnotation);
            }
            AddArgument(argument);
            return argument;
        }


        public bool AddArgument(ArgumentDefinition argument)
        {
            if (HasArgument(argument.Name))
            {
                throw new InvalidOperationException(
                    $"Cannot add {argument} to {this}. An argument with that name already exists.");
            }

            _arguments.Add(argument.Name, argument);
            return true;
        }

        public override string ToString() => $"field {Name}";


    }
}