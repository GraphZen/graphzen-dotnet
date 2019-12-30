// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;




namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Input object
    ///     http://facebook.github.io/graphql/June2018/#ObjectValue
    /// </summary>
    public partial class ObjectValueSyntax : ValueSyntax
    {
        public ObjectValueSyntax(IReadOnlyList<ObjectFieldSyntax> fields, SyntaxLocation? location = null) :
            base(location)
        {
            Fields = Check.NotNull(fields, nameof(fields));
        }

        /// <summary>
        ///     The fields of an object.
        /// </summary>


        public IReadOnlyList<ObjectFieldSyntax> Fields { get; }

        public override IEnumerable<SyntaxNode> Children => Fields;

        private bool Equals(ObjectValueSyntax other) => Fields.SequenceEqual(other.Fields);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            return obj is ObjectValueSyntax && Equals((ObjectValueSyntax) obj);
        }

        public override int GetHashCode() => Fields.GetHashCode();


        public override object GetValue() => Fields;
    }
}