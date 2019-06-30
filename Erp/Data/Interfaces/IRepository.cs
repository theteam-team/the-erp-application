using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IRepository<T, C> where T : class where C : DbContext
    {
        Task<List<T>> GetAll(byte[] error);               
        Task<List<T>> GetAll();

        //IEnumerable<T> Find(Func<T, bool> predicate);

        Task<T> GetById(object id);
        Task<T> GetById(string id, byte[] error);
        Task<int> Create(T entity, byte[] error);
        Task<int> Delete(string id, byte[] error);
        //Task Create(T entity); 
        Task Update(T ob);
        Task Insert(T ob);
        void setConnectionString(string databaseName);
        ClaimsPrincipal User { get; set; }
    }
}
