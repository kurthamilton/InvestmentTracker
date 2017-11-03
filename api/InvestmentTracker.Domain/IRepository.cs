using System.Collections.Generic;

namespace InvestmentTracker.Domain
{
    public interface IRepository<T>
    {
        IReadOnlyCollection<T> GetAll();

        void Save(IEnumerable<T> entities);
    }
}