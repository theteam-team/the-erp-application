using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IRepository<T> where T : class
    {
        //IEnumerable<T> GetAll();

        //IEnumerable<T> Find(Func<T, bool> predicate);

         Task<T> GetById(string id, byte[] error);

         Task<int> Create(T entity, byte[] error);

        //void Update(T entity);

        //void Delete(T entity);

        //int Count(Func<T, bool> predicate);
    }
}
