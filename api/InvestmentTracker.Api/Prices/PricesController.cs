using InvestmentTracker.ApplicationService.Prices;
using InvestmentTracker.Domain.Prices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InvestmentTracker.Api.Prices
{
    [Route("api/[controller]")]
    public class PricesController : Controller
    {
        private readonly IPriceApplicationService _priceApplicationService;

        public PricesController(IPriceApplicationService priceApplicationService)
        {
            _priceApplicationService = priceApplicationService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IReadOnlyCollection<Price> prices = _priceApplicationService.GetAll();
            return Json(prices);
        }

        // POST api/prices
        [HttpPost]
        public IActionResult Post([FromBody]Price price)
        {
            _priceApplicationService.Add(price);

            return GetAll();
        }
    }
}