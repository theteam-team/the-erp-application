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
    public class PaymentRepository : Repository<Payment, DataDbContext>, IPaymentRepository
    {
        public PaymentRepository(IConfiguration config, ILogger<Repository.Repository<Payment, DataDbContext>> ilogger, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {

        }

        public async Task<int> AddPayment(Payment entity, byte[] error)
        {
            int status = 0;

            Payment payment = (Payment)(object)(entity);
            status = await Task.Run(() => Warehouse_Wrapper.addPayment(payment, error, _ConnectionString));

            return status;
        }
    }
}
