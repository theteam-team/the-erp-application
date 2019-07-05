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
    public class OrderRepository : Repository<Order, DataDbContext> , IOrderRepository
    {
        public OrderRepository(IConfiguration config, ILogger<Repository.Repository<Order, DataDbContext>> ilogger, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {
           
        }

        public async Task<List<Order>> SearchOrders(string key, string value, byte[] error)
        {
            List<Order> orders = new List<Order>();
            IntPtr OrderPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.searchOrders(out OrderPtr, key, value, error, _ConnectionString);

                IntPtr current = OrderPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    Order order = (Order)Marshal.PtrToStructure(current, typeof(Order));

                    current = (IntPtr)((long)current + Marshal.SizeOf(order));
                    orders.Add(order);
                }
                Marshal.FreeCoTaskMem(OrderPtr);
            });
            return (List<Order>)(object)orders;
        }

        public async Task<int> EditOrder(Order entity, byte[] error)
        {
            int status = 0;
            Order order = (Order)(object)(entity);
            status = await Task.Run(() => Warehouse_Wrapper.editOrder(order, error, _ConnectionString));
            return status;
        }

        public async Task<List<Order>> ShowAllOrders(byte[] error)
        {
            List<Order> orders = new List<Order>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.showAllOrders(out ProductPtr, error, _ConnectionString);

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

        public async Task<List<Order>> ShowReceipts(byte[] error)
        {
            List<Order> orders = new List<Order>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.showReceipts(out ProductPtr, error, _ConnectionString);

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

        public async Task<List<Order>> ShowCompletedOrders(byte[] error)
        {
            List<Order> orders = new List<Order>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.showCompletedOrders(out ProductPtr, error, _ConnectionString);

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

        public async Task<List<Order>> ShowCompletedReceipts(byte[] error)
        {
            List<Order> orders = new List<Order>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.showCompletedReceipts(out ProductPtr, error, _ConnectionString);

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

                int number_fields = Warehouse_Wrapper.showReadyOrders(out ProductPtr, error, _ConnectionString);

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

                int number_fields = Warehouse_Wrapper.showOrdersInProgress(out ProductPtr, error, _ConnectionString);

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

        public async Task<List<Order>> ShowWaitingOrders(byte[] error)
        {
            List<Order> orders = new List<Order>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.showWaitingOrders(out ProductPtr, error, _ConnectionString);

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

        public async Task<List<Order>> ShowWaitingReceipts(byte[] error)
        {
            List<Order> orders = new List<Order>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.showWaitingReceipts(out ProductPtr, error, _ConnectionString);

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

        public async Task<int> AddPotentialOrder(Order entity, byte[] error)
        {
            int status = 0;

            Order order = (Order)(object)(entity);
            status = await Task.Run(() => Warehouse_Wrapper.addPotentialOrder(order, error, _ConnectionString));

            return status;
        }

        public async Task<int> AddToOrderTotal(Order entity, byte[] error)
        {
            int status = 0;
            Order order = (Order)(object)(entity);
            status = await Task.Run(() => Warehouse_Wrapper.addToOrderTotal(order, error, _ConnectionString));
            return status;
        }

        public async Task<int> RemoveFromOrderTotal(Order entity, byte[] error)
        {
            int status = 0;
            Order order = (Order)(object)(entity);
            status = await Task.Run(() => Warehouse_Wrapper.removeFromOrderTotal(order, error, _ConnectionString));
            return status;
        }

        public async Task<int> AddOrderPayment(Order entity, byte[] error)
        {
            int status = 0;
            Order order = (Order)(object)(entity);
            status = await Task.Run(() => Warehouse_Wrapper.addOrderPayment(order, error, _ConnectionString));
            return status;
        }
    }
}
