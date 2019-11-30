using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Infrastructure
{
    public abstract class DirectiveAnnotationsConfigurationFixture<TMemberDefinition, TMember> :ConfigurationFixture<IDirectiveAnnotations, IDirectiveAnnotationsDefinition, IMutableDirectiveAnnotationsDefinition, TMemberDefinition, TMember> where TMemberDefinition : AnnotatableMemberDefinition  where TMember : AnnotatableMember
    {}
}