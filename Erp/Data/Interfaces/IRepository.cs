using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll(byte[] error);
        Task<T> GetById(string id, byte[] error);
        Task<int> Create(T entity, byte[] error);
        Task<int> Delete(string id, byte[] error);
    }
}
