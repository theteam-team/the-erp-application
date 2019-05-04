using Erp.Interfaces;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Erp.ModulesWrappers;

namespace Erp.Repository
{
    public class OrderProductRepository : Repository<ProductInOrder>, IOrderProductRepository
    {
        public OrderProductRepository(Management management) : base(management)
        {

        }

        public async Task<List<ProductInOrder>> ShowProductsInOrder(string id, byte[] error)
        {
            List<ProductInOrder> product_In_Orders = new List<ProductInOrder>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.showProductsInOrder(id, out ProductPtr, error);

                IntPtr current = ProductPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    ProductInOrder product_In_Order = (ProductInOrder)Marshal.PtrToStructure(current, typeof(ProductInOrder));

                    current = (IntPtr)((long)current + Marshal.SizeOf(product_In_Order));
                    product_In_Orders.Add(product_In_Order);
                }
                Marshal.FreeCoTaskMem(ProductPtr);
            });
            return product_In_Orders;
        }
    }
}
