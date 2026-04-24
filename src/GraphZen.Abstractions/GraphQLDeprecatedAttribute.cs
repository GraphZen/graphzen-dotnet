// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using GraphZen.LanguageModel;

namespace GraphZen;

[Description("Marks an element of a GraphQL schema as no longer supported.")]
[ExcludeFromCodeCoverage]
public class GraphQLDeprecatedAttribute : Attribute, IGraphQLDirective
{
    /// <summary>
    ///     Marks an element of a GraphQL schema as no longer supported.
    /// </summary>
    /// <param name="reason">
    ///     Explains why this element was deprecated, usually also including a
    ///     suggestion for how to access supported similar data. Formatted
    ///     in [Markdown](https://daringfireball.net/projects/markdown/).
    /// </param>
    public GraphQLDeprecatedAttribute(
        [Description("Explains why this element was deprecated, usually also including a " +
                     "suggestion for how to access supported similar data. Formatted " +
                     "in [Markdown](https://daringfireball.net/projects/markdown/).")]
        string? reason = null) =>
        Reason = reason;


    [GraphQLCanBeNull] public string? Reason { get; }

    public IEnumerable<DirectiveLocation> GetDirectiveLocations()
    {
        yield return DirectiveLocation.FieldDefinition;
        yield return DirectiveLocation.EnumValue;
    }

    private bool Equals(GraphQLDeprecatedAttribute other) => base.Equals(other) && Reason == other.Reason;

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((GraphQLDeprecatedAttribute)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (base.GetHashCode() * 397) ^ (Reason != null ? Reason.GetHashCode() : 0);
        }
    }
}
