using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_dotnet.Dtos.Stock;
using app_dotnet.Models;

namespace app_dotnet.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                LasDiv = stockModel.LasDiv,
                Purchase = stockModel.Purchase,
                CompanyName = stockModel.CompanyName,
                Comments = stockModel.Comments.Select(c => c.ToCommentDto()).ToList(),
            };
        }
        public static Stock ToStockFormCreateDTO(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap,
                LasDiv = stockDto.LasDiv,
                Purchase = stockDto.Purchase,
                CompanyName = stockDto.CompanyName,
            };
        }
    }
}