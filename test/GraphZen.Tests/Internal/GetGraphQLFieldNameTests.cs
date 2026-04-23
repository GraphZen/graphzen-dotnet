// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.Tests.Internal
{
    [NoReorder]
    public class GetGraphQLFieldNameTests
    {
        [Fact]
        public void FieldNameFromProperty()
        {
            var property = SelectFoo(f => f.Property);
            Assert.Equal(("property", ConfigurationSource.Convention), property.GetGraphQLFieldName());

            var propertyWithCustomName = SelectFoo(_ => _.CustomProperty);
            Assert.Equal(("customAllTheWay", ConfigurationSource.DataAnnotation), propertyWithCustomName.GetGraphQLFieldName());
        }

        private PropertyInfo SelectFoo<T>(Expression<Func<Foo, T>> expr) => expr.GetPropertyInfoFromExpression();

        private class Foo
        {
            [GraphQLName("customAllTheWay")] public string CustomProperty { get; } = null!;

            public string Property { get; } = null!;

            [UsedImplicitly]
            public bool Method() => true;

            [UsedImplicitly]
            public void VoidMethod()
            {
            }

            // ReSharper disable once UnusedMember.Local
            public Task MethodAsync() => Task.CompletedTask;

            [UsedImplicitly]
            public Task<string> StringMethodAsync() => Task.FromResult("hello world");
        }
    }
}