// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.MetaModel;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException

namespace GraphZen
{
    public static class ConfigurationTestCaseGenerator
    {
        private static LeafElementConfigurationTests<INamed, IMutableNamed, ScalarTypeDefinition, ScalarType>
            TestCases =>
            throw new NotImplementedException();

        [NotNull]
        [ItemNotNull]
        public static IEnumerable<string> GetTestCasesForElement([NotNull] Element element)
        {
            switch (element)
            {
                case Collection collection:
                    return GetTestCasesForCollection(collection);
                case LeafElement leafElement:
                    return GetTestCasesForLeaf(leafElement);
                case Vector vector:
                    return GetTestCasesForVector(vector);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [NotNull]
        [ItemNotNull]
        private static IEnumerable<string> GetTestCasesForLeaf([NotNull] LeafElement element)
        {

            bool DefineIsConfiguredBy(ConfigurationSource source) => element.ConfigurationScenarios.Define.Contains(source);

            if (element.Optional)
            {
                yield return nameof(TestCases.optional_not_defined_by_convention);
            }

            if (DefineIsConfiguredBy(ConfigurationSource.Convention))
            {
                yield return nameof(TestCases.defined_by_convention);
            }

            if (DefineIsConfiguredBy(ConfigurationSource.DataAnnotation))
            {
                if (DefineIsConfiguredBy(ConfigurationSource.Explicit))
                {
                    yield return nameof(TestCases.define_by_data_annotation_overridden_by_explicit_configuration);
                }
                yield return nameof(TestCases.define_by_data_annotation);
            }
        }

        [NotNull]
        [ItemNotNull]
        // ReSharper disable once UnusedParameter.Local
        private static IEnumerable<string> GetTestCasesForVector([NotNull] Vector element) =>
            Enumerable.Empty<string>();

        [NotNull]
        [ItemNotNull]
        // ReSharper disable once UnusedParameter.Local
        private static IEnumerable<string> GetTestCasesForCollection([NotNull] Collection element) =>
            Enumerable.Empty<string>();
    }
}