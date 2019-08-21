using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen
{
    public abstract class CollectionConfigurationFixture<
        TMarker,
        TDefMarker,
        TMutableDefMarker,
        TCollectionItemDefinition,
        TCollectionItem,
        TParentMemberDefinition,
        TParentMember
    > :
        ConfigurationFixture<TMarker, TDefMarker, TMutableDefMarker,
            TParentMemberDefinition, TParentMember>, ICollectionConfigurationFixture
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
            GetCollection(SchemaBuilder sb, string parentName) =>
            GetCollection(GetParent(sb, parentName)).ToNamedCollection<IMutableNamed, TCollectionItemDefinition>();

        public NamedCollection<INamed> GetCollection(Schema schema, string parentName)
        {
            var collection = GetCollection(GetParent(schema, parentName));
            var casted = collection.ToNamedCollection<INamed, TCollectionItem>();
            return casted;
        }


        public abstract void AddItem(SchemaBuilder sb, string parentName, string name);
        public abstract void IgnoreItem(SchemaBuilder sb, string parentName, string name);

        public abstract void UnignoreItem(SchemaBuilder sb, string parentName,
            string name);

        public abstract void RenameItem(SchemaBuilder sb, string parentName, string itemName, string newName);

        public ConfigurationSource? FindIgnoredItemConfigurationSource(SchemaBuilder sb,
            string parentName,
            string itemName) => FindIgnoredItemConfigurationSource(GetParent(sb, parentName), itemName);

        [NotNull]
        public abstract NamedCollection<TCollectionItemDefinition> GetCollection(
            [NotNull] TParentMemberDefinition parent);

        [NotNull]
        public abstract NamedCollection<TCollectionItem> GetCollection([NotNull] TParentMember parent);

        public abstract ConfigurationSource? FindIgnoredItemConfigurationSource(
            [NotNull] TParentMemberDefinition parent, [NotNull] string name);

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