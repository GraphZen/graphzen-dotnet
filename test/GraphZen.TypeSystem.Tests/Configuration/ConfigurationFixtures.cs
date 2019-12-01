// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Tests.Configuration.Directives;
using GraphZen.TypeSystem.Tests.Configuration.Enums;
using GraphZen.TypeSystem.Tests.Configuration.Enums.Description;
using GraphZen.TypeSystem.Tests.Configuration.Enums.EnumValues;
using GraphZen.TypeSystem.Tests.Configuration.Enums.EnumValues.Description;
using GraphZen.TypeSystem.Tests.Configuration.Infrastructure;
using GraphZen.TypeSystem.Tests.Configuration.InputObjects;
using GraphZen.TypeSystem.Tests.Configuration.InputObjects.Description;
using GraphZen.TypeSystem.Tests.Configuration.InputObjects.Fields;
using GraphZen.TypeSystem.Tests.Configuration.InputObjects.Fields.Description;
using GraphZen.TypeSystem.Tests.Configuration.Interfaces;
using GraphZen.TypeSystem.Tests.Configuration.Interfaces.Description;
using GraphZen.TypeSystem.Tests.Configuration.Interfaces.Fields;
using GraphZen.TypeSystem.Tests.Configuration.Interfaces.Fields.Arguments;
using GraphZen.TypeSystem.Tests.Configuration.Interfaces.Fields.Arguments.Description;
using GraphZen.TypeSystem.Tests.Configuration.Interfaces.Fields.Description;
using GraphZen.TypeSystem.Tests.Configuration.Objects;
using GraphZen.TypeSystem.Tests.Configuration.Objects.Description;
using GraphZen.TypeSystem.Tests.Configuration.Objects.Fields;
using GraphZen.TypeSystem.Tests.Configuration.Objects.Fields.Arguments;
using GraphZen.TypeSystem.Tests.Configuration.Objects.Fields.Arguments.Description;
using GraphZen.TypeSystem.Tests.Configuration.Objects.Fields.Description;
using GraphZen.TypeSystem.Tests.Configuration.Objects.Interfaces;
using GraphZen.TypeSystem.Tests.Configuration.Scalars;
using GraphZen.TypeSystem.Tests.Configuration.Scalars.Description;
using GraphZen.TypeSystem.Tests.Configuration.Unions;
using GraphZen.TypeSystem.Tests.Configuration.Unions.Description;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration
{
    public static class ConfigurationFixtures
    {
        public static IEnumerable<T> GetAll<T>() where T : IConfigurationFixture =>
            new List<IConfigurationFixture>
            {
                // SCHEMA

                // Directives (Collection)
                new Schema_Directives_Explicit(),
                // new Schema_Directives_ViaObjectClrPropertyAttribute(), 


                // Objects (Collection)
                new Schema_Objects_Explicit(),
                new Schema_Objects_ViaClrClasses(),
                // Interfaces (Collection)
                new Schema_Interfaces_Explicit(),
                new Schema_Interfaces_ViaClrInterfaces(),
                // Unions (Collection)
                new Schema_Unions_Explicit(),
                new Schema_Unions_ViaClassWithMarkerInterface(),
                new Schema_Unions_ViaClrBaseClass(),
                new Schema_Unions_ViaClrChildClass(),
                new Schema_Unions_ViaMarkerInterface(),

                // Enums (Collection)
                new Schema_Enums_Explicit(),
                new Schema_Enums_ViaClrEnums(),
                // Scalars (Collection)
                new Schema_Scalars_Explicit(),
                // Input Objects (Collection)
                new Schema_InputObjects_Explicit(),
                new Schema_InputObjects_ViaClrClasses(),
                // Directives (Collection)
                // TODO: Schema_Directives_Explicit
                // TODO: Schema_Directives_ViaClrAttributes
                // Query Type (Leaf)
                // TODO: Schema_QueryType_Explicit
                // TODO: Schema_QueryType_ViaClassName
                // Mutation Type (Leaf)
                // TODO: Schema_MutationType_Explicit
                // TODO: Schema_MutationType_ViaClassName
                // Description (Leaf)
                // TODO: Schema_Description_Explicit
                // TODO: Schema_Description_ViaGraphQLContextAttribute

                // OBJECT
                // Description (Leaf)
                new Object_Explicit_Description(),
                new Object_ViaClrClass_Description(),
                // Fields (Collection)
                new Object_Fields_Explicit(),
                new Object_Fields_ViaClrProperties(),
                new Object_Fields_ViaClrMethods(),
                // Interfaces (Collection)
                new Object_Interfaces_Explicit(),
                new Object_Interfaces_ViaClrInterfaces(),
                // Directive Annotations (Collection)
                // TODO: Object_DirectiveAnnotations_Explicit
                // TODO: Object_DirectiveAnnotations_ViaClrClassAttributes
                // TODO: Object_DirectiveAnnotations_ViaClrInterfaceAttributes

                // OBJECT FIELD
                // Description (Leaf)
                new Object_Field_Explicit_Description(),
                new Object_Field_ViaClrMethod_Description(),
                new Object_Field_ViaClrProperty_Description(),
                // Type
                // TODO: Object_Field_Type_Explicit
                // TODO: Object_Field_Type_ViaClrMethod
                // TODO: Object_Field_Type_ViaClrProperty
                // Arguments (Collection)
                new Object_Field_Arguments_Explicit(),
                new Object_Field_Arguments_ViaClrMethodParameters(),
                // TODO: Object_Field_Arguments_Explicit
                // TODO: Object_Field_Arguments_ViaClrClassMethodArguments
                // TODO: Object_Field_Arguments_ViaClrInterfaceMethodArguments
                // Directive Annotations (Collection)
                // TODO: Object_Field_DirectiveAnnotations_Explicit
                // TODO: Object_Field_DirectiveAnnotations_ViaClrPropertyAttributes
                // TODO: Object_Field_DirectiveAnnotations_ViaClrMethodAttributes

                // OBJECT FIELD ARGUMENT
                new Object_Field_Argument_ViaClrMethod_Description(),
                new Object_Field_Argument_Explicit_Description(),

                // INTERFACE
                // Description (Leaf)

                new Interface_ViaClrClass_Description(),
                new Interface_Explicit_Description(),

                // Fields (Collection)
                // TODO: Interface_Fields_Explicit
                new Interface_Fields_Explicit(),
                new Interface_Fields_ViaClrProperties(),
                new Interface_Fields_ViaClrMethods(),
                // Directive Annotations (Collection)
                // TODO: Interface_DirectiveAnnotations_Explicit
                // TODO: Interface_DirectiveAnnotations_ViaClrClassAttributes
                // TODO: Interface_DirectiveAnnotations_ViaClrInterfaceAttributes

                // INTERFACE FIELD
                // Description (Leaf)
                new Interface_Field_Explicit_Description(),
                new Interface_Field_ViaClrProperty_Description(),
                // Arguments (Collection)
                new Interface_Field_Arguments_Explicit(),
                new Interface_Field_Arguments_ViaClrMethodParameters(),

                // Type
                // TODO: Interface_Field_Type_Explicit
                // TODO: Interface_Field_Type_ViaClrMethod
                // TODO: Interface_Field_Type_ViaClrProperty
                // TODO: Interface_Field_Arguments_Explicit
                // TODO: Interface_Field_Arguments_ViaClrClassMethodArguments
                // TODO: Interface_Field_Arguments_ViaClrInterfaceMethodArguments
                // Directive Annotations (Collection)
                // TODO: Interface_Field_DirectiveAnnotations_Explicit
                // TODO: Interface_Field_DirectiveAnnotations_ViaClrPropertyAttributes
                // TODO: Interface_Field_DirectiveAnnotations_ViaClrMethodAttributes

                // INTERFACE FIELD ARGUMENT
                // Description (Leaf)
                new Interface_Field_Argument_Explicit_Description(),
                new Interface_Field_Argument_ViaClrMethod_Description(),


                // Union
                // Description (Leaf)
                new Union_Explicit_Description(),
                new Union_ViaClrClass_Description(),
                new Union_ViaClrMarkerInterface_Description(),
                // MemberTypes (Collection)
                // TODO: Union_MemberTypes_Explicit
                // TODO: Union_MemberTypes_ViaMarkerInterfaces
                // TODO: Union_MemberTypes_ViaChildClasses
                // Directive Annotations (Collection)
                // TODO: Union_DirectiveAnnotations_Explicit
                // TODO: Union_DirectiveAnnotations_ViaClrBaseClassAttributes
                // TODO: Union_DirectiveAnnotations_ViaClrMarkerInterfaceAttributes

                // INPUT OBJECT
                // Description (Leaf)
                new InputObject_Explicit_Description(),
                new InputObject_ViaClrClass_Description(),
                // Fields (Collection)
                new InputObject_Fields_Explicit(),
                new InputObject_Fields_ViaClrProperties(),
                // Directive Annotations (Collection)
                // TODO: InputObject_DirectiveAnnotations_Explicit
                // TODO: InputObject_DirectiveAnnotations_ViaClrClassAttributes
                // TODO: InputObject_DirectiveAnnotations_ViaClrInterfaceAttributes

                // INPUT OBJECT FIELD
                new InputObject_Field_Explicit_Description(),
                new InputObject_Field_ViaClrProperty_Description(),


                // Scalar
                new Scalar_ViaClrClass_Description(),
                new Scalar_Explicit_Description(),

                // ENUM
                // Description (Leaf)
                new Enum_Explicit_Description(),
                new Enum_ViaClrEnum_Description(),
                // Values (Collection)
                new Enum_Values_Explicit(),
                new Enum_Values_ViaClrEnumValues(),
                // Directive Annotations (Collection)
                // TODO: InputObject_DirectiveAnnotations_Explicit
                // TODO: InputObject_DirectiveAnnotations_ViaClrClassAttributes
                // TODO: InputObject_DirectiveAnnotations_ViaClrInterfaceAttributes

                // ENUM VALUE
                new EnumValue_Explicit_Description(),
                new EnumValue_ViaClrEnumValue_Description()
            }.OfType<T>();
    }
}