// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.SpecAudit.SpecFx
{
    public class Spec
    {
        private Spec(FieldInfo field) :
            this(field.Name,
                field.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                field.GetCustomAttribute<DescriptionAttribute>()?.Description)
        {
        }

        private Spec(string id, string? name = null, string? description = null, IEnumerable<Spec>? children = null)
        {
            Id = id;
            Name = name ?? id;
            Description = description;
            Children = children?.ToImmutableList() ?? ImmutableList<Spec>.Empty;
        }

        public string Id { get; }
        public string Name { get; }
        public string? Description { get; }
        public ImmutableList<Spec> Children { get; }


        private static Spec From(Type type)
        {
            var id = type.Name;
            var name = type.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;
            var description = type.GetCustomAttribute<DescriptionAttribute>()?.Description;
            var children = SpecReflectionHelpers.GetConstFields(type).Select(_ => new Spec(_));
            return new Spec(id, name, description, children);
        }

        public static IEnumerable<Spec> From(params Type[] types) => types.Select(From);
    }
}