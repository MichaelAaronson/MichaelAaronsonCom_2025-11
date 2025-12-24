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

    public virtual DbSet<Domain> Domains { get; set; }

    public virtual DbSet<Flashcard> Flashcards { get; set; }

    public virtual DbSet<FlashcardTopic> FlashcardTopics { get; set; }

    public virtual DbSet<Flashcards202504> Flashcards202504s { get; set; }

    public virtual DbSet<FlashcardsPrev> FlashcardsPrevs { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PlayNumber> PlayNumbers { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Step> Steps { get; set; }

    public virtual DbSet<TaskPlay202507> TaskPlay202507s { get; set; }

    public virtual DbSet<View1> View1s { get; set; }

    public virtual DbSet<View2> View2s { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=tcp:s31.winhost.com;Initial Catalog=DB_129343_main;User ID=DB_129343_main_user;Password=yposelio;Integrated Security=False;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain>(entity =>
        {
            entity.ToTable("Domain");

            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Flashcard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Flashcards_1");

            entity.ToTable("Flashcard");

            entity.Property(e => e.English).HasMaxLength(50);
            entity.Property(e => e.Maori).HasMaxLength(150);
            entity.Property(e => e.Tag).HasMaxLength(50);
        });

        modelBuilder.Entity<FlashcardTopic>(entity =>
        {
            entity.ToTable("FlashcardTopic");

            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Flashcards202504>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Flashcards");

            entity.ToTable("Flashcards.2025-04");

            entity.Property(e => e.English).HasMaxLength(50);
            entity.Property(e => e.Maori).HasMaxLength(150);
            entity.Property(e => e.PowerAppsId).HasMaxLength(50);
        });

        modelBuilder.Entity<FlashcardsPrev>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("FlashcardsPrev");

            entity.Property(e => e.English).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Important).HasDefaultValue(true, "DF_Flashcards1_Important");
            entity.Property(e => e.ImportantPrev).HasMaxLength(50);
            entity.Property(e => e.Maori).HasMaxLength(150);
            entity.Property(e => e.PowerAppsId).HasMaxLength(50);
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.ToTable("Goal");

            entity.Property(e => e.Title).HasMaxLength(50);
        });

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

        modelBuilder.Entity<PlayNumber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PlayNumbers");

            entity.ToTable("PlayNumber");

            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Project");

            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Step>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Task");

            entity.ToTable("Step");

            entity.Property(e => e.TimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<TaskPlay202507>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TaskPlay2025-07");

            entity.Property(e => e.ProjectTitle).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<View1>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_1");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<View2>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_2");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
