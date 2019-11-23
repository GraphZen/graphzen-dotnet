// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace GraphZen.Infrastructure
{
    public class CoreOptionsExtension : IGraphQLContextOptionsExtension
    {
        public ISchema? Schema { get; private set; }
        public Type? QueryClrType { get; private set; }
        public Type? MutationClrType { get; private set; }
        public IServiceProvider? ApplicationServiceProvider { get; private set; }
        public IServiceProvider? InternalServiceProvider { get; private set; }
        public bool RevealInternalServerErrors { get; private set; }
        public CoreOptionsExtension() { }

        protected CoreOptionsExtension(CoreOptionsExtension copyFrom)
        {
            Schema = copyFrom.Schema;
            QueryClrType = copyFrom.QueryClrType;
            MutationClrType = copyFrom.MutationClrType;
            ApplicationServiceProvider = copyFrom.ApplicationServiceProvider;
            InternalServiceProvider = copyFrom.InternalServiceProvider;
            RevealInternalServerErrors = copyFrom.RevealInternalServerErrors;
        }

        protected CoreOptionsExtension Clone() => new CoreOptionsExtension(this);

        public void ApplyServices(IServiceCollection services) { }
        public void Validate(IGraphQLContextOptions options) { }
        public CoreOptionsExtension WithSchema(ISchema schema)
        {
            var clone = Clone();
            clone.Schema = schema;
            return clone;
        }

        public CoreOptionsExtension WithQueryClrType(Type clrType)
        {
            var clone = Clone();
            clone.QueryClrType = clrType;
            return clone;
        }

        public CoreOptionsExtension WithMutationClrType(Type clrType)
        {
            var clone = Clone();
            clone.MutationClrType = clrType;
            return clone;
        }

        public CoreOptionsExtension WithRevealInternalServerErrors(bool value)
        {
            var clone = Clone();
            clone.RevealInternalServerErrors = value;
            return clone;
        }


        public CoreOptionsExtension WithApplicationServiceProvider(IServiceProvider serviceProvider)
        {
            var clone = Clone();
            clone.ApplicationServiceProvider = serviceProvider;
            return clone;
        }

        public CoreOptionsExtension WithInternalServiceProvider(IServiceProvider serviceProvider)
        {
            var clone = Clone();
            clone.InternalServiceProvider = serviceProvider;
            return clone;
        }
    }
}