using System.Collections.Generic;

namespace InvestmentTracker.Domain.Investments
{
    public interface IInvestmentFactory
    {
        IInvestment Create(string name, InvestmentSettings settings);

        IEnumerable<string> GetInvestmentNames();
    }
}