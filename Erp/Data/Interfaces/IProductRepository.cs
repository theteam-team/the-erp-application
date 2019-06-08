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
        Task<List<Product>> SearchProducts(string key, string value, byte[] error);
        Task<int> EditProduct(Product entity, byte[] error);
        Task<int> addToStock(string id, int newUnits,byte[] error);

        Task<List<Product>> getSoldProduct(byte[] error);
    }
}
