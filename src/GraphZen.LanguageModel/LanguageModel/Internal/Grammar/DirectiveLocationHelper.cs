// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GraphZen.Infrastructure;


namespace GraphZen.LanguageModel.Internal.Grammar
{
    internal static class DirectiveLocationHelper
    {
        [NotNull] internal static readonly IReadOnlyDictionary<DirectiveLocation, string> ExecutableDirectiveLocations =
            new ReadOnlyDictionary<DirectiveLocation, string>(
                new Dictionary<DirectiveLocation, string>
                {
                    {DirectiveLocation.Query, "QUERY"},
                    {DirectiveLocation.Mutation, "MUTATION"},
                    {DirectiveLocation.Subscription, "SUBSCRIPTION"},
                    {DirectiveLocation.Field, "FIELD"},
                    {DirectiveLocation.FragmentDefinition, "FRAGMENT_DEFINITION"},
                    {DirectiveLocation.FragmentSpread, "FRAGMENT_SPREAD"},
                    {DirectiveLocation.InlineFragment, "INLINE_FRAGMENT"}
                });

        [NotNull]
        internal static readonly IReadOnlyDictionary<string, DirectiveLocation> ExecutableDirectiveLocationsByName =
            ExecutableDirectiveLocations.ToDictionary(_ => _.Value, _ => _.Key);

        [NotNull] internal static readonly IReadOnlyDictionary<DirectiveLocation, string> TypeSystemDirectiveLocations =
            new ReadOnlyDictionary<DirectiveLocation, string>(
                new Dictionary<DirectiveLocation, string>
                {
                    {DirectiveLocation.Schema, "SCHEMA"},
                    {DirectiveLocation.Scalar, "SCALAR"},
                    {DirectiveLocation.Object, "OBJECT"},
                    {DirectiveLocation.FieldDefinition, "FIELD_DEFINITION"},
                    {DirectiveLocation.ArgumentDefinition, "ARGUMENT_DEFINITION"},
                    {DirectiveLocation.Interface, "INTERFACE"},
                    {DirectiveLocation.Union, "UNION"},
                    {DirectiveLocation.Enum, "ENUM"},
                    {DirectiveLocation.EnumValue, "ENUM_VALUE"},
                    {DirectiveLocation.InputObject, "INPUT_OBJECT"},
                    {DirectiveLocation.InputFieldDefinition, "INPUT_FIELD_DEFINITION"}
                });

        [NotNull]
        internal static readonly IReadOnlyDictionary<string, DirectiveLocation> TypeSystemDirectiveLocationsByName =
            TypeSystemDirectiveLocations.ToDictionary(_ => _.Value, _ => _.Key);


        internal static string ToStringValue(this DirectiveLocation loc)
        {
            if (ExecutableDirectiveLocations.TryGetValue(loc, out var value) ||
                TypeSystemDirectiveLocations.TryGetValue(loc, out value))
            {
                return value;
            }

            throw new Exception($"No string value defined for {loc}");
        }

        internal static DirectiveLocation Parse(string value)
        {
            Check.NotNull(value, nameof(value));
            if (TypeSystemDirectiveLocationsByName.TryGetValue(value, out var result))
            {
                return result;
            }

            if (ExecutableDirectiveLocationsByName.TryGetValue(value, out result))
            {
                return result;
            }

            throw new Exception($"Unable to find Directive Location that matches value \"{value}\".");
        }
    }
}