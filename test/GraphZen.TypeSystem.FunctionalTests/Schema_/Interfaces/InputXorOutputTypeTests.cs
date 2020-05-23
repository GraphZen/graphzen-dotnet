// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;
using static GraphZen.TypeSystem.FunctionalTests.Specs.TypeSystemSpecs.InputXorOutputTypeSpecs;

namespace GraphZen.TypeSystem.FunctionalTests.Schema_.Interfaces
{
    [NoReorder]
    public class InputXorOutputTypeTests
    {
        // ReSharper disable once InconsistentNaming
        private interface PlainInterface
        {
        }

        [GraphQLName(AnnotatedName)]
        // ReSharper disable once InconsistentNaming
        private interface PlainInterfaceAnnotatedName
        {
            public const string AnnotatedName = nameof(AnnotatedName);
        }

        [GraphQLName("abc &*(")]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedType.Local
        private interface PlainInterfaceInvalidNameAnnotation
        {
        }



    }
}