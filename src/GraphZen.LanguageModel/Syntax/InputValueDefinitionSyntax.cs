// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Infrastructure.Extensions;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Input value definition
    ///     http://facebook.github.io/graphql/June2018/#InputValueDefinition
    /// </summary>
    public partial class InputValueDefinitionSyntax : SyntaxNode, IDirectivesSyntax, IDescribedSyntax
    {
        public InputValueDefinitionSyntax(
            NameSyntax name,
            TypeSyntax type,
            StringValueSyntax description = null,
            ValueSyntax defaultValue = null,
            IReadOnlyList<DirectiveSyntax> directives = null,
            SyntaxLocation location = null) : base(location)
        {
            Name = Check.NotNull(name, nameof(name));
            Type = Check.NotNull(type, nameof(type));
            DefaultValue = defaultValue;
            Directives = directives ?? DirectiveSyntax.EmptyList;
            Description = description;
        }

        /// <summary>
        ///     The name node of the input value.
        /// </summary>
        [NotNull]
        public NameSyntax Name { get; }


        /// <summary>
        ///     The type of the input value.
        /// </summary>
        [NotNull]
        public TypeSyntax Type { get; }

        /// <summary>
        ///     The default input value. (Optional)
        /// </summary>
        [CanBeNull]
        public ValueSyntax DefaultValue { get; }


        public override IEnumerable<SyntaxNode> Children => NodeExtensions.Concat(Name.ToEnumerable()
                .Concat(Type)
                .Concat(DefaultValue), (IEnumerable<SyntaxNode>) Directives);


        public StringValueSyntax Description { get; }

        /// <summary>
        ///     Input value directives.
        /// </summary>
        public IReadOnlyList<DirectiveSyntax> Directives { get; }


        private bool Equals([NotNull] LanguageModel.InputValueDefinitionSyntax other) =>
            Name.Equals(other.Name) && Equals(Description, other.Description) && Type.Equals(other.Type) &&
            Equals(DefaultValue, other.DefaultValue) && Directives.SequenceEqual(other.Directives);

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

            return obj is LanguageModel.InputValueDefinitionSyntax && Equals((LanguageModel.InputValueDefinitionSyntax) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name.GetHashCode();
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Type.GetHashCode();
                hashCode = (hashCode * 397) ^ (DefaultValue != null ? DefaultValue.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Directives.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString() => Name.ToString();
    }
}