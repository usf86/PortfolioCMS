namespace PortfolioCMS.Domain.Entities;

public enum DayOfWeekFr
{
    Lundi = 1, Mardi = 2, Mercredi = 3, Jeudi = 4,
    Vendredi = 5, Samedi = 6, Dimanche = 7
}

/// <summary>Horaires d'ouverture, jour par jour (gere aussi les coupures midi).</summary>
public class OpeningHour
{
    public int Id { get; set; }
    public int BusinessProfileId { get; set; }
    public BusinessProfile BusinessProfile { get; set; } = null!;

    public DayOfWeekFr Day { get; set; }
    public TimeSpan? OpenMorning { get; set; }
    public TimeSpan? CloseMorning { get; set; }
    public TimeSpan? OpenAfternoon { get; set; }
    public TimeSpan? CloseAfternoon { get; set; }
    public bool IsClosed { get; set; } = false;
}
