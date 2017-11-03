using InvestmentTracker.Domain.Prices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvestmentTracker.ApplicationService.Prices
{
    public class PriceApplicationService : IPriceApplicationService
    {
        private readonly IPricesRepository _pricesRepository;

        public PriceApplicationService(IPricesRepository pricesRepository)
        {
            _pricesRepository = pricesRepository;
        }

        public void Add(Price price)
        {
            List<Price> prices = new List<Price>(_pricesRepository.GetAll());

            Price existing = prices.SingleOrDefault(x => x.Date == price.Date && x.Fund.Equals(price.Fund, StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                prices.Remove(existing);
            }

            prices.Add(price);

            _pricesRepository.Save(prices);
        }

        public IReadOnlyCollection<Price> GetAll()
        {
            return _pricesRepository.GetAll();
        }
    }
}

