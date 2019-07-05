// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using GraphZen.Infrastructure;

namespace SimpleBlog.Models
{
    public class Query
    {
        public List<Post> Posts(int? postId)
        {
            if (postId != null)
            {
                return FakeBlogData.Posts.Where(_ => _.Id == postId.Value).ToList();
            }

            return FakeBlogData.Posts;
        }
    }
}