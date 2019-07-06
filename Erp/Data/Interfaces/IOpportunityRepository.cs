using Erp.Data;
using Erp.Models;
using Erp.ViewModels.CRN_Tabels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IOpportunityRepository : IRepository<Opportunity, DataDbContext>
    {
        
    }
}
