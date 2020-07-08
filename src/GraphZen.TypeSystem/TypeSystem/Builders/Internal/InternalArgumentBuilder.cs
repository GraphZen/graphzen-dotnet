// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalArgumentBuilder : InternalInputValueBuilder<ArgumentDefinition, InternalArgumentBuilder>
    {
        public InternalArgumentBuilder(ArgumentDefinition definition) : base(definition)
        {
        }

        public InternalArgumentBuilder ArgumentType(string type, ConfigurationSource configurationSource)
        {
            Definition.SetArgumentType(type, configurationSource);
            return this;
        }

        public InternalArgumentBuilder ArgumentType(Type clrType, ConfigurationSource configurationSource)
        {
            if (clrType.TryGetGraphQLTypeInfo(out var typeSyntax, out var innerClrType))
            {
                var identity = Schema.GetOrAddTypeIdentity(innerClrType);
                Definition.SetArgumentType(identity, typeSyntax, configurationSource);
            }

            return this;
        }

        public InternalArgumentBuilder SetName(string name, ConfigurationSource configurationSource)
        {
            Definition.SetName(name, configurationSource);
            return this;
        }
    }
}