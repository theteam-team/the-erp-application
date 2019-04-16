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
        Task<int> UpdateInfo(string id, string key, string value, byte[] error);
        Task<string> getInfo(string id, string key,  byte[] error);
  


        Task<int> Delete(string id, byte[] error);

        Task<int> Delete(T entity, byte[] error);

        //int Count(Func<T, bool> predicate);
    }
}
