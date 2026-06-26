using Microsoft.EntityFrameworkCore;
using PortfolioCMS.Domain.Entities;
using PortfolioCMS.Infrastructure.Persistence;

namespace PortfolioCMS.Infrastructure.Seed;

/// <summary>
/// Donnees de demonstration : une boulangerie et un garage, pour illustrer
/// la flexibilite du modele sur deux metiers totalement differents.
/// Appelee depuis Program.cs au demarrage en environnement de developpement.
/// </summary>
public static class SeedData
{
    public static async Task SeedAsync(PortfolioDbContext context)
    {
        await context.Database.MigrateAsync();

        if (await context.BusinessProfiles.AnyAsync())
            return; // deja initialise

        var boulangerie = new BusinessProfile
        {
            BusinessName = "Boulangerie Dupont",
            ActivityType = "boulangerie",
            Slogan = "Le pain de tradition depuis 1950",
            LogoUrl = "/uploads/boulangerie-dupont/logo.png",
            PrimaryColor = "#B45309",
            SecondaryColor = "#FCD34D",
            FontFamily = "'Playfair Display', serif",
            Address = "12 Rue des Artisans",
            City = "Lyon",
            PostalCode = "69001",
            Phone = "04 78 00 00 00",
            Email = "contact@boulangerie-dupont.fr",
            ContentSections = new List<ContentSection>
            {
                new() {
                    Type = SectionType.About,
                    Title = "A propos de nous",
                    Body = "Notre famille petrit le pain a l'ancienne depuis trois generations.",
                    DisplayOrder = 1
                },
                new() {
                    Type = SectionType.Gallery,
                    Title = "Galerie",
                    GalleryImagesJson = "[\"/uploads/four-a-bois.jpg\",\"/uploads/vitrine.jpg\"]",
                    DisplayOrder = 2
                }
            },
            ProductsOrServices = new List<ProductOrService>
            {
                new() { Title = "Croissant au beurre", Price = 1.30m, PriceUnit = "piece", Category = "Viennoiseries", IsFeatured = true },
                new() { Title = "Pain de campagne", Price = 3.90m, PriceUnit = "piece", Category = "Pains" }
            },
            OpeningHours = BuildStandardHours()
        };

        var garage = new BusinessProfile
        {
            BusinessName = "Garage Lefort",
            ActivityType = "garage",
            Slogan = "Votre mecanicien de confiance",
            LogoUrl = "/uploads/garage-lefort/logo.png",
            PrimaryColor = "#1E3A8A",
            SecondaryColor = "#DC2626",
            FontFamily = "'Roboto Condensed', sans-serif",
            Address = "5 Avenue de l'Industrie",
            City = "Toulouse",
            PostalCode = "31000",
            Phone = "05 61 00 00 00",
            Email = "contact@garage-lefort.fr",
            ContentSections = new List<ContentSection>
            {
                new() {
                    Type = SectionType.About,
                    Title = "Notre atelier",
                    Body = "Garage independant agree, toutes marques, depuis 1998.",
                    DisplayOrder = 1
                }
            },
            ProductsOrServices = new List<ProductOrService>
            {
                new() { Title = "Vidange + filtre a huile", Price = 79.00m, PriceUnit = "forfait", Category = "Entretien", IsFeatured = true },
                new() { Title = "Diagnostic electronique", Category = "Diagnostic" }
            },
            OpeningHours = BuildStandardHours()
        };

        context.BusinessProfiles.AddRange(boulangerie, garage);
        await context.SaveChangesAsync();
    }

    private static List<OpeningHour> BuildStandardHours()
    {
        var hours = new List<OpeningHour>();
        foreach (DayOfWeekFr day in Enum.GetValues<DayOfWeekFr>())
        {
            hours.Add(day == DayOfWeekFr.Dimanche
                ? new OpeningHour { Day = day, IsClosed = true }
                : new OpeningHour
                {
                    Day = day,
                    OpenMorning = new TimeSpan(8, 0, 0),
                    CloseMorning = new TimeSpan(12, 0, 0),
                    OpenAfternoon = new TimeSpan(14, 0, 0),
                    CloseAfternoon = new TimeSpan(19, 0, 0)
                });
        }
        return hours;
    }
}
