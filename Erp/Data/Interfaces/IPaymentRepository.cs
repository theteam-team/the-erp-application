using Erp.Data;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment, DataDbContext>
    {
        Task<int> AddPayment(Payment entity, byte[] error);
    }
}
