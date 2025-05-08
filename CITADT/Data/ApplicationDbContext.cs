using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CITADT.Models;

namespace CITADT.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<News> News { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure News
            builder.Entity<News>(entity =>
            {
                entity.HasOne(n => n.Category)
                    .WithMany(c => c.News)
                    .HasForeignKey(n => n.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(n => n.Author)
                    .WithMany()
                    .HasForeignKey(n => n.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(n => n.Title).IsRequired().HasMaxLength(200);
                entity.Property(n => n.Content).IsRequired();
                entity.Property(n => n.Summary).HasMaxLength(500);
                entity.Property(n => n.Slug).HasMaxLength(200);
                entity.Property(n => n.FeaturedImage).HasMaxLength(500);
            });

            // Configure Category
            builder.Entity<Category>(entity =>
            {
                entity.HasIndex(c => c.Slug)
                    .IsUnique();

                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Slug).HasMaxLength(450);
            });
        }
    }
} 