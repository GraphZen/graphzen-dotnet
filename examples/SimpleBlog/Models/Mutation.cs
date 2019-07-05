// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Linq;
using GraphZen.Infrastructure;

namespace SimpleBlog.Models
{
    public class Mutation
    {
        [Description("Add a new post")]
        public bool AddPost(string author, string title, string post)
        {
            var newId = FakeBlogData.Posts.Max(_ => _.Id) + 1;
            var postModel = new Post
            {
                Id = newId,
                Author = author,
                Title = title,
                Content = post
            };
            FakeBlogData.Posts.Add(postModel);
            return true;
        }


        [Description("Add a comment to a post")]
        public bool Comment(int postId, string author, string comment)
        {
            var newId = FakeBlogData.Comments.Max(_ => _.Id) + 1;
            var commentModel = new Comment
            {
                Id = newId,
                PostId = postId,
                Author = author,
                Content = comment
            };
            FakeBlogData.Comments.Add(commentModel);
            return true;
        }

        public bool DeleteComment(int id)
        {
            var commentModel = FakeBlogData.Comments.SingleOrDefault(_ => _.Id == id);
            if (commentModel != null)
            {
                FakeBlogData.Comments.Remove(commentModel);
            }

            return true;
        }

        public bool EditPost(int id, string author, string title, string post)
        {
            var postModel = FakeBlogData.Posts.SingleOrDefault(_ => _.Id == id);
            if (postModel == null)
            {
                return false;
            }

            postModel.Author = author;
            postModel.Title = title;
            postModel.Content = post;
            return true;
        }
    }
}