// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Infrastructure
{
    public abstract class TestDataFixtureRunner<T>
    {
        /// <summary>
        ///     Runs the text fixtures of the same type of the type parameter. Useful for debugging an individual fixture that has
        ///     failed.
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="fixture"></param>
        /// <param name="test"></param>
        protected void RunFixture<TFilter>(T fixture, Action test) where TFilter : T, new()
        {
            if (fixture is TFilter)
            {
                RunFixture(fixture, test);
                throw new Exception(
                    $"{typeof(TFilter).Name} was successful, but remove type parameter to test all fixtures");
            }
        }

        /// <summary>
        ///     Identifies which fixture has failed. Useful for test explorers such as NCrunch that can't pinpoint which fixture
        ///     caused the issue.
        /// </summary>
        /// <param name="fixture"></param>
        /// <param name="test"></param>
        protected void RunFixture(T fixture, Action test)
        {
            Check.NotNull(fixture, nameof(fixture));
            try
            {
                test();
            }
            catch (Exception ex)
            {
                throw new Exception($@"Failed fixture:

  {fixture.GetType().Name}  

Inner exception: 

{ex.GetType()}: {ex.Message}", ex);
            }
        }
    }
}