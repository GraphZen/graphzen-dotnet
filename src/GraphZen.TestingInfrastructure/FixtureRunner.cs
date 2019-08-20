// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen
{
    public abstract class FixtureRunner<T>
    {
        [NotNull]
        [ItemNotNull]
        protected abstract IEnumerable<T> GetFixtures();

        private static void TestFixtures([NotNull] Action<T> test,
            [NotNull] [ItemNotNull] IEnumerable<T> fixtures)
        {
            Check.NotNull(test, nameof(test));
            foreach (var fixture in fixtures)
            {
                try
                {
                    test(fixture);
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

        protected void TestFixtures(
            [NotNull] Action<T> test)
        {
            TestFixtures(test, GetFixtures());
        }

        [UsedImplicitly]
        [DebuggerHidden]
        protected void TestFixtures<TFilter>([NotNull] Action<T> test)
            where TFilter : T, new()
        {
            TestFixtures(test, GetFixtures().OfType<TFilter>().Cast<T>());
            throw new Exception($"{typeof(T).Name} was successful, but remove type parameter to test all fixtures");
        }
    }
}