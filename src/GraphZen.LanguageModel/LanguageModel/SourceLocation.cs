// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel;

public struct SourceLocation : IEquatable<SourceLocation>
{
    public SourceLocation(int line, int column)
    {
        Line = line;
        Column = column;
    }

    public int Line { get; }
    public int Column { get; }

    public bool Equals(SourceLocation other) => Line == other.Line && Column == other.Column;

    public override bool Equals(object? obj) => obj is SourceLocation other && Equals(other);

    public override int GetHashCode()
    {
        unchecked
        {
            return (Line * 397) ^ Column;
        }
    }

    public static bool operator ==(SourceLocation left, SourceLocation right) => left.Equals(right);

    public static bool operator !=(SourceLocation left, SourceLocation right) => !left.Equals(right);
}