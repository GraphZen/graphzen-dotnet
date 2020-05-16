// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.SpecAudit.SpecFx
{
    public class SpecTest
    {
        private SpecTest(string subjectPath, string specId, MethodInfo testMethod)
        {
            SubjectPath = subjectPath;
            SpecId = specId;
            TestMethod = testMethod;
            FactAttribute = testMethod.GetCustomAttribute<FactAttribute>();
            TheoryAttribute = testMethod.GetCustomAttribute<TheoryAttribute>();
            if (FactAttribute == null && TheoryAttribute == null)
            {
                throw new ArgumentException(
                    $"Method {TestMethod} was decorated with a {nameof(SpecAttribute)}, but is missing a {nameof(FactAttribute)} or {nameof(TheoryAttribute)}",
                    nameof(testMethod));
            }
        }

        public string SubjectPath { get; }
        public string SpecId { get; }
        public MethodInfo TestMethod { get; }
        public FactAttribute? FactAttribute { get; }
        public TheoryAttribute? TheoryAttribute { get; }
        public string? SkipReason => FactAttribute?.Skip ?? TheoryAttribute?.Skip;

        public static IEnumerable<SpecTest> DiscoverFrom(Assembly assembly)
        {
            foreach (var m in assembly.GetTypes().Where(_ => _.IsClass && !_.IsAbstract)
                .SelectMany(_ => _.GetMethods()))
            {
                var specAttr = m.GetCustomAttribute<SpecAttribute>();
                if (specAttr != null)
                {
                    var path = GetSubjectPath(m, specAttr);
                    yield return new SpecTest(path, specAttr.SpecId, m);
                }
            }
        }


        private static string GetSubjectPath(MethodInfo method, SpecAttribute specAttr)
        {
            var subjects = new List<string>();
            if (specAttr.Subject != null)
            {
                subjects.Add(specAttr.Subject);
            }

            var methodSubjAttr = method.GetCustomAttribute<SubjectAttribute>();
            if (methodSubjAttr != null)
            {
                subjects.AddRange(methodSubjAttr.Subjects);
            }


            var type = method.DeclaringType;
            while (type != null)
            {
                var typeSubjAttr = type.GetCustomAttribute<SubjectAttribute>();
                if (typeSubjAttr != null)
                {
                    subjects!.AddRange(typeSubjAttr.Subjects);
                }

                type = type.BaseType;
            }


            if (!subjects.Any())
            {
                return method.DeclaringType!.Namespace!.Replace("GraphZen.TypeSystem.FunctionalTests.", "");
            }

            subjects.Reverse();
            return string.Join("_", subjects);
        }
    }
}