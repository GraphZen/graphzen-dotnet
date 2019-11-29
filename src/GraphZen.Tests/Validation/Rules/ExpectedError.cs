// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GraphZen.Validation.Rules
{
    public class ExpectedError
    {
        public ExpectedError(GraphQLServerError error) : this(error.Message, error.Locations, error.Path)
        {
        }


        public ExpectedError(string message, IReadOnlyList<SourceLocation>? locations, IReadOnlyList<object>? path)
        {
            Message = message;
            Locations = locations != null && locations.Any() ? locations : null;
            Path = path;
        }

        public string Message { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IReadOnlyList<SourceLocation>? Locations { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IReadOnlyList<object>? Path { get; }


        protected bool Equals(ExpectedError other) =>
            string.Equals(Message, other.Message) &&
            (Locations == null && other.Locations == null ||
             // ReSharper disable once AssignNullToNotNullAttribute
             Locations.SequenceEqual(other.Locations))
            &&
            (Path == null && other.Path == null ||
             // ReSharper disable once AssignNullToNotNullAttribute
             Path.SequenceEqual(other.Path));


        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (obj.GetType() != GetType()) return false;

            return Equals((ExpectedError)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Message != null ? Message.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (Locations != null ? Locations.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Path != null ? Path.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}