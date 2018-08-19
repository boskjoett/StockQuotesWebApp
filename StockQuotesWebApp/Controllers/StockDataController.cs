using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockQuotesWebApp.DataProviders;
using StockQuotesWebApp.Models;

namespace StockQuotesWebApp.Controllers
{
    public class StockDataController : Controller
    {
        private IStockDataProvider stockDataProvider;

        public StockDataController(IStockDataProvider stockDataProvider)
        {
            this.stockDataProvider = stockDataProvider;
        }

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
        public async Task<ActionResult> LoadDataAsync(StockApiRequest viewModel)
        {
            return View("List", await stockDataProvider.GetStockDataAsync(viewModel.StockId, viewModel.StockExchange, viewModel.FromDate, viewModel.ToDate));
        }
    }
}