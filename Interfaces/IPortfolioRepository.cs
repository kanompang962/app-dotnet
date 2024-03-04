using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_dotnet.Models;

namespace app_dotnet.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolio(AppUser appUser);
        Task<Portfolio> CreateAsync(Portfolio portfolio);
        Task<Portfolio?> DeletePortfolio(AppUser appUser, string symbol);
    }
}