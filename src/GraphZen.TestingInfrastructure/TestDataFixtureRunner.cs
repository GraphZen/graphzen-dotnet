// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen
{
    public abstract class TestDataFixtureRunner<T>
    {
        /// <summary>
        ///     Runs the text fixtures of the same type of the type parameter. Useful for debugging an individual fixture that has
        ///     failed.
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="data"></param>
        /// <param name="test"></param>
        protected void RunFixture<TFilter>(T data, Action test) where TFilter : T, new()
        {
            if (data is TFilter)
            {
                RunFixture(data, test);
                throw new Exception(
                    $"{typeof(TFilter).Name} was successful, but remove type parameter to test all fixtures");
            }
        }

        /// <summary>
        ///     Identifies which fixture has failed. Useful for test explorers such as NCrunch that can't pinpoint which fixture
        ///     caused the issue.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="test"></param>
        protected void RunFixture(T data, Action test)
        {
            try
            {
                test();
            }
            catch (Exception ex)
            {
                throw new Exception($@"Failed fixture:

  {data.GetType().Name}  

Inner exception: 

{ex.GetType()}: {ex.Message}", ex);
            }
        }
    }
}