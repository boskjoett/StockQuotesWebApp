using System;
using System.Collections.Generic;

namespace StockQuotesWebApp.Models
{
    public class StockData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string StockExchange { get; set; }
        public string Description { get; set; }
        public List<TimeSeriesItem> TimeSeries { get; set; }
    }

    public class TimeSeriesItem
    {
        public DateTime Date { get; set; }
        public double Low { get; set; }
        public double High { get; set; }
        public double Close { get; set; }
        public double TradedVolume { get; set; }
    }
}
