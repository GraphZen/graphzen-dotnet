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
            this(field.Name, field, field.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                field.GetCustomAttribute<DescriptionAttribute>()?.Description)
        {
        }

        private Spec(string id, FieldInfo? fieldInfo, string? name = null, string? description = null,
            ImmutableList<Spec>? children = null)
        {
            Id = id;
            FieldInfo = fieldInfo;
            Name = name ?? id;
            Description = description;
            Children = children ?? ImmutableList<Spec>.Empty;
        }

        public string Id { get; }
        public string Name { get; }
        public string? Description { get; }
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
            var children = SpecReflectionHelpers.GetConstFields(type).Select(_ => new Spec(_)).ToImmutableList();
            return new Spec(id, null, name, description, children);
        }

        public static IEnumerable<Spec> GetSpecs(Type type) => type.GetNestedTypes().Select(From);
    }
}