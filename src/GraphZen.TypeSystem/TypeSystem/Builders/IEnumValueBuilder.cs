// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IEnumValueBuilder :
        IInfrastructure<MutableEnumValue>,
        IDirectivesBuilder<IEnumValueBuilder>,
        INameBuilder<IEnumValueBuilder>,
        IDescriptionBuilder<IEnumValueBuilder>,
        IInfrastructure<InternalEnumValueBuilder>
    {
        IEnumValueBuilder CustomValue(object? value);
        IEnumValueBuilder RemoveCustomValue();
    }
}