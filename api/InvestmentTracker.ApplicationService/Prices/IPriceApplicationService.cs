using InvestmentTracker.Domain.Investments;
using InvestmentTracker.Domain.Prices;
using System;
using System.Collections.Generic;

namespace InvestmentTracker.ApplicationService.Prices
{
    public interface IPriceApplicationService
    {
        Guid Add(Price price);

        void Add(IEnumerable<Price> prices);

        void Delete(Guid id);

        IReadOnlyCollection<Price> GetAll();

        Price GetById(Guid id);

        IReadOnlyCollection<string> GetInvestmentNames();

        IReadOnlyCollection<Price> Scrape(string investmentName, InvestmentSettings settings, DateTime from, DateTime? to, int intervalDays);
    }
}