using InvestmentTracker.Domain;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InvestmentTracker.Persistence
{
    public abstract class CsvRepository<T> : IRepository<T>
    {
        private readonly string _filePath;

        protected CsvRepository(string filePath)
        {
            _filePath = filePath;
        }

        public IReadOnlyCollection<T> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new T[] { };
            }

            IEnumerable<string> lines = File.ReadLines(_filePath);

            return lines.Select(x => FromValues(x.Split(','))).ToArray();
        }

        public void Save(IEnumerable<T> entities)
        {
            EnsureFileExists();

            IEnumerable<string> lines = entities.Select(x => string.Join(",", ToValues(x)));

            File.WriteAllLines(_filePath, lines);
        }

        protected abstract T FromValues(string[] values);

        protected abstract string[] ToValues(T entity);

        private void EnsureFileExists()
        {
            if (File.Exists(_filePath))
            {
                return;
            }

            FileInfo file = new FileInfo(_filePath);
            file.Directory.Create();

            using (file.Create())
            {
            }
        }
    }
}