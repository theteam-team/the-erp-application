using Erp.Data;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer, DataDbContext>
    {
        Task<List<Customer>> getCustomerById(string id, byte[] error);
        Task<List<AOrder>> getCustomerOrders(string id, byte[] error);
        Task<List<Account>> getCustomerAccount(string id, byte[] error);

        Task<List<Out>> reporting(byte[] error);
    }
}
