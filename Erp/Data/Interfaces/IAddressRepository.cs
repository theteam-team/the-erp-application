using Erp.Data;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IAddressRepository : IRepository<Address, DataDbContext>
    {
        Task<int> AddCustomerAddress(Address entity, byte[] error);
    }
}
