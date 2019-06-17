using Erp.Data;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee, DataDbContext>
    {
    }
}
