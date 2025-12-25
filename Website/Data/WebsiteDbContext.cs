using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Website.Models;

namespace Website.Data;

public partial class WebsiteDbContext : DbContext
{
    public WebsiteDbContext()
    {
    }

    public WebsiteDbContext(DbContextOptions<WebsiteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=tcp:s31.winhost.com;Initial Catalog=DB_129343_main;User ID=DB_129343_main_user;Password=yposelio;Integrated Security=False;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person__3214EC078DA6ACD1");

            entity.ToTable("Person");

            entity.Property(e => e.Company).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.ImageFilename).HasMaxLength(100);
            entity.Property(e => e.LastNme).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
