// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

namespace SimpleBlog.Models;

public class Comment
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public required string Author { get; set; }
    public required string Content { get; set; }
}
