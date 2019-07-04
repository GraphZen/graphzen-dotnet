// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface ILeafType : INamedType
    {
        [NotNull]
        Maybe<object> Serialize([CanBeNull] object value);

        bool IsValidValue([CanBeNull] string value);
        bool IsValidLiteral([NotNull] ValueSyntax value);

        [NotNull]
        Maybe<object> ParseValue([CanBeNull] object value);

        [NotNull]
        Maybe<object> ParseLiteral(ValueSyntax value);
    }
}