using PortfolioCMS.Application.DTOs;

namespace PortfolioCMS.Application.Services;

public interface IPortfolioService
{
    Task<PortfolioConfigDto?> GetFullConfigAsync(int businessProfileId);
    Task<PortfolioConfigDto?> GetFullConfigBySlugAsync(string activityType);
}
