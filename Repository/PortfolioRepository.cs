using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_dotnet.Data;
using app_dotnet.Interfaces;
using app_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace app_dotnet.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context;
        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio?> DeletePortfolio(AppUser appUser, string symbol)
        {
            var portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(
                x => x.AppUserId == appUser.Id && 
                x.Stock!.Symbol.ToLower() == symbol.ToLower()
            );

            if (portfolioModel == null)
            {
                return null;
            }

            _context.Portfolios.Remove(portfolioModel);
            await _context.SaveChangesAsync();
            return portfolioModel;
        }

        public async Task<List<Stock>> GetUserPortfolio(AppUser user)
        {
            return await _context.Portfolios
                    .Where(u => u.AppUserId == user.Id )
                    .Select(stock => new Stock{
                        Id = stock.StockId,
                        Symbol = stock.Stock!.Symbol,
                        CompanyName = stock.Stock.CompanyName,
                        Purchase = stock.Stock.Purchase,
                        LasDiv = stock.Stock.LasDiv,
                        Industry = stock.Stock.Industry,
                        MarketCap = stock.Stock.MarketCap
                    }).ToListAsync();
        }
    }
}