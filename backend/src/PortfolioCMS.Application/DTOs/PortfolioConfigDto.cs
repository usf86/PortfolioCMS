namespace PortfolioCMS.Application.DTOs;

/// <summary>
/// DTO racine renvoye en un seul appel : tout ce dont le front a besoin
/// pour construire dynamiquement la page d'accueil.
/// </summary>
public class PortfolioConfigDto
{
    public BusinessProfileDto Profile { get; set; } = null!;
    public List<OpeningHourDto> OpeningHours { get; set; } = new();
    public List<ContentSectionDto> Sections { get; set; } = new();
    public List<ProductOrServiceDto> Products { get; set; } = new();
    public List<TestimonialDto> Testimonials { get; set; } = new();
}

public class BusinessProfileDto
{
    public int Id { get; set; }
    public string BusinessName { get; set; } = string.Empty;
    public string ActivityType { get; set; } = string.Empty;
    public string Slogan { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public string PrimaryColor { get; set; } = string.Empty;
    public string SecondaryColor { get; set; } = string.Empty;
    public string FontFamily { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? FacebookUrl { get; set; }
    public string? InstagramUrl { get; set; }
}

public class OpeningHourDto
{
    public string Day { get; set; } = string.Empty;
    public string? OpenMorning { get; set; }
    public string? CloseMorning { get; set; }
    public string? OpenAfternoon { get; set; }
    public string? CloseAfternoon { get; set; }
    public bool IsClosed { get; set; }
}

public class ContentSectionDto
{
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public string? Body { get; set; }
    public string? ImageUrl { get; set; }
    public List<string> GalleryImages { get; set; } = new();
    public int DisplayOrder { get; set; }
}

public class ProductOrServiceDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public string? PriceUnit { get; set; }
    public string? ImageUrl { get; set; }
    public string Category { get; set; } = string.Empty;
    public bool IsFeatured { get; set; }
}

public class TestimonialDto
{
    public string AuthorName { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public int Rating { get; set; }
}
