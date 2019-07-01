// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Language
{
    public abstract class VisitAction
    {
        public static VisitAction Break { get; } = new Break();
        public static VisitAction Continue { get; } = new ContinueAction();
        public static VisitAction Skip { get; } = new Skip();

        public static implicit operator VisitAction(bool value) => value ? Continue : Skip;
    }
}