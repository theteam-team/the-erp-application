using Erp.Interfaces;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Erp.ModulesWrappers;
using Erp.Data;
using Microsoft.AspNetCore.Identity;
using Erp.Data.Entities;

namespace Erp.Repository
{
    public class ReportRepository : Repository<Report, DataDbContext>, IReportRepository
    {
        public ReportRepository(AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, accountDbContext, userManager)
        {

        }

        public async Task<Report> Reporting(byte[] error)
        {
            IntPtr reportPtr;
            int status = 0;
            Report report = null;

            await Task.Run(() =>
            {
                status = Warehouse_Wrapper.reporting(out reportPtr, error);
                report = (Report)Marshal.PtrToStructure(reportPtr, typeof(Report));
                Marshal.FreeCoTaskMem(reportPtr);
            });
            Console.WriteLine("status = " + status);
            return (Report)(object)report;
        }
    }
}
