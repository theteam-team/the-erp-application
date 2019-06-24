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
    public class InventoryProductRepository : Repository<ProductInInventory, DataDbContext>, IInventoryProductRepository
    {
        private AccountDbContext _accountdbContext;
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly DataDbContext _datadbContext;
        private Management _managment;

        public InventoryProductRepository(IConfiguration config, ILogger<InventoryProductRepository> ilogger, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager)
            : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {
            _accountdbContext = accountDbContext;
            _usermanager = userManager;
            _datadbContext = datadbContext;
            _managment = management;
        }

        public async Task<List<ProductInInventory>> ShowProductsInInventory(string id, byte[] error)
        {
            List<ProductInInventory> productsInInventory = new List<ProductInInventory>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.showProductsInInventory(id, out ProductPtr, error, _ConnectionString);

                IntPtr current = ProductPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    ProductInInventory productInInventory = (ProductInInventory)Marshal.PtrToStructure(current, typeof(ProductInInventory));

                    current = (IntPtr)((long)current + Marshal.SizeOf(productInInventory));
                    productsInInventory.Add(productInInventory);
                }
                Marshal.FreeCoTaskMem(ProductPtr);
            });
            return productsInInventory;
        }
        public async Task<List<ProductInInventory>> ShowProductsInInventory()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    await Task.Run(() => InitiateConnection());
            //    return _datadbContext.Set<ProductInInventory>().ToList();
            //}
            return null;
        }

        public async Task<int> EditProductInInventory(ProductInInventory entity, byte[] error)
        {
            int status = 0;
            ProductInInventory product = (ProductInInventory)(object)(entity);
            status = await Task.Run(() => Warehouse_Wrapper.editProductInInventory(product, error, _ConnectionString));
            return status;
        }

        public async Task<int> DeleteProductFromInventory(string iID, string pID, byte[] error)
        {
            int status = 10;
            status = await Task.Run(() => Warehouse_Wrapper.deleteProductFromInventory(iID, pID, error, _ConnectionString));
            return status;
        }

    }
}






