// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace SimpleBlog.Models
{
    public static class FakeBlogData
    {
        public static List<Post> Posts { get; } = new List<Post>
        {
            new Post {Id = 1, Author = "Jane", Title = "My first blog post", Content = "This is my first blog post."},
            new Post {Id = 2, Author = "Jane", Title = "Follow up post", Content = "This blog is really fun!"},
            new Post
            {
                Id = 3, Author = "Gene", Title = "Guest post",
                Content = "Jane has let me write a guest post for her blog."
            }
        };

        public static List<Comment> Comments { get; } = new List<Comment>
        {
            new Comment
            {
                Id = 1, PostId = 1, Author = "Gene", Content = "I'm glad you've decided to start blogging, Jane."
            },
            new Comment {Id = 2, PostId = 1, Author = "Syed", Content = "Have you thought about doing a podcast?"},
            new Comment {Id = 3, PostId = 2, Author = "Gene", Content = "This was a fun blog post."}
        };
    }
}