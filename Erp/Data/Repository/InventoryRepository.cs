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
    public class InventoryRepository : Repository<Inventory, DataDbContext>, IInventoryRepository
    {
        public InventoryRepository(IConfiguration config, ILogger<InventoryRepository> ilogger, IHttpContextAccessor httpContextAccessor,  AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager)
            : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {
            

        }

        public async Task<List<Inventory>> SearchInventories(string key, string value, byte[] error)
        {
            List<Inventory> inventories = new List<Inventory>();
            IntPtr InventoryPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.searchInventories(out InventoryPtr, key, value, error, _ConnectionString);

                IntPtr current = InventoryPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    Inventory inventory = (Inventory)Marshal.PtrToStructure(current, typeof(Inventory));

                    current = (IntPtr)((long)current + Marshal.SizeOf(inventory));
                    inventories.Add(inventory);
                }
                Marshal.FreeCoTaskMem(InventoryPtr);
            });
            return (List<Inventory>)(object)inventories;
        }
    }
}

