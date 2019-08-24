// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.Internal
{
    public class GraphZenDictionaryContainerExtensions
    {
        [UsedImplicitly]
        public static string PrintAccessorExtensions() =>
            DictionaryContainerCodeGenerator.GenerateDictionaryAccessorExtensions(generate =>
            {
                // Output fields
                // (generating for all implementations as intellisense will be different)
                // ReSharper disable once PossibleNullReferenceException
                generate.ForType<IMutableFieldsContainerDefinition>()
                    .ForDictionary(_ => _.Fields, "name", "Field", "fields");
                generate.ForType<FieldsContainerDefinition>().ForDictionary(_ => _.Fields, "name", "Field");
                generate.ForType<InterfaceTypeDefinition>().ForDictionary(_ => _.Fields, "name", "Field");
                generate.ForType<ObjectTypeDefinition>().ForDictionary(_ => _.Fields, "name", "Field");
                generate.ForType<IFieldsContainer>().ForDictionary(_ => _.Fields, "name", "Field", "fields");
                generate.ForType<ObjectType>().ForDictionary(_ => _.Fields, "name", "Field");
                generate.ForType<InterfaceType>().ForDictionary(_ => _.Fields, "name", "Field");


                generate.ForType<EnumTypeDefinition>().ForDictionary(_ => _.Values, "name", "Value");
                generate.ForType<EnumType>().ForDictionary(_ => _.Values, "name", "Value");
                generate.ForType<EnumType>().ForDictionary(_ => _.ValuesByValue, "value", "Value");

                // Output field arguments
                // (generating for all implementations as intellisense will be different)
                generate.ForType<IMutableArgumentsContainerDefinition>()
                    .ForDictionary(_ => _.Arguments, "name", "Argument", "arguments");
                generate.ForType<FieldDefinition>().ForDictionary(_ => _.Arguments, "name", "Argument");
                generate.ForType<DirectiveDefinition>().ForDictionary(_ => _.Arguments, "name", "Argument");
                generate.ForType<IArgumentsContainer>()
                    .ForDictionary(_ => _.Arguments, "name", "Argument", "arguments");
                generate.ForType<Field>().ForDictionary(_ => _.Arguments, "name", "Argument");
                generate.ForType<Directive>().ForDictionary(_ => _.Arguments, "name", "Argument");

                // Input fields
                generate.ForType<IMutableInputObjectTypeDefinition>()
                    .ForDictionary(_ => _.Fields, "name", "Field", "inputObjectDefinition");
                generate.ForType<IInputObjectType>().ForDictionary(_ => _.Fields, "name", "Field", "inputObject");
            });
    }
}