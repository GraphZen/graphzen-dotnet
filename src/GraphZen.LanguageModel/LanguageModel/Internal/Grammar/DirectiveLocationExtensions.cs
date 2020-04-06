using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel.Internal
{
    public static class DirectiveLocationExtensions
    {
        private static readonly Dictionary<DirectiveLocation, string> DirectiveLocationDisplayValues =
            new Dictionary<DirectiveLocation, string>
            {
                {DirectiveLocation.Query, "query"},
                {DirectiveLocation.Schema, "schema"},
                {DirectiveLocation.ArgumentDefinition, "argument"},
                {DirectiveLocation.Enum, "enum"},
                {DirectiveLocation.EnumValue, "enum value"},
                {DirectiveLocation.Object, "object"},
                {DirectiveLocation.InputObject, "input object"},
                {DirectiveLocation.InputFieldDefinition, "input field"},
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
            };

        public static string GetDisplayValue(this DirectiveLocation directiveLocation) =>
            DirectiveLocationDisplayValues[directiveLocation];
    }
}