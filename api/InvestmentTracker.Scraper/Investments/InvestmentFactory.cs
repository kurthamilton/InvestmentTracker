using InvestmentTracker.Domain.Investments;
using System;
using System.Collections.Generic;

namespace InvestmentTracker.Scraper.Investments
{
    public class InvestmentFactory : IInvestmentFactory
    {
        private const string LegalAndGeneral = "Legal and General";

        public IInvestment Create(string name, InvestmentSettings settings)
        {
            if (name.Equals(LegalAndGeneral, StringComparison.OrdinalIgnoreCase))
            {
                return new LegalAndGeneralInvestment(settings);
            }

            return null;
        }

        public IEnumerable<string> GetInvestmentNames()
        {
            yield return LegalAndGeneral;
        }
    }
}