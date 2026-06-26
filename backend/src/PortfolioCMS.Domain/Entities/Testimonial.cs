namespace PortfolioCMS.Domain.Entities;

public class Testimonial
{
    public int Id { get; set; }
    public int BusinessProfileId { get; set; }
    public BusinessProfile BusinessProfile { get; set; } = null!;

    public string AuthorName { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public int Rating { get; set; } = 5;
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    public bool IsApproved { get; set; } = false;
    public bool IsVisible { get; set; } = true;
}
