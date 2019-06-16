using Erp.Data;
using Erp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IProcRequestRepo : IRepository<ProcRequest, AccountDbContext>
    {
        
    }
}
