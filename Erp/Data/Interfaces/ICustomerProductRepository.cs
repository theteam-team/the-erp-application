using Erp.Data;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface ICustomerProductRepository : IRepository<CustomerProduct, DataDbContext>
    {
        Task<List<CustomerProduct>> ShowCustomerProducts(string id, byte[] error);
    }
}