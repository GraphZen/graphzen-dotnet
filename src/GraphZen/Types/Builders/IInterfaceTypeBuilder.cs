// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Types.Builders
{
    public interface
        IInterfaceTypeBuilder<TInterface, TContext> : IAnnotableBuilder<IInterfaceTypeBuilder<TInterface, TContext>>,
            IFieldsContainerDefinitionBuilder<IInterfaceTypeBuilder<TInterface, TContext>, TInterface, TContext>
        where TContext : GraphQLContext
    {
        [NotNull]
        IInterfaceTypeBuilder<TInterface, TContext> Description([CanBeNull] string description);


        [NotNull]
        IInterfaceTypeBuilder<TInterface, TContext>
            ResolveType(TypeResolver<TInterface, TContext> resolveTypeFn);

        //[NotNull]
        //IInterfaceTypeBuilder<object, TContext> ClrType(Type clrType);

        [NotNull]
        IInterfaceTypeBuilder<TInterface, TContext> Name(string newName);
    }
}