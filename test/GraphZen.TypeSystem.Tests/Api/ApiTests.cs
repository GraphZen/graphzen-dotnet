using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

namespace GraphZen.TypeSystem.Tests.Api
{
    public class ApiTests
    {
        private static IReadOnlyList<Type> PublicBuilderClasses { get; } =
            typeof(SchemaBuilder).Assembly.GetTypes()
                .Where(_ => !_.Name.Contains("Internal"))
                .Where(_ => _.Name.Contains("Builder")).Where(_ => _.IsClass && !_.IsAbstract).ToList().AsReadOnly();


        private static IReadOnlyList<Type> PublicBuilderInterfaces { get; } =
            PublicBuilderClasses.Select(_ => GetNonInheritedInterfaces(_).Single()).ToList().AsReadOnly();


        public static IEnumerable<Type> GetNonInheritedInterfaces(Type type)
        {
            Type[] allInterfaces = type.GetInterfaces();
            var exceptInheritedInterfaces = allInterfaces.Except(
                allInterfaces.SelectMany(t => t.GetInterfaces())
            );
            return exceptInheritedInterfaces;
        }


        [Theory]
        [InlineData(typeof(ObjectTypeBuilder<,>))]
        public void GetPublicBuilderClasses_should_contain_some_expected_builders(Type type)
        {
            PublicBuilderClasses.Should().Contain(_ => _ == type);
        }


        [Fact]
        public void all_builders_should_expose_definition_and_internal_builder_infrastructure()
        {
            var buildersToExclude = new List<Type>
            {
                typeof(GraphQLContextOptionsBuilder)
            }.Select(_ => _.Name).ToList();
            foreach (var builder in PublicBuilderInterfaces.Where(
                _ => !buildersToExclude.Any(ex => _.Name.Contains(ex))))
            {
                var member = builder.Name.TrimStart("I").Split("Builder")[0];
                var memberDefinitionName = $"{member}Definition";
                var internalBuilderName = $"Internal{member}Builder";
                var infrastructureInterfaces = builder.GetInterfaces().Where(_ =>
                        _.IsGenericType && _.GetGenericTypeDefinition() == typeof(IInfrastructure<>))
                    .SelectMany(_ => _.GetGenericArguments())
                    .ToList();

                void AssertImplementsAccessor(string name)
                {
                    var any = infrastructureInterfaces.SingleOrDefault(_ => _.Name == name);
                    if (any == null)
                    {
                        throw new Exception(
                            @$"\{builder.Name} should implement a {typeof(IInfrastructure<>).Name} for an {name}.

{name} IInfrastructure<{name}>.Instance =>  throw new NotImplementedException();

");
                    }
                }

                AssertImplementsAccessor(internalBuilderName);
                AssertImplementsAccessor(memberDefinitionName);
            }


            /*
            var buildersToExclude = new List<Type>
            {
                 typeof(GraphQLContextOptionsBuilder)
            }.Select(_ => _.Name).ToList();
            foreach (var builder in GetPublicBuilderClasses().Where(_ => !buildersToExclude.Any(ex => _.Name.Contains(ex)))
            )
            {
                var infrastructureInterfaces = builder.GetInterfaces().Where(_ =>
                    _.IsGenericType && _.GetGenericTypeDefinition() == typeof(IInfrastructure<>))
                    .SelectMany(_ => _.GetGenericArguments())
                    .ToList();

                var infra = infrastructureInterfaces.SingleOrDefault(_ =>
                  _.Name.StartsWith("Internal") && _.Name.EndsWith("Builder"));

                if (infra == null)
                {
                    throw new Exception($"\n\n{builder.Name} should implement a {typeof(IInfrastructure<>).Name} for an internal builder.\n");
                }

                infrastructureInterfaces.Should().ContainSingle(_ =>
                                   _.Name.EndsWith("Definition"), $"{builder.Name} should implement a {typeof(IInfrastructure<>).Name} for a definition");
            }
            */
        }

        [Fact]
        public void public_builders_should_only_have_one_non_inherited_interface()
        {
            foreach (var builderClass in PublicBuilderClasses)
            {
                GetNonInheritedInterfaces(builderClass).Should().HaveCount(1);
            }
        }
    }
}