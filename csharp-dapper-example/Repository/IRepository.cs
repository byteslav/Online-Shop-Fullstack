using System.Collections.Generic;

namespace csharp_dapper_example.Repository
{
    public interface IRepository<T>
    {
        void Add(T item);
        void Delete(int id);
        void Update(T item);
        T GetById(int id);
        IEnumerable<T> GetAll();
    }
}