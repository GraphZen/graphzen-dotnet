// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface IDirectiveDefinition :
        IName,
        IDescription,
        IClrType,
        IArguments,
        IMaybeSpec
    {
        public bool IsRepeatable { get; }
        IReadOnlyCollection<DirectiveLocation> Locations { get; }
        bool HasLocation(DirectiveLocation location);
    }
}