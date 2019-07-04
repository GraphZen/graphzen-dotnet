// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GraphZen.Infrastructure;
using GraphZen.Infrastructure.Extensions;
using GraphZen.LanguageModel;
using GraphZen.LanguageModel.Internal;
using GraphZen.Utilities;


namespace GraphZen.TypeSystem.Internal
{
    public class InternalSchemaBuilder : AnnotatableMemberDefinitionBuilder<SchemaDefinition>
    {
        public InternalSchemaBuilder([NotNull] SchemaDefinition schemaDefinition)
            : base(schemaDefinition, schemaDefinition.Builder)
        {
        }

        [NotNull]
        public IParser Parser { get; } = new SuperpowerParser();

        public override InternalSchemaBuilder SchemaBuilder => this;


        private static IReadOnlyList<string> IgnoredMethodNames { get; } =
            // ReSharper disable once PossibleNullReferenceException
            typeof(object).GetMethods().Select(_ => _.Name).ToReadOnlyList();


        public MemberDefinitionBuilder Type([NotNull] TypeIdentity identity)
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

        public MemberDefinitionBuilder Type([NotNull] Type clrType, bool? isInputType, bool? isOutputType)
        {
            if (Schema.TryGetTypeKind(clrType, isInputType, isOutputType, out var kind, out _))
            {
                return Type(clrType, kind);
            }

            return null;
        }


        public NamedTypeDefinition OutputType(Type clrType, ConfigurationSource configurationSource) =>
            OutputType(new TypeIdentity(clrType, Definition), configurationSource);

        public NamedTypeDefinition OutputType([NotNull] TypeIdentity id, ConfigurationSource configurationSource)
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

        public NamedTypeDefinition InputType(Type clrType, ConfigurationSource configurationSource) =>
            InputType(new TypeIdentity(clrType, Definition), configurationSource);

        public NamedTypeDefinition InputType([NotNull] TypeIdentity id, ConfigurationSource configurationSource)
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


        private MemberDefinitionBuilder Type([NotNull] Type clrType, TypeKind kind)
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


        private static string DuplicateClrOutputTypeErrorMessage(string type, Type clrType,
            NamedTypeDefinition existingType) =>
            $"Cannot add {type} using CLR type '{clrType}', existing type {existingType} already exists with that CLR type.";


        public InternalUnionTypeBuilder Union([NotNull] Type clrType, ConfigurationSource configurationSource) =>
            Union(new TypeIdentity(clrType, Definition), configurationSource);

        public InternalUnionTypeBuilder Union([NotNull] string name, ConfigurationSource configurationSource) =>
            Union(new TypeIdentity(name, Definition), configurationSource);

        private InternalUnionTypeBuilder Union([NotNull] in TypeIdentity id, ConfigurationSource configurationSource)
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
                : Definition.FindOutputType(id.ClrType);

            if (type is UnionTypeDefinition unionType)
            {
                unionType.UpdateConfigurationSource(configurationSource);
                return unionType.Builder;
            }

            if (type is null)
            {
                Definition.UnignoreType(id.Name);
                unionType = id.ClrType != null
                    ? Definition.AddUnion(id.ClrType, configurationSource)
                    : Definition.AddUnion(id.Name, configurationSource);
                if (unionType != null)
                {
                    OnUnionAdded(unionType);
                }
            }
            else
            {
                throw new InvalidOperationException(
                    DuplicateClrOutputTypeErrorMessage("union", id.ClrType, type));
            }

            return unionType?.Builder;
        }

        private void OnUnionAdded([NotNull] UnionTypeDefinition unionType)
        {
            var clrType = unionType.ClrType;
            if (clrType != null)
            {
                if (clrType.TryGetDescriptionFromDataAnnotation(out var description))
                {
                    unionType.Builder.Description(description, ConfigurationSource.DataAnnotation);
                }


                var implementingTypes = clrType.GetImplementingTypes();
                foreach (var implementingType in implementingTypes)
                {
                    var type = OutputType(implementingType, ConfigurationSource.Convention);
                    if (type is ObjectTypeDefinition)
                    {
                        var memberRef = Definition.NamedTypeReference(implementingType, TypeKind.Object);
                        unionType.AddType(memberRef);
                    }
                }
            }
        }


        public InternalScalarTypeBuilder Scalar([NotNull] Type clrType, ConfigurationSource configurationSource) =>
            Scalar(new TypeIdentity(clrType, Definition), configurationSource);

        public InternalScalarTypeBuilder Scalar([NotNull] string name, ConfigurationSource configurationSource) =>
            Scalar(new TypeIdentity(name, Definition), configurationSource);

        private InternalScalarTypeBuilder Scalar([NotNull] in TypeIdentity id, ConfigurationSource configurationSource)
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
                : Definition.FindTypes(id.ClrType).FirstOrDefault();

            if (type is ScalarTypeDefinition scalarType)
            {
                scalarType.UpdateConfigurationSource(configurationSource);
                return scalarType.Builder;
            }

            if (type is null)
            {
                Definition.UnignoreType(id.Name);
                scalarType = id.ClrType != null
                    ? Definition.AddScalar(id.ClrType, configurationSource)
                    : Definition.AddScalar(id.Name, configurationSource);
                if (scalarType != null)
                {
                    OnScalarAdded(scalarType);
                }
            }
            else
            {
                throw new InvalidOperationException(
                    DuplicateClrOutputTypeErrorMessage("scalar", id.ClrType, type));
            }

            return scalarType?.Builder;
        }

        private void OnScalarAdded([NotNull] ScalarTypeDefinition scalarType)
        {
            var clrType = scalarType.ClrType;
            if (clrType != null)
            {
                if (clrType.TryGetDescriptionFromDataAnnotation(out var description))
                {
                    scalarType.Builder.Description(description, ConfigurationSource.DataAnnotation);
                }
            }
        }


        public InternalInterfaceTypeBuilder
            Interface([NotNull] Type clrType, ConfigurationSource configurationSource) =>
            Interface(new TypeIdentity(clrType, Definition), configurationSource);

        public InternalInterfaceTypeBuilder Interface([NotNull] string name, ConfigurationSource configurationSource) =>
            Interface(new TypeIdentity(name, Definition), configurationSource);

        private InternalInterfaceTypeBuilder Interface([NotNull] in TypeIdentity id,
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
                : Definition.FindOutputType(id.ClrType);

            if (type is InterfaceTypeDefinition interfaceType)
            {
                interfaceType.UpdateConfigurationSource(configurationSource);
                return interfaceType.Builder;
            }

            if (type is null)
            {
                Definition.UnignoreType(id.Name);
                interfaceType = id.ClrType != null
                    ? Definition.AddInterface(id.ClrType, configurationSource)
                    : Definition.AddInterface(id.Name, configurationSource);
                if (interfaceType != null)
                {
                    OnInterfaceAdded(interfaceType);
                }
            }
            else
            {
                throw new InvalidOperationException(
                    DuplicateClrOutputTypeErrorMessage("Interface", id.ClrType, type));
            }

            return interfaceType?.Builder;
        }


        private void OnInterfaceAdded([NotNull] InterfaceTypeDefinition interfaceType)
        {
            var clrType = interfaceType.ClrType;
            if (clrType != null)
            {
                ConfigureOutputFields(interfaceType.Builder);
                if (clrType.TryGetDescriptionFromDataAnnotation(out var desc))
                {
                    interfaceType.SetDescription(desc, ConfigurationSource.DataAnnotation);
                }
            }
        }

        public InternalEnumTypeBuilder Enum([NotNull] Type clrType, ConfigurationSource configurationSource) =>
            Enum(new TypeIdentity(clrType, Definition), configurationSource);

        public InternalEnumTypeBuilder Enum([NotNull] string name, ConfigurationSource configurationSource) =>
            Enum(new TypeIdentity(name, Definition), configurationSource);

        private InternalEnumTypeBuilder Enum([NotNull] in TypeIdentity id, ConfigurationSource configurationSource)
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
                : Definition.FindTypes(id.ClrType).FirstOrDefault();

            if (type is EnumTypeDefinition enumType)
            {
                enumType.UpdateConfigurationSource(configurationSource);
                return enumType.Builder;
            }

            if (type is null)
            {
                Definition.UnignoreType(id.Name);
                enumType = id.ClrType != null
                    ? Definition.AddEnum(id.ClrType, configurationSource)
                    : Definition.AddEnum(id.Name, configurationSource);
                if (enumType != null)
                {
                    OnEnumAdded(enumType);
                }
            }
            else
            {
                throw new InvalidOperationException(
                    DuplicateClrOutputTypeErrorMessage("enum", id.ClrType, type));
            }

            return enumType?.Builder;
        }

        private void OnEnumAdded([NotNull] EnumTypeDefinition enumType)
        {
            var clrType = enumType.ClrType;
            if (clrType != null)
            {
                if (clrType.TryGetDescriptionFromDataAnnotation(out var desc))
                {
                    enumType.SetDescription(desc, ConfigurationSource.DataAnnotation);
                }

                foreach (var value in System.Enum.GetValues(clrType))
                {
                    var member = clrType.GetMember(value.ToString());
                    if (member.Length > 0)
                    {
                        var memberInfo = clrType.GetMember(value.ToString())[0] ??
                                         // ReSharper disable once ConstantNullCoalescingCondition
                                         throw new InvalidOperationException(
                                             $"Unable to get MemberInfo for enum value of type {enumType}");
                        var (name, nameConfigurationSource) = memberInfo.GetGraphQLNameForEnumValue();
                        var valueBuilder = enumType.Builder
                            .Value(name, nameConfigurationSource, ConfigurationSource.Convention).CustomValue(value);
                        if (memberInfo.TryGetDescriptionFromDataAnnotation(out var description))
                        {
                            valueBuilder.Description(description, ConfigurationSource.DataAnnotation);
                        }
                    }
                }
            }
        }


        public InternalInputObjectTypeBuilder InputObject([NotNull] Type clrType,
            ConfigurationSource configurationSource) =>
            InputObject(new TypeIdentity(clrType, Definition), configurationSource);

        public InternalInputObjectTypeBuilder InputObject([NotNull] string name,
            ConfigurationSource configurationSource) =>
            InputObject(new TypeIdentity(name, Definition), configurationSource);

        private InternalInputObjectTypeBuilder InputObject([NotNull] in TypeIdentity id,
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
                : Definition.FindInputType(id.ClrType);

            if (type is InputObjectTypeDefinition inputType)
            {
                inputType.UpdateConfigurationSource(configurationSource);
                return inputType.Builder;
            }

            if (type is null)
            {
                Definition.UnignoreType(id.Name);
                inputType = id.ClrType != null
                    ? Definition.AddInputObject(id.ClrType, configurationSource)
                    : Definition.AddInputObject(id.Name, configurationSource);
                if (inputType != null)
                {
                    OnInputObjectAdded(inputType);
                }
            }
            else
            {
                throw new InvalidOperationException(
                    DuplicateClrOutputTypeErrorMessage("InputObject", id.ClrType, type));
            }

            return inputType?.Builder;
        }

        private void OnInputObjectAdded([NotNull] InputObjectTypeDefinition inputType)
        {
            var clrType = inputType.ClrType;
            if (clrType != null)
            {
                if (clrType.TryGetDescriptionFromDataAnnotation(out var description))
                {
                    inputType.Builder.Description(description, ConfigurationSource.DataAnnotation);
                }

                var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
                // ReSharper disable once PossibleNullReferenceException
                // ReSharper disable once AssignNullToNotNullAttribute
                var fieldMembers = clrType.GetMembers(flags)
                    .OfType<PropertyInfo>()
                    .OrderBy(_ => _.MetadataToken);
                foreach (var property in fieldMembers)
                {
                    inputType.Builder.Field(property, ConfigurationSource.Convention);
                }
            }
        }

        public InternalObjectTypeBuilder Object([NotNull] Type clrType, ConfigurationSource configurationSource) =>
            Object(new TypeIdentity(clrType, Definition), configurationSource);

        public InternalObjectTypeBuilder Object([NotNull] string name, ConfigurationSource configurationSource) =>
            Object(new TypeIdentity(name, Definition), configurationSource);

        private InternalObjectTypeBuilder Object([NotNull] in TypeIdentity id, ConfigurationSource configurationSource)
        {
            if (id.ClrType != null && id.ClrType.IsIgnoredByDataAnnotation())
            {
                IgnoreType(id.ClrType, ConfigurationSource.DataAnnotation);
            }


            if (IsTypeIgnored(id, configurationSource))
            {
                return null;
            }

            var outputType = id.ClrType == null
                ? Definition.FindType(id.Name)
                : Definition.FindOutputType(id.ClrType);

            if (outputType is ObjectTypeDefinition objectType)
            {
                objectType.UpdateConfigurationSource(configurationSource);
                return objectType.Builder;
            }

            if (outputType is null)
            {
                Definition.UnignoreType(id.Name);
                objectType = id.ClrType != null
                    ? Definition.AddObject(id.ClrType, configurationSource)
                    : Definition.AddObject(id.Name, configurationSource);
                if (objectType != null)
                {
                    OnObjectAdded(objectType);
                }
            }
            else
            {
                throw new InvalidOperationException(
                    DuplicateClrOutputTypeErrorMessage("Object", id.ClrType, outputType));
            }

            return objectType?.Builder;
        }


        private void OnObjectAdded([NotNull] ObjectTypeDefinition objectType)
        {
            var clrType = objectType.ClrType;
            if (clrType != null)
            {
                ConfigureOutputFields(objectType.Builder);
                if (clrType.TryGetDescriptionFromDataAnnotation(out var desc))
                {
                    objectType.SetDescription(desc, ConfigurationSource.DataAnnotation);
                }

                var interfaces = clrType.GetInterfaces().Where(_ => _.NotIgnored())
                    // ReSharper disable once PossibleNullReferenceException
                    .Where(_ => !_.IsGenericType)
                    .OrderBy(_ => _.MetadataToken);
                foreach (var @interface in interfaces)
                {
                    if (@interface.GetCustomAttribute<GraphQLUnionAttribute>() != null)
                    {
                        Schema.Builder.Union(@interface, ConfigurationSource.DataAnnotation);
                    }
                    else
                    {
                        objectType.Builder.Interface(@interface, ConfigurationSource.Convention);
                    }
                }
            }
        }

        private void ConfigureOutputFields<T, TB>([NotNull] InternalFieldsContainerBuilder<T, TB> fields)
            where T : FieldsContainerDefinition
            where TB : InternalFieldsContainerBuilder<T, TB>
        {
            var def = fields.Definition;
            Check.NotNull(def, nameof(def));
            var flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
            // ReSharper disable once PossibleNullReferenceException
            // ReSharper disable once AssignNullToNotNullAttribute
            var fieldMembers = def.ClrType.GetMembers(flags)
                .Where(_ => !(_ is MethodInfo method) || method.DeclaringType != typeof(object) &&
                            method.ReturnType != typeof(void) &&
                            !IgnoredMethodNames.Contains(method.Name) && !method.IsSpecialName)
                .OrderBy(_ => _.MetadataToken);
            foreach (var fieldMember in fieldMembers)
            {
                switch (fieldMember)
                {
                    case MethodInfo method:
                    {
                        fields.Field(method, ConfigurationSource.Convention);
                    }
                        break;
                    case PropertyInfo property:
                    {
                        fields.Field(property, ConfigurationSource.Convention);
                    }
                        break;
                }
            }
        }


        public bool IgnoreType([NotNull] Type clrType, ConfigurationSource configurationSource) =>
            IgnoreType(clrType.GetGraphQLName(), configurationSource);

        public bool IgnoreType([NotNull] string name, ConfigurationSource configurationSource)
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

        public bool IgnoreType([NotNull] NamedTypeDefinition type, ConfigurationSource configurationSource)
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


        private bool IsTypeIgnored([NotNull] in TypeIdentity identity, ConfigurationSource configurationSource)
        {
            if (configurationSource == ConfigurationSource.Explicit)
            {
                return false;
            }

            var ignoredConfigurationSource = Definition.FindIgnoredTypeConfigurationSource(identity.Name);
            return ignoredConfigurationSource.HasValue && ignoredConfigurationSource.Overrides(configurationSource);
        }


        [NotNull]
        public InternalSchemaBuilder QueryType([NotNull] string type, ConfigurationSource configurationSource)
        {
            Check.NotNull(type, nameof(type));
            Definition.QueryType = Object(type, configurationSource)?.Definition;
            return this;
        }

        [NotNull]
        public InternalSchemaBuilder QueryType([NotNull] Type clrtType, ConfigurationSource configurationSource)
        {
            Check.NotNull(clrtType, nameof(clrtType));
            Definition.QueryType = Object(clrtType, configurationSource)?.Definition;
            return this;
        }

        [NotNull]
        public InternalSchemaBuilder SubscriptionType([NotNull] string type, ConfigurationSource configurationSource)
        {
            Check.NotNull(type, nameof(type));
            Definition.SubscriptionType = Object(type, configurationSource)?.Definition;
            return this;
        }


        [NotNull]
        public InternalSchemaBuilder MutationType([NotNull] string type, ConfigurationSource configurationSource)
        {
            Definition.MutationType = Object(type, configurationSource)?.Definition;
            return this;
        }

        [NotNull]
        public InternalSchemaBuilder MutationType([NotNull] Type clrType, ConfigurationSource configurationSource)
        {
            Definition.MutationType = Object(clrType, configurationSource)?.Definition;
            return this;
        }

        [NotNull]
        public InternalDirectiveBuilder Directive([NotNull] string name, ConfigurationSource configurationSource) =>
            Definition.GetOrAddDirective(name, configurationSource).GetInfrastructure();


        public bool RemoveType([NotNull] NamedTypeDefinition type, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(type.GetConfigurationSource()))
            {
                return false;
            }

            Schema.RemoveType(type);

            return true;
        }
    }
}