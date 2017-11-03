using InvestmentTracker.Domain.Prices;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InvestmentTracker.Api.Prices
{
    [Route("api/[controller]")]
    public class PricesController : Controller
    {
        private readonly IPricesRepository _pricesRepository;

        public PricesController(IPricesRepository pricesRepository)
        {
            _pricesRepository = pricesRepository;
        }

        // POST api/prices
        [HttpPost]
        public void Post([FromBody]string html, DateTime date)
        {
        }
    }
}