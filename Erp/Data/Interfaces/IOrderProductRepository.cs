using Erp.Data;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IOrderProductRepository : IRepository<ProductInOrder, DataDbContext>
    {
        Task<int> EditProductInOrder(ProductInOrder entity, byte[] error);
        Task<List<ProductInOrder>> ShowProductsInOrder(string id, byte[] error);
        Task<int> DeleteProductFromOrder(string oID, string pID, byte[] error);
        Task<int> removeFromStock(ProductInOrder product, byte[] error);

        Task<int> AddPotentialProduct(ProductInOrder entity, byte[] error);
    }
}
