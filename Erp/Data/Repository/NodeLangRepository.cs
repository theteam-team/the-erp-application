using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Data;
using Erp.Data.Entities;
using Erp.Interfaces;
using Erp.Repository;
using Microsoft.AspNetCore.Identity;

namespace Erp.Repository
{
    public class NodeLangRepository : Repository<NodeLangWorkflow> , INodeLangRepository
    {
        private readonly Management _management;

        public NodeLangRepository(Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, userManager)
        {
            _management = management;
        }
    }
}
