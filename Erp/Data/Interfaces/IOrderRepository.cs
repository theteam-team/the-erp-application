using Erp.Data;
using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Interfaces
{
    public interface IOrderRepository : IRepository<Order, DataDbContext>
    {
        Task<List<Order>> SearchOrders(string key, string value, byte[] error);
        Task<int> EditOrder(Order entity, byte[] error);
        Task<List<Order>> ShowAllOrders(byte[] error);
        Task<List<Order>> ShowReceipts(byte[] error);
        Task<List<Order>> ShowCompletedOrders(byte[] error);
        Task<List<Order>> ShowCompletedReceipts(byte[] error);
        Task<List<Order>> ShowReadyOrders(byte[] error);
        Task<List<Order>> ShowOrdersInProgress(byte[] error);
        Task<List<Order>> ShowWaitingOrders(byte[] error);
        Task<List<Order>> ShowWaitingReceipts(byte[] error);

        Task<int> AddPotentialOrder(Order entity, byte[] error);
        Task<int> AddToOrderTotal(Order entity, byte[] error);
        Task<int> RemoveFromOrderTotal(Order entity, byte[] error);
        Task<int> AddOrderPayment(Order entity, byte[] error);
    }
}
