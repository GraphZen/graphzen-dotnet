// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;
using Xunit;

#nullable disable


namespace GraphZen.TypeSystem.Tests
{
    public class BlogExampleSchemaTests
    {
        [Fact]
        public void DefinesMutationSchema()
        {
            var schema = new BlogMutationContext().Schema;
            var mutationType = schema.GetObject("Mutation");

            Assert.Equal(mutationType, schema.MutationType);

            var writeMutation = mutationType.FindField("writeArticle")!;
            Assert.Equal(schema.GetType("Article"), writeMutation.FieldType);
            Assert.Equal("Article", ((ObjectType)writeMutation.FieldType).Name);
            Assert.Equal("writeArticle", writeMutation.Name);
        }

        [Fact]
        [SuppressMessage("ReSharper", "UnusedVariable")]
        public void DefinesQueryOnlySchema()
        {
            var context = new BlogContext();
            var schema = context.Schema;
            var imageType = schema.GetType("Image");
            var authorType = schema.GetType("Author");
            var articleType = schema.GetType("Article");
            var queryType = (ObjectType)schema.GetType("Query");
            var mutationType = schema.GetType("Mutation");
            var subscriptionType = schema.GetType("Subscription");

            Assert.Equal(queryType, schema.QueryType);

            var articleField = queryType.FindField("article")!;
            var articleFieldType = (ObjectType)articleField.FieldType;
            Assert.Equal(articleType, articleFieldType);
            Assert.Equal("Article", articleFieldType.Name);
            Assert.Equal("article", articleField.Name);

            var titleField = articleFieldType.FindField("title")!;
            Assert.Equal("title", titleField.Name);
            Assert.Equal(SpecScalars.String, titleField.FieldType);
            Assert.Equal("String", ((ScalarType)titleField.FieldType).Name);

            var authorField = articleFieldType.FindField("author")!;
            var authorFieldType = (ObjectType)authorField.FieldType;
            var recentArticleField = authorFieldType.FindField("recentArticle")!;

            Assert.Equal(articleType, recentArticleField.FieldType);

            var feedField = queryType.FindField("feed")!;
            Assert.Equal(articleType, ((ListType)feedField.FieldType).OfType);
            Assert.Equal("feed", feedField.Name);
        }

        [Fact]
        public void DefinesSubscriptionSchema()
        {
            var schema = new BlogSubscriptionContext().Schema;
            var subscriptionType = schema.GetType("Subscription");

            Assert.Equal(subscriptionType, schema.SubscriptionType);
            var sub = ((ObjectType)subscriptionType).FindField("articleSubscribe")!;
            Assert.Equal(schema.GetType("Article"), sub.FieldType);
            Assert.Equal("articleSubscribe", sub.Name);
        }
    }
}
