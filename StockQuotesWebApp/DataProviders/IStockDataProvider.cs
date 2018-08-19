using System;
using System.Threading.Tasks;
using StockQuotesWebApp.Models;

namespace StockQuotesWebApp.DataProviders
{
    public interface IStockDataProvider
    {
        Task<StockData> GetStockDataAsync(string stockId, string stockExchange, DateTime fromDate, DateTime toDate);
    }
}
