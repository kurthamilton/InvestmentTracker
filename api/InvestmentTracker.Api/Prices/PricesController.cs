using InvestmentTracker.ApplicationService.Prices;
using InvestmentTracker.Domain.Investments;
using InvestmentTracker.Domain.Prices;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace InvestmentTracker.Api.Prices
{
    public class PricesController : ControllerBase
    {
        private readonly IPriceApplicationService _priceApplicationService;

        public PricesController(IPriceApplicationService priceApplicationService)
        {
            _priceApplicationService = priceApplicationService;
        }

        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            _priceApplicationService.Delete(id);
            return GetAll();
        }

        [HttpGet]
        public IHttpActionResult Get(Guid id)
        {
            Price price = _priceApplicationService.GetById(id);
            return JsonResult(price);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            IReadOnlyCollection<Price> prices = _priceApplicationService.GetAll();
            return JsonResult(prices);
        }

        [HttpPost]
        public IHttpActionResult Post(CreatePriceModel model)
        {
            Price price = new Price(model.Date, model.Fund, model.Value);

            Guid id = _priceApplicationService.Add(price);

            return Get(id);
        }

        [HttpPost]
        public IHttpActionResult Scrape(string investment, string url, string username, string password, DateTime from, DateTime? to = null)
        {
            InvestmentSettings settings = new InvestmentSettings
            {
                Password = password,
                Url = url,
                Username = username
            };

            IReadOnlyCollection<Price> prices = _priceApplicationService.Scrape(investment, settings, from, to);

            _priceApplicationService.Add(prices);

            return GetAll();
        }
    }
}