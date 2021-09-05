using System.Collections.Generic;
using DevReviews.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevReviews.API.Persistence
{
    public class DevReviewsDbContext : DbContext
    {
        public DevReviewsDbContext(DbContextOptions<DevReviewsDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(p =>
            {
                p.ToTable("Product");
                p.HasKey(i => i.Id);
                p.HasMany(r => r.Reviews)
                   .WithOne()
                   .HasForeignKey(r => r.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductReview>(pr =>
            {
                pr.ToTable("ProductReviews");
                pr.HasKey(i => i.Id);
                pr.Property(i => i.Author)
                    .HasMaxLength(50)
                    .IsRequired();
            });
        }
    }
}