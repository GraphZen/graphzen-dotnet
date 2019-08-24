// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel
{
    public interface IArgumentsContainerNode
    {
        
        
        IReadOnlyList<ArgumentSyntax> Arguments { get; }
    }
}