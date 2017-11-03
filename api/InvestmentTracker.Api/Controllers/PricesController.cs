using InvestmentTracker.Domain.Prices;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InvestmentTracker.Api.Controllers
{
    [Route("api/[controller]")]
    public class PricesController : Controller
    {
        private readonly IPricesRepository _pricesRepository;

        public PricesController(IPricesRepository pricesRepository)
        {
            _pricesRepository = pricesRepository;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string html, DateTime date)
        {
        }
    }
}