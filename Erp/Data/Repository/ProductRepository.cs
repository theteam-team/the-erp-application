using Erp.Interfaces;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.ModulesWrappers;
using Erp.Data;
using Microsoft.AspNetCore.Identity;

namespace Erp.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, userManager)
        {
           
        }
        public async Task<int> checkunitsInStock(string id, byte[] error)
        {
           return await Task.Run(()=> Warehouse_Wrapper.checkUnitsInStock(id, error));  
        }
        public async Task<int> addToStock(string id, int newUnits,byte[] error)
        {
            return await Task.Run(() => Warehouse_Wrapper.addToStock(id, newUnits, error));
        }
        public async Task<int> removeFromStock(string id, int newUnits,byte[] error)
        {
            return await Task.Run(() => Warehouse_Wrapper.removeFromStock(id, newUnits, error));
        }
        //public async Task<int> updateProductInfo(string id, string key, string value, byte[] error)
        //{
        //    //byte[] id = Encoding.ASCII.GetBytes(id);
        //    return await Task.Run(() => Warehouse_Wrapper.updateProductInfo(id, key, value, error));
        //}

    }
}
