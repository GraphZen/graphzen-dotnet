using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IDirectiveAnnotationsDefinition
    {
        DirectiveLocation DirectiveLocation { get; }
        IEnumerable<IDirectiveAnnotation> GetDirectiveAnnotations();
    }
}