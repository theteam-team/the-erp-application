using Erp.Data;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IInventoryProductRepository : IRepository<ProductInInventory, DataDbContext>
    {
        Task<List<ProductInInventory>> ShowProductsInInventory(string id, byte[] error);
        Task<int> EditProductInInventory(ProductInInventory entity, byte[] error);
        Task<int> DeleteProductFromInventory(string iID, string pID, byte[] error);
    }
}
