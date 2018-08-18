using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StockQuotesWebApp.Models;

namespace StockQuotesWebApp.Controllers
{
    public class StockDataController : Controller
    {
        // GET: StockData/Create
        [HttpGet]
        public ActionResult Create()
        {
            // Create view model with default values
            StockApiRequest viewModel = new StockApiRequest
            {
                StockId = "VOW3_X",
                StockExchange = "FSE",
                FromDate = new DateTime(2016, 1, 1),
                ToDate = DateTime.Today
            };

            return View(viewModel);
        }

        // POST: StockData/LoadData
        [HttpPost]
        public ActionResult LoadData(StockApiRequest viewModel)
        {
            StockDataSet stockDataSet = new StockDataSet
            {
                Id = "10095279",
                Name = viewModel.StockId,
                StockExchange = viewModel.StockExchange,
                Description = "Stock Prices for Volkswagen  Vz (VOW3) from the Frankfurt Stock Exchange",

                TimeSeries = new List<StockData>
                {
                    new StockData
                    {
                        Date = new DateTime(2018,08,10),
                        High = 139.72,
                        Low = 136.54,
                        Close = 138.74,
                        TradedVolume = 1226563
                    },
                    new StockData
                    {
                        Date = new DateTime(2018,08,11),
                        High = 140.74,
                        Low = 138.9,
                        Close = 139.44,
                        TradedVolume = 995530
                    },
                    new StockData
                    {
                        Date = new DateTime(2018,08,12),
                        High = 143.78,
                        Low = 137.46,
                        Close = 138.44,
                        TradedVolume = 1962637
                    }
                }
            };

            return View("List", stockDataSet);
        }
    }
}