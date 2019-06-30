using Erp.Data;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IReportRepository : IRepository<Report, DataDbContext>
    {
        Task<Report> Reporting(byte[] error);
    }
}
