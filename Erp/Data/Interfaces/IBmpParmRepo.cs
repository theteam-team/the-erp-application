using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Data;
using Erp.Data.Entities;
namespace Erp.Interfaces
{
    public interface IBmpParmRepo : IRepository<BpmWorkflowParameters, AccountDbContext> 
    {
        
    }
}
