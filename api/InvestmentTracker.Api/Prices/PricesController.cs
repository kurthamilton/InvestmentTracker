using InvestmentTracker.ApplicationService.Prices;
using InvestmentTracker.Domain.Prices;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _priceApplicationService.Delete(id);
            return GetAll();
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            Price price = _priceApplicationService.GetById(id);
            return Json(price);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IReadOnlyCollection<Price> prices = _priceApplicationService.GetAll();
            return Json(prices);
        }

        // POST api/prices
        [HttpPost]
        public IActionResult Post([FromBody]CreatePriceModel model)
        {
            Price price = new Price(model.Date, model.Fund, model.Value);

            Guid id =_priceApplicationService.Add(price);

            return Get(id);
        }
    }
}