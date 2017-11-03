using InvestmentTracker.Domain.Prices;
using System;
using System.Collections.Generic;

namespace InvestmentTracker.ApplicationService.Prices
{
    public interface IPriceApplicationService
    {
        Guid Add(Price price);

        void Delete(Guid id);

        Price GetById(Guid id);

        IReadOnlyCollection<Price> GetAll();
    }
}
