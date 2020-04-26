// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    public class Source
    {
        private static readonly Regex LineBreak = new Regex("\r\n?|\n");

        public Source(string body)
        {
            Body = Check.NotNull(body, nameof(body));
        }


        public string Body { get; }

        protected bool Equals(Source other) => string.Equals(Body, other.Body);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
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

            return Equals((Source)obj);
        }

        public override int GetHashCode() => Body.GetHashCode();

        public SourceLocation GetLocation(int position)
        {
            var line = 1;
            var column = position + 1;

            var matches = LineBreak.Matches(Body);
            for (var i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                if (match.Index >= position)
                {
                    break;
                }

                line += 1;
                column = position + 1 - (match.Index + match.Length);
            }

            return new SourceLocation(line, column);
        }
    }
}