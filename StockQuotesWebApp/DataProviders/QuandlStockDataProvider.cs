using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using StockQuotesWebApp.Models;

namespace StockQuotesWebApp.DataProviders
{
    public class QuandlStockDataProvider : IStockDataProvider
    {
        private const string ApiKey = "yaFKHkzdk8JmYm91JXaB";

        public async Task<StockData> GetStockDataAsync(string stockId, string stockExchange, DateTime fromDate, DateTime toDate)
        {
            HttpClient client = new HttpClient();
            string baseUrl = $"https://www.quandl.com/api/v3/datasets/{stockExchange}/{stockId}.json";
            var uriBuilder = new UriBuilder(baseUrl);

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (fromDate != null)
            {
                query["start_date"] = fromDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }

            if (toDate != null)
            {
                query["end_date"] = toDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            }

            query["api_key"] = ApiKey;
            uriBuilder.Query = query.ToString();

            HttpResponseMessage response = await client.GetAsync(uriBuilder.ToString());

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string jsonText = await response.Content.ReadAsStringAsync();
            DataSetContainer dataSetContainer = JsonConvert.DeserializeObject<DataSetContainer>(jsonText);

            StockData stockData = new StockData
            {
                Id = dataSetContainer.dataset.DatasetCode,
                Name = dataSetContainer.dataset.Name,
                Description = dataSetContainer.dataset.Description,
                StockExchange = dataSetContainer.dataset.DatabaseCode,
                TimeSeries = new List<TimeSeriesItem>()
            };

            stockData.TimeSeries = ExtractTimeSeriesData(dataSetContainer.dataset);

            return stockData;
        }

        private List<TimeSeriesItem> ExtractTimeSeriesData(DataSet dataset)
        {
            if (dataset.Data == null)
                return null;

            List<TimeSeriesItem> timeseries = new List<TimeSeriesItem>();

            for (int i = 0; i < dataset.Data.Count; i++)
            {
                int dateIndex = dataset.ColumnNames.IndexOf("Date");
                int highIndex = dataset.ColumnNames.IndexOf("High");
                int lowIndex = dataset.ColumnNames.IndexOf("Low");
                int openIndex = dataset.ColumnNames.IndexOf("Open");
                int closeIndex = dataset.ColumnNames.IndexOf("Close");
                int volumeIndex = dataset.ColumnNames.IndexOf("Traded Volume");

                string dateAsString = (string)dataset.Data[i][dateIndex];

                TimeSeriesItem dataEntry = new TimeSeriesItem
                {
                    Date = DateTime.ParseExact(dateAsString, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                };

                if (highIndex != -1 && dataset.Data[i][highIndex] != null)
                {
                    dataEntry.High = (double)dataset.Data[i][highIndex];
                }

                if (lowIndex != -1 && dataset.Data[i][lowIndex] != null)
                {
                    dataEntry.Low = (double)dataset.Data[i][lowIndex];
                }

                if (openIndex != -1 && dataset.Data[i][openIndex] != null)
                {
                    dataEntry.Open = (double)dataset.Data[i][openIndex];
                }

                if (closeIndex != -1 && dataset.Data[i][closeIndex] != null)
                {
                    dataEntry.Close = (double)dataset.Data[i][closeIndex];
                }

                if (volumeIndex != -1 && dataset.Data[i][volumeIndex] != null)
                {
                    dataEntry.TradedVolume = (double)dataset.Data[i][volumeIndex];
                }

                timeseries.Add(dataEntry);
            }

            return timeseries;
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


        /// <summary>
        /// API class for deserialization of API JSON response.
        /// </summary>
        class DataSetContainer
        {
            public DataSet dataset { get; set; }
        }

        /// <summary>
        /// API class for deserialization of API JSON response.
        /// </summary>
        class DataSet
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("dataset_code")]
            public string DatasetCode { get; set; }

            [JsonProperty("database_code")]
            public string DatabaseCode { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("refreshed_at")]
            public DateTime RefreshedAt { get; set; }

            [JsonProperty("column_names")]
            public List<string> ColumnNames { get; set; }

            [JsonProperty("data")]
            public List<List<object>> Data { get; set; }
        }
    }
}