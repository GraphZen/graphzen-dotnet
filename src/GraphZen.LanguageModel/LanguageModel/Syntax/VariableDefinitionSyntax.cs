// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Variable definition
    ///     http://facebook.github.io/graphql/June2018/#VariableDefinition
    /// </summary>
    public partial class VariableDefinitionSyntax : SyntaxNode
    {
        public VariableDefinitionSyntax(VariableSyntax variable, TypeSyntax type,
            ValueSyntax defaultValue = null, SyntaxLocation location = null) : base(location)
        {
            Variable = Check.NotNull(variable, nameof(variable));
            VariableType = Check.NotNull(type, nameof(type));
            DefaultValue = defaultValue;
        }


        /// <summary>
        ///     The variable being assigned.
        /// </summary>

        public VariableSyntax Variable { get; }

        /// <summary>
        ///     The type of the variable.
        /// </summary>

        public TypeSyntax VariableType { get; }

        /// <summary>
        ///     The variable's default value. (Optional)
        /// </summary>
        public ValueSyntax DefaultValue { get; }

        public override IEnumerable<SyntaxNode> Children
        {
            get
            {
                yield return Variable;
                yield return VariableType;
                if (DefaultValue != null) yield return DefaultValue;
            }
        }


        private bool Equals(VariableDefinitionSyntax other) =>
            Variable.Equals(other.Variable)
            && VariableType.Equals(other.VariableType)
            && Equals(DefaultValue, other.DefaultValue);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            if (ReferenceEquals(this, obj)) return true;

            return obj is VariableDefinitionSyntax && Equals((VariableDefinitionSyntax) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Variable.GetHashCode();
                hashCode = (hashCode * 397) ^ VariableType.GetHashCode();
                hashCode = (hashCode * 397) ^ (DefaultValue != null ? DefaultValue.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}