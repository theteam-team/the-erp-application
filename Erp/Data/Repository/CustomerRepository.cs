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
    public class CustomerRepository : Repository<Customer, DataDbContext>, ICustomerRepository
    {
        public CustomerRepository(AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, accountDbContext, userManager)
        {

        }
        public async Task<List<Customer>> getCustomerById(string id, byte[] error)
        {
            List<Customer> customers = new List<Customer>();
            IntPtr CustomerPtr;
            await Task.Run(() =>
            {

                int number_fields = Accounting_Wrapper.getCustomerById(id, out CustomerPtr,  error);

                IntPtr current = CustomerPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    Customer customer = (Customer)Marshal.PtrToStructure(current, typeof(Customer));
                    current = (IntPtr)((long)current + Marshal.SizeOf(customer));
                    customers.Add(customer);
                }
                Marshal.FreeCoTaskMem(CustomerPtr);
            });
            return customers;
        }

        public async Task<List<Order>> getCustomerOrders(string id, byte[] error)
        {
            List<Order> orders = new List<Order>();
            IntPtr OrderPtr;
            await Task.Run(() =>
            {

                int number_fields = Accounting_Wrapper.getCustomerOrders(id, out OrderPtr, error);

                IntPtr current = OrderPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    Order order = (Order)Marshal.PtrToStructure(current, typeof(Order));
                    current = (IntPtr)((long)current + Marshal.SizeOf(order));
                    orders.Add(order);
                }
                Marshal.FreeCoTaskMem(OrderPtr);
            });
            return orders;
        }
    }
}
