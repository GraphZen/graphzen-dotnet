// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

#nullable enable

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    public partial class ArgumentSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ArgumentSyntax" /> nodes.</summary>
        public static IReadOnlyList<ArgumentSyntax> EmptyList { get; } = ImmutableList<ArgumentSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ArgumentSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterArgument(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ArgumentSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveArgument(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ArgumentSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterArgument(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ArgumentSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveArgument(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.Argument;
    }

    public partial class BooleanValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="BooleanValueSyntax" /> nodes.</summary>
        public static IReadOnlyList<BooleanValueSyntax> EmptyList { get; } = ImmutableList<BooleanValueSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="BooleanValueSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterBooleanValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="BooleanValueSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveBooleanValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="BooleanValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterBooleanValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="BooleanValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveBooleanValue(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.BooleanValue;
    }

    public partial class DirectiveDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="DirectiveDefinitionSyntax" /> nodes.</summary>
        public static IReadOnlyList<DirectiveDefinitionSyntax> EmptyList { get; } =
            ImmutableList<DirectiveDefinitionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="DirectiveDefinitionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterDirectiveDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="DirectiveDefinitionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveDirectiveDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="DirectiveDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterDirectiveDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="DirectiveDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveDirectiveDefinition(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.DirectiveDefinition;
    }

    public partial class DirectiveSyntax
    {
        /// <summary>Empty, read-only list of <see cref="DirectiveSyntax" /> nodes.</summary>
        public static IReadOnlyList<DirectiveSyntax> EmptyList { get; } = ImmutableList<DirectiveSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="DirectiveSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterDirective(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="DirectiveSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveDirective(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="DirectiveSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterDirective(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="DirectiveSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveDirective(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.Directive;
    }

    public partial class DocumentSyntax
    {
        /// <summary>Empty, read-only list of <see cref="DocumentSyntax" /> nodes.</summary>
        public static IReadOnlyList<DocumentSyntax> EmptyList { get; } = ImmutableList<DocumentSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="DocumentSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterDocument(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="DocumentSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveDocument(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="DocumentSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterDocument(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="DocumentSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveDocument(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.Document;
    }

    public partial class EnumTypeDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="EnumTypeDefinitionSyntax" /> nodes.</summary>
        public static IReadOnlyList<EnumTypeDefinitionSyntax> EmptyList { get; } =
            ImmutableList<EnumTypeDefinitionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="EnumTypeDefinitionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterEnumTypeDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="EnumTypeDefinitionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveEnumTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="EnumTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterEnumTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="EnumTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveEnumTypeDefinition(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.EnumTypeDefinition;
    }

    public partial class EnumTypeExtensionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="EnumTypeExtensionSyntax" /> nodes.</summary>
        public static IReadOnlyList<EnumTypeExtensionSyntax> EmptyList { get; } =
            ImmutableList<EnumTypeExtensionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="EnumTypeExtensionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterEnumTypeExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="EnumTypeExtensionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveEnumTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="EnumTypeExtensionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterEnumTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="EnumTypeExtensionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveEnumTypeExtension(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.EnumTypeExtension;
    }

    public partial class EnumValueDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="EnumValueDefinitionSyntax" /> nodes.</summary>
        public static IReadOnlyList<EnumValueDefinitionSyntax> EmptyList { get; } =
            ImmutableList<EnumValueDefinitionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="EnumValueDefinitionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterEnumValueDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="EnumValueDefinitionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveEnumValueDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="EnumValueDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterEnumValueDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="EnumValueDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveEnumValueDefinition(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.EnumValueDefinition;
    }

    public partial class EnumValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="EnumValueSyntax" /> nodes.</summary>
        public static IReadOnlyList<EnumValueSyntax> EmptyList { get; } = ImmutableList<EnumValueSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="EnumValueSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterEnumValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="EnumValueSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveEnumValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="EnumValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterEnumValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="EnumValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveEnumValue(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.EnumValue;
    }

    public partial class FieldDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="FieldDefinitionSyntax" /> nodes.</summary>
        public static IReadOnlyList<FieldDefinitionSyntax> EmptyList { get; } =
            ImmutableList<FieldDefinitionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="FieldDefinitionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterFieldDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="FieldDefinitionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveFieldDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="FieldDefinitionSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterFieldDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="FieldDefinitionSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveFieldDefinition(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.FieldDefinition;
    }

    public partial class FieldSyntax
    {
        /// <summary>Empty, read-only list of <see cref="FieldSyntax" /> nodes.</summary>
        public static IReadOnlyList<FieldSyntax> EmptyList { get; } = ImmutableList<FieldSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="FieldSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterField(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="FieldSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveField(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="FieldSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) => visitor.EnterField(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="FieldSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) => visitor.LeaveField(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.Field;
    }

    public partial class FloatValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="FloatValueSyntax" /> nodes.</summary>
        public static IReadOnlyList<FloatValueSyntax> EmptyList { get; } = ImmutableList<FloatValueSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="FloatValueSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterFloatValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="FloatValueSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveFloatValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="FloatValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterFloatValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="FloatValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveFloatValue(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.FloatValue;
    }

    public partial class FragmentDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="FragmentDefinitionSyntax" /> nodes.</summary>
        public static IReadOnlyList<FragmentDefinitionSyntax> EmptyList { get; } =
            ImmutableList<FragmentDefinitionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="FragmentDefinitionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterFragmentDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="FragmentDefinitionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveFragmentDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="FragmentDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterFragmentDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="FragmentDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveFragmentDefinition(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.FragmentDefinition;
    }

    public partial class FragmentSpreadSyntax
    {
        /// <summary>Empty, read-only list of <see cref="FragmentSpreadSyntax" /> nodes.</summary>
        public static IReadOnlyList<FragmentSpreadSyntax> EmptyList { get; } =
            ImmutableList<FragmentSpreadSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="FragmentSpreadSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterFragmentSpread(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="FragmentSpreadSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveFragmentSpread(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="FragmentSpreadSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterFragmentSpread(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="FragmentSpreadSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveFragmentSpread(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.FragmentSpread;
    }

    public partial class InlineFragmentSyntax
    {
        /// <summary>Empty, read-only list of <see cref="InlineFragmentSyntax" /> nodes.</summary>
        public static IReadOnlyList<InlineFragmentSyntax> EmptyList { get; } =
            ImmutableList<InlineFragmentSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="InlineFragmentSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterInlineFragment(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="InlineFragmentSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveInlineFragment(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="InlineFragmentSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterInlineFragment(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="InlineFragmentSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveInlineFragment(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.InlineFragment;
    }

    public partial class InputObjectTypeDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="InputObjectTypeDefinitionSyntax" /> nodes.</summary>
        public static IReadOnlyList<InputObjectTypeDefinitionSyntax> EmptyList { get; } =
            ImmutableList<InputObjectTypeDefinitionSyntax>.Empty;

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="InputObjectTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterInputObjectTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="InputObjectTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveInputObjectTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a
        ///     <see cref="InputObjectTypeDefinitionSyntax" /> node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterInputObjectTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a
        ///     <see cref="InputObjectTypeDefinitionSyntax" /> node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveInputObjectTypeDefinition(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.InputObjectTypeDefinition;
    }

    public partial class InputObjectTypeExtensionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="InputObjectTypeExtensionSyntax" /> nodes.</summary>
        public static IReadOnlyList<InputObjectTypeExtensionSyntax> EmptyList { get; } =
            ImmutableList<InputObjectTypeExtensionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="InputObjectTypeExtensionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterInputObjectTypeExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="InputObjectTypeExtensionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveInputObjectTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a
        ///     <see cref="InputObjectTypeExtensionSyntax" /> node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterInputObjectTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a
        ///     <see cref="InputObjectTypeExtensionSyntax" /> node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveInputObjectTypeExtension(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.InputObjectTypeExtension;
    }

    public partial class InputValueDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="InputValueDefinitionSyntax" /> nodes.</summary>
        public static IReadOnlyList<InputValueDefinitionSyntax> EmptyList { get; } =
            ImmutableList<InputValueDefinitionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="InputValueDefinitionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterInputValueDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="InputValueDefinitionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveInputValueDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="InputValueDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterInputValueDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="InputValueDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveInputValueDefinition(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.InputValueDefinition;
    }

    public partial class InterfaceTypeDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="InterfaceTypeDefinitionSyntax" /> nodes.</summary>
        public static IReadOnlyList<InterfaceTypeDefinitionSyntax> EmptyList { get; } =
            ImmutableList<InterfaceTypeDefinitionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="InterfaceTypeDefinitionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterInterfaceTypeDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="InterfaceTypeDefinitionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveInterfaceTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a
        ///     <see cref="InterfaceTypeDefinitionSyntax" /> node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterInterfaceTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a
        ///     <see cref="InterfaceTypeDefinitionSyntax" /> node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveInterfaceTypeDefinition(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.InterfaceTypeDefinition;
    }

    public partial class InterfaceTypeExtensionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="InterfaceTypeExtensionSyntax" /> nodes.</summary>
        public static IReadOnlyList<InterfaceTypeExtensionSyntax> EmptyList { get; } =
            ImmutableList<InterfaceTypeExtensionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="InterfaceTypeExtensionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterInterfaceTypeExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="InterfaceTypeExtensionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveInterfaceTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a
        ///     <see cref="InterfaceTypeExtensionSyntax" /> node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterInterfaceTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a
        ///     <see cref="InterfaceTypeExtensionSyntax" /> node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveInterfaceTypeExtension(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.InterfaceTypeExtension;
    }

    public partial class IntValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="IntValueSyntax" /> nodes.</summary>
        public static IReadOnlyList<IntValueSyntax> EmptyList { get; } = ImmutableList<IntValueSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="IntValueSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterIntValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="IntValueSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveIntValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="IntValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterIntValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="IntValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveIntValue(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.IntValue;
    }

    public partial class ListTypeSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ListTypeSyntax" /> nodes.</summary>
        public static IReadOnlyList<ListTypeSyntax> EmptyList { get; } = ImmutableList<ListTypeSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ListTypeSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterListType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ListTypeSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveListType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ListTypeSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterListType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ListTypeSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveListType(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.ListType;
    }

    public partial class ListValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ListValueSyntax" /> nodes.</summary>
        public static IReadOnlyList<ListValueSyntax> EmptyList { get; } = ImmutableList<ListValueSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ListValueSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterListValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ListValueSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveListValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ListValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterListValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ListValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveListValue(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.ListValue;
    }

    public partial class NamedTypeSyntax
    {
        /// <summary>Empty, read-only list of <see cref="NamedTypeSyntax" /> nodes.</summary>
        public static IReadOnlyList<NamedTypeSyntax> EmptyList { get; } = ImmutableList<NamedTypeSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="NamedTypeSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterNamedType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="NamedTypeSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveNamedType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="NamedTypeSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterNamedType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="NamedTypeSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveNamedType(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.NamedType;
    }

    public partial class NameSyntax
    {
        /// <summary>Empty, read-only list of <see cref="NameSyntax" /> nodes.</summary>
        public static IReadOnlyList<NameSyntax> EmptyList { get; } = ImmutableList<NameSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="NameSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterName(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="NameSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveName(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="NameSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) => visitor.EnterName(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="NameSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) => visitor.LeaveName(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.Name;
    }

    public partial class NonNullTypeSyntax
    {
        /// <summary>Empty, read-only list of <see cref="NonNullTypeSyntax" /> nodes.</summary>
        public static IReadOnlyList<NonNullTypeSyntax> EmptyList { get; } = ImmutableList<NonNullTypeSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="NonNullTypeSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterNonNullType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="NonNullTypeSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveNonNullType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="NonNullTypeSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterNonNullType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="NonNullTypeSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveNonNullType(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.NonNullType;
    }

    public partial class NullValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="NullValueSyntax" /> nodes.</summary>
        public static IReadOnlyList<NullValueSyntax> EmptyList { get; } = ImmutableList<NullValueSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="NullValueSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterNullValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="NullValueSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveNullValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="NullValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterNullValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="NullValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveNullValue(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.NullValue;
    }

    public partial class ObjectFieldSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ObjectFieldSyntax" /> nodes.</summary>
        public static IReadOnlyList<ObjectFieldSyntax> EmptyList { get; } = ImmutableList<ObjectFieldSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ObjectFieldSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterObjectField(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ObjectFieldSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveObjectField(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ObjectFieldSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterObjectField(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ObjectFieldSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveObjectField(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.ObjectField;
    }

    public partial class ObjectTypeDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ObjectTypeDefinitionSyntax" /> nodes.</summary>
        public static IReadOnlyList<ObjectTypeDefinitionSyntax> EmptyList { get; } =
            ImmutableList<ObjectTypeDefinitionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ObjectTypeDefinitionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterObjectTypeDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ObjectTypeDefinitionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveObjectTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ObjectTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterObjectTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ObjectTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveObjectTypeDefinition(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.ObjectTypeDefinition;
    }

    public partial class ObjectTypeExtensionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ObjectTypeExtensionSyntax" /> nodes.</summary>
        public static IReadOnlyList<ObjectTypeExtensionSyntax> EmptyList { get; } =
            ImmutableList<ObjectTypeExtensionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ObjectTypeExtensionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterObjectTypeExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ObjectTypeExtensionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveObjectTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ObjectTypeExtensionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterObjectTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ObjectTypeExtensionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveObjectTypeExtension(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.ObjectTypeExtension;
    }

    public partial class ObjectValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ObjectValueSyntax" /> nodes.</summary>
        public static IReadOnlyList<ObjectValueSyntax> EmptyList { get; } = ImmutableList<ObjectValueSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ObjectValueSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterObjectValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ObjectValueSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveObjectValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ObjectValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterObjectValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ObjectValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveObjectValue(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.ObjectValue;
    }

    public partial class OperationDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="OperationDefinitionSyntax" /> nodes.</summary>
        public static IReadOnlyList<OperationDefinitionSyntax> EmptyList { get; } =
            ImmutableList<OperationDefinitionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="OperationDefinitionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterOperationDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="OperationDefinitionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveOperationDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="OperationDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterOperationDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="OperationDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveOperationDefinition(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.OperationDefinition;
    }

    public partial class OperationTypeDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="OperationTypeDefinitionSyntax" /> nodes.</summary>
        public static IReadOnlyList<OperationTypeDefinitionSyntax> EmptyList { get; } =
            ImmutableList<OperationTypeDefinitionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="OperationTypeDefinitionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterOperationTypeDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="OperationTypeDefinitionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveOperationTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a
        ///     <see cref="OperationTypeDefinitionSyntax" /> node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterOperationTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a
        ///     <see cref="OperationTypeDefinitionSyntax" /> node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveOperationTypeDefinition(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.OperationTypeDefinition;
    }

    public partial class PunctuatorSyntax
    {
        /// <summary>Empty, read-only list of <see cref="PunctuatorSyntax" /> nodes.</summary>
        public static IReadOnlyList<PunctuatorSyntax> EmptyList { get; } = ImmutableList<PunctuatorSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="PunctuatorSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterPunctuator(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="PunctuatorSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeavePunctuator(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="PunctuatorSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterPunctuator(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="PunctuatorSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeavePunctuator(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.Punctuator;
    }

    public partial class ScalarTypeDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ScalarTypeDefinitionSyntax" /> nodes.</summary>
        public static IReadOnlyList<ScalarTypeDefinitionSyntax> EmptyList { get; } =
            ImmutableList<ScalarTypeDefinitionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ScalarTypeDefinitionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterScalarTypeDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ScalarTypeDefinitionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveScalarTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ScalarTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterScalarTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ScalarTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveScalarTypeDefinition(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.ScalarTypeDefinition;
    }

    public partial class ScalarTypeExtensionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ScalarTypeExtensionSyntax" /> nodes.</summary>
        public static IReadOnlyList<ScalarTypeExtensionSyntax> EmptyList { get; } =
            ImmutableList<ScalarTypeExtensionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ScalarTypeExtensionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterScalarTypeExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ScalarTypeExtensionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveScalarTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ScalarTypeExtensionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterScalarTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ScalarTypeExtensionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveScalarTypeExtension(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.ScalarTypeExtension;
    }

    public partial class SchemaDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="SchemaDefinitionSyntax" /> nodes.</summary>
        public static IReadOnlyList<SchemaDefinitionSyntax> EmptyList { get; } =
            ImmutableList<SchemaDefinitionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="SchemaDefinitionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterSchemaDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="SchemaDefinitionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveSchemaDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="SchemaDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterSchemaDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="SchemaDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveSchemaDefinition(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.SchemaDefinition;
    }

    public partial class SchemaExtensionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="SchemaExtensionSyntax" /> nodes.</summary>
        public static IReadOnlyList<SchemaExtensionSyntax> EmptyList { get; } =
            ImmutableList<SchemaExtensionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="SchemaExtensionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterSchemaExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="SchemaExtensionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveSchemaExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="SchemaExtensionSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterSchemaExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="SchemaExtensionSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveSchemaExtension(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.SchemaExtension;
    }

    public partial class SelectionSetSyntax
    {
        /// <summary>Empty, read-only list of <see cref="SelectionSetSyntax" /> nodes.</summary>
        public static IReadOnlyList<SelectionSetSyntax> EmptyList { get; } = ImmutableList<SelectionSetSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="SelectionSetSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterSelectionSet(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="SelectionSetSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveSelectionSet(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="SelectionSetSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterSelectionSet(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="SelectionSetSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveSelectionSet(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.SelectionSet;
    }

    public partial class StringValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="StringValueSyntax" /> nodes.</summary>
        public static IReadOnlyList<StringValueSyntax> EmptyList { get; } = ImmutableList<StringValueSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="StringValueSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterStringValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="StringValueSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveStringValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="StringValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterStringValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="StringValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveStringValue(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.StringValue;
    }

    public partial class UnionTypeDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="UnionTypeDefinitionSyntax" /> nodes.</summary>
        public static IReadOnlyList<UnionTypeDefinitionSyntax> EmptyList { get; } =
            ImmutableList<UnionTypeDefinitionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="UnionTypeDefinitionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterUnionTypeDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="UnionTypeDefinitionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveUnionTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="UnionTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterUnionTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="UnionTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveUnionTypeDefinition(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.UnionTypeDefinition;
    }

    public partial class UnionTypeExtensionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="UnionTypeExtensionSyntax" /> nodes.</summary>
        public static IReadOnlyList<UnionTypeExtensionSyntax> EmptyList { get; } =
            ImmutableList<UnionTypeExtensionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="UnionTypeExtensionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterUnionTypeExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="UnionTypeExtensionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveUnionTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="UnionTypeExtensionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterUnionTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="UnionTypeExtensionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveUnionTypeExtension(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.UnionTypeExtension;
    }

    public partial class VariableDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="VariableDefinitionSyntax" /> nodes.</summary>
        public static IReadOnlyList<VariableDefinitionSyntax> EmptyList { get; } =
            ImmutableList<VariableDefinitionSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="VariableDefinitionSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterVariableDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="VariableDefinitionSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveVariableDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="VariableDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterVariableDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="VariableDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveVariableDefinition(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.VariableDefinition;
    }

    public partial class VariableSyntax
    {
        /// <summary>Empty, read-only list of <see cref="VariableSyntax" /> nodes.</summary>
        public static IReadOnlyList<VariableSyntax> EmptyList { get; } = ImmutableList<VariableSyntax>.Empty;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="VariableSyntax" /> node.</summary>
        public override void VisitEnter(GraphQLSyntaxVisitor visitor) => visitor.EnterVariable(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="VariableSyntax" /> node.</summary>
        public override void VisitLeave(GraphQLSyntaxVisitor visitor) => visitor.LeaveVariable(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="VariableSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterVariable(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="VariableSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>(GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveVariable(this);

        public override SyntaxKind Kind { get; } = SyntaxKind.Variable;
    }
}