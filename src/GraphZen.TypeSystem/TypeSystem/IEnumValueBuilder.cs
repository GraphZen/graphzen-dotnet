// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;


namespace GraphZen.TypeSystem
{
    public interface IEnumValueBuilder : IAnnotableBuilder<IEnumValueBuilder>
    {
        [NotNull]
        IEnumValueBuilder Description([CanBeNull] string description);

        [NotNull]
        IEnumValueBuilder CustomValue(object value);

        [NotNull]
        IEnumValueBuilder Deprecated(bool deprecated = true);

        [NotNull]
        IEnumValueBuilder Deprecated(string reason);
    }
}