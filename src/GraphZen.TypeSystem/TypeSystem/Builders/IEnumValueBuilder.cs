// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    internal interface IEnumValueBuilder :
        IInfrastructure<EnumValueDefinition>,
        IAnnotableBuilder<EnumValueBuilder>,
        INamedBuilder<EnumValueBuilder>,
        IDescriptionBuilder<EnumValueBuilder>,
        IInfrastructure<InternalEnumValueBuilder>
    {
        EnumValueBuilder CustomValue(object value);
        EnumValueBuilder RemoveCustomValue();
    }
}