// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalSchemaBuilder : AnnotatableMemberDefinitionBuilder<SchemaDefinition>
    {
        public InternalSchemaBuilder(SchemaDefinition schemaDefinition)
            : base(schemaDefinition, schemaDefinition.Builder)
        {
        }

        public IParser Parser { get; } = new SuperpowerParser();

        public override InternalSchemaBuilder SchemaBuilder => this;


        public MemberDefinitionBuilder? Type(TypeIdentity identity)
        {
            if (identity.ClrType == null)
            {
                return null;
            }

            if (identity.Kind != null)
            {
                return Type(identity.ClrType, identity.Kind.Value);
            }

            return Type(identity.ClrType, identity.IsInputType, identity.IsOutputType);
        }

        public MemberDefinitionBuilder? Type(Type clrType, bool? isInputType, bool? isOutputType)
        {
            if (Schema.TryGetTypeKind(clrType, isInputType, isOutputType, out var kind, out _))
            {
                return Type(clrType, kind);
            }

            return null;
        }


        public NamedTypeDefinition? OutputType(Type clrType, ConfigurationSource configurationSource) =>
            OutputType(new TypeIdentity(clrType, Definition), configurationSource);

        public NamedTypeDefinition? OutputType(TypeIdentity id, ConfigurationSource configurationSource)
        {
            var clrType = id.ClrType;
            if (clrType == null)
            {
                return null;
            }

            if (IsTypeIgnored(id, configurationSource))
            {
                return null;
            }

            var def = Schema.FindOutputType(clrType);
            if (def == null)
            {
                if (clrType.IsEnum)
                {
                    return Enum(clrType, configurationSource)?.Definition;
                }

                if (clrType.IsValueType)
                {
                    return Scalar(clrType, configurationSource)?.Definition;
                }

                if (clrType.IsInterface)
                {
                    if (clrType.GetCustomAttribute<GraphQLUnionAttribute>() != null)
                    {
                        return Union(clrType, configurationSource.Max(ConfigurationSource.DataAnnotation))?.Definition;
                    }

                    return Interface(clrType, configurationSource)?.Definition;
                }

                if (clrType.GetCustomAttribute<GraphQLObjectAttribute>() != null)
                {
                    return Object(id, configurationSource)?.Definition;
                }

                if (clrType.IsClass && clrType.IsAbstract)
                {
                    return Union(clrType, configurationSource)?.Definition;
                }

                return Object(id, configurationSource)?.Definition;
            }

            return def;
        }

        public NamedTypeDefinition? InputType(Type clrType, ConfigurationSource configurationSource) =>
            InputType(new TypeIdentity(clrType, Definition), configurationSource);

        public NamedTypeDefinition? InputType(TypeIdentity id, ConfigurationSource configurationSource)
        {
            var clrType = id.ClrType;
            if (clrType == null)
            {
                return null;
            }

            if (IsTypeIgnored(id, configurationSource))
            {
                return null;
            }

            var def = Schema.FindInputType(clrType);
            if (def == null)
            {
                if (clrType.IsEnum)
                {
                    return Enum(clrType, configurationSource)?.Definition;
                }

                if (clrType.IsValueType)
                {
                    return Scalar(clrType, configurationSource)?.Definition;
                }

                if (clrType.IsClass)
                {
                    return InputObject(id, configurationSource)?.Definition;
                }
            }

            return def;
        }


        private MemberDefinitionBuilder? Type(Type clrType, TypeKind kind)
        {
            switch (kind)
            {
                case TypeKind.Scalar:
                    return Scalar(clrType, ConfigurationSource.Convention);
                case TypeKind.Object:
                    return Object(clrType, ConfigurationSource.Convention);
                case TypeKind.Interface:
                    return Interface(clrType, ConfigurationSource.Convention);
                case TypeKind.Union:
                    return Union(clrType, ConfigurationSource.Convention);
                case TypeKind.Enum:
                    return Enum(clrType, ConfigurationSource.Convention);
                case TypeKind.InputObject:
                    return InputObject(clrType, ConfigurationSource.Convention);
                case TypeKind.List:
                case TypeKind.NonNull:
                    throw new InvalidOperationException("List and Non-Null types cannot be user-defined");
                default:
                    throw new InvalidOperationException("Unsupported type kind");
            }
        }


        private static string InvalidTypeAddition(TypeKind kind, TypeIdentity identity,
            NamedTypeDefinition existingType)
        {
            var clrType = identity.ClrType;
            return clrType != null && clrType == existingType.ClrType
                ? $"Cannot add {kind.ToDisplayStringLower()} using CLR type '{clrType}', an existing {existingType.Kind.ToDisplayStringLower()} already exists with that CLR type."
                : $"Cannot add {kind.ToDisplayStringLower()} named '{identity.Name}', an existing {existingType.Kind.ToDisplayStringLower()} already exists with that name.";
        }

        public InternalUnionTypeBuilder? Union(Type clrType, string name, ConfigurationSource configurationSource) =>
            Union(name, configurationSource)?.ClrType(clrType, configurationSource);

        public InternalUnionTypeBuilder? Union(Type clrType, ConfigurationSource configurationSource)
        {
            AssertValidName(clrType, TypeKind.Union);
            return Union(new TypeIdentity(clrType, Definition), configurationSource);
        }

        public InternalUnionTypeBuilder? Union(string name, ConfigurationSource configurationSource)
        {
            AssertValidName(name, TypeKind.Union);
            return Union(new TypeIdentity(name, Definition), configurationSource);
        }

        private InternalUnionTypeBuilder? Union(in TypeIdentity id, ConfigurationSource configurationSource)
        {
            if (id.ClrType != null && id.ClrType.IsIgnoredByDataAnnotation())
            {
                IgnoreType(id.ClrType, ConfigurationSource.DataAnnotation);
            }

            if (IsTypeIgnored(id, configurationSource))
            {
                return null;
            }

            var type = id.ClrType == null
                ? Definition.FindType(id.Name)
                : Definition.FindOutputType(id.ClrType) ?? Definition.FindType(id.Name);

            if (type is UnionTypeDefinition unionType)
            {
                unionType.UpdateConfigurationSource(configurationSource);
                if (id.ClrType != null && id.ClrType != unionType.ClrType)
                {
                    unionType.Builder.ClrType(id.ClrType, ConfigurationSource.Explicit);
                }

                return unionType.Builder;
            }

            if (type is null)
            {
                Definition.UnignoreType(id.Name);
                unionType = id.ClrType != null
                    ? Definition.AddUnion(id.ClrType, configurationSource)
                    : Definition.AddUnion(id.Name, configurationSource);

                OnUnionAdded(unionType);
            }
            else
            {
                throw new InvalidOperationException(InvalidTypeAddition(TypeKind.Union, id, type));
            }

            return unionType.Builder;
        }

        private void OnUnionAdded(UnionTypeDefinition unionType)
        {
            var clrType = unionType.ClrType;
            if (clrType != null)
            {
                unionType.Builder.ConfigureFromClrType();
            }
        }


        public InternalScalarTypeBuilder? Scalar(Type clrType, ConfigurationSource configurationSource)
        {
            AssertValidName(clrType, TypeKind.Scalar);
            return Scalar(new TypeIdentity(clrType, Definition), configurationSource);
        }

        public InternalScalarTypeBuilder? Scalar(Type clrType, string name, ConfigurationSource configurationSource)
        {
            AssertValidName(name, TypeKind.Scalar);
            return Scalar(new TypeIdentity(name, Definition), configurationSource)
                ?.ClrType(clrType, configurationSource);
        }

        public InternalScalarTypeBuilder? Scalar(string name, ConfigurationSource configurationSource)
        {
            AssertValidName(name, TypeKind.Scalar);
            return Scalar(new TypeIdentity(name, Definition), configurationSource);
        }

        private InternalScalarTypeBuilder? Scalar(in TypeIdentity id, ConfigurationSource configurationSource)
        {
            if (id.ClrType != null && id.ClrType.IsIgnoredByDataAnnotation())
            {
                IgnoreType(id.ClrType, ConfigurationSource.DataAnnotation);
            }

            if (IsTypeIgnored(id, configurationSource))
            {
                return null;
            }

            var type = id.ClrType == null
                ? Definition.FindType(id.Name)
                : Definition.FindScalar(id.ClrType) ?? Definition.FindType(id.Name);

            if (type is ScalarTypeDefinition scalarType)
            {
                scalarType.UpdateConfigurationSource(configurationSource);
                if (id.ClrType != null && id.ClrType != type.ClrType)
                {
                    scalarType.Builder.ClrType(id.ClrType, configurationSource);
                }

                return scalarType.Builder;
            }

            if (type is null)
            {
                Definition.UnignoreType(id.Name);
                scalarType = id.ClrType != null
                    ? Definition.AddScalar(id.ClrType, configurationSource)
                    : Definition.AddScalar(id.Name, configurationSource);

                OnScalarAdded(scalarType);
            }
            else
            {
                throw new InvalidOperationException(InvalidTypeAddition(TypeKind.Scalar, id, type));
            }

            return scalarType.Builder;
        }

        private void OnScalarAdded(ScalarTypeDefinition scalarType)
        {
            var clrType = scalarType.ClrType;
            if (clrType != null)
            {
                scalarType.Builder.ConfigureFromClrType();
            }
        }


        public InternalInterfaceTypeBuilder?
            Interface(Type clrType, ConfigurationSource configurationSource)
        {
            AssertValidName(clrType, TypeKind.Interface);
            return Interface(new TypeIdentity(clrType, Definition), configurationSource);
        }

        public InternalInterfaceTypeBuilder? Interface(string name, ConfigurationSource configurationSource)
        {
            AssertValidName(name, TypeKind.Interface);
            return Interface(new TypeIdentity(name, Definition), configurationSource);
        }

        public InternalInterfaceTypeBuilder? Interface(Type clrType, string name,
            ConfigurationSource configurationSource) => throw new NotImplementedException();

        private InternalInterfaceTypeBuilder? Interface(in TypeIdentity id,
            ConfigurationSource configurationSource)
        {
            if (id.ClrType != null && id.ClrType.IsIgnoredByDataAnnotation())
            {
                IgnoreType(id.ClrType, ConfigurationSource.DataAnnotation);
            }

            if (IsTypeIgnored(id, configurationSource))
            {
                return null;
            }

            var type = id.ClrType == null
                ? Definition.FindType(id.Name)
                : Definition.FindOutputType(id.ClrType) ?? Definition.FindType(id.Name);

            if (type is InterfaceTypeDefinition interfaceType)
            {
                interfaceType.UpdateConfigurationSource(configurationSource);
                if (type.ClrType != id.ClrType && id.ClrType != null)
                {
                    interfaceType.Builder.ClrType(id.ClrType, configurationSource);
                }

                return interfaceType.Builder;
            }

            if (type is null)
            {
                Definition.UnignoreType(id.Name);


                interfaceType = id.ClrType != null
                    ? Definition.AddInterface(id.ClrType, configurationSource)
                    : Definition.AddInterface(id.Name, configurationSource);
                OnInterfaceAdded(interfaceType);
            }
            else
            {
                throw new InvalidOperationException(InvalidTypeAddition(TypeKind.Interface, id, type));
            }

            return interfaceType.Builder;
        }


        private void OnInterfaceAdded(InterfaceTypeDefinition interfaceType)
        {
            var clrType = interfaceType.ClrType;
            if (clrType != null)
            {
                interfaceType.Builder.ConfigureInterfaceFromClrType();
            }
        }

        public InternalEnumTypeBuilder? Enum(Type clrType, ConfigurationSource configurationSource)
        {
            AssertValidName(clrType, TypeKind.Enum);
            return Enum(new TypeIdentity(clrType, Definition), configurationSource);
        }

        public InternalEnumTypeBuilder? Enum(Type clrType, string name, ConfigurationSource configurationSource) =>
            throw new NotImplementedException();


        public InternalEnumTypeBuilder? Enum(string name, ConfigurationSource configurationSource)
        {
            AssertValidName(name, TypeKind.Enum);
            return Enum(new TypeIdentity(name, Definition), configurationSource);
        }


        private InternalEnumTypeBuilder? Enum(in TypeIdentity id, ConfigurationSource configurationSource)
        {
            if (id.ClrType != null && id.ClrType.IsIgnoredByDataAnnotation())
            {
                IgnoreType(id.ClrType, ConfigurationSource.DataAnnotation);
            }

            if (IsTypeIgnored(id, configurationSource))
            {
                return null;
            }

            var type = id.ClrType == null
                ? Definition.FindType(id.Name)
                : Definition.FindType(id.ClrType) ?? Definition.FindType(id.Name);

            if (type is EnumTypeDefinition enumType)
            {
                enumType.UpdateConfigurationSource(configurationSource);
                if (id.ClrType != null && id.ClrType != type.ClrType)
                {
                    enumType.Builder.ClrType(id.ClrType, ConfigurationSource.Explicit);
                }

                return enumType.Builder;
            }

            if (type is null)
            {
                Definition.UnignoreType(id.Name);
                enumType = id.ClrType != null
                    ? Definition.AddEnum(id.ClrType, configurationSource)
                    : Definition.AddEnum(id.Name, configurationSource);
                OnEnumAdded(enumType);
            }
            else
            {
                throw new InvalidOperationException(InvalidTypeAddition(TypeKind.Enum, id, type));
            }

            return enumType.Builder;
        }

        private void OnEnumAdded(EnumTypeDefinition enumType)
        {
            var clrType = enumType.ClrType;
            if (clrType != null)
            {
                enumType.Builder.ConfigureEnumFromClrType();
            }
        }


        public InternalInputObjectTypeBuilder? InputObject(Type clrType,
            ConfigurationSource configurationSource)
        {
            AssertValidName(clrType, TypeKind.InputObject);
            return InputObject(new TypeIdentity(clrType, Definition), configurationSource);
        }

        public InternalInputObjectTypeBuilder? InputObject(Type clrType, string name,
            ConfigurationSource configurationSource) =>
            throw new NotImplementedException();

        public InternalInputObjectTypeBuilder? InputObject(string name,
            ConfigurationSource configurationSource)
        {
            AssertValidName(name, TypeKind.InputObject);
            return InputObject(new TypeIdentity(name, Definition), configurationSource);
        }

        private InternalInputObjectTypeBuilder? InputObject(in TypeIdentity id,
            ConfigurationSource configurationSource)
        {
            if (id.ClrType != null && id.ClrType.IsIgnoredByDataAnnotation())
            {
                IgnoreType(id.ClrType, ConfigurationSource.DataAnnotation);
            }

            if (IsTypeIgnored(id, configurationSource))
            {
                return null;
            }

            if (IsTypeIgnored(id, configurationSource))
            {
                return null;
            }

            var type = id.ClrType == null
                ? Definition.FindType(id.Name)
                : Definition.FindInputType(id.ClrType) ?? Definition.FindType(id.Name);

            if (type is InputObjectTypeDefinition inputType)
            {
                inputType.UpdateConfigurationSource(configurationSource);
                if (id.ClrType != null && id.ClrType != type.ClrType)
                {
                    inputType.Builder.ClrType(id.ClrType, configurationSource);
                }

                return inputType.Builder;
            }

            if (type is null)
            {
                Definition.UnignoreType(id.Name);
                inputType = id.ClrType != null
                    ? Definition.AddInputObject(id.ClrType, configurationSource)
                    : Definition.AddInputObject(id.Name, configurationSource);

                OnInputObjectAdded(inputType);
            }
            else
            {
                throw new InvalidOperationException(InvalidTypeAddition(TypeKind.InputObject, id, type));
            }

            return inputType.Builder;
        }

        private void OnInputObjectAdded(InputObjectTypeDefinition inputType)
        {
            var clrType = inputType.ClrType;
            if (clrType != null)
            {
                inputType.Builder.ConfigureFromClrType();
            }
        }


        private static void AssertValidName(string name, TypeKind kind)
        {
            if (!name.IsValidGraphQLName())
            {
                throw new InvalidNameException(
                    TypeSystemExceptionMessages.InvalidNameException.CannotGetOrCreateTypeBuilderWithInvalidName(name,
                        kind));
            }
        }

        private static void AssertValidName(Type clrType, TypeKind kind)
        {
            if (clrType.TryGetGraphQLNameFromDataAnnotation(out var annotated))
            {
                if (!annotated.IsValidGraphQLName())
                {
                    throw new InvalidNameException(
                        TypeSystemExceptionMessages.InvalidNameException
                            .CannotGetOrCreateBuilderForClrTypeWithInvalidNameAttribute(clrType, annotated,
                                kind));
                }
            }
            else if (!clrType.Name.IsValidGraphQLName())
            {
                throw new InvalidNameException(
                    TypeSystemExceptionMessages.InvalidNameException.CannotGetOrCreateBuilderForClrTypeWithInvalidName(
                        clrType, kind));
            }
        }

        public InternalObjectTypeBuilder? Object(Type clrType, string name, ConfigurationSource configurationSource) =>
            throw new NotImplementedException();


        public InternalObjectTypeBuilder? Object(string name, ConfigurationSource configurationSource)
        {
            AssertValidName(name, TypeKind.Object);
            return Object(new TypeIdentity(name, Definition), configurationSource);
        }

        public InternalObjectTypeBuilder? Object(Type clrType, ConfigurationSource configurationSource)
        {
            AssertValidName(clrType, TypeKind.Object);
            return Object(new TypeIdentity(clrType, Definition), configurationSource);
        }

        private InternalObjectTypeBuilder? Object(in TypeIdentity id, ConfigurationSource configurationSource)
        {
            if (id.ClrType != null && id.ClrType.IsIgnoredByDataAnnotation())
            {
                IgnoreType(id.ClrType, ConfigurationSource.DataAnnotation);
            }


            if (IsTypeIgnored(id, configurationSource))
            {
                return null;
            }

            var type = id.ClrType == null
                ? Definition.FindType(id.Name)
                : Definition.FindOutputType(id.ClrType) ?? Definition.FindType(id.Name);

            if (type is ObjectTypeDefinition objectType)
            {
                objectType.UpdateConfigurationSource(configurationSource);
                if (objectType.ClrType != id.ClrType && id.ClrType != null)
                {
                    objectType.Builder.SetClrType(id.ClrType, configurationSource);
                }

                return objectType.Builder;
            }

            if (type is null)
            {
                Definition.UnignoreType(id.Name);
                objectType = id.ClrType != null
                    ? Definition.AddObject(id.ClrType, configurationSource)
                    : Definition.AddObject(id.Name, configurationSource);

                OnObjectAdded(objectType);
            }
            else
            {
                throw new InvalidOperationException(InvalidTypeAddition(TypeKind.Object, id, type));
            }

            return objectType.Builder;
        }

        public InternalDirectiveBuilder?
            Directive(Type clrType, string name, ConfigurationSource configurationSource) =>
            throw new NotImplementedException();

        public InternalDirectiveBuilder? Directive(string name, ConfigurationSource configurationSource)
        {
            if (IsDirectiveIgnored(name, configurationSource))
            {
                return null;
            }

            var directive = Definition.FindDirective(name);

            if (directive != null)
            {
                directive.UpdateConfigurationSource(configurationSource);
                return directive.Builder;
            }

            Definition.UnignoreDirective(name, configurationSource);
            directive = Definition.AddDirective(name, configurationSource);

            OnDirectiveAdded(directive);

            return directive.Builder;
        }

        public InternalDirectiveBuilder? Directive(Type clrType, ConfigurationSource configurationSource)
        {
            if (clrType.TryGetGraphQLNameFromDataAnnotation(out var annotatedName) &&
                !annotatedName.IsValidGraphQLName())
            {
                throw new InvalidNameException(
                    TypeSystemExceptionMessages.InvalidNameException
                        .CannotCreateDirectiveFromClrTypeWithInvalidNameAttribute(clrType, annotatedName));
            }

            if (clrType.IsIgnoredByDataAnnotation())
            {
                Definition.IgnoreDirective(clrType, ConfigurationSource.DataAnnotation);
            }

            if (IsDirectiveIgnored(clrType, configurationSource))
            {
                return null;
            }

            var directive = Definition.FindDirective(clrType);

            if (directive != null)
            {
                directive.UpdateConfigurationSource(configurationSource);
                return directive.Builder;
            }

            Definition.UnignoreDirective(clrType, configurationSource);
            directive = Definition.AddDirective(clrType, configurationSource);

            OnDirectiveAdded(directive);

            return directive.Builder;
        }

        // ReSharper disable once UnusedParameter.Local
        private void OnDirectiveAdded(DirectiveDefinition directive)
        {
        }

        private void OnObjectAdded(ObjectTypeDefinition objectType)
        {
            var clrType = objectType.ClrType;
            if (clrType != null)
            {
                objectType.Builder.ConfigureObjectFromClrType();
            }
        }

        public bool UnignoreType(string name, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = Definition.FindIgnoredTypeConfigurationSource(name);
            if (!configurationSource.Overrides(ignoredConfigurationSource))
            {
                return false;
            }

            Definition.UnignoreType(name);
            return true;
        }

        public bool UnignoreType(Type clrType, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = Definition.FindIgnoredTypeConfigurationSource(clrType);
            if (!configurationSource.Overrides(ignoredConfigurationSource))
            {
                return false;
            }

            Definition.UnignoreType(clrType);
            return true;
        }


        public bool IgnoreType(Type clrType, ConfigurationSource configurationSource) =>
            IgnoreType(clrType.GetGraphQLName(), configurationSource);

        public bool IgnoreType(string name, ConfigurationSource configurationSource)
        {
            var ignoredConfigurationSource = Definition.FindIgnoredTypeConfigurationSource(name);
            if (ignoredConfigurationSource.HasValue)
            {
                if (configurationSource.Overrides(ignoredConfigurationSource) &&
                    configurationSource != ignoredConfigurationSource)
                {
                    Definition.IgnoreType(name, configurationSource);
                }

                return true;
            }

            var type = Definition.FindType(name);
            if (type != null)
            {
                return IgnoreType(type, configurationSource);
            }

            Definition.IgnoreType(name, configurationSource);
            return true;
        }

        public bool IgnoreType(NamedTypeDefinition type, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(type.GetConfigurationSource()))
            {
                return false;
            }

            if (type.ClrType != null)
            {
                Definition.IgnoreType(type.ClrType, configurationSource);
            }
            else
            {
                Definition.IgnoreType(type.Name, configurationSource);
            }

            return RemoveType(type, configurationSource);
        }

        private bool IsDirectiveIgnored(Type clrType, ConfigurationSource configurationSource) =>
            IsDirectiveIgnored(clrType.GetGraphQLName(), configurationSource);

        private bool IsDirectiveIgnored(string name, ConfigurationSource configurationSource)
        {
            if (configurationSource == ConfigurationSource.Explicit)
            {
                return false;
            }

            var ignoredConfigurationSource = Definition.FindIgnoredDirectiveConfigurationSource(name);
            return !configurationSource.Overrides(ignoredConfigurationSource);
        }

        private bool IsTypeIgnored(in TypeIdentity identity, ConfigurationSource configurationSource)
        {
            if (configurationSource == ConfigurationSource.Explicit)
            {
                return false;
            }

            var ignoredConfigurationSource = Definition.FindIgnoredTypeConfigurationSource(identity.Name);
            return ignoredConfigurationSource.HasValue && ignoredConfigurationSource.Overrides(configurationSource);
        }


        public InternalSchemaBuilder QueryType(string name, ConfigurationSource configurationSource)
        {
            Check.NotNull(name, nameof(name));
            Definition.SetQueryType(Object(name, configurationSource)?.Definition, configurationSource);
            return this;
        }


        public InternalSchemaBuilder QueryType(Type clrtType, ConfigurationSource configurationSource)
        {
            Check.NotNull(clrtType, nameof(clrtType));
            Definition.SetQueryType(Object(clrtType, configurationSource)?.Definition, configurationSource);
            return this;
        }


        public InternalSchemaBuilder SubscriptionType(string type, ConfigurationSource configurationSource)
        {
            Check.NotNull(type, nameof(type));
            Definition.SetSubscriptionType(Object(type, configurationSource)?.Definition, configurationSource);
            return this;
        }


        public InternalSchemaBuilder MutationType(string name, ConfigurationSource configurationSource)
        {
            Definition.SetMutationType(Object(name, configurationSource)?.Definition, configurationSource);
            return this;
        }


        public InternalSchemaBuilder MutationType(Type clrType, ConfigurationSource configurationSource)
        {
            Definition.SetMutationType(Object(clrType, configurationSource)?.Definition, configurationSource);
            return this;
        }


        public bool RemoveType(Type clrType, ConfigurationSource configurationSource) =>
            throw new NotImplementedException();

        public bool RemoveType(string name, ConfigurationSource configurationSource) =>
            throw new NotImplementedException();


        public bool RemoveType(NamedTypeDefinition type, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(type.GetConfigurationSource()))
            {
                return false;
            }

            Schema.RemoveType(type);

            return true;
        }

        public bool RemoveDirective(DirectiveDefinition definition, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(definition.GetConfigurationSource()))
            {
                return false;
            }

            Definition.RemoveDirective(definition);

            return true;
        }

        public void IgnoreDirective(Type clrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void IgnoreDirective(string name, ConfigurationSource configurationSource)
        {
            Definition.IgnoreDirective(name, configurationSource);
        }

        public void UnignoreDirective(Type clrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void UnignoreDirective(string name, ConfigurationSource configurationSource)
        {
            Definition.UnignoreDirective(name, configurationSource);
        }

        public void UnignoreObject(Type clrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void UnignoreObject(string name, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void IgnoreObject(Type clrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void IgnoreObject(string name, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void UnignoreUnion(Type clrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void UnignoreUnion(string name, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void IgnoreUnion(Type clrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void IgnoreUnion(string name, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void UnignoreScalar(Type clrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void UnignoreScalar(string name, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void IgnoreScalar(string name, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void IgnoreScalar(Type clrTYpe, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void UnignoreEnum(Type clrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void UnignoreEnum(string name, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void IgnoreEnum(Type clrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void IgnoreEnum(string name, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void UnignoreInterface(Type clrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void UnignoreInterface(string name, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void IgnoreInterface(Type clrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void IgnoreInterface(string name, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void UnignoreInputObject(Type clrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void UnignoreInputObject(string name, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void IgnoreInputObject(Type clrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void IgnoreInputObject(string name, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public bool RemoveDirective(Type clrType, ConfigurationSource configurationSource) =>
            Definition.TryGetDirective(clrType, out var directive) && RemoveDirective(directive, configurationSource);

        public bool RemoveDirective(string name, ConfigurationSource configurationSource) =>
            Definition.TryGetDirective(name, out var directive) && RemoveDirective(directive, configurationSource);


        public void RemoveObject(Type clrType, ConfigurationSource configurationSource)
        {
            if (Definition.TryGetObject(clrType, out var ot))
            {
                RemoveType(ot, configurationSource);
            }
        }

        public bool RemoveObject(string name, ConfigurationSource configurationSource) =>
            Definition.TryGetObject(name, out var obje) && RemoveType(obje, configurationSource);

        public void RemoveUnion(Type clrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void RemoveUnion(string name, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void RemoveScalar(Type clrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void RemoveScalar(string name, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void RemoveEnum(Type clrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void RemoveEnum(string name, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void RemoveInterface(Type clrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void RemoveInterface(string name, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void RemoveInputObject(Type clrType, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }

        public void RemoveInputObject(string name, ConfigurationSource configurationSource)
        {
            throw new NotImplementedException();
        }
    }
}