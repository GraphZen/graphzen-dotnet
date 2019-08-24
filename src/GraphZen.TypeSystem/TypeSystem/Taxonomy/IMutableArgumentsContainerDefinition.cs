// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IMutableArgumentsContainerDefinition : IArgumentsContainerDefinition
    {
        
        IReadOnlyDictionary<string, ArgumentDefinition> Arguments { get; }

        bool RenameArgument( ArgumentDefinition argument,  string name,
            ConfigurationSource configurationSource);

        
        
        new IEnumerable<ArgumentDefinition> GetArguments();
    }
}