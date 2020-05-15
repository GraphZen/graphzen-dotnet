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
        private Spec(FieldInfo field, Spec? parent) :
            this(field.Name, parent, field, field.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                field.GetCustomAttribute<DescriptionAttribute>()?.Description)
        {
        }

        private Spec(string id, Spec? parent, FieldInfo? fieldInfo, string? name = null, string? description = null,
            Func<Spec, ImmutableList<Spec>>? children = null)
        {
            Id = id;
            FieldInfo = fieldInfo;
            Name = name ?? id;
            Description = description;
            Children = children != null ? children(this) : ImmutableList<Spec>.Empty;
            Parent = parent;
        }

        public string Id { get; }
        public string Name { get; }
        public string? Description { get; }

        public Spec? Parent { get; }
        public FieldInfo? FieldInfo { get; }
        public ImmutableList<Spec> Children { get; }

        public override string ToString()
        {
            if (FieldInfo != null)
            {
                return $"{FieldInfo.DeclaringType!.Name}.{FieldInfo.Name}";
            }

            return Name;
        }

        public IEnumerable<Spec> GetSelfAndDescendants()
        {
            yield return this;
            foreach (var desc in Children.SelectMany(_ => _.GetSelfAndDescendants()))
            {
                yield return desc;
            }
        }


        private static Spec From(Type type)
        {
            var id = type.Name;
            var name = type.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;
            var description = type.GetCustomAttribute<DescriptionAttribute>()?.Description;
            return new Spec(id, null, null, name, description,
                parent => SpecReflectionHelpers.GetConstFields(type).Select(_ => new Spec(_, parent))
                    .ToImmutableList());
        }

        public static IEnumerable<Spec> GetSpecs(Type type) => type.GetNestedTypes().Select(From);
    }
}