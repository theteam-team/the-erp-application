using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IProductRepository :IRepository<Product>
    {
        Task<int> checkunitsInStock(string id, byte[] error);
        Task<int> addToStock(string id, int newUnits,byte[] error);
        Task<int> removeFromStock(string id, int newUnits, byte[] error);
        //Task<int> updateProductInfo(string id, string key, string value, byte[] error);
    }
}
