using InvestmentTracker.Domain.Prices;
using System;
using System.Globalization;

namespace InvestmentTracker.Persistence.Prices
{
    public class PriceRepository : CsvRepository<Price>, IPriceRepository
    {
        public PriceRepository(string filePath)
            : base(filePath)
        {
        }

        protected override Price FromValues(string[] values)
        {
            return new Price(
                Guid.Parse(values[0]),
                DateTime.ParseExact(values[1], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                values[2],
                double.Parse(values[3], CultureInfo.InvariantCulture));
        }

        protected override string[] ToValues(Price entity)
        {
            return new[]
            {
                entity.Id.ToString(),
                entity.Date.ToString("yyyy-MM-dd"),
                entity.Fund,
                entity.Value.ToString(CultureInfo.InvariantCulture)
            };
        }
    }
}