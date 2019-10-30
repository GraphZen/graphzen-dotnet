// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel
{
    [DebuggerDisplay("Start = {Start}, End = {End}")]
    public class SyntaxLocation
    {
        public SyntaxLocation(SyntaxNode start, SyntaxNode end) : this(Check.NotNull(start, nameof(start)).Location,
            Check.NotNull(end, nameof(end)).Location)
        {
        }


        public SyntaxLocation(SyntaxLocation start, SyntaxLocation end) :
            this(
                Check.NotNull(start, nameof(start)).Start,
                Check.NotNull(end, nameof(end)).End,
                start.Line,
                start.Column, start.Source)
        {
        }

        public SyntaxLocation(int start, int end, int line, int column, Source source)
        {
            if (start < 0) throw new ArgumentException($"Location start index ({start}) must be greater than 0.");

            if (end < start) throw new ArgumentException($"Location start (${start}) must precede end (${end}).");

            Start = start;
            End = end;
            Line = line;
            Column = column;
            Source = source;
        }

        public int Line { get; }
        public int Column { get; }

        public int Start { get; }
        public int End { get; }

        public Source Source { get; }

        protected bool Equals(SyntaxLocation other) => Start == other.Start && End == other.End;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != GetType()) return false;

            return Equals((SyntaxLocation)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Start * 397) ^ End;
            }
        }

        public static SyntaxLocation FromMany(params ISyntaxNodeLocation[] nodes)
        {
            return FromMany(nodes.Select(_ => _?.Location));
        }

        public static SyntaxLocation FromMany(params SyntaxLocation[] locations) => FromMany(locations.AsEnumerable());

        private static SyntaxLocation FromMany(IEnumerable<SyntaxLocation> locations)
        {
            Check.NotNull(locations, nameof(locations));

            var locs = locations.Where(l => l != null).OrderBy(_ => _.Start).ToArray();
            if (locs.Length == 0) return null;

            var min = locs[0];
            var max = locs[locs.Length - 1];

            Debug.Assert(min != null, nameof(min) + " != null");
            Debug.Assert(max != null, nameof(max) + " != null");
            return new SyntaxLocation(min.Start, max.End, min.Line, min.Column, min.Source);
        }
    }
}