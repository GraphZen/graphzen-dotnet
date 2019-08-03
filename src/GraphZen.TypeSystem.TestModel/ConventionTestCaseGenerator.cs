// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        protected static LeafElementTestCaseGeneratorTests MetaCases => throw new NotImplementedException();




        [NotNull]
        [ItemNotNull]
        public static IEnumerable<string> GetTestCasesForLeaf([NotNull] LeafElement element, bool conventionContext, bool filterMetaCases = true)
        {
            IEnumerable<string> Result()
            {
                // All leafs (a) 
                yield return nameof(MetaCases.all_A);
                yield return nameof(TestCases.configured_explicitly_reconfigured_explicitly);

                // All optional leafs (b)
                if (element.Optional)
                {
                    yield return nameof(MetaCases.all_optional_B);
                    yield return nameof(TestCases.optional_not_defined_by_convention_when_parent_configured_explicitly);
                    yield return nameof(TestCases.optional_not_defined_by_convention_then_configured_explicitly);
                    yield return nameof(TestCases.optional_not_defined_by_convention_then_configured_explicitly_then_removed);
                }
                // All required leafs  (c)
                else
                {
                    yield return nameof(MetaCases.all_required_C);
                }

                if (!conventionContext && element.ExplicitOnly)
                {
                    // Only explicit test cases (d)
                    yield return nameof(MetaCases.explicit_only_D);
                }
                else if (conventionContext)
                {



                    if (element.ConfiguredByConvention && element.ConfiguredByDataAnnotation)
                    {
                        // All leafs configured by convention and data annotation (e)
                        yield return nameof(MetaCases.convention_and_data_annotation_E);
                        // Optional leafs configured by convention and data annotation (f)
                        if (element.Optional)
                        {
                            yield return nameof(MetaCases.optional_convention_and_data_annotation_F);
                        }
                        // Required leafs configured by convention and data annotation (g)
                        else
                        {
                            yield return nameof(MetaCases.required_convention_and_data_annotation_G);
                        }
                    }


                    if (element.ConfiguredByConvention && !element.ConfiguredByDataAnnotation)
                    {
                        // All leafs configured by convention only (h)
                        yield return nameof(MetaCases.convention_only_H);
                        // Optional leafs configured by convention only (i)
                        if (element.Optional)
                        {
                            yield return nameof(MetaCases.optional_convention_only_I);
                        }
                        // Required leafs configured by convention only (j)
                        else
                        {
                            yield return nameof(MetaCases.required_convention_only_J);
                        }
                    }

                    if (!element.ConfiguredByConvention && element.ConfiguredByDataAnnotation)
                    {
                        // All leafs configured by data annotation only (k)
                        yield return nameof(MetaCases.data_annotation_only_K);
                        // Optional leafs configured by data annotation only (l)
                        if (element.Optional)
                        {
                            yield return nameof(MetaCases.optional_data_annotation_only_l);
                        }
                        // Required leafs configured by data annotation only (m)
                        else
                        {
                            yield return nameof(MetaCases.required_data_annotation_only_m);
                        }
                    }

                    if (element.ConfiguredByConvention)
                    {

                        // All leafs configured by convention
                        yield return nameof(MetaCases.all_convention);
                        // Optional leafs configured by convention
                        if (element.Optional)
                        {
                            yield return nameof(MetaCases.optional_convention);
                        }
                        // Required leafs configured by convention
                        else
                        {
                            yield return nameof(MetaCases.required_convention);
                        }
                    }

                    if (element.ConfiguredByDataAnnotation)
                    {
                        // All leafs configured by data annotation 
                        yield return nameof(MetaCases.all_data_annotation);
                        yield return nameof(TestCases.configured_by_data_annotation);
                        yield return nameof(TestCases.configured_by_data_annotation_then_reconfigured_explicitly);
                        // Optional leafs configured by data annotation 
                        if (element.Optional)
                        {
                            yield return nameof(MetaCases.optional_data_annotation);
                            yield return nameof(TestCases
                                .optional_configured_by_data_annotation_then_removed_explicitly);
                        }
                        // Required leafs configured by data annotation only 
                        else
                        {
                            yield return nameof(MetaCases.required_data_annotation);
                        }

                    }
                }
            }

            if (filterMetaCases)
            {
                var metaCases = typeof(LeafElementTestCaseGeneratorTests)
                    .GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    .Select(_ => _.Name).ToArray();
                return Result().Where(_ => !metaCases.Contains(_));
            }

            return Result();

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