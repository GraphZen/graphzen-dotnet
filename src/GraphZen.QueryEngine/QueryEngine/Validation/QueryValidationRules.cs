// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Validation;
using GraphZen.QueryEngine.Validation.Rules;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.QueryEngine.Validation
{
    public static class QueryValidationRules
    {
        public static ValidationRule ExecutableDefinitions { get; } =
            _ => new ExecutableDefinitions((QueryValidationContext)_);


        public static ValidationRule ValuesOfCorrectType { get; } =
            _ => new ValuesOfCorrectType((QueryValidationContext)_);


        public static ValidationRule FieldsOnCorrectType { get; } =
            _ => new FieldsOnCorrectType((QueryValidationContext)_);


        public static ValidationRule FragmentsOnCompositeTypes { get; } =
            _ => new FragmentsOnCompositeTypes((QueryValidationContext)_);


        public static ValidationRule KnownArgumentNames { get; } =
            _ => new KnownArgumentNames((QueryValidationContext)_);


        public static ValidationRule KnownDirectives { get; } = _ => new KnownDirectives((QueryValidationContext)_);


        public static ValidationRule KnownFragmentNames { get; } =
            _ => new KnownFragmentNames((QueryValidationContext)_);


        public static ValidationRule KnownTypeNames { get; } = _ => new KnownTypeNames((QueryValidationContext)_);


        public static ValidationRule LoneAnonymousOperation { get; } =
            _ => new LoneAnonymousOperation((QueryValidationContext)_);


        public static ValidationRule NoFragmentCycles { get; } = _ => new NoFragmentCycles((QueryValidationContext)_);


        public static ValidationRule NoUndefinedVariables { get; } =
            _ => new NoUndefinedVariables((QueryValidationContext)_);


        public static ValidationRule NoUnusedFragments { get; } =
            _ => new NoUnusedFragments((QueryValidationContext)_);


        public static ValidationRule NoUnusedVariables { get; } =
            _ => new NoUnusedVariables((QueryValidationContext)_);


        public static ValidationRule OverlappingFieldsCanBeMerged { get; } =
            _ => new OverlappingFieldsCanBeMerged((QueryValidationContext)_);


        public static ValidationRule PossibleFragmentSpreads { get; } =
            _ => new PossibleFragmentSpreads((QueryValidationContext)_);


        public static ValidationRule ProvidedRequiredArguments { get; } =
            _ => new ProvidedRequiredArguments((QueryValidationContext)_);


        public static ValidationRule ScalarLeafs { get; } = _ => new ScalarLeafs((QueryValidationContext)_);


        public static ValidationRule UniqueArgumentNames { get; } =
            _ => new UniqueArgumentNames((QueryValidationContext)_);


        public static ValidationRule UniqueDirectivesPerLocation { get; } =
            _ => new UniqueDirectivesPerLocation((QueryValidationContext)_);


        public static ValidationRule UniqueFragmentNames { get; } =
            _ => new UniqueFragmentNames((QueryValidationContext)_);


        public static ValidationRule UniqueInputFieldNames { get; } =
            _ => new UniqueInputFieldNames((QueryValidationContext)_);


        public static ValidationRule UniqueOperationNames { get; } =
            _ => new UniqueOperationNames((QueryValidationContext)_);


        public static ValidationRule UniqueVariableNames { get; } =
            _ => new UniqueVariableNames((QueryValidationContext)_);


        public static ValidationRule VariablesAreInputTypes { get; } =
            _ => new VariablesAreInputTypes((QueryValidationContext)_);


        public static ValidationRule VariablesInAllowedPosition { get; } =
            _ => new VariablesInAllowedPosition((QueryValidationContext)_);


        public static ValidationRule InputDocumentNonConflictingVariableInference { get; } =
            _ => new InputDocumentNonConflictingVariableInference((QueryValidationContext)_);


        public static ValidationRule SingleFieldSubscriptions { get; } =
            _ => new SingleFieldSubscriptions((QueryValidationContext)_);


        public static IReadOnlyList<ValidationRule> SpecifiedQueryRules { get; } = new[]
        {
            ExecutableDefinitions,
            UniqueOperationNames,
            LoneAnonymousOperation,
            SingleFieldSubscriptions,
            KnownTypeNames,
            FragmentsOnCompositeTypes,
            VariablesAreInputTypes,
            ScalarLeafs,
            FieldsOnCorrectType,
            UniqueFragmentNames,
            KnownFragmentNames,
            NoUnusedFragments,
            PossibleFragmentSpreads,
            NoFragmentCycles,
            UniqueVariableNames,
            NoUndefinedVariables,
            NoUnusedVariables,
            KnownDirectives,
            UniqueDirectivesPerLocation,
            KnownArgumentNames,
            UniqueArgumentNames,
            ValuesOfCorrectType,
            ProvidedRequiredArguments,
            VariablesInAllowedPosition,
            OverlappingFieldsCanBeMerged,
            UniqueInputFieldNames
        };
    }
}