// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
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
            var mutationType = schema.GetObject("Mutation").As<ObjectType>();

            schema.MutationType.Should().Be(mutationType);

            var writeMutation = mutationType.GetField("writeArticle");
            writeMutation.FieldType.Should().Be(schema.GetType("Article"));
            writeMutation.FieldType.As<ObjectType>().Name.Should().Be("Article");
            writeMutation.Name.Should().Be("writeArticle");
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
            var queryType = schema.GetType("Query").As<ObjectType>();
            var mutationType = schema.GetType("Mutation");
            var subscriptionType = schema.GetType("Subscription");

            schema.QueryType.Should().Be(queryType);

            var articleField = queryType.GetField("article");
            var articleFieldType = (ObjectType)articleField.FieldType;
            articleFieldType.Should().Be(articleType);
            articleFieldType.Name.Should().Be("Article");
            articleField.Name.Should().Be("article");

            var titleField = articleFieldType.GetField("title");
            titleField.Name.Should().Be("title");
            titleField.FieldType.As<ScalarType>().Name.Should().Be("String");

            var authorField = articleFieldType.GetField("author");
            var authorFieldType = authorField.FieldType.As<ObjectType>();
            var recentArticleField = authorFieldType.As<ObjectType>().GetField("recentArticle");

            recentArticleField.FieldType.Should().Be(articleType);

            var feedField = queryType.GetField("feed");
            feedField.FieldType.As<ListType>().OfType.Should().Be(articleType);
            feedField.Name.Should().Be("feed");
        }

        [Fact]
        public void DefinesSubscriptionSchema()
        {
            var schema = new BlogSubscriptionContext().Schema;
            var subscriptionType = schema.GetType("Subscription");

            schema.SubscriptionType.Should().Be(subscriptionType);
            var sub = subscriptionType.As<ObjectType>().GetField("articleSubscribe");
            sub.FieldType.Should().Be(schema.GetType("Article"));
            sub.Name.Should().Be("articleSubscribe");
        }
    }
}