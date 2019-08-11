// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IInputValue : IInputValueDefinition, ISyntaxConvertable 
    {
        [NotNull]
        new IGraphQLType InputType { get; }
    }
}