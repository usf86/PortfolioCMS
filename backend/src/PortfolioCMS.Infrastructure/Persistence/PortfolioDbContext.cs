using Microsoft.EntityFrameworkCore;
using PortfolioCMS.Domain.Entities;

namespace PortfolioCMS.Infrastructure.Persistence;

public class PortfolioDbContext : DbContext
{
    public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options) { }

    public DbSet<BusinessProfile> BusinessProfiles => Set<BusinessProfile>();
    public DbSet<OpeningHour> OpeningHours => Set<OpeningHour>();
    public DbSet<ContentSection> ContentSections => Set<ContentSection>();
    public DbSet<ProductOrService> ProductsOrServices => Set<ProductOrService>();
    public DbSet<Testimonial> Testimonials => Set<Testimonial>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // --- BusinessProfile ---
        modelBuilder.Entity<BusinessProfile>(entity =>
        {
            entity.ToTable("business_profiles");
            entity.HasKey(b => b.Id);
            entity.Property(b => b.BusinessName).IsRequired().HasMaxLength(150);
            entity.Property(b => b.ActivityType).IsRequired().HasMaxLength(50);
            entity.Property(b => b.PrimaryColor).HasMaxLength(7);
            entity.Property(b => b.SecondaryColor).HasMaxLength(7);
            entity.HasIndex(b => b.ActivityType);
        });

        // --- OpeningHour ---
        modelBuilder.Entity<OpeningHour>(entity =>
        {
            entity.ToTable("opening_hours");
            entity.HasKey(o => o.Id);
            entity.HasOne(o => o.BusinessProfile)
                  .WithMany(b => b.OpeningHours)
                  .HasForeignKey(o => o.BusinessProfileId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.Property(o => o.Day).HasConversion<string>().HasMaxLength(20);
        });

        // --- ContentSection ---
        modelBuilder.Entity<ContentSection>(entity =>
        {
            entity.ToTable("content_sections");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Type).HasConversion<string>().HasMaxLength(30);
            entity.Property(c => c.Title).IsRequired().HasMaxLength(150);
            entity.Property(c => c.Body).HasColumnType("text");
            entity.Property(c => c.GalleryImagesJson).HasColumnType("json");
            entity.HasOne(c => c.BusinessProfile)
                  .WithMany(b => b.ContentSections)
                  .HasForeignKey(c => c.BusinessProfileId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(c => new { c.BusinessProfileId, c.DisplayOrder });
        });

        // --- ProductOrService ---
        modelBuilder.Entity<ProductOrService>(entity =>
        {
            entity.ToTable("products_or_services");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Title).IsRequired().HasMaxLength(150);
            entity.Property(p => p.Price).HasColumnType("decimal(10,2)");
            entity.Property(p => p.Category).HasMaxLength(100);
            entity.HasOne(p => p.BusinessProfile)
                  .WithMany(b => b.ProductsOrServices)
                  .HasForeignKey(p => p.BusinessProfileId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(p => new { p.BusinessProfileId, p.Category });
        });

        // --- Testimonial ---
        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.ToTable("testimonials");
            entity.HasKey(t => t.Id);
            entity.Property(t => t.AuthorName).IsRequired().HasMaxLength(100);
            entity.Property(t => t.Comment).HasColumnType("text");
            entity.HasOne(t => t.BusinessProfile)
                  .WithMany(b => b.Testimonials)
                  .HasForeignKey(t => t.BusinessProfileId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
