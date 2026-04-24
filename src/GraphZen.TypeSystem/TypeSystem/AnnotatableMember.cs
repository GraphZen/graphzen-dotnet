// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Immutable;

namespace GraphZen.TypeSystem;

[GraphQLIgnore]
public abstract class AnnotatableMember : Member, IDirectiveAnnotations
{
    protected AnnotatableMember(IReadOnlyList<IDirectiveAnnotation>? directives) =>
        DirectiveAnnotations = directives ?? ImmutableArray<IDirectiveAnnotation>.Empty;

    [GraphQLIgnore] public IReadOnlyList<IDirectiveAnnotation> DirectiveAnnotations { get; }

    [GraphQLIgnore] public abstract DirectiveLocation DirectiveLocation { get; }
    public IEnumerable<IDirectiveAnnotation> GetDirectiveAnnotations() => DirectiveAnnotations;


    [GraphQLIgnore]
    public IDirectiveAnnotation? FindDirectiveAnnotation(string name)
    {
        Check.NotNull(name, nameof(name));
        return DirectiveAnnotations.SingleOrDefault(_ => _.Name == name);
    }
}
