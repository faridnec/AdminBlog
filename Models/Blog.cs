using MessagePack;
using System;
using System.Collections.Generic;

namespace AdminBlog.Models;

public partial class Blog
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Subtitle { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string ImagePath { get; set; } = null!;

    public bool IsPublish { get; set; }

    public DateTime CreateTime { get; set; }

    public int AuthorId { get; set; }

    public int CategoryId { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;
}
