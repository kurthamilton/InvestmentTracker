using System;

namespace InvestmentTracker.Api.Prices
{
    public class CreatePriceModel
    {
        public DateTime Date { get; set; }

        public string Fund { get; set; }

        public double Value { get; set; }
    }
}
