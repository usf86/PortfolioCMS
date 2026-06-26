namespace PortfolioCMS.Domain.Entities;

/// <summary>
/// Item generique : une viennoiserie, une piece de boucherie, une prestation de garage...
/// Le prix est optionnel (un garagiste affiche rarement un prix fixe).
/// </summary>
public class ProductOrService
{
    public int Id { get; set; }
    public int BusinessProfileId { get; set; }
    public BusinessProfile BusinessProfile { get; set; } = null!;

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public string? PriceUnit { get; set; }                      // "piece", "kg", "heure", "forfait"
    public string? ImageUrl { get; set; }
    public string Category { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public bool IsAvailable { get; set; } = true;
    public bool IsFeatured { get; set; } = false;
}
