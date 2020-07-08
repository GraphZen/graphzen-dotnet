// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class ScalarTypeBuilder : IScalarTypeBuilder
    {
        public ScalarTypeBuilder(InternalScalarTypeBuilder builder)
        {
            Builder = builder;
        }

        protected InternalScalarTypeBuilder Builder { get; }
        InternalScalarTypeBuilder IInfrastructure<InternalScalarTypeBuilder>.Instance => Builder;
        ScalarTypeDefinition IInfrastructure<ScalarTypeDefinition>.Instance => Builder.Definition;

        public INamedBuilder Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.SetName(name, ConfigurationSource.Explicit);
            return this;
        }

        public IDescriptionBuilder Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public IDescriptionBuilder RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

        public IClrTypeBuilder ClrType(Type clrType, bool inferName = false)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.ClrType(clrType, inferName, ConfigurationSource.Explicit);
            return this;
        }

        public IClrTypeBuilder ClrType(Type clrType, string name)
        {
            Check.NotNull(clrType, nameof(clrType));
            Check.NotNull(name, nameof(name));
            Builder.ClrType(clrType, name, ConfigurationSource.Explicit);
            return this;
        }

        public IClrTypeBuilder RemoveClrType()
        {
            Builder.RemoveClrType(ConfigurationSource.Explicit);
            return this;
        }

        public IAnnotableBuilder AddDirectiveAnnotation(string name, object value)
        {
            Check.NotNull(name, nameof(name));
            Builder.AddDirectiveAnnotation(name, value, ConfigurationSource.Explicit);
            return this;
        }

        public IAnnotableBuilder AddDirectiveAnnotation(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.AddDirectiveAnnotation(name, null, ConfigurationSource.Explicit);
            return this;
        }

        public IAnnotableBuilder RemoveDirectiveAnnotations(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.RemoveDirectiveAnnotations(name, ConfigurationSource.Explicit);
            return this;
        }

        public IAnnotableBuilder ClearDirectiveAnnotations()
        {
            Builder.ClearDirectiveAnnotations(ConfigurationSource.Explicit);
            return this;
        }
    }

    public class ScalarTypeBuilder<TScalar, TValueNode> : ScalarTypeBuilder, IScalarTypeBuilder<TScalar, TValueNode>
        where TValueNode : ValueSyntax
        where TScalar : notnull
    {
        public ScalarTypeBuilder(InternalScalarTypeBuilder builder) : base(builder)
        {
        }


        [DebuggerStepThrough]
        public new ScalarTypeBuilder<object, TValueNode> ClrType(Type clrType, bool inferName = false)
        {
            base.ClrType(clrType, inferName);
            return new ScalarTypeBuilder<object, TValueNode>(Builder);
        }


        [DebuggerStepThrough]
        public new ScalarTypeBuilder<object, TValueNode> ClrType(Type clrType, string name)
        {
            base.ClrType(clrType, name);
            return new ScalarTypeBuilder<object, TValueNode>(Builder);
        }

        [DebuggerStepThrough]
        public ScalarTypeBuilder<T, TValueNode> ClrType<T>(string name) where T : notnull =>
            new ScalarTypeBuilder<T, TValueNode>(Builder.ClrType(typeof(T), Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit));

        [DebuggerStepThrough]
        public new ScalarTypeBuilder<object, TValueNode> RemoveClrType()
        {
            base.RemoveClrType();
            return new ScalarTypeBuilder<object, TValueNode>(Builder);
        }

        [DebuggerStepThrough]
        public ScalarTypeBuilder<T, TValueNode> ClrType<T>(bool inferName = false) where T : notnull
        {
            base.ClrType(typeof(T), inferName);
            return new ScalarTypeBuilder<T, TValueNode>(Builder);
        }

        [DebuggerStepThrough]
        public new ScalarTypeBuilder<TScalar, TValueNode> Description(string description) =>
            (ScalarTypeBuilder<TScalar, TValueNode>)base.Description(description);

        [DebuggerStepThrough]
        public new ScalarTypeBuilder<TScalar, TValueNode> RemoveDescription() =>
            (ScalarTypeBuilder<TScalar, TValueNode>)base.RemoveDescription();

        [DebuggerStepThrough]
        public ScalarTypeBuilder<TScalar, TValueNode> Serializer(LeafSerializer serializer)
        {
            Check.NotNull(serializer, nameof(serializer));
            Builder.Serializer(value => serializer(value));
            return this;
        }

        public ScalarTypeBuilder<TScalar, TValueNode> LiteralParser(
            LeafLiteralParser<object, TValueNode> literalParser)
        {
            Check.NotNull(literalParser, nameof(literalParser));
            Builder.LiteralParser(value =>
            {
                Debug.Assert(value != null, nameof(value) + " != null");
                var parsed = literalParser((TValueNode)value);
                Debug.Assert(parsed != null, nameof(parsed) + " != null");
                return parsed.Cast<object>();
            });
            return this;
        }

        public ScalarTypeBuilder<TScalar, TValueNode> ValueParser(LeafValueParser<object> valueParser)
        {
            Check.NotNull(valueParser, nameof(valueParser));

            Builder.ValueParser(value =>
            {
                var parsed = valueParser(value);
                Debug.Assert(parsed != null, nameof(parsed) + " != null");
                return parsed.Cast<object>();
            });

            return this;
        }


        public new ScalarTypeBuilder<TScalar, TValueNode> Name(string name) =>
            (ScalarTypeBuilder<TScalar, TValueNode>)base.Name(name);


        public new ScalarTypeBuilder<TScalar, TValueNode> AddDirectiveAnnotation(string name, object value) =>
            (ScalarTypeBuilder<TScalar, TValueNode>)base.AddDirectiveAnnotation(name, value);


        public new ScalarTypeBuilder<TScalar, TValueNode> AddDirectiveAnnotation(string name) =>
            (ScalarTypeBuilder<TScalar, TValueNode>)base.AddDirectiveAnnotation(name);

        public new ScalarTypeBuilder<TScalar, TValueNode> RemoveDirectiveAnnotations(string name) =>
            (ScalarTypeBuilder<TScalar, TValueNode>)base.RemoveDirectiveAnnotations(name);

        public new ScalarTypeBuilder<TScalar, TValueNode> ClearDirectiveAnnotations() =>
            (ScalarTypeBuilder<TScalar, TValueNode>)base.ClearDirectiveAnnotations();
    }
}