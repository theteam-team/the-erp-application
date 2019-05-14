using Erp.Interfaces;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Erp.ModulesWrappers;
using Erp.Data;
using Microsoft.AspNetCore.Identity;

namespace Erp.Repository
{
    public class OrderProductRepository : Repository<ProductInOrder>, IOrderProductRepository
    {
        public OrderProductRepository(AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, accountDbContext, userManager)
        {

        }

        public async Task<List<ProductInOrder>> ShowProductsInOrder(string id, byte[] error)
        {
            List<ProductInOrder> productsInOrder = new List<ProductInOrder>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.showProductsInOrder(id, out ProductPtr, error);

                IntPtr current = ProductPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    ProductInOrder productInOrder = (ProductInOrder)Marshal.PtrToStructure(current, typeof(ProductInOrder));

                    current = (IntPtr)((long)current + Marshal.SizeOf(productInOrder));
                    productsInOrder.Add(productInOrder);
                }
                Marshal.FreeCoTaskMem(ProductPtr);
            });
            return productsInOrder;
        }

        public async Task<int> EditProductInOrder(ProductInOrder entity, byte[] error)
        {
            int status = 0;
            ProductInOrder product = (ProductInOrder)(object)(entity);
            status = await Task.Run(() => Warehouse_Wrapper.editProductInOrder(product, error));
            return status;
        }

        public async Task<int> DeleteProductFromOrder(string oID, string pID, byte[] error)
        {
            int status = 10;
            status = await Task.Run(() => Warehouse_Wrapper.deleteProductFromOrder(oID, pID, error));
            return status;
        }

        public async Task<int> removeFromStock(ProductInOrder product, byte[] error)
        {
            return await Task.Run(() => Warehouse_Wrapper.removeFromStock(product, error));
        }
    }
}
