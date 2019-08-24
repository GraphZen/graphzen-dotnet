// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;

namespace GraphZen.TypeSystem
{
    public interface IDirectiveBuilder
    {
        
        IDirectiveBuilder Description(string description);

        
        IDirectiveBuilder Locations(params DirectiveLocation[] locations);

        
        IDirectiveBuilder Argument(string name, string type,
            Action<InputValueBuilder> argumentBuilder = null);

        
        IDirectiveBuilder Argument<TArg>(string name,
            Action<InputValueBuilder> argumentBuilder = null);
    }
}