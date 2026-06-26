namespace PortfolioCMS.Domain.Entities;

/// <summary>
/// Represente l'identite d'une micro-entreprise (boulangerie, garage, etc.)
/// C'est le point d'entree de toute la configuration du site.
/// </summary>
public class BusinessProfile
{
    public int Id { get; set; }

    // Identite
    public string BusinessName { get; set; } = string.Empty;       // "Boulangerie Dupont"
    public string ActivityType { get; set; } = string.Empty;       // "boulangerie", "garage", "boucherie"
    public string Slogan { get; set; } = string.Empty;             // "Le pain de tradition depuis 1950"
    public string? LogoUrl { get; set; }
    public string? FaviconUrl { get; set; }

    // Theme visuel (injecte en variables CSS cote front)
    public string PrimaryColor { get; set; } = "#2563EB";
    public string SecondaryColor { get; set; } = "#F59E0B";
    public string FontFamily { get; set; } = "Inter, sans-serif";

    // Contact
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? GoogleMapsEmbedUrl { get; set; }

    // Reseaux sociaux
    public string? FacebookUrl { get; set; }
    public string? InstagramUrl { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation
    public ICollection<OpeningHour> OpeningHours { get; set; } = new List<OpeningHour>();
    public ICollection<ContentSection> ContentSections { get; set; } = new List<ContentSection>();
    public ICollection<ProductOrService> ProductsOrServices { get; set; } = new List<ProductOrService>();
    public ICollection<Testimonial> Testimonials { get; set; } = new List<Testimonial>();
}
