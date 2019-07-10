// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem
{
    public class UnionTypeBuilder<TUnion, TContext> : IUnionTypeBuilder<TUnion, TContext>,
        IInfrastructure<InternalUnionTypeBuilder> where TContext : GraphQLContext
    {
        public UnionTypeBuilder(InternalUnionTypeBuilder builder)
        {
            Builder = Check.NotNull(builder, nameof(builder));
        }

        [NotNull]
        private InternalUnionTypeBuilder Builder { get; }

        InternalUnionTypeBuilder IInfrastructure<InternalUnionTypeBuilder>.Instance => Builder;


        public IUnionTypeBuilder<TUnion, TContext> Description(string description)
        {
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public IUnionTypeBuilder<TUnion, TContext> ResolveType(TypeResolver<TUnion, TContext> resolveTypeFn)
        {
            Check.NotNull(resolveTypeFn, nameof(resolveTypeFn));
            Builder.ResolveType((value, context, info) => resolveTypeFn((TUnion)value, (TContext)context, info));
            return this;
        }

        public IUnionTypeBuilder<TUnion, TContext> OfTypes(params string[] objectTypes)
        {
            Check.NotNull(objectTypes, nameof(objectTypes));
            foreach (var type in objectTypes)
            {
                Check.NotNull(type, nameof(type));
                Builder.IncludesType(type, ConfigurationSource.Explicit);
            }

            return this;
        }

        public IUnionTypeBuilder<TUnion, TContext> OfTypes(params Type[] types)
        {
            Check.NotEmpty(types, nameof(types));
            foreach (var type in types)
            {
                if (type != null)
                {
                    Builder.IncludesType(type, ConfigurationSource.Explicit);
                }
            }

            return this;
        }

        public IUnionTypeBuilder<TUnion, TContext> OfTypes<TObject>()
        {
            Builder.IncludesType(typeof(TObject), ConfigurationSource.Explicit);
            return this;
        }

        public IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2>() =>
            OfTypes(typeof(T1), typeof(T2));

        public IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3>() =>
            OfTypes(typeof(T1), typeof(T2), typeof(T3));

        public IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4>() =>
            OfTypes(typeof(T1), typeof(T2), typeof(T3), typeof(T4));

        public IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5>() =>
            OfTypes(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));

        public IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6>() =>
            OfTypes(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));

        public IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6, T7>() =>
            OfTypes(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));

        public IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>() =>
            OfTypes(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8),
                typeof(T9));

        public IUnionTypeBuilder<TUnion, TContext> OfTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>() =>
            OfTypes(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8),
                typeof(T9), typeof(T10));

        public IUnionTypeBuilder<TUnion, TContext> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }

        public IUnionTypeBuilder<TUnion, TContext> DirectiveAnnotation(string name) => DirectiveAnnotation(name, null);

        public IUnionTypeBuilder<TUnion, TContext> DirectiveAnnotation(string name, object value)
        {
            Check.NotNull(name, nameof(name));
            Builder.AddOrUpdateDirectiveAnnotation(name, value);
            return this;
        }

        public IUnionTypeBuilder<TUnion, TContext> RemoveDirectiveAnnotation(string name)
        {
            Builder.RemoveDirectiveAnnotation(Check.NotNull(name, nameof(name)));
            return this;
        }
    }
}