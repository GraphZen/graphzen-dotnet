// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel.Validation.Rules;
using JetBrains.Annotations;

namespace GraphZen.LanguageModel.Validation
{
    public static class DocumentValidationRules
    {
        public static ValidationRule LoneSchemaDefinition { get; } =
            _ => new LoneSchemaDefinition((DocumentValidationContext) _);


        public static ValidationRule SchemaMustHaveRootObjectTypes { get; } =
            _ => new SchemaMustHaveRootObjectTypes((DocumentValidationContext) _);


        public static ValidationRule FieldArgsMustBeProperlyNamed { get; } =
            _ => new FieldArgsMustBeProperlyNamed((DocumentValidationContext) _);


        public static ValidationRule UnionTypesMustBeValid { get; } =
            _ => new UnionTypesMustBeValid((DocumentValidationContext) _);


        public static ValidationRule InputObjectsMustHaveFields { get; } =
            _ => new InputObjectsMustHaveFields((DocumentValidationContext) _);


        public static ValidationRule EnumTypesMustBeWellDefined { get; } =
            _ => new EnumTypesMustBeWellDefined((DocumentValidationContext) _);


        public static ValidationRule ObjectFieldsMustHaveOutputTypes { get; } =
            _ => new ObjectFieldsMustHaveOutputTypes((DocumentValidationContext) _);


        public static ValidationRule ObjectsCanOnlyImplementUniqueInterfaces { get; } = _ =>
            new ObjectsCanOnlyImplementUniqueInterfaces((DocumentValidationContext) _);


        public static ValidationRule InterfaceExtensionsShouldBeValid { get; } =
            _ => new InterfaceExtensionsShouldBeValid((DocumentValidationContext) _);


        public static ValidationRule InterfaceFieldsMustHaveOutputTypes { get; } = _ =>
            new InterfaceFieldsMustHaveOutputTypes((DocumentValidationContext) _);


        public static ValidationRule FieldArgumentsMustHaveInputTypes { get; } =
            _ => new FieldArgumentsMustHaveInputTypes((DocumentValidationContext) _);


        public static ValidationRule ObjectsMustAdhereToInterfaceTheyImplement { get; } = _ =>
            new ObjectsMustAdhereToInterfaceTheyImplement((DocumentValidationContext) _);


        public static ValidationRule ObjectsMustHaveFields { get; } = _ => new ObjectsMustHaveFields(_);


        public static ValidationRule InputObjectFieldsMustHaveInputTypes { get; } =
            _ => new InputObjectFieldsMustHaveInputTypes(_);


        public static IReadOnlyList<ValidationRule> SpecifiedSDLRules { get; } = new[]
        {
            LoneSchemaDefinition
            // KnownDirectives,
            // UniqueDirectivesPerLocation,
            // UniqueArgumentNames,
            // UniqueInputFieldNames
        };


        public static IReadOnlyList<ValidationRule> SpecifiedSchemaRules { get; } =
            SpecifiedSDLRules.Concat(
                new[]
                {
                    SchemaMustHaveRootObjectTypes,
                    FieldArgsMustBeProperlyNamed,
                    UnionTypesMustBeValid,
                    InputObjectsMustHaveFields,
                    EnumTypesMustBeWellDefined,
                    ObjectFieldsMustHaveOutputTypes,
                    ObjectsCanOnlyImplementUniqueInterfaces,
                    InterfaceExtensionsShouldBeValid,
                    InterfaceFieldsMustHaveOutputTypes,
                    FieldArgumentsMustHaveInputTypes,
                    ObjectsMustAdhereToInterfaceTheyImplement,
                    ObjectsMustHaveFields,
                    InputObjectFieldsMustHaveInputTypes
                }).ToList();
    }
}