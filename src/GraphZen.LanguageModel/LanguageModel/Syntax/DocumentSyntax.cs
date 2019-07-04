// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GraphZen.Infrastructure;


namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     A GraphQL document contains multiple definitions, either executable or representative of a GraphQL type system.
    ///     http://facebook.github.io/graphql/June2018/#sec-Language.Document
    /// </summary>
    public partial class DocumentSyntax : SyntaxNode
    {
        [NotNull] [ItemNotNull]
        private readonly Lazy<IReadOnlyDictionary<string, TypeDefinitionSyntax>> _abstractTypeMap;

        [NotNull] [ItemNotNull]
        private readonly Lazy<IReadOnlyDictionary<string, IReadOnlyCollection<ObjectTypeDefinitionSyntax>>>
            _implementations;

        [NotNull] [ItemNotNull] private readonly Lazy<IReadOnlyList<TypeDefinitionSyntax>> _inputTypeDefinitions;

        [NotNull] [ItemNotNull]
        private readonly Lazy<IReadOnlyDictionary<string, ObjectTypeDefinitionSyntax>> _objectTypeMap;

        [NotNull] [ItemNotNull] private readonly Lazy<IReadOnlyList<TypeDefinitionSyntax>> _outputTypeDefinitions;

        [NotNull] private readonly Dictionary<string, IReadOnlyDictionary<string, bool>> _possibleTypeMap =
            new Dictionary<string, IReadOnlyDictionary<string, bool>>();


        public DocumentSyntax(IReadOnlyList<DefinitionSyntax> definitions,
            SyntaxLocation location = null) : base(location)
        {
            Definitions = Check.NotNull(definitions, nameof(definitions));
            _inputTypeDefinitions = new Lazy<IReadOnlyList<TypeDefinitionSyntax>>(() => Definitions
                .OfType<TypeDefinitionSyntax>().Where(_ => _.IsInputType)
                .ToReadOnlyList());

            _outputTypeDefinitions = new Lazy<IReadOnlyList<TypeDefinitionSyntax>>(() =>
                Definitions.OfType<TypeDefinitionSyntax>().Where(_ => _.IsOutputType).ToReadOnlyList());

            _abstractTypeMap = new Lazy<IReadOnlyDictionary<string, TypeDefinitionSyntax>>(() =>
                Definitions.Where(_ => _ is UnionTypeDefinitionSyntax || _ is InterfaceTypeDefinitionSyntax)
                    // ReSharper disable once PossibleNullReferenceException
                    .Cast<TypeDefinitionSyntax>().ToReadOnlyDictionaryIgnoringDuplicates(_ => _.Name.Value));
            _objectTypeMap = new Lazy<IReadOnlyDictionary<string, ObjectTypeDefinitionSyntax>>(() =>
                Definitions.OfType<ObjectTypeDefinitionSyntax>()
                    // ReSharper disable once PossibleNullReferenceException
                    .ToReadOnlyDictionaryIgnoringDuplicates(_ => _.Name.Value));

            _implementations = new Lazy<IReadOnlyDictionary<string, IReadOnlyCollection<ObjectTypeDefinitionSyntax>>>(
                () =>
                {
                    var implementations = new Dictionary<string, IReadOnlyCollection<ObjectTypeDefinitionSyntax>>();

                    foreach (var objectType in GetObjectTypeMap().Values)
                    {
                        foreach (var iface in objectType.Interfaces)
                        {
                            if (implementations.TryGetValue(iface.Name.Value, out
                                var impls))
                            {
                                ((HashSet<ObjectTypeDefinitionSyntax>) impls).Add(objectType);
                            }
                            else
                            {
                                implementations[iface.Name.Value] =
                                    new HashSet<ObjectTypeDefinitionSyntax> {objectType};
                            }
                        }
                    }

                    foreach (var abstractType in GetAbstractTypeMap().Values)
                    {
                        if (!implementations.ContainsKey(abstractType.Name.Value))
                        {
                            implementations[abstractType.Name.Value] = new HashSet<ObjectTypeDefinitionSyntax>();
                        }
                    }

                    return new ReadOnlyDictionary<string, IReadOnlyCollection<ObjectTypeDefinitionSyntax>>(
                        implementations);
                });
        }

        [NotNull]
        [ItemNotNull]
        public IReadOnlyList<DefinitionSyntax> Definitions { get; }

        public override IEnumerable<SyntaxNode> Children => Definitions;

        [NotNull]
        [ItemNotNull]
        public IReadOnlyList<TypeDefinitionSyntax> GetInputTypeDefinitions() => _inputTypeDefinitions.Value;

        [NotNull]
        [ItemNotNull]
        public IReadOnlyList<TypeDefinitionSyntax> GetOutputTypeDefinitions() => _outputTypeDefinitions.Value;

        [NotNull]
        private IReadOnlyDictionary<string, TypeDefinitionSyntax> GetAbstractTypeMap() => _abstractTypeMap.Value;

        [NotNull]
        private IReadOnlyDictionary<string, ObjectTypeDefinitionSyntax> GetObjectTypeMap() => _objectTypeMap.Value;

        [NotNull]
        private IReadOnlyDictionary<string, IReadOnlyCollection<ObjectTypeDefinitionSyntax>> GetImplementationMap() =>
            _implementations.Value;


        public bool IsTypeSubTypeOf(TypeSyntax maybeSubType, TypeSyntax superType)
        {
            Check.NotNull(maybeSubType, nameof(maybeSubType));
            Check.NotNull(superType, nameof(superType));
            if (maybeSubType.Equals(superType))
            {
                return true;
            }

            if (superType is NonNullTypeSyntax nnSuper)
            {
                if (maybeSubType is NonNullTypeSyntax nnMaybeSub)
                {
                    return IsTypeSubTypeOf(nnMaybeSub.OfType, nnSuper.OfType);
                }

                return false;
            }

            if (maybeSubType is NonNullTypeSyntax nnMaybeSubType)
            {
                return IsTypeSubTypeOf(nnMaybeSubType.OfType, superType);
            }

            if (superType is ListTypeSyntax listSuper)
            {
                if (maybeSubType is ListTypeSyntax listMaybeSub)
                {
                    return IsTypeSubTypeOf(listMaybeSub.OfType, listSuper.OfType);
                }

                return false;
            }

            if (maybeSubType is ListTypeSyntax)
            {
                return false;
            }

            if (
                // Is super type abstract type?
                GetAbstractTypeMap().TryGetValue(((NamedTypeSyntax) superType).Name.Value, out var abstractSuperType)
                // Is possible sub type object type?
                && GetObjectTypeMap().TryGetValue(((NamedTypeSyntax) maybeSubType).Name.Value,
                    out var maybeSubTypeObjectType)
                && IsPossibleType(abstractSuperType, maybeSubTypeObjectType))

            {
                return true;
            }

            return false;
        }

        [NotNull]
        [ItemNotNull]
        private IReadOnlyCollection<ObjectTypeDefinitionSyntax> GetPossibleTypes(
            [NotNull] TypeDefinitionSyntax abstractType)
        {
            if (abstractType is UnionTypeDefinitionSyntax unionType)
            {
                return unionType.MemberTypes.Select(_ =>
                        // ReSharper disable once PossibleNullReferenceException
                        GetObjectTypeMap().TryGetValue(_.Name.Value, out var outputType) ? outputType : null)
                    .Where(_ => _ != null).ToReadOnlyList();
            }

            return GetImplementationMap().TryGetValue(abstractType.Name.Value, out var possibleTypes)
                ? possibleTypes
                : new List<ObjectTypeDefinitionSyntax>();
        }

        private bool IsPossibleType([NotNull] TypeDefinitionSyntax abstractType,
            [NotNull] ObjectTypeDefinitionSyntax possibleType)
        {
            if (!_possibleTypeMap.ContainsKey(abstractType.Name.Value))
            {
                _possibleTypeMap[abstractType.Name.Value] =
                    GetPossibleTypes(abstractType).ToDictionary(_ => _.Name.Value, _ => true);
            }

            return _possibleTypeMap.TryGetValue(abstractType.Name.Value, out var possibleTypesMap) &&
                   possibleTypesMap.ContainsKey(possibleType.Name.Value);
        }


        [NotNull]
        public DocumentSyntax WithFilteredDefinitions(Func<DefinitionSyntax, bool> predicate)
        {
            Check.NotNull(predicate, nameof(predicate));
            return new DocumentSyntax(Definitions.Where(predicate).ToList());
        }


        private bool Equals([NotNull] DocumentSyntax other) => Definitions.SequenceEqual(other.Definitions);

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

            return obj is DocumentSyntax syntax && Equals(syntax);
        }

        public override int GetHashCode() => Definitions.GetHashCode();
    }
}