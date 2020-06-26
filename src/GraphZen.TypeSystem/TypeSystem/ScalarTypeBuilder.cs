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
    public class ScalarTypeBuilder<TScalar, TValueNode> : IScalarTypeBuilder<TScalar, TValueNode>
        where TValueNode : ValueSyntax
        where TScalar : notnull
    {
        public ScalarTypeBuilder(InternalScalarTypeBuilder builder)
        {
            Builder = Check.NotNull(builder, nameof(builder));
        }


        private InternalScalarTypeBuilder Builder { get; }

        InternalScalarTypeBuilder IInfrastructure<InternalScalarTypeBuilder>.Instance => Builder;

        [DebuggerStepThrough]
        public ScalarTypeBuilder<object, TValueNode> ClrType(Type clrType, bool inferName = false) =>
            new ScalarTypeBuilder<object, TValueNode>(Builder.ClrType(Check.NotNull(clrType, nameof(clrType)),
                inferName, ConfigurationSource.Explicit));

        [DebuggerStepThrough]
        public ScalarTypeBuilder<object, TValueNode> ClrType(Type clrType, string name) =>
            new ScalarTypeBuilder<object, TValueNode>(
                Builder.ClrType(Check.NotNull(clrType, nameof(clrType)), Check.NotNull(name, nameof(name)),
                    ConfigurationSource.Explicit)
            );

        [DebuggerStepThrough]
        public ScalarTypeBuilder<T, TValueNode> ClrType<T>(string name) where T : notnull =>
            new ScalarTypeBuilder<T, TValueNode>(Builder.ClrType(typeof(T), Check.NotNull(name, nameof(name)),
                ConfigurationSource.Explicit));

        [DebuggerStepThrough]
        public ScalarTypeBuilder<object, TValueNode> RemoveClrType()
        {
            Builder.RemoveClrType(ConfigurationSource.Explicit);
            return new ScalarTypeBuilder<object, TValueNode>(Builder);
        }

        [DebuggerStepThrough]
        public ScalarTypeBuilder<T, TValueNode> ClrType<T>(bool inferName = false) where T : notnull
        {
            Builder.ClrType(typeof(T), inferName, ConfigurationSource.Explicit);
            return new ScalarTypeBuilder<T, TValueNode>(Builder);
        }

        [DebuggerStepThrough]
        public ScalarTypeBuilder<TScalar, TValueNode> Description(string description)
        {
            Check.NotNull(description, nameof(description));
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        [DebuggerStepThrough]
        public ScalarTypeBuilder<TScalar, TValueNode> RemoveDescription()
        {
            Builder.RemoveDescription(ConfigurationSource.Explicit);
            return this;
        }

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


        public ScalarTypeBuilder<TScalar, TValueNode> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.SetName(name, ConfigurationSource.Explicit);
            return this;
        }


        public ScalarTypeBuilder<TScalar, TValueNode> AddDirectiveAnnotation(string name, object value) => throw new NotImplementedException();

        public ScalarTypeBuilder<TScalar, TValueNode> AddDirectiveAnnotation(string name) => throw new NotImplementedException();


        public ScalarTypeBuilder<TScalar, TValueNode> RemoveDirectiveAnnotations(string name) =>
            throw new NotImplementedException();

        public ScalarTypeBuilder<TScalar, TValueNode> RemoveDirectiveAnnotations() =>
            throw new NotImplementedException();
    }
}