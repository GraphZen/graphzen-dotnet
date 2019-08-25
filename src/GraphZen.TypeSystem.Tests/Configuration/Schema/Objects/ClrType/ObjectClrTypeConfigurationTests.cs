// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using JetBrains.Annotations;
using Xunit;




namespace GraphZen.Objects.ClrType
{
    [NoReorder]
    public class ObjectClrTypeConfigurationTests
    {
        public const string DataAnnotationName = nameof(DataAnnotationName);

        public class ExampleObject
        {
        }

        [Fact]
        public void object_added_explicitly_subsequently_referenced_by_matching_clr_type_should_have_clr_type_set()
        {
            var schema = Schema.Create(sb =>
            {
                sb.Object(nameof(ExampleObject));
                var def = sb.GetDefinition().GetObject(nameof(ExampleObject));
                sb.Object<ExampleObject>();
                def.ClrType.Should().Be<ExampleObject>();
            });
            schema.GetObject<ExampleObject>().ClrType.Should().Be<ExampleObject>();
        }

        [Fact]
        public void
            object_added_explicitly_subsequently_referenced_by_matching_clr_type_via_field_should_have_clr_type_set()
        {
            var schema = Schema.Create(sb =>
            {
                sb.Object(nameof(ExampleObject));
                sb.Object("Object").Field<ExampleObject>("field");
                var def = sb.GetDefinition().GetObject(nameof(ExampleObject));
                sb.Object<ExampleObject>();
                def.ClrType.Should().Be<ExampleObject>();
            });
            schema.GetObject<ExampleObject>().ClrType.Should().Be<ExampleObject>();
        }
    }
}