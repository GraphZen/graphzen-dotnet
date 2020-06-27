// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel.Internal
{
    public static class DirectiveLocationExtensions
    {
        public static readonly ImmutableDictionary<DirectiveLocation, string> DirectiveLocationDisplayValues =
            new Dictionary<DirectiveLocation, string>
            {
                {DirectiveLocation.Query, "query"},
                {DirectiveLocation.Schema, "schema"},
                {DirectiveLocation.ArgumentDefinition, "argument"},
                {DirectiveLocation.Enum, "enum"},
                {DirectiveLocation.EnumValue, "enum value"},
                {DirectiveLocation.Object, "object"},
                {DirectiveLocation.InputObject, "input object"},
                {DirectiveLocation.InputFieldDefinition, "input field definition"},
                {DirectiveLocation.Field, "field"},
                {DirectiveLocation.FieldDefinition, "field definition"},
                {DirectiveLocation.FragmentDefinition, "fragment definition"},
                {DirectiveLocation.Mutation, "mutation"},
                {DirectiveLocation.Subscription, "subscription"},
                {DirectiveLocation.FragmentSpread, "fragment spread"},
                {DirectiveLocation.InlineFragment, "inline fragment"},
                {DirectiveLocation.Scalar, "scalar"},
                {DirectiveLocation.Union, "union"},
                {DirectiveLocation.Interface, "interface"}
            }.ToImmutableDictionary();

        public static string GetDisplayValue(this DirectiveLocation directiveLocation) =>
            DirectiveLocationDisplayValues[directiveLocation];

        public static string GetPluralizedDisplayValue(this DirectiveLocation directiveLocation) =>
            directiveLocation switch
            {
                DirectiveLocation.Schema => "the schema",
                DirectiveLocation.Query => "queries",
                _ => GetDisplayValue(directiveLocation) + "s"
            };
    }
}