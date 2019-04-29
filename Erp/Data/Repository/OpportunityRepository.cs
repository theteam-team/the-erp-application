using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Models;
using Erp.Interfaces;
using Erp.ViewModels.CRN_Tabels;

namespace Erp.Repository
{
    public class OpportunityRepository : Repository<Opportunities_product>, IOpportunityRepository
    {
        public OpportunityRepository(Management management):base(management)
        {

        }
    }
}

