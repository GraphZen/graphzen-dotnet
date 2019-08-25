// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

#nullable disable

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class FieldDefinition : AnnotatableMemberDefinition, IMutableFieldDefinition
    {
        private readonly Dictionary<string, ArgumentDefinition> _arguments =
            new Dictionary<string, ArgumentDefinition>();


        private readonly Dictionary<string, ConfigurationSource> _ignoredArguments =
            new Dictionary<string, ConfigurationSource>();

        private string _deprecationReason;
        private bool _isDeprecated;
        private ConfigurationSource _nameConfigurationSource;


        public FieldDefinition(string name, ConfigurationSource nameConfigurationSource,
            SchemaDefinition schema,
            FieldsContainerDefinition declaringType,
            ConfigurationSource configurationSource, MemberInfo clrInfo) : base(configurationSource)
        {
            Check.NotNull(schema, nameof(schema));
            ClrInfo = clrInfo;
            Name = name;
            Schema = schema;
            _nameConfigurationSource = nameConfigurationSource;
            DeclaringType = Check.NotNull(declaringType, nameof(declaringType));
            Builder = new InternalFieldBuilder(this, schema.Builder);
        }


        private string DebuggerDisplay => $"field {Name}";


        public SchemaDefinition Schema { get; }


        public InternalFieldBuilder Builder { get; }


        public FieldsContainerDefinition DeclaringType { get; }

        public MemberInfo ClrInfo { get; }

        public bool RenameArgument(ArgumentDefinition argument, string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(argument.GetNameConfigurationSource())) return false;

            if (this.TryGetArgument(name, out var existing) && existing != argument)
                throw new InvalidOperationException(
                    $"Cannot rename {argument} to '{name}'. {this} already contains a field named '{name}'.");

            _arguments.Remove(argument.Name);
            _arguments[name] = argument;
            return true;
        }


        public IEnumerable<ArgumentDefinition> GetArguments()
        {
            return _arguments.Values;
        }

        public IGraphQLTypeReference FieldType { get; set; }
        public Resolver<object, object> Resolver { get; set; }

        IFieldsContainerDefinition IFieldDefinition.DeclaringType => DeclaringType;

        public string Name { get; private set; }

        public bool IsDeprecated
        {
            get => _isDeprecated || DeprecationReason != null;
            set
            {
                _isDeprecated = value;
                if (!_isDeprecated) DeprecationReason = null;
            }
        }

        public string DeprecationReason
        {
            get => _deprecationReason;
            set
            {
                _deprecationReason = value;
                if (_deprecationReason != null) IsDeprecated = true;
            }
        }

        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.FieldDefinition;

        public IReadOnlyDictionary<string, ArgumentDefinition> Arguments => _arguments;

        IMutableFieldsContainerDefinition IMutableFieldDefinition.DeclaringType => DeclaringType;

        public bool SetName(string name, ConfigurationSource configurationSource)
        {
            Check.NotNull(name, nameof(name));
            if (!configurationSource.Overrides(_nameConfigurationSource)) return false;

            if (Name != name) DeclaringType.RenameField(this, name, configurationSource);

            Name = name;
            _nameConfigurationSource = configurationSource;
            return true;
        }

        public ConfigurationSource GetNameConfigurationSource()
        {
            return _nameConfigurationSource;
        }

        IEnumerable<IArgumentDefinition> IArgumentsContainerDefinition.GetArguments()
        {
            return GetArguments();
        }

        object IClrInfo.ClrInfo => ClrInfo;

        public bool MarkAsDeprecated(string reason, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public bool RemoveDeprecation(ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public ConfigurationSource? FindIgnoredArgumentConfigurationSource(string name)
        {
            if (_ignoredArguments.TryGetValue(name, out var cs)) return cs;

            return null;
        }

        public bool IgnoreArgument(string name, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = FindIgnoredArgumentConfigurationSource(name);
            if (ignoredConfigurationSource.HasValue &&
                ignoredConfigurationSource.Overrides(configurationSource)) return true;

            if (ignoredConfigurationSource != null)
                configurationSource = configurationSource.Max(ignoredConfigurationSource);

            _ignoredArguments[name] = configurationSource;
            var existing = this.FindArgument(name);

            if (existing != null) return IgnoreArgument(existing, configurationSource);

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

        public ArgumentDefinition FindArgument(ParameterInfo member)
        {
            // ReSharper disable once PossibleNullReferenceException
            var memberMatch = _arguments.Values.SingleOrDefault(_ => _.ClrInfo == member);
            if (memberMatch != null) return memberMatch;

            var (argumentName, _) = member.GetGraphQLArgumentName();
            return this.FindArgument(argumentName);
        }

        public void RemoveArgument(ArgumentDefinition argument)
        {
            _arguments.Remove(argument.Name);
        }

        public void UnignoreArgument(string name)
        {
            _ignoredArguments.Remove(name);
        }

        public ArgumentDefinition AddArgument(ParameterInfo parameter,
            ConfigurationSource configurationSource)
        {
            var (argName, nameConfigurationSource) = parameter.GetGraphQLArgumentName();
            var argument = new ArgumentDefinition(argName, nameConfigurationSource, Schema, configurationSource, this,
                parameter);


            var ab = argument.Builder;
            argument.InputType = Schema.GetOrAddTypeReference(parameter, this);
            ab.DefaultValue(parameter, configurationSource);
            if (parameter.TryGetDescriptionFromDataAnnotation(out var description))
                ab.Description(description, ConfigurationSource.DataAnnotation);

            return AddArgument(argument);
        }


        private ArgumentDefinition AddArgument(ArgumentDefinition argument)
        {
            if (this.HasArgument(argument.Name))
                throw new InvalidOperationException(
                    $"Cannot add {argument} to {this}. An argument with that name already exists.");

            _arguments.Add(argument.Name, argument);
            return argument;
        }

        public override string ToString()
        {
            return $"field {Name}";
        }


        public ArgumentDefinition GetOrAddArgument(string name, ConfigurationSource configurationSource)
        {
            if (!_arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var argument))
            {
                argument = new ArgumentDefinition(name, configurationSource, Builder.Schema,
                    configurationSource, this, null);
                _arguments[name] = argument;
            }

            return argument;
        }
    }
}