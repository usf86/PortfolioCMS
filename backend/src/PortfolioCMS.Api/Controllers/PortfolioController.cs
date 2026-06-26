using Microsoft.AspNetCore.Mvc;
using PortfolioCMS.Application.Services;

namespace PortfolioCMS.Api.Controllers;

/// <summary>
/// Expose la configuration complete d'un site (profil, sections, produits, avis)
/// en un seul appel afin de minimiser la latence cote client.
/// </summary>
[ApiController]
[Route("api/portfolio")]
public class PortfolioController : ControllerBase
{
    private readonly IPortfolioService _portfolioService;

    public PortfolioController(IPortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }

    /// <summary>Recupere la configuration via l'identifiant du profil metier.</summary>
    // GET /api/portfolio/1
    [HttpGet("{businessProfileId:int}")]
    public async Task<IActionResult> GetById(int businessProfileId)
    {
        var config = await _portfolioService.GetFullConfigAsync(businessProfileId);
        if (config is null)
            return NotFound(new { message = "Profil introuvable ou inactif." });

        return Ok(config);
    }

    /// <summary>Recupere la configuration via le type d'activite (mode "site unique").</summary>
    // GET /api/portfolio/by-activity/boulangerie
    [HttpGet("by-activity/{activityType}")]
    public async Task<IActionResult> GetByActivityType(string activityType)
    {
        var config = await _portfolioService.GetFullConfigBySlugAsync(activityType);
        if (config is null)
            return NotFound(new { message = "Aucun site trouve pour cette activite." });

        return Ok(config);
    }
}
