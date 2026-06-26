using Microsoft.EntityFrameworkCore;
using PortfolioCMS.Application.DTOs;
using PortfolioCMS.Infrastructure.Persistence;
using System.Text.Json;

namespace PortfolioCMS.Application.Services;

/// <summary>
/// Construit la configuration complete d'un site en une seule requete optimisee
/// (evite le N+1 grace aux Include() et a la projection directe en DTO).
/// </summary>
public class PortfolioService : IPortfolioService
{
    private readonly PortfolioDbContext _context;

    public PortfolioService(PortfolioDbContext context)
    {
        _context = context;
    }

    public async Task<PortfolioConfigDto?> GetFullConfigAsync(int businessProfileId)
    {
        var profile = await _context.BusinessProfiles
            .AsNoTracking()
            .Include(b => b.OpeningHours)
            .Include(b => b.ContentSections.Where(s => s.IsVisible))
            .Include(b => b.ProductsOrServices.Where(p => p.IsAvailable))
            .Include(b => b.Testimonials.Where(t => t.IsApproved && t.IsVisible))
            .FirstOrDefaultAsync(b => b.Id == businessProfileId && b.IsActive);

        if (profile is null) return null;

        return new PortfolioConfigDto
        {
            Profile = new BusinessProfileDto
            {
                Id = profile.Id,
                BusinessName = profile.BusinessName,
                ActivityType = profile.ActivityType,
                Slogan = profile.Slogan,
                LogoUrl = profile.LogoUrl,
                PrimaryColor = profile.PrimaryColor,
                SecondaryColor = profile.SecondaryColor,
                FontFamily = profile.FontFamily,
                Address = profile.Address,
                City = profile.City,
                Phone = profile.Phone,
                Email = profile.Email,
                FacebookUrl = profile.FacebookUrl,
                InstagramUrl = profile.InstagramUrl
            },
            OpeningHours = profile.OpeningHours.Select(o => new OpeningHourDto
            {
                Day = o.Day.ToString(),
                OpenMorning = o.OpenMorning?.ToString(@"hh\:mm"),
                CloseMorning = o.CloseMorning?.ToString(@"hh\:mm"),
                OpenAfternoon = o.OpenAfternoon?.ToString(@"hh\:mm"),
                CloseAfternoon = o.CloseAfternoon?.ToString(@"hh\:mm"),
                IsClosed = o.IsClosed
            }).OrderBy(o => o.Day).ToList(),

            Sections = profile.ContentSections.OrderBy(s => s.DisplayOrder).Select(s => new ContentSectionDto
            {
                Type = s.Type.ToString(),
                Title = s.Title,
                Subtitle = s.Subtitle,
                Body = s.Body,
                ImageUrl = s.ImageUrl,
                DisplayOrder = s.DisplayOrder,
                GalleryImages = string.IsNullOrEmpty(s.GalleryImagesJson)
                    ? new List<string>()
                    : JsonSerializer.Deserialize<List<string>>(s.GalleryImagesJson) ?? new()
            }).ToList(),

            Products = profile.ProductsOrServices.OrderBy(p => p.DisplayOrder).Select(p => new ProductOrServiceDto
            {
                Title = p.Title,
                Description = p.Description,
                Price = p.Price,
                PriceUnit = p.PriceUnit,
                ImageUrl = p.ImageUrl,
                Category = p.Category,
                IsFeatured = p.IsFeatured
            }).ToList(),

            Testimonials = profile.Testimonials.Select(t => new TestimonialDto
            {
                AuthorName = t.AuthorName,
                Comment = t.Comment,
                Rating = t.Rating
            }).ToList()
        };
    }

    public async Task<PortfolioConfigDto?> GetFullConfigBySlugAsync(string activityType)
    {
        var profile = await _context.BusinessProfiles
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.ActivityType == activityType && b.IsActive);

        return profile is null ? null : await GetFullConfigAsync(profile.Id);
    }
}
