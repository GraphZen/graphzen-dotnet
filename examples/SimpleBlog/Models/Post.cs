// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;

namespace SimpleBlog.Models;

public class Post
{
    public int Id { get; set; }

    [Description("some click-baity title")]
    public required string Title { get; set; }

    public required string Author { get; set; }
    public required string Content { get; set; }
}
