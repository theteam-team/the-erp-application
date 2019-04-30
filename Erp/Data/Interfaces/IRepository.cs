using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IRepository<T> where T : class
    {
         ClaimsPrincipal User { get;  set; }
        Task<List<T>> GetAll(byte[] error);

        //IEnumerable<T> Find(Func<T, bool> predicate);

        Task<T> GetById(string id, byte[] error);

        Task<int> Create(T entity, byte[] error);
        Task Create(T entity);
        Task<int> UpdateInfo(string id, string key, string value, byte[] error);
        Task<string> getInfo(string id, string key,  byte[] error);
  


        Task<int> Delete(string id, byte[] error);

        Task<int> Delete(T entity, byte[] error);

        //int Count(Func<T, bool> predicate);
    }
}
