using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public interface IDirectives : IParentMember
    {
        DirectiveLocation DirectiveLocation { get; }
        IReadOnlyList<IDirective> DirectiveAnnotations { get; }
        IEnumerable<IDirective> FindDirectiveAnnotations(string name);
        bool HasDirectiveAnnotation(string name);
        IEnumerable<IDirective> FindDirectiveAnnotations(Func<IDirective, bool> predicate);
    }
}