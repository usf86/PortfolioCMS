namespace PortfolioCMS.Domain.Entities;

/// <summary>
/// Section generique de contenu pour la page d'accueil.
/// Le "SectionType" permet au front de choisir le bon template d'affichage.
/// </summary>
public enum SectionType
{
    Hero,
    About,          // "A propos"
    Gallery,        // "Galerie"
    Services,       // "Nos Services" / "Nos Produits"
    Contact,
    CustomText
}

public class ContentSection
{
    public int Id { get; set; }
    public int BusinessProfileId { get; set; }
    public BusinessProfile BusinessProfile { get; set; } = null!;

    public SectionType Type { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Subtitle { get; set; }
    public string? Body { get; set; }
    public string? ImageUrl { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsVisible { get; set; } = true;

    // Galerie : liste d'images additionnelles, stockee en JSON
    public string? GalleryImagesJson { get; set; }
}
