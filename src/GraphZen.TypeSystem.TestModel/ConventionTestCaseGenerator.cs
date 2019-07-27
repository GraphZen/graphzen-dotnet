// Copyright (c) GraphZen LLC. All rights reserved.
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
        public static IEnumerable<string> GetTestCasesForLeaf([NotNull] LeafElement element, bool conventionContext)
        {
            // All leafs (a) 

            yield return "all";

            // All optional leafs (b)
            if (element.Optional)
            {
                yield return "all_optional";
                yield return nameof(TestCases.optional_not_defined_by_convention_when_parent_configured_explicitly);
                yield return nameof(TestCases.optional_not_defined_by_convention_then_configured_explicitly);
            }
            // All required leafs  (c)
            else
            {
                yield return "all_required";
            }

            if (!conventionContext && element.ExplicitOnly)
            {
                // Only explicit test cases (d)
                yield return nameof(TestCases.configured_explicitly_reconfigured_explicitly);
            }
            else if (conventionContext)
            {
                if (element.ConfiguredByConvention && element.ConfiguredByDataAnnotation)
                {
                    // All leafs configured by convention and data annotation (e)
                    yield return "convention_and_data_annotation_E";
                    // Optional leafs configured by convention and data annotation (f)
                    if (element.Optional)
                    {
                        yield return "optional_convention_and_data_annotation_F";
                    }
                    // Required leafs configured by convention and data annotation (g)
                    else
                    {
                        yield return "required_convention_and_data_annotation_G";
                    }
                }


                if (element.ConfiguredByConvention && !element.ConfiguredByDataAnnotation)
                {
                    // All leafs configured by convention only (h)
                    yield return "convention_only_H";
                    // Optional leafs configured by convention only (i)
                    if (element.Optional)
                    {
                        yield return "optional_convention_only_I";
                    }
                    // Required leafs configured by convention only (j)
                    else
                    {
                        yield return "required_convention_only_J";
                    }
                }

                if (!element.ConfiguredByConvention && element.ConfiguredByDataAnnotation)
                {
                    // All leafs configured by data annotation only (k)
                    yield return "data_annotation_only_K";
                    // Optional leafs configured by data annotation only (l)
                    if (element.Optional)
                    {
                        yield return "optional_data_annotation_only_l";
                    }
                    // Required leafs configured by data annotation only (m)
                    else
                    {
                        yield return "required_data_annotation_only_m";
                    }
                }
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