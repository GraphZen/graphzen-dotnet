// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Language;
using GraphZen.Types;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GraphZen.Infrastructure
{
    public class GraphQLError
    {
        public GraphQLError(string message,
            IReadOnlyList<SyntaxNode> nodes = null,
            Source source = null,
            IReadOnlyList<int> positions = null,
            IReadOnlyList<object> path = null,
            Exception innerException = null
        )
        {
            Message = Check.NotNull(message, nameof(message));
            Nodes = nodes;
            Path = path;
            Positions = positions ?? Nodes?.Where(_ => _?.Location != null).Select(_ => _.Location.Start).ToList();
            Positions = Positions != null && Positions.Count == 0 ? null : Positions;
            Source = source ?? nodes?.FirstOrDefault()?.Location?.Source;
            InnerException = innerException;
            if (Positions != null && Source != null)
            {
                Locations = Positions.Select(Source.GetLocation).ToList();
            }
            else if (Nodes != null && Source != null)
            {
                Locations = Nodes
                    .Where(_ => _?.Location != null)
                    .Select(n =>
                    {
                        Debug.Assert(n.Location != null, "n.Location != null");
                        return Source.GetLocation(n.Location.Start);
                    }).ToList();
            }
        }

        [NotNull]
        public string Message { get; internal set; }

        [JsonIgnore]
        [CanBeNull]
        [ItemNotNull]
        public IReadOnlyList<SyntaxNode> Nodes { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IReadOnlyList<SourceLocation> Locations { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IReadOnlyList<object> Path { get; }

        [JsonIgnore]
        public IReadOnlyList<int> Positions { get; }

        [JsonIgnore]
        public Source Source { get; }

        [JsonIgnore]
        public Exception InnerException { get; }

        private bool Equals([NotNull] GraphQLError other) => string.Equals(Message, other.Message) &&
                                                             Equals(Locations, other.Locations) &&
                                                             Equals(Path, other.Path);

        public override bool Equals(object obj)
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

            return Equals((GraphQLError) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // ReSharper disable once NonReadonlyMemberInGetHashCode
                var hashCode = Message.GetHashCode();
                hashCode = (hashCode * 397) ^ (Locations != null ? Locations.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Path != null ? Path.GetHashCode() : 0);
                return hashCode;
            }
        }

        public void Throw() => throw new GraphQLException(this);

        public override string ToString() => Json.SerializeObject(this) ?? Message;


        public GraphQLError WithLocationInfo(IReadOnlyList<SyntaxNode> nodes, ResponsePath path) =>
            new GraphQLError(Message, nodes, Source, Positions, Check.NotNull(path, nameof(path)).AsReadOnlyList(),
                InnerException);
    }
}