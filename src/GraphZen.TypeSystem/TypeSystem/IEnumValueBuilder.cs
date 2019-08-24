// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem
{
    public interface IEnumValueBuilder : IAnnotableBuilder<IEnumValueBuilder>
    {
        
        IEnumValueBuilder Description( string description);

        
        IEnumValueBuilder CustomValue(object value);

        
        IEnumValueBuilder Deprecated(bool deprecated = true);

        
        IEnumValueBuilder Deprecated(string reason);
    }
}