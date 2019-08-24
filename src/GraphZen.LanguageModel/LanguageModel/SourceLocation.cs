#nullable disable
// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel
{
    public struct SourceLocation
    {
        public SourceLocation(int line, int column)
        {
            Line = line;
            Column = column;
        }

        public int Line { get; }
        public int Column { get; }
    }
}