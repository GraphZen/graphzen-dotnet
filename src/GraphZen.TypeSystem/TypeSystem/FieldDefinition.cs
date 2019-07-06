// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class FieldDefinition : AnnotatableMemberDefinition, IMutableFieldDefinition
    {
        [NotNull] private readonly Dictionary<string, ArgumentDefinition> _arguments =
            new Dictionary<string, ArgumentDefinition>();

        [NotNull] private readonly Dictionary<string, ConfigurationSource> _ignoredArguments =
            new Dictionary<string, ConfigurationSource>();

        private string _deprecationReason;
        private bool _isDeprecated;
        private ConfigurationSource _nameConfigurationSource;


        public FieldDefinition([NotNull] string name, ConfigurationSource nameConfigurationSource,
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

        [NotNull]
        public SchemaDefinition Schema { get; }


        [NotNull]
        public InternalFieldBuilder Builder { get; }

        [NotNull]
        public FieldsContainerDefinition DeclaringType { get; }

        public MemberInfo ClrInfo { get; }

        public bool RenameArgument(ArgumentDefinition argument, string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(argument.GetNameConfigurationSource()))
            {
                return false;
            }

            if (this.TryGetArgument(name, out var existing) && existing != argument)
            {
                throw new InvalidOperationException(
                    $"Cannot rename {argument} to '{name}'. {this} already contains a field named '{name}'.");
            }

            _arguments.Remove(argument.Name);
            _arguments[name] = argument;
            return true;
        }


        public IEnumerable<ArgumentDefinition> GetArguments() => _arguments.Values;
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
                if (!_isDeprecated)
                {
                    DeprecationReason = null;
                }
            }
        }

        public string DeprecationReason
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

        public IReadOnlyDictionary<string, ArgumentDefinition> Arguments => _arguments;

        IMutableFieldsContainerDefinition IMutableFieldDefinition.DeclaringType => DeclaringType;

        public bool SetName(string name, ConfigurationSource configurationSource)
        {
            Check.NotNull(name, nameof(name));
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

        IEnumerable<IArgumentDefinition> IArgumentsContainerDefinition.GetArguments() => GetArguments();

        object IClrInfo.ClrInfo => ClrInfo;

        public bool MarkAsDeprecated(string reason, ConfigurationSource configurationSource) =>
            throw new NotImplementedException();

        public bool RemoveDeprecation(ConfigurationSource configurationSource) => throw new NotImplementedException();

        public ConfigurationSource? FindIgnoredArgumentConfigurationSource([NotNull] string fieldName)
        {
            if (_ignoredArguments.TryGetValue(fieldName, out var cs))
            {
                return cs;
            }

            return null;
        }

        public bool IgnoreArgument([NotNull] string name, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = FindIgnoredArgumentConfigurationSource(name);
            if (ignoredConfigurationSource.HasValue && ignoredConfigurationSource.Overrides(configurationSource))
            {
                return true;
            }

            if (ignoredConfigurationSource != null)
            {
                configurationSource = configurationSource.Max(ignoredConfigurationSource);
            }

            _ignoredArguments[name] = configurationSource;
            var existing = this.FindArgument(name);

            if (existing != null)
            {
                return IgnoreArgument(existing, configurationSource);
            }

            return true;
        }

        private bool IgnoreArgument([NotNull] ArgumentDefinition argument, ConfigurationSource configurationSource)
        {
            if (configurationSource.Overrides(argument.GetConfigurationSource()))
            {
                _arguments.Remove(argument.Name);
                return true;
            }

            return false;
        }

        public ArgumentDefinition FindArgument([NotNull] ParameterInfo member)
        {
            // ReSharper disable once PossibleNullReferenceException
            var memberMatch = _arguments.Values.SingleOrDefault(_ => _.ClrInfo == member);
            if (memberMatch != null)
            {
                return memberMatch;
            }

            var (argumentName, _) = member.GetGraphQLArgumentName();
            return this.FindArgument(argumentName);
        }

        public void RemoveArgument([NotNull] ArgumentDefinition argument)
        {
            _arguments.Remove(argument.Name);
        }

        public void UnignoreArgument([NotNull] string name)
        {
            _ignoredArguments.Remove(name);
        }

        public ArgumentDefinition AddArgument([NotNull] ParameterInfo parameter,
            ConfigurationSource configurationSource)
        {
            if (ClrInfo == null)
            {
                throw new InvalidOperationException(
                    "Cannot add Argument from property on a type that does not have a CLR type mapped.");
            }

            // ReSharper disable once AssignNullToNotNullAttribute
            //if (!ClrType.IsSameOrSubclass(parameter.DeclaringType))
            //{
            //    throw new InvalidOperationException(
            //        $"Cannot add Argument from property with a declaring type ({parameter.DeclaringType}) that does not exist on the parent's {Kind.ToString().ToLower()} type's mapped CLR type ({ClrType}).");
            //}

            var (argName, nameConfigurationSource) = parameter.GetGraphQLArgumentName();
            var argument = new ArgumentDefinition(argName, nameConfigurationSource, Schema, configurationSource, this,
                parameter);


            var ab = argument.Builder;
            argument.InputType = Schema.GetOrAddTypeReference(parameter, this);
            ab.DefaultValue(parameter, configurationSource);

            if (parameter.TryGetDescriptionFromDataAnnotation(out var description))
            {
                ab.Description(description, ConfigurationSource.DataAnnotation);
            }

            return AddArgument(argument);
        }


        private ArgumentDefinition AddArgument([NotNull] ArgumentDefinition argument)
        {
            if (this.HasArgument(argument.Name))
            {
                throw new InvalidOperationException(
                    $"Cannot add {argument} to {this}. An argument with that name already exists.");
            }

            _arguments.Add(argument.Name, argument);
            return argument;
        }

        public override string ToString() => $"field {Name}";


        [NotNull]
        public ArgumentDefinition GetOrAddArgument(string name, ConfigurationSource configurationSource)
        {
            if (!_arguments.TryGetValue(Check.NotNull(name, nameof(name)), out var argument))
            {
                argument = new ArgumentDefinition(name, ConfigurationSource.Convention, Builder.Schema,
                    configurationSource, this, null);
                _arguments[name] = argument;
            }

            return argument;
        }
    }
}