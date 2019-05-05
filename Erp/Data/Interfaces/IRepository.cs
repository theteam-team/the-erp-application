using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll(byte[] error);
        ClaimsPrincipal User { get;  set; }        
        Task<List<T>> GetAll();

        //IEnumerable<T> Find(Func<T, bool> predicate);

        Task<T> GetById(object id);
        Task<T> GetById(string id, byte[] error);
        Task<int> Create(T entity, byte[] error);
        Task Create(T entity); 
        Task<int> Delete(string id, byte[] error);
    }
}
