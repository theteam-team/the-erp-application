using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Erp.Interfaces;
using Erp.Data.Entities;
using Erp.Data;
namespace Erp.Interfaces
{
    public interface IOrganizationRepository : IRepository<Organization, AccountDbContext>
    {
        Task<Organization> OraganizationExist(string organizationName);
    }
}
