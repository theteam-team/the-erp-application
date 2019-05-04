using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> ShowCompletedOrders(byte[] error);
        Task<List<Order>> ShowReadyOrders(byte[] error);
        Task<List<Order>> ShowOrdersInProgress(byte[] error);
    }
}
