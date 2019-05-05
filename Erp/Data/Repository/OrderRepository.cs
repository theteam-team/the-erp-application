using Erp.Data;
using Erp.Interfaces;
using Erp.Models;
using Erp.ModulesWrappers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Erp.Repository
{
    public class OrderRepository : Repository<Order> , IOrderRepository
    {
        public OrderRepository(AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, accountDbContext, userManager)
        {

        }

        public async Task<List<Order>> ShowCompletedOrders(byte[] error)
        {
            List<Order> orders = new List<Order>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.showCompletedOrders(out ProductPtr, error);

                IntPtr current = ProductPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    Order order = (Order)Marshal.PtrToStructure(current, typeof(Order));

                    current = (IntPtr)((long)current + Marshal.SizeOf(order));
                    orders.Add(order);
                }
                Marshal.FreeCoTaskMem(ProductPtr);
            });
            return orders;

        }

        public async Task<List<Order>> ShowReadyOrders(byte[] error)
        {
            List<Order> orders = new List<Order>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.showReadyOrders(out ProductPtr, error);

                IntPtr current = ProductPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    Order order = (Order)Marshal.PtrToStructure(current, typeof(Order));

                    current = (IntPtr)((long)current + Marshal.SizeOf(order));
                    orders.Add(order);
                }
                Marshal.FreeCoTaskMem(ProductPtr);
            });
            return orders;

        }

        public async Task<List<Order>> ShowOrdersInProgress(byte[] error)
        {
            List<Order> orders = new List<Order>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.showOrdersInProgress(out ProductPtr, error);

                IntPtr current = ProductPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    Order order = (Order)Marshal.PtrToStructure(current, typeof(Order));

                    current = (IntPtr)((long)current + Marshal.SizeOf(order));
                    orders.Add(order);
                }
                Marshal.FreeCoTaskMem(ProductPtr);
            });
            return orders;
        }
    }
}
