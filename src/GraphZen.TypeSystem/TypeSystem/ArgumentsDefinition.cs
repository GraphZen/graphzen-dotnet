// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    internal partial class ArgumentsDefinition : IMutableArgumentsDefinition
    {
        private readonly Dictionary<string, ArgumentDefinition> _arguments =
            new Dictionary<string, ArgumentDefinition>();

        private readonly Dictionary<string, ConfigurationSource> _ignoredArguments =
            new Dictionary<string, ConfigurationSource>();


        public ArgumentsDefinition(IMutableArgumentsDefinition declaringMember)
        {
            DeclaringMember = declaringMember;
        }

        private IMutableArgumentsDefinition DeclaringMember { get; }

        public ConfigurationSource GetConfigurationSource() => DeclaringMember.GetConfigurationSource();

        public SchemaDefinition Schema => DeclaringMember.Schema;

        ISchemaDefinition IMemberDefinition.Schema => Schema;

        [GenDictionaryAccessors(nameof(ArgumentDefinition.Name), "Argument")]
        public IReadOnlyDictionary<string, ArgumentDefinition> Arguments => _arguments;

        public ArgumentDefinition? GetOrAddArgument(string name, string type, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = FindIgnoredArgumentConfigurationSource(name);
            if (ignoredConfigurationSource.HasValue)
            {
                if (!configurationSource.Overrides(ignoredConfigurationSource))
                {
                    return null;
                }

                UnignoreArgument(name);
            }


            var argument = FindArgument(name);
            if (argument != null)
            {
                argument.UpdateConfigurationSource(configurationSource);
                argument.Builder.ArgumentType(type, configurationSource);
                return argument;
            }

            TypeSyntax typeNode;
            try
            {
                typeNode = Schema.Builder.Parser.ParseType(type);
            }
            catch (Exception e)
            {
                throw new InvalidTypeReferenceException(
                    "Unable to parse type reference. See inner exception for details.", e);
            }


            var argumentTypeName = typeNode.GetNamedType().Name.Value;
            var typeIdentity = Schema.GetOrAddTypeIdentity(argumentTypeName);
            argument = new ArgumentDefinition(name, configurationSource, typeIdentity, typeNode, configurationSource,
                DeclaringMember, null);
            AddArgument(argument);
            return argument;
        }

        public ArgumentDefinition? GetOrAddArgument(string name, Type clrType, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = FindIgnoredArgumentConfigurationSource(name);
            if (ignoredConfigurationSource.HasValue)
            {
                if (!configurationSource.Overrides(ignoredConfigurationSource))
                {
                    return null;
                }

                _ignoredArguments.Remove(name);
            }

            var argument = FindArgument(name);
            if (argument != null)
            {
                argument.UpdateConfigurationSource(configurationSource);
                argument.Builder.ArgumentType(clrType, configurationSource);
                return argument;
            }

            if (!clrType.TryGetGraphQLTypeInfo(out var typeNode, out var innerClrType))
            {
                throw new InvalidOperationException($"Unable to get field type info from {clrType}");
            }

            var typeIdentity = Schema.GetOrAddOutputTypeIdentity(innerClrType);
            argument = new ArgumentDefinition(name, configurationSource, typeIdentity, typeNode, configurationSource,
                DeclaringMember, null);
            AddArgument(argument);
            return argument;
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

        public bool AddArgument(ArgumentDefinition argument)
        {
            _arguments[argument.Name] = argument;
            return true;
        }

        public ConfigurationSource? FindIgnoredArgumentConfigurationSource(string name)
            => _ignoredArguments.TryGetValue(name, out var cs) ? (ConfigurationSource?)cs : null;


        public IEnumerable<ArgumentDefinition> GetArguments() => _arguments.Values;

        IEnumerable<IArgumentDefinition> IArgumentsDefinition.GetArguments() => GetArguments();


        public bool RenameArgument(ArgumentDefinition argument, string name, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(argument.GetNameConfigurationSource()))
            {
                return false;
            }

            if (TryGetArgument(name, out var existing) && existing != argument)
            {
                throw TypeSystemExceptions.DuplicateItemException.ForRename(this, name);
            }

            if (RemoveArgument(argument))
            {
                _arguments[name] = argument;
            }

            return true;
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

        public void UnignoreArgument(string name)
        {
            _ignoredArguments.Remove(name);
        }
    }
}