using InvestmentTracker.Domain.Prices;
using System;
using System.Collections.Generic;

namespace InvestmentTracker.Domain.Investments
{
    public interface IInvestment
    {
        IReadOnlyCollection<Price> GetPrices(DateTime from, DateTime? to);
    }
}