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
using Erp.Data.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Erp.Repository
{
    public class OrderProductRepository : Repository<ProductInOrder, DataDbContext>, IOrderProductRepository
    {
        private AccountDbContext _accountdbContext;
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly DataDbContext _datadbContext;
        private Management _managment;

        public OrderProductRepository(IConfiguration config, ILogger<OrderProductRepository> ilogger, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {
            _accountdbContext = accountDbContext;
            _usermanager = userManager;
            _datadbContext = datadbContext;
            _managment = management;
           
        }
        

        public async Task<List<ProductInOrder>> ShowProductsInOrder(string id, byte[] error)
        {
            List<ProductInOrder> productsInOrder = new List<ProductInOrder>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.showProductsInOrder(id, out ProductPtr, error, _ConnectionString);

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
        public async Task<List<ProductInOrder>> ShowProductsInOrder()
        {
           
            return null;
        }

        public async Task<List<ProductInOrder>> ShowCustomerProducts(string id, byte[] error)
        {
            List<ProductInOrder> productsInOrder = new List<ProductInOrder>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.showCustomerProducts(id, out ProductPtr, error, _ConnectionString);

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
            status = await Task.Run(() => Warehouse_Wrapper.editProductInOrder(product, error, _ConnectionString));
            return status;
        }

        public async Task<int> DeleteProductFromOrder(string oID, string pID, byte[] error)
        {
            int status = 10;
            status = await Task.Run(() => Warehouse_Wrapper.deleteProductFromOrder(oID, pID, error, _ConnectionString));
            return status;
        }

        public async Task<int> removeFromStock(ProductInOrder product, byte[] error)
        {
            return await Task.Run(() => Warehouse_Wrapper.removeFromStock(product, error, _ConnectionString));
        }

        public async Task<int> AddPotentialProduct(ProductInOrder entity, byte[] error)
        {
            int status = 0;

            ProductInOrder product = (ProductInOrder)(object)(entity);
            status = await Task.Run(() => Warehouse_Wrapper.addPotentialProduct(product, error, _ConnectionString));

            return status;
        }
    }
}
