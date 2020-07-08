// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

// ReSharper disable InconsistentNaming
// ReSharper disable once PossibleInterfaceMemberAmbiguity

namespace GraphZen.TypeSystem
{
    public static partial class EnumerableBuilderExtensions
    {
        #region EnumerableBuilderExtensionsGenerator

        public static IEnumerable<DirectiveBuilder> Where(this IEnumerable<DirectiveBuilder> source,
            Func<IDirectiveDefinition, bool> predicate) =>
            source.Where(_ => predicate(_.GetInfrastructure<DirectiveDefinition>()));


        public static IEnumerable<ObjectTypeBuilder> Where(this IEnumerable<ObjectTypeBuilder> source,
            Func<IObjectTypeDefinition, bool> predicate) =>
            source.Where(_ => predicate(_.GetInfrastructure<ObjectTypeDefinition>()));


        public static IEnumerable<UnionTypeBuilder> Where(this IEnumerable<UnionTypeBuilder> source,
            Func<IUnionTypeDefinition, bool> predicate) =>
            source.Where(_ => predicate(_.GetInfrastructure<UnionTypeDefinition>()));


        public static IEnumerable<ScalarTypeBuilder> Where(this IEnumerable<ScalarTypeBuilder> source,
            Func<IScalarTypeDefinition, bool> predicate) =>
            source.Where(_ => predicate(_.GetInfrastructure<ScalarTypeDefinition>()));


        public static IEnumerable<EnumTypeBuilder> Where(this IEnumerable<EnumTypeBuilder> source,
            Func<IEnumTypeDefinition, bool> predicate) =>
            source.Where(_ => predicate(_.GetInfrastructure<EnumTypeDefinition>()));


        public static IEnumerable<InterfaceTypeBuilder> Where(this IEnumerable<InterfaceTypeBuilder> source,
            Func<IInterfaceTypeDefinition, bool> predicate) =>
            source.Where(_ => predicate(_.GetInfrastructure<InterfaceTypeDefinition>()));


        public static IEnumerable<InputObjectTypeBuilder> Where(this IEnumerable<InputObjectTypeBuilder> source,
            Func<IInputObjectTypeDefinition, bool> predicate) =>
            source.Where(_ => predicate(_.GetInfrastructure<InputObjectTypeDefinition>()));

        #endregion
    }
}
// Source Hash Code: 9642188662127746181