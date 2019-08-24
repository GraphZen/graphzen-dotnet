using System;
using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.MetaModel;
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
        [NotNull]
        public abstract IReadOnlyDictionary<string, TCollectionItemDefinition> GetCollection(TParentMemberDefinition parent);

        [NotNull]
        public abstract IReadOnlyDictionary<string, TCollectionItem> GetCollection(TParentMember parent);

        public Type CollectionItemMemberType { get; } = typeof(TCollectionItem);
        public Type CollectionItemMemberDefinitionType { get; } = typeof(TCollectionItemDefinition);

        public IReadOnlyDictionary<string, IMutableNamed>
            GetCollection(SchemaBuilder sb, string parentName) => GetCollection(GetParent(sb, parentName))
            .ToDictionary(_ => _.Key, _ => _.Value as IMutableNamed);

        public IReadOnlyDictionary<string, INamed> GetCollection(Schema schema, string parentName) =>
            GetCollection(GetParent(schema, parentName)).ToDictionary(_ => _.Key, _ => _.Value as INamed);

        public abstract ConfigurationSource? FindItemIgnoredConfigurationSource(TParentMemberDefinition parent, string name);

        public ConfigurationSource? FindItemIgnoredConfigurationSource(SchemaBuilder sb, string parentName, string name)
            => FindItemIgnoredConfigurationSource(GetParent(sb, parentName), name);

        public abstract void AddItem(SchemaBuilder sb, string parentName, string name);
        public abstract void IgnoreItem(SchemaBuilder sb, string parentName, string name);
        public abstract void UnignoreItem(SchemaBuilder sb, string parentName, string name);
        public abstract void RenameItem(SchemaBuilder sb, string parentName, string name, string newName);
        ConfigurationSource? ICollectionElementConfigurationFixture.FindIgnoredItemConfigurationSource(SchemaBuilder sb, string parentName, string itemName) => throw new NotImplementedException();
    }
}