using System;
using System.Collections.Generic;
using System.Text;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using Xunit;

namespace GraphZen
{
    public abstract class LeafElementConfigurationTests
    {

        public virtual void DefineByConvention(SchemaBuilder sb) => throw new NotImplementedException();
        public virtual object ConventionalValue => throw new NotImplementedException();
        public virtual object DataAnnotationValue => throw new NotImplementedException();
        public virtual object ExplicitValue => throw new NotImplementedException();
        public abstract ConfigurationSource GetConfigurationSource();
        public virtual object GetConventionalValue(Schema schema) => throw new NotImplementedException();
        public virtual object GetDefinitionValue(SchemaDefinition schemaDefinition) => throw new NotImplementedException();
        public virtual object GetSchemaValue(Schema schema) => throw new NotImplementedException();

        public virtual void defined_by_convention()
        {
            var schema = Schema.Create(sb =>
            {
                DefineByConvention(sb);

            });
        }
    }

    public abstract class LeafElementDefinedByConventionTests<TParent, TParentDef> where TParent : Member where TParentDef : MemberDefinition
    {
        public abstract void DefineByConvention(SchemaBuilder sb);
        public abstract object ExpectedConventionalValue { get; }
        public abstract object GetConventionalValue(Schema schema);


        [Fact]
        public void defined_by_convention()
        {
            var schema = Schema.Create(sb =>
            {
                DefineByConvention(sb);

            });

        }
    }
}
