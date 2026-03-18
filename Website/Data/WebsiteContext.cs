using Microsoft.EntityFrameworkCore;
using Website.Models;

namespace Website.Data
{
    public class WebsiteContext : DbContext
    {
        public WebsiteContext (DbContextOptions<WebsiteContext> options)
            : base(options)
        {
        }

        public DbSet<Website.Models.PlayNumber> PlayNumber { get; set; } = default!;
        public DbSet<Website.Models.Person> Person { get; set; } = default!;

        public DbSet<Website.Models.Group> Group { get; set; } = default!;
        public DbSet<Website.Models.PersonGroup> PersonGroup { get; set; } = default!;
        public DbSet<Website.Models.Job> Job { get; set; } = default!;
        public DbSet<Website.Models.PlayNumber1> PlayNumber1 { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship via join table
            modelBuilder.Entity<PersonGroup>(entity =>
            {
                // Composite primary key
                entity.HasKey(pg => new { pg.PersonId, pg.GroupId });

                // Relationship to Person
                entity.HasOne(pg => pg.Person)
                      .WithMany(p => p.PersonGroups)
                      .HasForeignKey(pg => pg.PersonId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Relationship to Group
                entity.HasOne(pg => pg.Group)
                      .WithMany(g => g.PersonGroups)
                      .HasForeignKey(pg => pg.GroupId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure many-to-many relationship between JobDetail and JobSkill
            modelBuilder.Entity<JobDetailSkill>(entity =>
            {
                // Composite primary key
                entity.HasKey(jds => new { jds.JobDetailId, jds.JobSkillId });
                // Relationship to JobDetail
                entity.HasOne(jds => jds.JobDetail)
                      .WithMany(jd => jd.JobDetailSkills)
                      .HasForeignKey(jds => jds.JobDetailId)
                      .OnDelete(DeleteBehavior.Cascade);
                // Relationship to JobSkill
                entity.HasOne(jds => jds.JobSkill)
                      .WithMany(js => js.JobDetailSkills)
                      .HasForeignKey(jds => jds.JobSkillId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            //modelBuilder.Entity<JobDetailSkill>()
            //    .HasKey(jds => new { jds.JobDetailId, jds.JobSkillId });

            //// Configure Group table
            //modelBuilder.Entity<Group>(entity =>
            //{
            //    entity.ToTable("Group");
            //    entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            //    entity.Property(e => e.Description).HasMaxLength(500);
            //});
        }
        public DbSet<Website.Models.JobDetail> JobDetail { get; set; } = default!;


    }
}
