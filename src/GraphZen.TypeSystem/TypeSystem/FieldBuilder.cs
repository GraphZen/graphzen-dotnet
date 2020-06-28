// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class FieldBuilder<TDeclaringType, TField, TContext> : IFieldBuilder<TDeclaringType, TField, TContext>
        where TContext : GraphQLContext
    {
        public FieldBuilder(InternalFieldBuilder builder)
        {
            Builder = builder;
        }

        private InternalFieldBuilder Builder { get; }


        public FieldBuilder<TDeclaringType, object, TContext> FieldType(string type)
        {
            Check.NotNull(type, nameof(type));
            Builder.FieldType(type, ConfigurationSource.Explicit);
            return new FieldBuilder<TDeclaringType, object, TContext>(Builder);
        }

        public FieldBuilder<TDeclaringType, TField, TContext> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.SetName(name, ConfigurationSource.Explicit);
            return this;
        }

        [SuppressMessage("ReSharper", "MethodOverloadWithOptionalParameter")]
        public FieldBuilder<TDeclaringType, TFieldNew, TContext> FieldType<TFieldNew>(bool canBeNull = false,
            bool itemCanBeNull = false) where TFieldNew : IEnumerable
        {
            Builder.FieldType(typeof(TFieldNew), ConfigurationSource.Explicit);
            return new FieldBuilder<TDeclaringType, TFieldNew, TContext>(Builder);
        }

        public FieldBuilder<TDeclaringType, TField, TContext> Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public FieldBuilder<TDeclaringType, TField, TContext> RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public FieldBuilder<TDeclaringType, TField, TContext> Resolve(Func<TField> resolver)
        {
            Check.NotNull(resolver, nameof(resolver));
            Builder.Resolve((source, args, context, info) => resolver());
            return this;
        }

        public FieldBuilder<TDeclaringType, TField, TContext> Resolve(Func<TDeclaringType, TField> resolver)
        {
            Check.NotNull(resolver, nameof(resolver));
            Builder.Resolve((source, args, context, info) => resolver((TDeclaringType)source));
            return this;
        }

        public FieldBuilder<TSource, TField, TContext> Resolve<TSource>(Func<TSource, TField> resolver) =>
            new FieldBuilder<TSource, TField, TContext>(Builder).Resolve(resolver);

        public FieldBuilder<TDeclaringType, TField, TContext> Resolve(Func<TDeclaringType, dynamic, TField> resolver)
        {
            Check.NotNull(resolver, nameof(resolver));
            Builder.Resolve((source, args, context, info) => resolver((TDeclaringType)source, args));
            return this;
        }

        public FieldBuilder<TDeclaringType, TField, TContext> Resolve(
            Func<TDeclaringType, dynamic, GraphQLContext, TField> resolver)
        {
            Check.NotNull(resolver, nameof(resolver));
            Builder.Resolve((source, args, context, info) => resolver((TDeclaringType)source, args, context));
            return this;
        }

        public FieldBuilder<TDeclaringType, TField, TContext> Resolve(
            Func<TDeclaringType, dynamic, GraphQLContext, ResolveInfo, TField> resolver)
        {
            Check.NotNull(resolver, nameof(resolver));
            Builder.Resolve((source, args, context, info) => resolver((TDeclaringType)source, args, context, info));
            return this;
        }

        public ArgumentBuilder<object?> Argument(string name) =>
            new ArgumentBuilder<object?>(Builder.Argument(Check.NotNull(name, nameof(name))));

        public FieldBuilder<TDeclaringType, TField, TContext> Argument(string name, string type)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Builder.Argument(name, type, ConfigurationSource.Explicit);
            return this;
        }


        public FieldBuilder<TDeclaringType, TField, TContext> Argument(string name, string type,
            Action<ArgumentBuilder<object?>> configurator)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            Check.NotNull(configurator, nameof(configurator));
            var argBuilder = Builder.Argument(name, type, ConfigurationSource.Explicit)!;
            configurator(new ArgumentBuilder<object?>(argBuilder));
            return this;
        }

        public FieldBuilder<TDeclaringType, TField, TContext> Argument<TArgument>(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Argument(name, typeof(TArgument), ConfigurationSource.Explicit);
            return this;
        }

        public FieldBuilder<TDeclaringType, TField, TContext> RemoveArgument(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveArgument(name, ConfigurationSource.Explicit);

            return this;
        }

        public FieldBuilder<TDeclaringType, TField, TContext> Argument(string name,
            Action<ArgumentBuilder<object?>> configurator)
        {
            Check.NotNull(name, nameof(name));
            var ib = Builder.Argument(name);
            configurator(new ArgumentBuilder<object?>(ib));
            return this;
        }

        public FieldBuilder<TDeclaringType, TField, TContext> Argument<TArgument>(string name,
            Action<ArgumentBuilder<TArgument>> configurator)
        {
            Check.NotNull(name, nameof(name));
            var argBuilder = Builder.Argument(name, typeof(TArgument), ConfigurationSource.Explicit)!;
            configurator(new ArgumentBuilder<TArgument>(argBuilder));
            return this;
        }

        public FieldBuilder<TDeclaringType, TField, TContext> IgnoreArgument(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreArgument(name, ConfigurationSource.Explicit);
            return this;
        }

        public FieldBuilder<TDeclaringType, TField, TContext> UnignoreArgument(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreArgument(name, ConfigurationSource.Explicit);
            return this;
        }


        public FieldBuilder<TDeclaringType, TField, TContext> AddDirectiveAnnotation(string name, object value)
        {
            Check.NotNull(name, nameof(name));
            Builder.AddDirectiveAnnotation(name, value, ConfigurationSource.Explicit);
            return this;
        }

        public FieldBuilder<TDeclaringType, TField, TContext> AddDirectiveAnnotation(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.AddDirectiveAnnotation(name, null, ConfigurationSource.Explicit);
            return this;
        }


        public FieldBuilder<TDeclaringType, TField, TContext> RemoveDirectiveAnnotations(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveDirectiveAnnotations(name, ConfigurationSource.Explicit);
            return this;
        }

        public FieldBuilder<TDeclaringType, TField, TContext> ClearDirectiveAnnotations()
        {
            Builder.ClearDirectiveAnnotations(ConfigurationSource.Explicit);
            return this;
        }

        InternalFieldBuilder IInfrastructure<InternalFieldBuilder>.Instance => Builder;
        IFieldDefinition IInfrastructure<IFieldDefinition>.Instance => Builder.Definition;

        public FieldBuilder<TDeclaringType, TFieldNew, TContext> FieldType<TFieldNew>(bool canBeNull = false)
        {
            Builder.FieldType(typeof(TFieldNew), ConfigurationSource.Explicit);
            return new FieldBuilder<TDeclaringType, TFieldNew, TContext>(Builder);
        }
    }
}