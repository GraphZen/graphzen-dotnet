using System;
using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen
{
    public abstract class CollectionElementConfigurationFixture<
        TMarker,
        TDefMarker,
        TMutableDefMarker,
        TCollectionItemDefinition,
        TCollectionItem,
        TParentMemberDefinition,
        TParentMember
    > :
        ElementConfigurationFixture<TMarker, TDefMarker, TMutableDefMarker,
            TParentMemberDefinition, TParentMember>, ICollectionElementConfigurationFixture
        where TMutableDefMarker : TDefMarker
        where TParentMemberDefinition : MemberDefinition, TMutableDefMarker
        where TParentMember : Member, TMarker
        where TCollectionItemDefinition : MemberDefinition, IMutableNamed
        where TCollectionItem : Member, INamed
        where TMarker : TDefMarker
    {
        protected const string Grandparent = nameof(Grandparent);
        protected const string GreatGrandparent = nameof(GreatGrandparent);

        public Type CollectionItemMemberType { get; } = typeof(TCollectionItem);
        public Type CollectionItemMemberDefinitionType { get; } = typeof(TCollectionItemDefinition);

        public NamedCollection<IMutableNamed>
            GetCollection([NotNull] SchemaBuilder sb, [NotNull] string parentName) =>
            new DictionaryWrapper<TCollectionItemDefinition, IMutableNamed>(GetCollection(GetParent(sb, parentName)));

        public NamedCollection<INamed>
            GetCollection([NotNull] Schema schema, [NotNull] string parentName) =>
            new DictionaryWrapper<TCollectionItem, INamed>(
                GetCollection(GetParent(schema, parentName))
            );


        public abstract void AddItem([NotNull] SchemaBuilder sb, [NotNull] string parentName, [NotNull] string name);
        public abstract void IgnoreItem([NotNull] SchemaBuilder sb, [NotNull] string parentName, [NotNull] string name);

        public abstract void UnignoreItem([NotNull] SchemaBuilder sb, [NotNull] string parentName,
            [NotNull] string name);

        public ConfigurationSource? FindIgnoredItemConfigurationSource([NotNull] SchemaBuilder sb,
            [NotNull] string parentName,
            [NotNull] string itemName) => FindIgnoredItemConfigurationSource(GetParent(sb, parentName), itemName);

        [NotNull]
        public abstract IReadOnlyDictionary<string, TCollectionItemDefinition> GetCollection(
            [NotNull] TParentMemberDefinition parent);

        [NotNull]
        public abstract IReadOnlyDictionary<string, TCollectionItem> GetCollection([NotNull] TParentMember parent);

        public abstract ConfigurationSource? FindIgnoredItemConfigurationSource(
            [NotNull] TParentMemberDefinition parent, [NotNull] string name);

        public abstract void RenameItem([NotNull] SchemaBuilder sb, [NotNull] string parentName, [NotNull] string name,
            [NotNull] string newName);

        //public class CollectionWrapper<T>
        //{
        //    public CollectionWrapper([NotNull]IReadOnlyDictionary<string, TInner> innerDictionary)
        //    {
        //        InnerDictionary = innerDictionary;
        //    }

        //    private IReadOnlyDictionary<string, TInner> InnerDictionary { get; }


        //    public int Count => InnerDictionary.Count;
        //    public bool ContainsKey(string key) => throw new NotImplementedException();

        //    public bool TryGetValue(string key, out T value) => throw new NotImplementedException();

        //    public T this[string key] => throw new NotImplementedException();
        //}
    }
}