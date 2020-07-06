// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IDirectiveDefinition :
        INamed,
        IDescription,
        IClrType,
        IArgumentsDefinition,
        IDirectiveLocationsDefinition,
        IMemberParentDefinition,
        IMaybeRepeatableDefinition, IMaybeSpecDefinition

    {
    }

    public interface IMaybeIntrospectionDefinition
    {
        bool IsIntrospection { get; }
    }

    public interface IMaybeIntrospection : IMaybeIntrospectionDefinition
    {
    }





    public interface IMaybeSpecDefinition
    {

        public bool IsSpec { get; }
    }

    public interface IMaybeSpec : IMaybeSpecDefinition
    {

    }

    public interface IMaybeRepeatableDefinition
    {

        public bool IsRepeatable { get; }
    }

    public interface IMutableMaybeRepeatableDefinition : IMaybeRepeatableDefinition
    {
        bool SetRepeatable(bool repeatable, ConfigurationSource configurationSource);
        ConfigurationSource GetRepeatableConfigurationSource();

    }

    public interface IMaybeRepeatable : IMaybeRepeatableDefinition { }

}