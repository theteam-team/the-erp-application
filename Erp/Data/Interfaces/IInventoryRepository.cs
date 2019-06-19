using Erp.Data;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IInventoryRepository : IRepository<Inventory, DataDbContext>
    {
        Task<List<Inventory>> SearchInventories(string key, string value, byte[] error);
    }
}
