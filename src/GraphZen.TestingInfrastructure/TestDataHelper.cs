// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;

namespace GraphZen
{
    public static class TestDataHelpers
    {
        
        
        public static IEnumerable<object[]> ToTestData<T>( this IEnumerable<T> source) =>
            source.Select(_ => new object[] { _ });
    }

    public abstract class TestDataHelper<T>
    {
        protected void TestData<TFilter>( T data,  Action test) where TFilter : T, new()
        {
            if (data is TFilter)
            {
                TestData(data, test);
                throw new Exception(
                    $"{typeof(TFilter).Name} was successful, but remove type parameter to test all fixtures");
            }
        }

        protected void TestData( T data,  Action test)
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