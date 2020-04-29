#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

using GraphZen.TypeSystem.Internal;

// ReSharper disable InconsistentNaming

namespace GraphZen.TypeSystem
{
    public partial class SchemaBuilder
    {
        #region Directives


        public IDirectiveBuilder<object> Directive(string name)
        {
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Directive(name, ConfigurationSource.Explicit);
            var builder = new DirectiveBuilder<object>(internalBuilder);
            return builder;
        }


        public IDirectiveBuilder<TDirective> Directive<TDirective>() where TDirective : notnull
        {
            var internalBuilder = Builder.Directive(typeof(TDirective), ConfigurationSource.Explicit);
            var builder = new DirectiveBuilder<TDirective>(internalBuilder);
            return builder;
        }

        public IDirectiveBuilder<object> Directive(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            var internalBuilder = Builder.Directive(clrType, ConfigurationSource.Explicit);
            var builder = new DirectiveBuilder<object>(internalBuilder);
            return builder;
        }







        public ISchemaBuilder<GraphQLContext> UnignoreDirective<TDirective>() where TDirective : notnull
        {
            Builder.UnignoreDirective(typeof(TDirective), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> UnignoreDirective(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreDirective(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> UnignoreDirective(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreDirective(name, ConfigurationSource.Explicit);
            return this;
        }


        public ISchemaBuilder<GraphQLContext> IgnoreDirective<TDirective>() where TDirective : notnull
        {
            Builder.IgnoreDirective(typeof(TDirective), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> IgnoreDirective(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreDirective(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> IgnoreDirective(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreDirective(name, ConfigurationSource.Explicit);
            return this;
        }



        #endregion
        #region Types



        public ISchemaBuilder<GraphQLContext> UnignoreType<TClrType>() where TClrType : notnull
        {
            Builder.UnignoreType(typeof(TClrType), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> UnignoreType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreType(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> UnignoreType(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreType(name, ConfigurationSource.Explicit);
            return this;
        }


        public ISchemaBuilder<GraphQLContext> IgnoreType<TClrType>() where TClrType : notnull
        {
            Builder.IgnoreType(typeof(TClrType), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> IgnoreType(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreType(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> IgnoreType(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreType(name, ConfigurationSource.Explicit);
            return this;
        }



        #endregion
        #region Objects



        public ISchemaBuilder<GraphQLContext> UnignoreObject<TObject>() where TObject : notnull
        {
            Builder.UnignoreObject(typeof(TObject), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> UnignoreObject(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreObject(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> UnignoreObject(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreObject(name, ConfigurationSource.Explicit);
            return this;
        }


        public ISchemaBuilder<GraphQLContext> IgnoreObject<TObject>() where TObject : notnull
        {
            Builder.IgnoreObject(typeof(TObject), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> IgnoreObject(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreObject(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> IgnoreObject(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreObject(name, ConfigurationSource.Explicit);
            return this;
        }



        #endregion
        #region Unions



        //   IUnionTypeBuilder<object, GraphQLContext> Union(string name);


        //  IUnionTypeBuilder<TUnion, GraphQLContext> Union<TUnion>() where TUnion : notnull;


        //   IUnionTypeBuilder<object, GraphQLContext> Union(Type clrType); 









        public ISchemaBuilder<GraphQLContext> UnignoreUnion<TUnion>() where TUnion : notnull
        {
            Builder.UnignoreUnion(typeof(TUnion), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> UnignoreUnion(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreUnion(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> UnignoreUnion(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreUnion(name, ConfigurationSource.Explicit);
            return this;
        }


        public ISchemaBuilder<GraphQLContext> IgnoreUnion<TUnion>() where TUnion : notnull
        {
            Builder.IgnoreUnion(typeof(TUnion), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> IgnoreUnion(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreUnion(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> IgnoreUnion(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreUnion(name, ConfigurationSource.Explicit);
            return this;
        }



        #endregion
        #region Scalars



        public ISchemaBuilder<GraphQLContext> UnignoreScalar<TScalar>() where TScalar : notnull
        {
            Builder.UnignoreScalar(typeof(TScalar), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> UnignoreScalar(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreScalar(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> UnignoreScalar(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreScalar(name, ConfigurationSource.Explicit);
            return this;
        }


        public ISchemaBuilder<GraphQLContext> IgnoreScalar<TScalar>() where TScalar : notnull
        {
            Builder.IgnoreScalar(typeof(TScalar), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> IgnoreScalar(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreScalar(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> IgnoreScalar(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreScalar(name, ConfigurationSource.Explicit);
            return this;
        }



        #endregion
        #region Enums


        public IEnumTypeBuilder<string> Enum(string name)
        {
            Check.NotNull(name, nameof(name));
            var internalBuilder = Builder.Enum(name, ConfigurationSource.Explicit)!;
            var builder = new EnumTypeBuilder<string>(internalBuilder);
            return builder;
        }


        public IEnumTypeBuilder<TEnum> Enum<TEnum>() where TEnum : notnull
        {
            var internalBuilder = Builder.Enum(typeof(TEnum), ConfigurationSource.Explicit)!;
            var builder = new EnumTypeBuilder<TEnum>(internalBuilder);
            return builder;
        }

        public IEnumTypeBuilder<string> Enum(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            var internalBuilder = Builder.Enum(clrType, ConfigurationSource.Explicit)!;
            var builder = new EnumTypeBuilder<string>(internalBuilder);
            return builder;
        }







        public ISchemaBuilder<GraphQLContext> UnignoreEnum<TEnum>() where TEnum : notnull
        {
            Builder.UnignoreEnum(typeof(TEnum), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> UnignoreEnum(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreEnum(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> UnignoreEnum(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreEnum(name, ConfigurationSource.Explicit);
            return this;
        }


        public ISchemaBuilder<GraphQLContext> IgnoreEnum<TEnum>() where TEnum : notnull
        {
            Builder.IgnoreEnum(typeof(TEnum), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> IgnoreEnum(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreEnum(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> IgnoreEnum(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreEnum(name, ConfigurationSource.Explicit);
            return this;
        }



        #endregion
        #region Interfaces



        public ISchemaBuilder<GraphQLContext> UnignoreInterface<TInterface>() where TInterface : notnull
        {
            Builder.UnignoreInterface(typeof(TInterface), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> UnignoreInterface(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreInterface(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> UnignoreInterface(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreInterface(name, ConfigurationSource.Explicit);
            return this;
        }


        public ISchemaBuilder<GraphQLContext> IgnoreInterface<TInterface>() where TInterface : notnull
        {
            Builder.IgnoreInterface(typeof(TInterface), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> IgnoreInterface(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreInterface(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> IgnoreInterface(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreInterface(name, ConfigurationSource.Explicit);
            return this;
        }



        #endregion
        #region InputObjects



        public ISchemaBuilder<GraphQLContext> UnignoreInputObject<TInputObject>() where TInputObject : notnull
        {
            Builder.UnignoreInputObject(typeof(TInputObject), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> UnignoreInputObject(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.UnignoreInputObject(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> UnignoreInputObject(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.UnignoreInputObject(name, ConfigurationSource.Explicit);
            return this;
        }


        public ISchemaBuilder<GraphQLContext> IgnoreInputObject<TInputObject>() where TInputObject : notnull
        {
            Builder.IgnoreInputObject(typeof(TInputObject), ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> IgnoreInputObject(Type clrType)
        {
            Check.NotNull(clrType, nameof(clrType));
            Builder.IgnoreInputObject(clrType, ConfigurationSource.Explicit);
            return this;
        }

        public ISchemaBuilder<GraphQLContext> IgnoreInputObject(string name)
        {
            Check.NotNull(name, nameof(name));
            Builder.IgnoreInputObject(name, ConfigurationSource.Explicit);
            return this;
        }



        #endregion
    }
    public partial class SchemaBuilder<TContext>
    {
        // hello GraphZen.TypeSystem.SchemaBuilder`1[TContext] 
    }
}
