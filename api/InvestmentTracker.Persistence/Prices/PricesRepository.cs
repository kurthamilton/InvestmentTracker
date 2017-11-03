using InvestmentTracker.Domain.Prices;
using System;
using System.Globalization;

namespace InvestmentTracker.Persistence.Prices
{
    public class PricesRepository : CsvRepository<Price>, IPricesRepository
    {
        public PricesRepository(string filePath)
            : base(filePath)
        {
        }

        protected override Price FromValues(string[] values)
        {
            return new Price(
                DateTime.ParseExact(values[0], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                values[1],
                double.Parse(values[2], CultureInfo.InvariantCulture));
        }

        protected override string[] ToValues(Price entity)
        {
            return new []
            {
                entity.Date.ToString("yyyy-MM-dd"),
                entity.Fund,
                entity.Value.ToString(CultureInfo.InvariantCulture)
            };
        }
    }
}
