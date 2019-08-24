// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using System;
using System.Diagnostics;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem
{
    public class ScalarTypeBuilder<TScalar, TValueNode> : IInfrastructure<InternalScalarTypeBuilder>,
        IScalarTypeBuilder<TScalar, TValueNode> where TValueNode : ValueSyntax
    {
        public ScalarTypeBuilder(InternalScalarTypeBuilder builder)
        {
            Builder = Check.NotNull(builder, nameof(builder));
        }

        
        private InternalScalarTypeBuilder Builder { get; }

        InternalScalarTypeBuilder IInfrastructure<InternalScalarTypeBuilder>.Instance => Builder;

        public IScalarTypeBuilder<object, TValueNode> ClrType(Type clrType)
        {

            return new ScalarTypeBuilder<object, TValueNode>(Builder);
        }

        public IScalarTypeBuilder<T, TValueNode> ClrType<T>()
        {
            Builder.ClrType(typeof(T), ConfigurationSource.Explicit);

            return new ScalarTypeBuilder<T, TValueNode>(Builder);
        }

        public IScalarTypeBuilder<TScalar, TValueNode> Description(string description)
        {
            Builder.Description(description, ConfigurationSource.Explicit);
            return this;
        }

        public IScalarTypeBuilder<TScalar, TValueNode> Serializer(LeafSerializer serializer)
        {
            Check.NotNull(serializer, nameof(serializer));
            Builder.Serializer(value => serializer(value));
            return this;
        }

        public IScalarTypeBuilder<TScalar, TValueNode> LiteralParser(
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

        public IScalarTypeBuilder<TScalar, TValueNode> ValueParser(LeafValueParser<object> valueParser)
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


        public IScalarTypeBuilder<TScalar, TValueNode> Name(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.Name(name, ConfigurationSource.Explicit);
            return this;
        }

        public IScalarTypeBuilder<TScalar, TValueNode> DirectiveAnnotation(string name) =>
            DirectiveAnnotation(name, null);

        public IScalarTypeBuilder<TScalar, TValueNode> DirectiveAnnotation(string name, object value)
        {
            Builder.AddOrUpdateDirectiveAnnotation(Check.NotNull(name, nameof(name)), value);
            return this;
        }

        public IScalarTypeBuilder<TScalar, TValueNode> RemoveDirectiveAnnotation(string name)
        {
            Builder.RemoveDirectiveAnnotation(Check.NotNull(name, nameof(name)));
            return this;
        }
    }
}