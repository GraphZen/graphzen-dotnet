// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;


// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.TypeSystem
{
    public static partial class EnumerableBuilderExtensions
    {
        #region EnumerableBuilderExtensionsGenerator




        public static IEnumerable<ObjectTypeBuilder> Where(this IEnumerable<ObjectTypeBuilder> source,
            Func<IObjectTypeDefinition, bool> predicate) => Enumerable.Where(source, _ => predicate(_.GetInfrastructure<IObjectTypeDefinition>()));





        #endregion
    }
}
// Source Hash Code: 16880981642281738278