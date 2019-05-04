using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IOrderProductRepository : IRepository<ProductInOrder>
    {
        Task<List<ProductInOrder>> ShowProductsInOrder(string id, byte[] error);
    }
}
