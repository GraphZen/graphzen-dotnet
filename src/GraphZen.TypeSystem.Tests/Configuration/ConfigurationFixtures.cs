// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Interfaces.Fields;
using GraphZen.Objects;
using GraphZen.Objects.Fields;
using GraphZen.Objects.Fields.Arguments;

namespace GraphZen
{
    public static class ConfigurationFixtures
    {
        [NotNull]
        [ItemNotNull]
        public static IEnumerable<T> GetAll<T>() where T : IConfigurationFixture => new List<IConfigurationFixture>
        {
            // SCHEMA
            // Objects (Collection)
            new Schema_Objects_Explicit(),
            new Schema_Objects_ViaClrClasses(),
            // Interfaces (Collection)
            // TODO: Schema_Interfaces_Explicit
            // TODO: Schema_Interfaces_ViaClrInterfaces
            // Unions (Collection)
            // TODO: Schema_Unions_Explicit
            // TODO: Schema_Unions_ViaMarkerInterface
            // TODO: Schema_Unions_ViaBaseClass
            // Enums (Collection)
            // TODO: Schema_Enums_Explicit
            // TODO: Schema_Enums_ViaClrEnums
            // Scalars (Collection)
            // TODO: Schema_Scalars_Explicit
            // Input Objects (Collection)
            // TODO: Schema_InputObjects_Explicit
            // TODO: Schema_InputObjects_ViaClrClasses
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
            // TODO: Object_Explicit_Description
            // TODO: Object_ViaClrClass_Description
            // Fields (Collection)
            new Object_Fields_Explicit(),
            new Object_Fields_ViaClrProperties(),
            // TODO: Object_Fields_ViaClrMethods
            // Interfaces (Collection)
            // TODO: Object_Interfaces_Explicit
            // TODO: Object_Interfaces_ViaClrInterfaces
            // Directive Annotations (Collection)
            // TODO: Object_DirectiveAnnotations_Explicit
            // TODO: Object_DirectiveAnnotations_ViaClrClassAttributes
            // TODO: Object_DirectiveAnnotations_ViaClrInterfaceAttributes

            // OBJECT FIELD
            // Description (Leaf)
            // TODO: Object_Field_Explicit_Description
            // TODO: Object_Field_ViaClrProperty_Description
            // TODO: Object_Field_ViaClrMethod_Description
            // Type
            // TODO: Object_Field_Type_Explicit
            // TODO: Object_Field_Type_ViaClrMethod
            // TODO: Object_Field_Type_ViaClrProperty
            // Arguments (Collection)
            // TODO: Object_Field_Arguments_Explicit
            // TODO: Object_Field_Arguments_ViaClrClassMethodArguments
            // TODO: Object_Field_Arguments_ViaClrInterfaceMethodArguments
            // Directive Annotations (Collection)
            // TODO: Object_Field_DirectiveAnnotations_Explicit
            // TODO: Object_Field_DirectiveAnnotations_ViaClrPropertyAttributes
            // TODO: Object_Field_DirectiveAnnotations_ViaClrMethodAttributes


            // INTERFACE
            // Description (Leaf)
            // TODO: Interface_Explicit_Description
            // TODO: Interface_ViaClrClass_Description
            // Fields (Collection)
            // TODO: Interface_Fields_Explicit
            // TODO: Interface_Fields_ViaClrProperties
            // TODO: Interface_Fields_ViaClrMethods
            // Directive Annotations (Collection)
            // TODO: Interface_DirectiveAnnotations_Explicit
            // TODO: Interface_DirectiveAnnotations_ViaClrClassAttributes
            // TODO: Interface_DirectiveAnnotations_ViaClrInterfaceAttributes

            // INTERFACE FIELD
            // Description (Leaf)
            // TODO: Interface_Field_Explicit_Description
            // TODO: Interface_Field_ViaClrProperty_Description
            // TODO: Interface_Field_ViaClrMethod_Description
            // Type
            // TODO: Interface_Field_Type_Explicit
            // TODO: Interface_Field_Type_ViaClrMethod
            // TODO: Interface_Field_Type_ViaClrProperty
            // Arguments (Collection)
            // TODO: Interface_Field_Arguments_Explicit
            // TODO: Interface_Field_Arguments_ViaClrClassMethodArguments
            // TODO: Interface_Field_Arguments_ViaClrInterfaceMethodArguments
            // Directive Annotations (Collection)
            // TODO: Interface_Field_DirectiveAnnotations_Explicit
            // TODO: Interface_Field_DirectiveAnnotations_ViaClrPropertyAttributes
            // TODO: Interface_Field_DirectiveAnnotations_ViaClrMethodAttributes

            // Union
            // Description (Leaf)
            // TODO: Union_Explicit_Description
            // TODO: Union_ViaMarkerInterface_Description
            // TODO: Union_ViaMarkerInterface_Description
            // MemberTypes (Collection)
            // TODO: Union_MemberTypes_Explicit
            // TODO: Union_MemberTypes_ViaMarkerInterfaces
            // TODO: Union_MemberTypes_ViaChildClasses
            // Directive Annotations (Collection)
            // TODO: Union_DirectiveAnnotations_Explicit
            // TODO: Union_DirectiveAnnotations_ViaClrBaseClassAttributes
            // TODO: Union_DirectiveAnnotations_ViaClrMarkerInterfaceAttributes

            // Object field arguments
            new ObjectField_Arguments_Explicit(),
            new InterfaceFields_Explicit(),
            new Object_ViaClrClass_Description(),
            new Object_Explicit_Description()
        }.OfType<T>();
    }
}