// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DebuggerDisplay("{" + nameof(DebuggerDisplay) + ",nq}")]
    public class ArgumentDefinition : InputValueDefinition, IMutableArgumentDefinition
    {
        public ArgumentDefinition(string name,
            ConfigurationSource nameConfigurationSource,
            SchemaDefinition schema,
            ConfigurationSource configurationSource, IMutableArgumentsContainerDefinition declaringMember,
            ParameterInfo? clrInfo) : base(
            Check.NotNull(name, nameof(name)), nameConfigurationSource,
            Check.NotNull(schema, nameof(schema)), configurationSource, clrInfo, declaringMember)
        {
        }

        private string DebuggerDisplay => $"argument {Name}";


        public override DirectiveLocation DirectiveLocation { get; } = DirectiveLocation.ArgumentDefinition;


        public override bool SetName(string name, ConfigurationSource configurationSource)
        {
            Check.NotNull(name, nameof(name));
            if (!configurationSource.Overrides(GetNameConfigurationSource())) return false;

            if (Name != name) DeclaringMember.RenameArgument(this, name, configurationSource);

            Name = name;
            NameConfigurationSource = configurationSource;
            return true;
        }

        public new IMutableArgumentsContainerDefinition DeclaringMember =>
            (IMutableArgumentsContainerDefinition)base.DeclaringMember;

        public new ParameterInfo? ClrInfo => base.ClrInfo as ParameterInfo;
        IArgumentsContainerDefinition IArgumentDefinition.DeclaringMember => DeclaringMember;

        public override string ToString() => $"argument {Name}";
    }
}