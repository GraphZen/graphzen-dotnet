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

    [GraphQLIgnore]
    public interface IMaybeIntrospectionDefinition
    {
        bool IsIntrospection { get; }
    }

    [GraphQLIgnore]
    public interface IMaybeIntrospection : IMaybeIntrospectionDefinition
    {
    }





    [GraphQLIgnore]
    public interface IMaybeSpecDefinition
    {

        [GraphQLIgnore]
        public bool IsSpec { get; }
    }

    [GraphQLIgnore]
    public interface IMaybeSpec : IMaybeSpecDefinition
    {

    }

    [GraphQLIgnore]
    public interface IMaybeRepeatableDefinition
    {

        public bool IsRepeatable { get; }
    }

    [GraphQLIgnore]
    public interface IMutableMaybeRepeatableDefinition : IMaybeRepeatableDefinition
    {
        bool SetRepeatable(bool repeatable, ConfigurationSource configurationSource);
        ConfigurationSource GetRepeatableConfigurationSource();

    }

    [GraphQLIgnore]
    public interface IMaybeRepeatable : IMaybeRepeatableDefinition { }

}