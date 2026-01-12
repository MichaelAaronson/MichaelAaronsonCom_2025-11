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

            //// Configure Group table
            //modelBuilder.Entity<Group>(entity =>
            //{
            //    entity.ToTable("Group");
            //    entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            //    entity.Property(e => e.Description).HasMaxLength(500);
            //});
        }


    }
}
