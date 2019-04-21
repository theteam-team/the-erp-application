﻿using Erp.Interfaces;
using Erp.Models;
using Erp.ModulesWrappers;
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
        public OrderRepository(Management management) : base(management)
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

        public async Task<List<Product_In_Order>> ShowProductsInOrder(string id, byte[] error)
        {
            List<Product_In_Order> product_In_Orders = new List<Product_In_Order>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.showProductsInOrder(id ,out ProductPtr, error);

                IntPtr current = ProductPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    Product_In_Order product_In_Order = (Product_In_Order)Marshal.PtrToStructure(current, typeof(Product_In_Order));
  
                    current = (IntPtr)((long)current + Marshal.SizeOf(product_In_Order));
                    product_In_Orders.Add(product_In_Order);
                }
                Marshal.FreeCoTaskMem(ProductPtr);
            });
            return product_In_Orders;
        }
    }
}