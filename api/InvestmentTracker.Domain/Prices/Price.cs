using System;

namespace InvestmentTracker.Domain.Prices
{
    public class Price
    {
        public Price(DateTime date, string fund, double value)
        {
            Date = date;
            Fund = fund;
            Value = value;
        }

        public DateTime Date { get; private set; }

        public string Fund { get; private set; }

        public double Value { get; private set; }
    }
}