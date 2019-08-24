// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem
{
    public class FieldBuilder<TDeclaringType, TField, TContext> : IFieldBuilder<TDeclaringType, TField, TContext>,
        IInfrastructure<InternalFieldBuilder>
        where TContext : GraphQLContext
    {
        public FieldBuilder( InternalFieldBuilder builder)
        {
            Builder = builder;
        }

        
        private InternalFieldBuilder Builder { get; }


        public IFieldBuilder<TDeclaringType, object, TContext> FieldType(string type)
        {
            Check.NotNull(type, nameof(type));
            Builder.FieldType(type);
            return new FieldBuilder<TDeclaringType, object, TContext>(Builder);
        }

        public IFieldBuilder<TDeclaringType, TField, TContext> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }

        [SuppressMessage("ReSharper", "MethodOverloadWithOptionalParameter")]
        public IFieldBuilder<TDeclaringType, TFieldNew, TContext> FieldType<TFieldNew>(bool canBeNull = false,
            bool itemCanBeNull = false) where TFieldNew : IEnumerable
        {
            Builder.FieldType(typeof(TFieldNew));
            return new FieldBuilder<TDeclaringType, TFieldNew, TContext>(Builder);
        }

        public IFieldBuilder<TDeclaringType, TField, TContext> Description(string description)
        {
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public IFieldBuilder<TDeclaringType, TField, TContext> Resolve(Func<TField> resolver)
        {
            Check.NotNull(resolver, nameof(resolver));
            Builder.Resolve((source, args, context, info) => resolver());
            return this;
        }

        public IFieldBuilder<TDeclaringType, TField, TContext> Resolve(Func<TDeclaringType, TField> resolver)
        {
            Check.NotNull(resolver, nameof(resolver));
            Builder.Resolve((source, args, context, info) => resolver((TDeclaringType)source));
            return this;
        }

        public IFieldBuilder<TSource, TField, TContext> Resolve<TSource>(Func<TSource, TField> resolver) =>
            new FieldBuilder<TSource, TField, TContext>(Builder).Resolve(resolver);

        public IFieldBuilder<TDeclaringType, TField, TContext> Resolve(Func<TDeclaringType, dynamic, TField> resolver)
        {
            Check.NotNull(resolver, nameof(resolver));
            Builder.Resolve((source, args, context, info) => resolver((TDeclaringType)source, args));
            return this;
        }

        public IFieldBuilder<TDeclaringType, TField, TContext> Resolve(
            Func<TDeclaringType, dynamic, GraphQLContext, TField> resolver)
        {
            Check.NotNull(resolver, nameof(resolver));
            Builder.Resolve((source, args, context, info) => resolver((TDeclaringType)source, args, context));
            return this;
        }

        public IFieldBuilder<TDeclaringType, TField, TContext> Resolve(
            Func<TDeclaringType, dynamic, GraphQLContext, ResolveInfo, TField> resolver)
        {
            Check.NotNull(resolver, nameof(resolver));
            Builder.Resolve((source, args, context, info) => resolver((TDeclaringType)source, args, context, info));
            return this;
        }

        public IFieldBuilder<TDeclaringType, TField, TContext> Argument(string name, string type,
            Action<InputValueBuilder> argumentBuilder = null)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(type, nameof(type));
            var argBuilder = Builder.Argument(name, ConfigurationSource.Explicit).Type(type);
            argumentBuilder?.Invoke(new InputValueBuilder(argBuilder));
            return this;
        }

        public IFieldBuilder<TDeclaringType, TField, TContext> Argument(string name, Action<InputValueBuilder> argumentBuilder = null)
        {
            Check.NotNull(name, nameof(name));
            var argBuilder = Builder.Argument(name, ConfigurationSource.Explicit);
            argumentBuilder?.Invoke(new InputValueBuilder(argBuilder));
            return this;
        }

        public IFieldBuilder<TDeclaringType, TField, TContext> Argument<TArg>(string name,
            Action<InputValueBuilder> argumentBuilder = null)
        {
            Check.NotNull(name, nameof(name));
            var argBuilder = Builder.Argument(name, ConfigurationSource.Explicit).Type(typeof(TArg));
            argumentBuilder?.Invoke(new InputValueBuilder(argBuilder));
            return this;
        }

        public IFieldBuilder<TDeclaringType, TField, TContext> IgnoreArgument(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreArgument(name, ConfigurationSource.Explicit);
            return this;
        }

        public IFieldBuilder<TDeclaringType, TField, TContext> UnignoreArgument(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreArgument(name, ConfigurationSource.Explicit);
            return this;
        }

        public IFieldBuilder<TDeclaringType, TField, TContext> Deprecated(string reason)
        {
            Check.NotNull(reason, nameof(reason));
            Builder.Deprecated(reason);
            return this;
        }

        public IFieldBuilder<TDeclaringType, TField, TContext> Deprecated(bool deprecated = true)
        {
            Builder.Deprecated(deprecated);
            return this;
        }

        public IFieldBuilder<TDeclaringType, TField, TContext> DirectiveAnnotation(string name) =>
            DirectiveAnnotation(name, null);

        public IFieldBuilder<TDeclaringType, TField, TContext> DirectiveAnnotation(string name, object value)
        {
            Builder.AddOrUpdateDirectiveAnnotation(Check.NotNull(name, nameof(name)), value);
            return this;
        }

        public IFieldBuilder<TDeclaringType, TField, TContext> RemoveDirectiveAnnotation(string name)
        {
            Builder.RemoveDirectiveAnnotation(Check.NotNull(name, nameof(name)));
            return this;
        }

        InternalFieldBuilder IInfrastructure<InternalFieldBuilder>.Instance => Builder;

        public IFieldBuilder<TDeclaringType, TFieldNew, TContext> FieldType<TFieldNew>(bool canBeNull = false)
        {
            Builder.FieldType(typeof(TFieldNew));
            return new FieldBuilder<TDeclaringType, TFieldNew, TContext>(Builder);
        }
    }
}