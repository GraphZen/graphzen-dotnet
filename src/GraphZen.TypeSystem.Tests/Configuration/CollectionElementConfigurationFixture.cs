using System;
using System.Collections.Generic;
using System.Linq;
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
        [NotNull]
        public abstract IReadOnlyDictionary<string, TCollectionItemDefinition> GetCollection([NotNull]TParentMemberDefinition parent);

        [NotNull]
        public abstract IReadOnlyDictionary<string, TCollectionItem> GetCollection([NotNull]TParentMember parent);

        public Type CollectionItemMemberType { get; } = typeof(TCollectionItem);
        public Type CollectionItemMemberDefinitionType { get; } = typeof(TCollectionItemDefinition);

        public IReadOnlyDictionary<string, IMutableNamed>
            GetCollection([NotNull]SchemaBuilder sb, [NotNull] string parentName) => GetCollection(GetParent(sb, parentName))
            .ToDictionary(_ => _.Key, _ => _.Value as IMutableNamed);

        public IReadOnlyDictionary<string, INamed> GetCollection([NotNull]Schema schema, [NotNull] string parentName) =>
            GetCollection(GetParent(schema, parentName)).ToDictionary(_ => _.Key, _ => _.Value as INamed);

        public abstract ConfigurationSource? FindIgnoredItemConfigurationSource([NotNull]TParentMemberDefinition parent, [NotNull]string name);


        public abstract void AddItem([NotNull]SchemaBuilder sb, [NotNull] string parentName, [NotNull]string name);
        public abstract void IgnoreItem([NotNull]SchemaBuilder sb, [NotNull]string parentName, [NotNull]string name);
        public abstract void UnignoreItem([NotNull]SchemaBuilder sb, [NotNull]string parentName, [NotNull]string name);

        public ConfigurationSource? FindIgnoredItemConfigurationSource([NotNull]SchemaBuilder sb, [NotNull] string parentName,
           [NotNull] string itemName) => FindIgnoredItemConfigurationSource(GetParent(sb, parentName), itemName);

        public abstract void RenameItem([NotNull]SchemaBuilder sb, [NotNull]string parentName, [NotNull]string name, [NotNull]string newName);
    }
}