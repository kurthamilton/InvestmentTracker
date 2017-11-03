using System;

namespace InvestmentTracker.Domain.Prices
{
    public class Price
    {
        public Price(DateTime date, string fund, double value)
            : this(Guid.NewGuid(), date, fund, value)
        {
        }

        public Price(Guid id, DateTime date, string fund, double value)
        {
            Date = date;
            Fund = fund;
            Id = id;
            Value = value;
        }

        public DateTime Date { get; private set; }

        public string Fund { get; private set; }

        public Guid Id { get; private set; }

        public double Value { get; private set; }
    }
}