// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using GraphZen.Infrastructure;
using GraphZen.Language.Internal;
using GraphZen.Types.Builders;
using GraphZen.Types.Internal;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    public class TypeIdentity
    {
        private readonly TypeKind? _kind;
        [NotNull] private readonly SchemaDefinition _schema;

        private bool? _isInputType;
        private bool? _isOutputType;

        private string _name;

        private IGraphQLTypeDefinition _typeDefinition;

        public TypeIdentity(string name, SchemaDefinition schema, TypeKind? kind = null)
        {
            _schema = Check.NotNull(schema, nameof(schema));
            _name = Check.NotNull(name, nameof(name)).ThrowIfInvalidGraphQLName();

            // TODO: validate name
            _kind = kind;
            Name.ThrowIfInvalidGraphQLName();
        }

        public TypeIdentity(Type clrType, SchemaDefinition schema, TypeKind? kind = null)
        {
            Check.NotNull(clrType, nameof(clrType));
            _schema = Check.NotNull(schema, nameof(schema));
            ClrType = clrType.GetEffectiveClrType();
            _kind = kind;
            Name.ThrowIfInvalidGraphQLName();
        }


        public IGraphQLTypeDefinition Definition
        {
            get => _typeDefinition;
            set
            {
                if (_typeDefinition != null)
                {
                    throw new InvalidOperationException(
                        $"Cannot set property {nameof(TypeIdentity)}.{nameof(Definition)} with value {value}, it's value has already been set with {_typeDefinition}.");
                }

                _typeDefinition =
                    value ?? throw new InvalidOperationException(
                        $"Cannot set property {nameof(TypeIdentity)}.{nameof(Definition)} to null.");
            }
        }

        public TypeKind? Kind => _typeDefinition?.Kind ?? _kind;

        [NotNull]
        public string Name
        {
            get => (_typeDefinition is NamedType named ? named.Name : _name ?? ClrType?.GetGraphQLName()) ??
                   throw new InvalidOperationException();
            set
            {
                var newName = value;
                newName.ThrowIfInvalidGraphQLName();
                var newId = new TypeIdentity(newName, _schema);
                var existing = _schema.FindTypeIdentity(newId);
                if (existing != null && !existing.Equals(this))
                {
                    throw new InvalidOperationException(
                        $"Cannot rename type \"{Name}\" to \"{newName}\", type named \"{newName}\" already exists.");
                }

                _name = newName;
            }
        }

        [CanBeNull]
        public Type ClrType { get; set; }

        public bool? IsInputType
        {
            get => Kind?.IsInputType() ?? _isInputType;
            set
            {
                if (_kind.HasValue)
                {
                    throw new InvalidOperationException(
                        $"Cannot set property {nameof(TypeIdentity)}.{nameof(IsInputType)}, because the identity's type kind ({Kind}) is already set.");
                }

                _isInputType = value;
            }
        }

        public bool? IsOutputType
        {
            get => Kind?.IsOutputType() ?? _isOutputType;
            set
            {
                if (_kind.HasValue)
                {
                    throw new InvalidOperationException(
                        $"Cannot set property {nameof(TypeIdentity)}.{nameof(IsOutputType)}, because the type identity's kind ({Kind}) is already set.");
                }

                _isOutputType = value;
            }
        }

        private bool Equals([NotNull] TypeIdentity other) =>
            Overlaps(other);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((TypeIdentity) obj);
        }

        // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
        public override int GetHashCode() => base.GetHashCode();


        public bool Overlaps(TypeIdentity identity)
        {
            Check.NotNull(identity, nameof(identity));

            if (ClrType != null && identity.ClrType != null)
            {
                if (IsInputType == true && identity.IsInputType == true
                    || IsOutputType == true && identity.IsOutputType == true)
                {
                    return ClrType == identity.ClrType;
                }
            }

            return string.Equals(Name, identity.Name);
        }

        public override string ToString() =>
            $"Identity:{(name: Name, clrType: ClrType, Kind, IsInputType, IsOutputType)}";
    }
}