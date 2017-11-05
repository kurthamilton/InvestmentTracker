using InvestmentTracker.Domain.Investments;
using InvestmentTracker.Domain.Prices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvestmentTracker.ApplicationService.Prices
{
    public class PriceApplicationService : IPriceApplicationService
    {
        private readonly IInvestmentFactory _investmentFactory;

        private readonly IPriceRepository _pricesRepository;

        public PriceApplicationService(IPriceRepository pricesRepository, IInvestmentFactory investmentFactory)
        {
            _investmentFactory = investmentFactory;
            _pricesRepository = pricesRepository;
        }

        public Guid Add(Price price)
        {
            List<Price> prices = new List<Price>(_pricesRepository.GetAll());

            Merge(prices, price);

            _pricesRepository.Save(prices);

            return price.Id;
        }

        public void Add(IEnumerable<Price> prices)
        {
            List<Price> repositoryprices = new List<Price>(_pricesRepository.GetAll());

            foreach (Price price in prices)
            {
                Merge(repositoryprices, price);
            }

            _pricesRepository.Save(repositoryprices);
        }

        public void Delete(Guid id)
        {
            List<Price> prices = new List<Price>(_pricesRepository.GetAll());

            Price price = prices.SingleOrDefault(x => x.Id == id);

            if (price == null)
            {
                return;
            }

            prices.Remove(price);

            _pricesRepository.Save(prices);
        }

        public IReadOnlyCollection<Price> GetAll()
        {
            return _pricesRepository.GetAll();
        }

        public Price GetById(Guid id)
        {
            return GetAll().SingleOrDefault(x => x.Id == id);
        }

        public IReadOnlyCollection<string> GetInvestmentNames()
        {
            return _investmentFactory.GetInvestmentNames().ToArray();
        }

        public IReadOnlyCollection<Price> Scrape(string investmentName, InvestmentSettings settings, DateTime from, DateTime? to)
        {
            IInvestment investment = _investmentFactory.Create(investmentName, settings);
            if (investment == null)
            {
                return new List<Price>();
            }

            return investment.GetPrices(from, to);
        }

        private static void Merge(IList<Price> prices, Price price)
        {
            Price existing = prices.SingleOrDefault(x => x.Date == price.Date && x.Fund.Equals(price.Fund, StringComparison.OrdinalIgnoreCase));

            if (existing != null)
            {
                prices.Remove(existing);
            }

            prices.Add(price);
        }
    }
}