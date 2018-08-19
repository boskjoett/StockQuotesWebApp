using System;
using StockQuotesWebApp.Models;

namespace StockQuotesWebApp.DataProviders
{
    public interface IStockDataProvider
    {
        StockData GetStockData(string stockId, string stockExchange, DateTime fromDate, DateTime toDate);
    }
}
