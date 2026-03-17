using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Website.Models;

public partial class Db126687MainContext : DbContext
{
    public Db126687MainContext()
    {
    }

    public Db126687MainContext(DbContextOptions<Db126687MainContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobDetail> JobDetails { get; set; }

    public virtual DbSet<JobSkill> JobSkills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=tcp:s30.winhost.com;Initial Catalog=DB_126687_main;User ID=DB_126687_main_user;Password=yposelio;Integrated Security=False;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Job$PrimaryKey");

            entity.ToTable("Job");

            entity.Property(e => e.Company).HasMaxLength(255);
            entity.Property(e => e.Dates).HasMaxLength(255);
            entity.Property(e => e.EndDate).HasMaxLength(255);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(255);
            entity.Property(e => e.StartDate).HasMaxLength(255);
        });

        modelBuilder.Entity<JobDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId).HasName("JobDetail$PrimaryKey");

            entity.ToTable("JobDetail");

            entity.Property(e => e.SsmaTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("SSMA_TimeStamp");

            entity.HasMany(d => d.JobSkills).WithMany(p => p.JobDetails)
                .UsingEntity<Dictionary<string, object>>(
                    "JobDetailSkill",
                    r => r.HasOne<JobSkill>().WithMany()
                        .HasForeignKey("JobSkillId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("JobDetailSkill$JobSkillJobDetailJobSkill"),
                    l => l.HasOne<JobDetail>().WithMany()
                        .HasForeignKey("JobDetailId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("JobDetailSkill$JobDetailJobDetailJobSkill"),
                    j =>
                    {
                        j.HasKey("JobDetailId", "JobSkillId").HasName("JobDetailSkill$PrimaryKey");
                        j.ToTable("JobDetailSkill");
                        j.HasIndex(new[] { "JobSkillId" }, "JobDetailSkill$JobSkillId");
                    });
        });

        modelBuilder.Entity<JobSkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("JobSkill$PrimaryKey");

            entity.ToTable("JobSkill");

            entity.Property(e => e.Comments).HasMaxLength(100);
            entity.Property(e => e.Count).HasDefaultValue(0);
            entity.Property(e => e.SsmaTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("SSMA_TimeStamp");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
