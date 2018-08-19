using System;
using System.Collections.Generic;
using StockQuotesWebApp.Models;

namespace StockQuotesWebApp.DataProviders
{
    public class QuandlStockDataProvider : IStockDataProvider
    {
        private const string ApiKey = "yaFKHkzdk8JmYm91JXaB";

        public StockData GetStockData(string stockId, string stockExchange, DateTime fromDate, DateTime toDate)
        {



            return GenerateDummyStockData();
        }

        private StockData GenerateDummyStockData()
        {
            return new StockData
            {
                Id = "10095279",
                Name = "VOW3_X",
                StockExchange = "FSE",
                Description = "Stock Prices for Volkswagen  Vz (VOW3) from the Frankfurt Stock Exchange",

                TimeSeries = new List<TimeSeriesItem>
                {
                    new TimeSeriesItem
                    {
                        Date = new DateTime(2018,08,10),
                        High = 139.72,
                        Low = 136.54,
                        Close = 138.74,
                        TradedVolume = 1226563
                    },
                    new TimeSeriesItem
                    {
                        Date = new DateTime(2018,08,11),
                        High = 140.74,
                        Low = 138.9,
                        Close = 139.44,
                        TradedVolume = 995530
                    },
                    new TimeSeriesItem
                    {
                        Date = new DateTime(2018,08,12),
                        High = 143.78,
                        Low = 137.46,
                        Close = 138.44,
                        TradedVolume = 1962637
                    }
                }
            };
        }
    }
}
