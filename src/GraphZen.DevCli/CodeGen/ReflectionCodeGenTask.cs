// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.CodeGen
{
    public abstract class ReflectionCodeGenTask
    {
        protected ReflectionCodeGenTask(Type targetType)
        {
            TargetType = targetType;
        }


        public Type TargetType { get; }

        private static readonly Lazy<IReadOnlyList<string>> _csharpFiles =
            new Lazy<IReadOnlyList<string>>(() => Directory.GetFiles("./", "*.cs", SearchOption.AllDirectories));

        public static IReadOnlyList<string> CSharpFiles => _csharpFiles.Value;

        public static IEnumerable<ReflectionCodeGenTask> GetAllFromTypes(IReadOnlyList<Type> types)
        {
            foreach (var genAccessorsTask in GenAccessorsTask.FromTypes(types))
            {
                yield return genAccessorsTask;
            }
        }

        public static void GenForAssemblyWithType<T>()
        {
            var tasksByTarget = GetAllFromTypes(ReflectionCodeGenerator.GetSourceTypes<T>()).GroupBy(_ => _.TargetType)
                .Select(_ => (targetType: _.Key, tasks: _.ToReadOnlyList()));

            foreach (var (targetType, tasks) in tasksByTarget)
            {
                var targetFilename = targetType.Name + ".cs";
                var targetPath = CSharpFiles.SingleOrDefault(_ => Path.GetFileName(_) == targetFilename);
                if (targetPath == null)
                    throw new InvalidOperationException($"A code-gen task could not find file: {targetFilename}");
            }
        }

        public static string GetTargetFileName(Type targetType) => $"{targetType.Name}.cs";

        public static string? GetTargetPath(Type targetType) =>
            CSharpFiles.SingleOrDefault(path => Path.GetFileName(path) == GetTargetFileName(targetType));
    }
}