using Erp.Data;
using Erp.Data.Entities;
using Erp.Interfaces;
using Erp.Models;
using Erp.ModulesWrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Repository
{
    public class ReportRepository : Repository<Report, DataDbContext>, IReportRepository
    {
        public ReportRepository(IConfiguration config, ILogger<Repository.Repository<Report, DataDbContext>> ilogger, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {

        }

        public async Task<Report> Reporting(byte[] error)
        {
            IntPtr reportPtr;
            int status = 0;
            Report report = null;

            await Task.Run(() =>
            {
                try
                {

                    status = Warehouse_Wrapper.reporting(out reportPtr, error, _ConnectionString);
                    report = (Report)Marshal.PtrToStructure(reportPtr, typeof(Report));
                    Marshal.FreeCoTaskMem(reportPtr);
                }
                catch (Exception)
                {

                    
                }
            });
            Console.WriteLine("status = " + status);
            return (Report)(object)report;
        }
    }
}
