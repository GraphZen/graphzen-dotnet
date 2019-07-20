﻿// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.MetaModel;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;

// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException

namespace GraphZen
{
    public class TestCaseGenerator
    {
        protected static LeafElementConfigurationTests<INamed, IMutableNamed, ScalarTypeDefinition, ScalarType, string>
            TestCases =>
            throw new NotImplementedException();


        //[NotNull]
        //[ItemNotNull]
        //public IEnumerable<string> GetTestCasesForElement([NotNull] Element element)
        //{
        //    switch (element)
        //    {
        //        case Collection collection:
        //            return GetTestCasesForCollection(collection);
        //        case LeafElement leafElement:
        //            return GetTestCasesForLeaf(leafElement);
        //        case Vector vector:
        //            return GetTestCasesForVector(vector);
        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }
        //}

        [NotNull]
        [ItemNotNull]
        public static IEnumerable<string> GetTestCasesForLeaf([NotNull] LeafElement element)

        {
            //yield break;
             yield return nameof(TestCases.configured_explicitly_reconfigured_explicitly);
            if (element.Optional)
            {
                yield return nameof(TestCases.optional_not_defined_by_convention_when_parent_configured_explicitly);
                yield return nameof(TestCases.optional_not_defined_by_convention_then_configured_explicitly);
            }
        }

        [NotNull]
        [ItemNotNull]
        public IEnumerable<string> GetTestCasesForVector([NotNull] Vector element)
        {
            yield break;
        }

        [NotNull]
        [ItemNotNull]
        public IEnumerable<string> GetTestCasesForCollection([NotNull] Collection element)
        {
            yield break;
        }
    }

    //public class ExplicitTestCaseGenerator : TestCaseGenerator { }

    //public class ConventionTestCaseGenerator : TestCaseGenerator
    //{
    //    protected override IEnumerable<string> GetTestCasesForLeaf(LeafElement element)
    //    {
    //        if (element.Optional)
    //        {
    //            yield return nameof(TestCases.optional_not_defined_by_convention);
    //        }

    //        //if (DefineIsConfiguredBy(ConfigurationSource.Convention))
    //        //{
    //        //    yield return nameof(TestCases.defined_by_convention);
    //        //}

    //        //if (DefineIsConfiguredBy(ConfigurationSource.DataAnnotation))
    //        //{
    //        //    if (DefineIsConfiguredBy(ConfigurationSource.Explicit))
    //        //    {
    //        //        yield return nameof(TestCases.define_by_data_annotation_overridden_by_explicit_configuration);
    //        //    }
    //        //    yield return nameof(TestCases.define_by_data_annotation);
    //        //}
    //    }

    //    // ReSharper disable once UnusedParameter.Local
    //    protected override IEnumerable<string> GetTestCasesForVector(Vector element) =>
    //        Enumerable.Empty<string>();

    //    // ReSharper disable once UnusedParameter.Local
    //    protected override IEnumerable<string> GetTestCasesForCollection(Collection element) =>
    //        Enumerable.Empty<string>();
    //}
}