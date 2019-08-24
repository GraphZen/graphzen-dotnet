#nullable disable
using System;
using System.Collections.Immutable;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.MetaModel;
using Xunit;

namespace GraphZen
{
    public class LeafElementCodeGeneratorTests
    {
        [Theory(Skip = "wip")]
        [InlineData("")]
        public void ShouldContainTest(string expectedTestClassName)
        {
            var models =
                new MetaModelTestCaseCodeGenerator(false).GenerateCode(ImmutableArray<Element>.Empty,
                    GraphQLMetaModel.Schema());
            // ReSharper disable once AssignNullToNotNullAttribute
            var names = models
                // ReSharper disable once AssignNullToNotNullAttribute
                .Concat(models.SelectMany(_ => _.SubClasses))
                .Select(_ => _.Name)
                .ToArray();
            if (!names.Contains(expectedTestClassName))
            {
                throw new Exception(
                    $"expect {expectedTestClassName}, names ({names.Length}) were: \n\n{string.Join('\n', names)}\n\n");
            }
        }
    }
}