using System.Collections.Generic;

namespace InvestmentTracker.Domain
{
    public interface IRepository<T>
    {
        void Add(T Entity);

        IReadOnlyCollection<T> GetAll();
    }
}