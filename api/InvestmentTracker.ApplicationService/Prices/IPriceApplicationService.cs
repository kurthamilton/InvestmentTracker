using InvestmentTracker.Domain.Prices;
using System.Collections.Generic;

namespace InvestmentTracker.ApplicationService.Prices
{
    public interface IPriceApplicationService
    {
        void Add(Price price);

        IReadOnlyCollection<Price> GetAll();
    }
}
