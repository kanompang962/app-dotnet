using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_dotnet.Data;
using app_dotnet.Dtos.Stock;
using app_dotnet.Helpers;
using app_dotnet.Interfaces;
using app_dotnet.Mappers;
using app_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace app_dotnet.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (stock == null)
            {
                return null;
            }
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject query)
        {
            // Query
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
            }
            // SortBy
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
               if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
               {
                 stocks = query.IsDecsending ?stocks.OrderByDescending(s => s.Symbol) :stocks.OrderBy(s => s.Symbol);
               }
            }
            // Pagination
            if (query.pageNumber != 0 || query.pageSize != 0)
            {
                var skipNumber = (query.pageNumber -1) * query.pageSize;
                return await stocks.Skip(skipNumber).Take(query.pageSize).ToListAsync();
            }
            return await stocks.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
         return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingStock == null)
            {
                return null;
            }
            existingStock.Symbol = stockDto.Symbol;
            existingStock.Industry = stockDto.Industry;
            existingStock.MarketCap = stockDto.MarketCap;
            existingStock.LasDiv = stockDto.LasDiv;
            existingStock.Purchase = stockDto.Purchase;
            existingStock.CompanyName = stockDto.CompanyName;

            await _context.SaveChangesAsync();
            return existingStock;
        }
    }
}