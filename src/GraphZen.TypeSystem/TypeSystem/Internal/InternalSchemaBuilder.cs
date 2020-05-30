// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public class InternalSchemaBuilder : AnnotatableMemberDefinitionBuilder<SchemaDefinition>
    {
        private readonly Lazy<SchemaBuilder> _builder;

        public InternalSchemaBuilder(SchemaDefinition schema) : base(schema)
        {
            _builder = new Lazy<SchemaBuilder>(() => new SchemaBuilder(schema));
        }

        public SchemaBuilder Builder => _builder.Value;

        public IParser Parser { get; } = new SuperpowerParser();


        public NamedTypeDefinition? DefineType(TypeReference reference)
        {
            if (reference.Identity.Definition != null)
            {
                throw new InvalidOperationException("type is already defined");
            }

            if (reference.Identity.ClrType == null)
            {
                return Scalar(reference.Identity.Name, reference.DeclaringMember.GetConfigurationSource())?.Definition;
            }

            if (Schema.TryGetTypeKind(reference.Identity.ClrType, reference.Identity.IsInputType(),
                reference.Identity.IsOutputType(), out var kind, out _))
            {
                return Type(reference.Identity.ClrType, kind, ConfigurationSource.Convention)?.Definition as
                    NamedTypeDefinition;
            }

            return null;
        }

        public MemberDefinitionBuilder? Type(Type clrType, bool? isInputType, bool? isOutputType)
        {
            if (Schema.TryGetTypeKind(clrType, isInputType, isOutputType, out var kind, out _))
            {
                return Type(clrType, kind, ConfigurationSource.Convention);
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


        private MemberDefinitionBuilder? Type(Type clrType, TypeKind kind, ConfigurationSource configurationSource)
        {
            return kind switch
            {
                TypeKind.Scalar => Scalar(clrType, configurationSource),
                TypeKind.Object => Object(clrType, configurationSource),
                TypeKind.Interface => Interface(clrType, configurationSource),
                TypeKind.Union => Union(clrType, configurationSource),
                TypeKind.Enum => Enum(clrType, configurationSource),
                TypeKind.InputObject => InputObject(clrType, configurationSource),
                TypeKind.List => throw new InvalidOperationException("List and Non-Null types cannot be user-defined"),
                TypeKind.NonNull => throw new InvalidOperationException(
                    "List and Non-Null types cannot be user-defined"),
                _ => throw new InvalidOperationException("Unsupported type kind")
            };
        }


        private static string InvalidTypeAddition(TypeKind kind, TypeIdentity identity,
            NamedTypeDefinition existingType)
        {
            var clrType = identity.ClrType;
            return clrType != null && clrType == existingType.ClrType
                ? $"Cannot add {kind.ToDisplayStringLower()} using CLR type '{clrType}', an existing {existingType.Kind.ToDisplayStringLower()} already exists with that CLR type."
                : $"Cannot add {kind.ToDisplayStringLower()} named '{identity.Name}', an existing {existingType.Kind.ToDisplayStringLower()} already exists with that name.";
        }

        public InternalUnionTypeBuilder? Union(Type clrType, string name, ConfigurationSource configurationSource)
        {
            return CreateByNameOrType<UnionTypeDefinition, InternalUnionTypeBuilder>(clrType, name,
                () => Union(name, configurationSource)?.ClrType(clrType, name, configurationSource),
                () => Union(clrType, configurationSource)?.ClrType(clrType, name, configurationSource));
        }

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
                    unionType.Builder.ClrType(id.ClrType, false, ConfigurationSource.Explicit);
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

        public InternalScalarTypeBuilder? Scalar(Type clrType, string name, ConfigurationSource configurationSource) =>
            CreateByNameOrType<ScalarTypeDefinition, InternalScalarTypeBuilder>(clrType, name,
                () => Scalar(name, configurationSource)?.ClrType(clrType, name, configurationSource),
                () => Scalar(clrType, configurationSource)?.ClrType(clrType, name, configurationSource));

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
                    scalarType.Builder.ClrType(id.ClrType, false, configurationSource);
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
            ConfigurationSource configurationSource)
        {
            return CreateByNameOrType<InterfaceTypeDefinition, InternalInterfaceTypeBuilder>(clrType, name,
                () => Interface(name, configurationSource)?.ClrType(clrType, name, configurationSource),
                () => Interface(clrType, configurationSource)?.ClrType(clrType, name, configurationSource));
        }

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
                    interfaceType.Builder.ClrType(id.ClrType, false, configurationSource);
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

        public InternalEnumTypeBuilder? Enum(Type clrType, string name, ConfigurationSource configurationSource)
        {
            return CreateByNameOrType<EnumTypeDefinition, InternalEnumTypeBuilder>(clrType, name,
                () => Enum(name, configurationSource)?.ClrType(clrType, name, configurationSource),
                () => Enum(clrType, configurationSource)?.ClrType(clrType, name, configurationSource));
        }

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
                : Definition.FindFirstType(id.ClrType) ?? Definition.FindType(id.Name);

            if (type is EnumTypeDefinition enumType)
            {
                enumType.UpdateConfigurationSource(configurationSource);
                if (id.ClrType != null && id.ClrType != type.ClrType)
                {
                    enumType.Builder.ClrType(id.ClrType, false, ConfigurationSource.Explicit);
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
            CreateByNameOrType<InputObjectTypeDefinition, InternalInputObjectTypeBuilder>(clrType, name,
                () => InputObject(name, configurationSource)?.ClrType(clrType, name, configurationSource),
                () => InputObject(clrType, configurationSource)?.ClrType(clrType, name, configurationSource));

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
                    inputType.Builder.ClrType(id.ClrType, false, configurationSource);
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
                    TypeSystemExceptions.InvalidNameException.CannotGetOrCreateTypeBuilderWithInvalidName(name,
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
                        TypeSystemExceptions.InvalidNameException
                            .CannotGetOrCreateBuilderForClrTypeWithInvalidNameAttribute(clrType, annotated,
                                kind));
                }
            }
            else if (!clrType.Name.IsValidGraphQLName())
            {
                throw new InvalidNameException(
                    TypeSystemExceptions.InvalidNameException.CannotGetOrCreateBuilderForClrTypeWithInvalidName(
                        clrType, kind));
            }
        }

        public InternalObjectTypeBuilder? Object(Type clrType, string name, ConfigurationSource configurationSource)
        {
            return CreateByNameOrType<ObjectTypeDefinition, InternalObjectTypeBuilder>(clrType, name,
                () => Object(name, configurationSource)?.ClrType(clrType, name, configurationSource),
                () => Object(clrType, configurationSource)?.ClrType(clrType, name, configurationSource));
        }

        private TBuilder? CreateByNameOrType<T, TBuilder>(Type clrType, string name, Func<TBuilder?> createByName,
            Func<TBuilder?> createByType)
            where TBuilder : AnnotatableMemberDefinitionBuilder<T>
            where T : NamedTypeDefinition
        {
            var byName = Definition.FindType(name);
            var byType = Definition.FindType<T>(clrType);
            if (byType != null)
            {
                if (byName != null)
                {
                    if (!byName.Equals(byType))
                    {
                        throw new DuplicateItemException(
                            TypeSystemExceptions.DuplicateItemException.CannotCreateTypeWithDuplicateNameAndType(
                                byName.Kind, name,
                                clrType, byName, byType));
                    }

                    return createByName();
                }

                return createByType();
            }

            return createByName();
        }


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
                    objectType.Builder.ClrType(id.ClrType, false, configurationSource);
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

        public InternalDirectiveBuilder? Directive(Type clrType, string name, ConfigurationSource configurationSource)
        {
            if (IsDirectiveIgnored(clrType, configurationSource) || IsDirectiveIgnored(name, configurationSource))
            {
                return null;
            }

            var typed = Definition.FindDirective(clrType);
            var named = Definition.FindDirective(name);
            if (typed != null && named != null && !typed.Equals(named))
            {
                throw new DuplicateItemException(
                    TypeSystemExceptions.DuplicateItemException.CannotCreateDirectiveWithConflictingNameAndType(
                        name, clrType, named, typed));
            }

            if (typed != null)
            {
                var clrB = Directive(clrType, configurationSource);
                clrB?.SetName(name, configurationSource);
                return clrB;
            }

            var ib = Directive(name, configurationSource);
            ib?.ClrType(clrType, false, configurationSource);
            return ib;
        }

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
                    TypeSystemExceptions.InvalidNameException
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

            var directive = Definition.FindDirective(clrType) ??
                            Definition.FindDirective(clrType.GetGraphQLNameAnnotation());

            if (directive != null)
            {
                directive.UpdateConfigurationSource(configurationSource);
                if (directive.ClrType == null)
                {
                    directive.SetClrType(clrType, false, configurationSource);
                }

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

            if (objectType.Name == "Query")
            {
                Definition.SetQueryType(objectType, ConfigurationSource.Convention);
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
            IgnoreType(clrType.GetGraphQLNameAnnotation(), configurationSource);

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

        private bool IsDirectiveIgnored(Type clrType, ConfigurationSource configurationSource)
        {
            var name = clrType.TryGetGraphQLNameFromDataAnnotation(out var annotated) ? annotated : clrType.Name;
            return IsDirectiveIgnored(name, configurationSource);
        }

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
            var queryType = Object(name, configurationSource)?.Definition;
            if (queryType != null)
            {
                Definition.SetQueryType(queryType, configurationSource);
            }

            return this;
        }


        public InternalSchemaBuilder QueryType(Type clrType, ConfigurationSource configurationSource)
        {
            Check.NotNull(clrType, nameof(clrType));
            var queryType = Object(clrType, configurationSource)?.Definition;
            if (queryType != null)
            {
                Definition.SetQueryType(queryType, configurationSource);
            }

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


        public bool RemoveObject(Type clrType, ConfigurationSource configurationSource) =>
            RemoveType<ObjectTypeDefinition>(clrType, configurationSource);

        public bool RemoveObject(string name, ConfigurationSource configurationSource) =>
            RemoveType<ObjectTypeDefinition>(name, configurationSource);

        public bool RemoveUnion(Type clrType, ConfigurationSource configurationSource) =>
            RemoveType<UnionTypeDefinition>(clrType, configurationSource);

        public bool RemoveUnion(string name, ConfigurationSource configurationSource) =>
            RemoveType<UnionTypeDefinition>(name, configurationSource);

        public bool RemoveScalar(Type clrType, ConfigurationSource configurationSource) =>
            RemoveType<ScalarTypeDefinition>(clrType, configurationSource);

        public bool RemoveScalar(string name, ConfigurationSource configurationSource) =>
            RemoveType<ScalarTypeDefinition>(name, configurationSource);

        public bool RemoveEnum(Type clrType, ConfigurationSource configurationSource) =>
            RemoveType<EnumTypeDefinition>(clrType, configurationSource);

        public bool RemoveEnum(string name, ConfigurationSource configurationSource) =>
            RemoveType<EnumTypeDefinition>(name, configurationSource);

        public bool RemoveInterface(Type clrType, ConfigurationSource configurationSource) =>
            RemoveType<InterfaceTypeDefinition>(clrType, configurationSource);

        public bool RemoveInterface(string name, ConfigurationSource configurationSource) =>
            RemoveType<InterfaceTypeDefinition>(name, configurationSource);

        public bool RemoveInputObject(Type clrType, ConfigurationSource configurationSource) =>
            RemoveType<InputObjectTypeDefinition>(clrType, configurationSource);

        public bool RemoveInputObject(string name, ConfigurationSource configurationSource) =>
            RemoveType<InputObjectTypeDefinition>(name, configurationSource);

        private bool RemoveType<T>(Type clrType, ConfigurationSource configurationSource)
            where T : NamedTypeDefinition =>
            Definition.TryGetType<T>(clrType, out var type) && RemoveType(type, configurationSource);

        private bool RemoveType<T>(string name, ConfigurationSource configurationSource)
            where T : NamedTypeDefinition =>
            Definition.TryGetType<T>(name, out var type) && RemoveType(type, configurationSource);


        public InternalSchemaBuilder Description(string description, ConfigurationSource configurationSource)
        {
            Definition.SetDescription(description, configurationSource);
            return this;
        }
    }
}