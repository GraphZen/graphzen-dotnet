// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel
{
    public enum SyntaxKind
    {
        /// <summary>Indicates an <see cref="ArgumentSyntax" /> node.</summary>
        Argument,

        /// <summary>Indicates an <see cref="BooleanValueSyntax" /> node.</summary>
        BooleanValue,

        /// <summary>Indicates an <see cref="DirectiveDefinitionSyntax" /> node.</summary>
        DirectiveDefinition,

        /// <summary>Indicates an <see cref="DirectiveSyntax" /> node.</summary>
        Directive,

        /// <summary>Indicates an <see cref="DocumentSyntax" /> node.</summary>
        Document,

        /// <summary>Indicates an <see cref="EnumTypeDefinitionSyntax" /> node.</summary>
        EnumTypeDefinition,

        /// <summary>Indicates an <see cref="EnumTypeExtensionSyntax" /> node.</summary>
        EnumTypeExtension,

        /// <summary>Indicates an <see cref="EnumValueDefinitionSyntax" /> node.</summary>
        EnumValueDefinition,

        /// <summary>Indicates an <see cref="EnumValueSyntax" /> node.</summary>
        EnumValue,

        /// <summary>Indicates an <see cref="FieldDefinitionSyntax" /> node.</summary>
        FieldDefinition,

        /// <summary>Indicates an <see cref="FieldSyntax" /> node.</summary>
        Field,

        /// <summary>Indicates an <see cref="FloatValueSyntax" /> node.</summary>
        FloatValue,

        /// <summary>Indicates an <see cref="FragmentDefinitionSyntax" /> node.</summary>
        FragmentDefinition,

        /// <summary>Indicates an <see cref="FragmentSpreadSyntax" /> node.</summary>
        FragmentSpread,

        /// <summary>Indicates an <see cref="InlineFragmentSyntax" /> node.</summary>
        InlineFragment,

        /// <summary>Indicates an <see cref="InputObjectTypeDefinitionSyntax" /> node.</summary>
        InputObjectTypeDefinition,

        /// <summary>Indicates an <see cref="InputObjectTypeExtensionSyntax" /> node.</summary>
        InputObjectTypeExtension,

        /// <summary>Indicates an <see cref="InputValueDefinitionSyntax" /> node.</summary>
        InputValueDefinition,

        /// <summary>Indicates an <see cref="InterfaceTypeDefinitionSyntax" /> node.</summary>
        InterfaceTypeDefinition,

        /// <summary>Indicates an <see cref="InterfaceTypeExtensionSyntax" /> node.</summary>
        InterfaceTypeExtension,

        /// <summary>Indicates an <see cref="IntValueSyntax" /> node.</summary>
        IntValue,

        /// <summary>Indicates an <see cref="ListTypeSyntax" /> node.</summary>
        ListType,

        /// <summary>Indicates an <see cref="ListValueSyntax" /> node.</summary>
        ListValue,

        /// <summary>Indicates an <see cref="NamedTypeSyntax" /> node.</summary>
        NamedType,

        /// <summary>Indicates an <see cref="NameSyntax" /> node.</summary>
        Name,

        /// <summary>Indicates an <see cref="NonNullTypeSyntax" /> node.</summary>
        NonNullType,

        /// <summary>Indicates an <see cref="NullValueSyntax" /> node.</summary>
        NullValue,

        /// <summary>Indicates an <see cref="ObjectFieldSyntax" /> node.</summary>
        ObjectField,

        /// <summary>Indicates an <see cref="ObjectTypeDefinitionSyntax" /> node.</summary>
        ObjectTypeDefinition,

        /// <summary>Indicates an <see cref="ObjectTypeExtensionSyntax" /> node.</summary>
        ObjectTypeExtension,

        /// <summary>Indicates an <see cref="ObjectValueSyntax" /> node.</summary>
        ObjectValue,

        /// <summary>Indicates an <see cref="OperationDefinitionSyntax" /> node.</summary>
        OperationDefinition,

        /// <summary>Indicates an <see cref="OperationTypeDefinitionSyntax" /> node.</summary>
        OperationTypeDefinition,

        /// <summary>Indicates an <see cref="PunctuatorSyntax" /> node.</summary>
        Punctuator,

        /// <summary>Indicates an <see cref="ScalarTypeDefinitionSyntax" /> node.</summary>
        ScalarTypeDefinition,

        /// <summary>Indicates an <see cref="ScalarTypeExtensionSyntax" /> node.</summary>
        ScalarTypeExtension,

        /// <summary>Indicates an <see cref="SchemaDefinitionSyntax" /> node.</summary>
        SchemaDefinition,

        /// <summary>Indicates an <see cref="SchemaExtensionSyntax" /> node.</summary>
        SchemaExtension,

        /// <summary>Indicates an <see cref="SelectionSetSyntax" /> node.</summary>
        SelectionSet,

        /// <summary>Indicates an <see cref="StringValueSyntax" /> node.</summary>
        StringValue,

        /// <summary>Indicates an <see cref="UnionTypeDefinitionSyntax" /> node.</summary>
        UnionTypeDefinition,

        /// <summary>Indicates an <see cref="UnionTypeExtensionSyntax" /> node.</summary>
        UnionTypeExtension,

        /// <summary>Indicates an <see cref="VariableDefinitionSyntax" /> node.</summary>
        VariableDefinition,

        /// <summary>Indicates an <see cref="VariableSyntax" /> node.</summary>
        Variable
    }

    public sealed partial class ArgumentSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ArgumentSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<ArgumentSyntax> EmptyList { get; } = new List<ArgumentSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.Argument;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ArgumentSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterArgument(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ArgumentSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveArgument(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ArgumentSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterArgument(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ArgumentSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveArgument(this);
    }

    public sealed partial class BooleanValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="BooleanValueSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<BooleanValueSyntax> EmptyList { get; } =
            new List<BooleanValueSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.BooleanValue;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="BooleanValueSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterBooleanValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="BooleanValueSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveBooleanValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="BooleanValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterBooleanValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="BooleanValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveBooleanValue(this);
    }

    public sealed partial class DirectiveDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="DirectiveDefinitionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<DirectiveDefinitionSyntax> EmptyList { get; } =
            new List<DirectiveDefinitionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.DirectiveDefinition;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="DirectiveDefinitionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.EnterDirectiveDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="DirectiveDefinitionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.LeaveDirectiveDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="DirectiveDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterDirectiveDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="DirectiveDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveDirectiveDefinition(this);
    }

    public sealed partial class DirectiveSyntax
    {
        /// <summary>Empty, read-only list of <see cref="DirectiveSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<DirectiveSyntax> EmptyList { get; } = new List<DirectiveSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.Directive;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="DirectiveSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterDirective(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="DirectiveSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveDirective(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="DirectiveSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterDirective(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="DirectiveSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveDirective(this);
    }

    public sealed partial class DocumentSyntax
    {
        /// <summary>Empty, read-only list of <see cref="DocumentSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<DocumentSyntax> EmptyList { get; } = new List<DocumentSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.Document;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="DocumentSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterDocument(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="DocumentSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveDocument(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="DocumentSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterDocument(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="DocumentSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveDocument(this);
    }

    public sealed partial class EnumTypeDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="EnumTypeDefinitionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<EnumTypeDefinitionSyntax> EmptyList { get; } =
            new List<EnumTypeDefinitionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.EnumTypeDefinition;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="EnumTypeDefinitionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.EnterEnumTypeDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="EnumTypeDefinitionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.LeaveEnumTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="EnumTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterEnumTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="EnumTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveEnumTypeDefinition(this);
    }

    public sealed partial class EnumTypeExtensionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="EnumTypeExtensionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<EnumTypeExtensionSyntax> EmptyList { get; } =
            new List<EnumTypeExtensionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.EnumTypeExtension;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="EnumTypeExtensionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterEnumTypeExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="EnumTypeExtensionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveEnumTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="EnumTypeExtensionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterEnumTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="EnumTypeExtensionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveEnumTypeExtension(this);
    }

    public sealed partial class EnumValueDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="EnumValueDefinitionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<EnumValueDefinitionSyntax> EmptyList { get; } =
            new List<EnumValueDefinitionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.EnumValueDefinition;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="EnumValueDefinitionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.EnterEnumValueDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="EnumValueDefinitionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.LeaveEnumValueDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="EnumValueDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterEnumValueDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="EnumValueDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveEnumValueDefinition(this);
    }

    public sealed partial class EnumValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="EnumValueSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<EnumValueSyntax> EmptyList { get; } = new List<EnumValueSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.EnumValue;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="EnumValueSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterEnumValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="EnumValueSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveEnumValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="EnumValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterEnumValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="EnumValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveEnumValue(this);
    }

    public sealed partial class FieldDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="FieldDefinitionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<FieldDefinitionSyntax> EmptyList { get; } =
            new List<FieldDefinitionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.FieldDefinition;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="FieldDefinitionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterFieldDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="FieldDefinitionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveFieldDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="FieldDefinitionSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterFieldDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="FieldDefinitionSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveFieldDefinition(this);
    }

    public sealed partial class FieldSyntax
    {
        /// <summary>Empty, read-only list of <see cref="FieldSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<FieldSyntax> EmptyList { get; } = new List<FieldSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.Field;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="FieldSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterField(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="FieldSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveField(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="FieldSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterField(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="FieldSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveField(this);
    }

    public sealed partial class FloatValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="FloatValueSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<FloatValueSyntax> EmptyList { get; } = new List<FloatValueSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.FloatValue;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="FloatValueSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterFloatValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="FloatValueSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveFloatValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="FloatValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterFloatValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="FloatValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveFloatValue(this);
    }

    public sealed partial class FragmentDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="FragmentDefinitionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<FragmentDefinitionSyntax> EmptyList { get; } =
            new List<FragmentDefinitionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.FragmentDefinition;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="FragmentDefinitionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.EnterFragmentDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="FragmentDefinitionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.LeaveFragmentDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="FragmentDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterFragmentDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="FragmentDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveFragmentDefinition(this);
    }

    public sealed partial class FragmentSpreadSyntax
    {
        /// <summary>Empty, read-only list of <see cref="FragmentSpreadSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<FragmentSpreadSyntax> EmptyList { get; } =
            new List<FragmentSpreadSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.FragmentSpread;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="FragmentSpreadSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterFragmentSpread(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="FragmentSpreadSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveFragmentSpread(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="FragmentSpreadSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterFragmentSpread(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="FragmentSpreadSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveFragmentSpread(this);
    }

    public sealed partial class InlineFragmentSyntax
    {
        /// <summary>Empty, read-only list of <see cref="InlineFragmentSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<InlineFragmentSyntax> EmptyList { get; } =
            new List<InlineFragmentSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.InlineFragment;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="InlineFragmentSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterInlineFragment(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="InlineFragmentSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveInlineFragment(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="InlineFragmentSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterInlineFragment(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="InlineFragmentSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveInlineFragment(this);
    }

    public sealed partial class InputObjectTypeDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="InputObjectTypeDefinitionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<InputObjectTypeDefinitionSyntax> EmptyList { get; } =
            new List<InputObjectTypeDefinitionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.InputObjectTypeDefinition;

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="InputObjectTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.EnterInputObjectTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="InputObjectTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.LeaveInputObjectTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a
        ///     <see cref="InputObjectTypeDefinitionSyntax" /> node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterInputObjectTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a
        ///     <see cref="InputObjectTypeDefinitionSyntax" /> node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveInputObjectTypeDefinition(this);
    }

    public sealed partial class InputObjectTypeExtensionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="InputObjectTypeExtensionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<InputObjectTypeExtensionSyntax> EmptyList { get; } =
            new List<InputObjectTypeExtensionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.InputObjectTypeExtension;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="InputObjectTypeExtensionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.EnterInputObjectTypeExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="InputObjectTypeExtensionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.LeaveInputObjectTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a
        ///     <see cref="InputObjectTypeExtensionSyntax" /> node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterInputObjectTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a
        ///     <see cref="InputObjectTypeExtensionSyntax" /> node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveInputObjectTypeExtension(this);
    }

    public sealed partial class InputValueDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="InputValueDefinitionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<InputValueDefinitionSyntax> EmptyList { get; } =
            new List<InputValueDefinitionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.InputValueDefinition;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="InputValueDefinitionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.EnterInputValueDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="InputValueDefinitionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.LeaveInputValueDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="InputValueDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterInputValueDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="InputValueDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveInputValueDefinition(this);
    }

    public sealed partial class InterfaceTypeDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="InterfaceTypeDefinitionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<InterfaceTypeDefinitionSyntax> EmptyList { get; } =
            new List<InterfaceTypeDefinitionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.InterfaceTypeDefinition;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="InterfaceTypeDefinitionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.EnterInterfaceTypeDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="InterfaceTypeDefinitionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.LeaveInterfaceTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a
        ///     <see cref="InterfaceTypeDefinitionSyntax" /> node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterInterfaceTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a
        ///     <see cref="InterfaceTypeDefinitionSyntax" /> node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveInterfaceTypeDefinition(this);
    }

    public sealed partial class InterfaceTypeExtensionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="InterfaceTypeExtensionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<InterfaceTypeExtensionSyntax> EmptyList { get; } =
            new List<InterfaceTypeExtensionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.InterfaceTypeExtension;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="InterfaceTypeExtensionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.EnterInterfaceTypeExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="InterfaceTypeExtensionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.LeaveInterfaceTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a
        ///     <see cref="InterfaceTypeExtensionSyntax" /> node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterInterfaceTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a
        ///     <see cref="InterfaceTypeExtensionSyntax" /> node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveInterfaceTypeExtension(this);
    }

    public sealed partial class IntValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="IntValueSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<IntValueSyntax> EmptyList { get; } = new List<IntValueSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.IntValue;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="IntValueSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterIntValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="IntValueSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveIntValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="IntValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterIntValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="IntValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveIntValue(this);
    }

    public sealed partial class ListTypeSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ListTypeSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<ListTypeSyntax> EmptyList { get; } = new List<ListTypeSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.ListType;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ListTypeSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterListType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ListTypeSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveListType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ListTypeSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterListType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ListTypeSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveListType(this);
    }

    public sealed partial class ListValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ListValueSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<ListValueSyntax> EmptyList { get; } = new List<ListValueSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.ListValue;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ListValueSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterListValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ListValueSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveListValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ListValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterListValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ListValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveListValue(this);
    }

    public sealed partial class NamedTypeSyntax
    {
        /// <summary>Empty, read-only list of <see cref="NamedTypeSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<NamedTypeSyntax> EmptyList { get; } = new List<NamedTypeSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.NamedType;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="NamedTypeSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterNamedType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="NamedTypeSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveNamedType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="NamedTypeSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterNamedType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="NamedTypeSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveNamedType(this);
    }

    public sealed partial class NameSyntax
    {
        /// <summary>Empty, read-only list of <see cref="NameSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<NameSyntax> EmptyList { get; } = new List<NameSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.Name;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="NameSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterName(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="NameSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveName(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="NameSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterName(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="NameSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveName(this);
    }

    public sealed partial class NonNullTypeSyntax
    {
        /// <summary>Empty, read-only list of <see cref="NonNullTypeSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<NonNullTypeSyntax> EmptyList { get; } = new List<NonNullTypeSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.NonNullType;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="NonNullTypeSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterNonNullType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="NonNullTypeSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveNonNullType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="NonNullTypeSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterNonNullType(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="NonNullTypeSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveNonNullType(this);
    }

    public sealed partial class NullValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="NullValueSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<NullValueSyntax> EmptyList { get; } = new List<NullValueSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.NullValue;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="NullValueSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterNullValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="NullValueSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveNullValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="NullValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterNullValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="NullValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveNullValue(this);
    }

    public sealed partial class ObjectFieldSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ObjectFieldSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<ObjectFieldSyntax> EmptyList { get; } = new List<ObjectFieldSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.ObjectField;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ObjectFieldSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterObjectField(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ObjectFieldSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveObjectField(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ObjectFieldSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterObjectField(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ObjectFieldSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveObjectField(this);
    }

    public sealed partial class ObjectTypeDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ObjectTypeDefinitionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<ObjectTypeDefinitionSyntax> EmptyList { get; } =
            new List<ObjectTypeDefinitionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.ObjectTypeDefinition;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ObjectTypeDefinitionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.EnterObjectTypeDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ObjectTypeDefinitionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.LeaveObjectTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ObjectTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterObjectTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ObjectTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveObjectTypeDefinition(this);
    }

    public sealed partial class ObjectTypeExtensionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ObjectTypeExtensionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<ObjectTypeExtensionSyntax> EmptyList { get; } =
            new List<ObjectTypeExtensionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.ObjectTypeExtension;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ObjectTypeExtensionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.EnterObjectTypeExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ObjectTypeExtensionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.LeaveObjectTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ObjectTypeExtensionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterObjectTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ObjectTypeExtensionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveObjectTypeExtension(this);
    }

    public sealed partial class ObjectValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ObjectValueSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<ObjectValueSyntax> EmptyList { get; } = new List<ObjectValueSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.ObjectValue;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ObjectValueSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterObjectValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ObjectValueSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveObjectValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ObjectValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterObjectValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ObjectValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveObjectValue(this);
    }

    public sealed partial class OperationDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="OperationDefinitionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<OperationDefinitionSyntax> EmptyList { get; } =
            new List<OperationDefinitionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.OperationDefinition;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="OperationDefinitionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.EnterOperationDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="OperationDefinitionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.LeaveOperationDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="OperationDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterOperationDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="OperationDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveOperationDefinition(this);
    }

    public sealed partial class OperationTypeDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="OperationTypeDefinitionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<OperationTypeDefinitionSyntax> EmptyList { get; } =
            new List<OperationTypeDefinitionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.OperationTypeDefinition;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="OperationTypeDefinitionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.EnterOperationTypeDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="OperationTypeDefinitionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.LeaveOperationTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a
        ///     <see cref="OperationTypeDefinitionSyntax" /> node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterOperationTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a
        ///     <see cref="OperationTypeDefinitionSyntax" /> node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveOperationTypeDefinition(this);
    }

    public sealed partial class PunctuatorSyntax
    {
        /// <summary>Empty, read-only list of <see cref="PunctuatorSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<PunctuatorSyntax> EmptyList { get; } = new List<PunctuatorSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.Punctuator;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="PunctuatorSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterPunctuator(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="PunctuatorSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeavePunctuator(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="PunctuatorSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterPunctuator(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="PunctuatorSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeavePunctuator(this);
    }

    public sealed partial class ScalarTypeDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ScalarTypeDefinitionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<ScalarTypeDefinitionSyntax> EmptyList { get; } =
            new List<ScalarTypeDefinitionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.ScalarTypeDefinition;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ScalarTypeDefinitionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.EnterScalarTypeDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ScalarTypeDefinitionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.LeaveScalarTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ScalarTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterScalarTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ScalarTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveScalarTypeDefinition(this);
    }

    public sealed partial class ScalarTypeExtensionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="ScalarTypeExtensionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<ScalarTypeExtensionSyntax> EmptyList { get; } =
            new List<ScalarTypeExtensionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.ScalarTypeExtension;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="ScalarTypeExtensionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.EnterScalarTypeExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="ScalarTypeExtensionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.LeaveScalarTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="ScalarTypeExtensionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterScalarTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="ScalarTypeExtensionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveScalarTypeExtension(this);
    }

    public sealed partial class SchemaDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="SchemaDefinitionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<SchemaDefinitionSyntax> EmptyList { get; } =
            new List<SchemaDefinitionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.SchemaDefinition;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="SchemaDefinitionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterSchemaDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="SchemaDefinitionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveSchemaDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="SchemaDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterSchemaDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="SchemaDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveSchemaDefinition(this);
    }

    public sealed partial class SchemaExtensionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="SchemaExtensionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<SchemaExtensionSyntax> EmptyList { get; } =
            new List<SchemaExtensionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.SchemaExtension;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="SchemaExtensionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterSchemaExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="SchemaExtensionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveSchemaExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="SchemaExtensionSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterSchemaExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="SchemaExtensionSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveSchemaExtension(this);
    }

    public sealed partial class SelectionSetSyntax
    {
        /// <summary>Empty, read-only list of <see cref="SelectionSetSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<SelectionSetSyntax> EmptyList { get; } =
            new List<SelectionSetSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.SelectionSet;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="SelectionSetSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterSelectionSet(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="SelectionSetSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveSelectionSet(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="SelectionSetSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterSelectionSet(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="SelectionSetSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveSelectionSet(this);
    }

    public sealed partial class StringValueSyntax
    {
        /// <summary>Empty, read-only list of <see cref="StringValueSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<StringValueSyntax> EmptyList { get; } = new List<StringValueSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.StringValue;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="StringValueSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterStringValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="StringValueSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveStringValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="StringValueSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterStringValue(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="StringValueSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveStringValue(this);
    }

    public sealed partial class UnionTypeDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="UnionTypeDefinitionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<UnionTypeDefinitionSyntax> EmptyList { get; } =
            new List<UnionTypeDefinitionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.UnionTypeDefinition;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="UnionTypeDefinitionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.EnterUnionTypeDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="UnionTypeDefinitionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.LeaveUnionTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="UnionTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterUnionTypeDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="UnionTypeDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveUnionTypeDefinition(this);
    }

    public sealed partial class UnionTypeExtensionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="UnionTypeExtensionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<UnionTypeExtensionSyntax> EmptyList { get; } =
            new List<UnionTypeExtensionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.UnionTypeExtension;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="UnionTypeExtensionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.EnterUnionTypeExtension(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="UnionTypeExtensionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.LeaveUnionTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="UnionTypeExtensionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterUnionTypeExtension(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="UnionTypeExtensionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveUnionTypeExtension(this);
    }

    public sealed partial class VariableDefinitionSyntax
    {
        /// <summary>Empty, read-only list of <see cref="VariableDefinitionSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<VariableDefinitionSyntax> EmptyList { get; } =
            new List<VariableDefinitionSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.VariableDefinition;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="VariableDefinitionSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.EnterVariableDefinition(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="VariableDefinitionSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) =>
            visitor.LeaveVariableDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="VariableDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterVariableDefinition(this);

        /// <summary>
        ///     Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="VariableDefinitionSyntax" />
        ///     node.
        /// </summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveVariableDefinition(this);
    }

    public sealed partial class VariableSyntax
    {
        /// <summary>Empty, read-only list of <see cref="VariableSyntax" /> nodes.</summary>
        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<VariableSyntax> EmptyList { get; } = new List<VariableSyntax>(0).AsReadOnly();

        public override SyntaxKind Kind { get; } = SyntaxKind.Variable;

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> enters a <see cref="VariableSyntax" /> node.</summary>
        public override void VisitEnter([NotNull] GraphQLSyntaxVisitor visitor) => visitor.EnterVariable(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor" /> leaves a <see cref="VariableSyntax" /> node.</summary>
        public override void VisitLeave([NotNull] GraphQLSyntaxVisitor visitor) => visitor.LeaveVariable(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> enters a <see cref="VariableSyntax" /> node.</summary>
        public override TResult VisitEnter<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.EnterVariable(this);

        /// <summary>Called when a <see cref="GraphQLSyntaxVisitor{TResult}" /> leaves a <see cref="VariableSyntax" /> node.</summary>
        public override TResult VisitLeave<TResult>([NotNull] GraphQLSyntaxVisitor<TResult> visitor) =>
            visitor.LeaveVariable(this);
    }
}