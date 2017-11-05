using InvestmentTracker.ApplicationService.Prices;
using System.Collections.Generic;
using System.Web.Http;

namespace InvestmentTracker.Api.Investments
{
    public class InvestmentsController : ControllerBase
    {
        private readonly IPriceApplicationService _priceApplicationService;

        public InvestmentsController(IPriceApplicationService priceApplicationService)
        {
            _priceApplicationService = priceApplicationService;
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            IReadOnlyCollection<string> investments = _priceApplicationService.GetInvestmentNames();

            return JsonResult(investments);
        }
    }
}