// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using GraphZen.Infrastructure;

namespace SimpleBlog.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Description("some click-baity title")]
        public string Title { get; set; }

        public string Author { get; set; }
        public string Content { get; set; }
    }
}