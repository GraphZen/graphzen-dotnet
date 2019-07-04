// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure.Extensions
{
    internal static class TaskExtensions
    {
        [NotNull]
        public static async Task<object> GetResultAsync([CanBeNull] this object value)
        {
            if (value is Task awaitable)
            {
                await awaitable;
                return awaitable.GetResult();
            }

            return value;
        }

        [CanBeNull]
        public static object GetResult(this Task task)
        {
            Check.NotNull(task, nameof(task));
            if (!task.IsCompleted)
            {
                throw new InvalidOperationException(
                    "Attempted to get result of task prior to completion, ensure you are await task prior to getting its value.");
            }

            var resultProp = task.GetType().GetProperty("Result");
            if (resultProp != null)
            {
                return resultProp.GetValue(task);
            }

            throw new InvalidOperationException("Unable to get result from task");
        }
    }
}