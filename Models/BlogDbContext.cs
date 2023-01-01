using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminBlog.Models;

public partial class BlogDbContext : IdentityDbContext<IdentityUser>
{
    public BlogDbContext()
    {
    }

    public BlogDbContext(DbContextOptions<BlogDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Author { get; set; }

    public virtual DbSet<Blog> Blog { get; set; }

    public virtual DbSet<Category> Category { get; set; }

    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=HP14-NECOS;Database=BlogDb;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;");
    */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Authors");

            entity.ToTable("Author");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Blogs");

            entity.ToTable("Blog");

            entity.HasIndex(e => e.AuthorId, "IX_Blogs_AuthorId");

            entity.HasIndex(e => e.CategoryId, "IX_Blogs_CategoryId");

            entity.HasOne(d => d.Author).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Blogs_Authors_AuthorId");

            entity.HasOne(d => d.Category).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Blogs_Categories_CategoryId");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Categories");

            entity.ToTable("Category");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
