using Erp.Models;
using Erp.ModulesWrappers;
using Erp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Erp.ViewModels.CRN_Tabels;

namespace Erp.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private Management _managment;

        //public Repository( )
        //{
        //   // _context = context;
        //}
        ////otected void Save() => _context.SaveChanges();

        //public int Count(Func<T, bool> predicate)
        //{
        //    //return _context.Set<T>().Where(predicate).Count();
        //}
        public Repository(Management management)
        {
            _managment = management;
        }
        public async Task<int> Create(T entity, byte[] error)
        {
            int status = 0;
            
            if (typeof(T) == typeof(Customer))
            {
                Customer customer = (Customer)(object)entity;
                status = await Task.Run( ()=> Crm_Wrapper.AddCustomer(customer , error));
            }
            if (typeof(T) == typeof(Employee))
            {
                Employee employee = (Employee)(object)entity;
                string role_id = await _managment.getRoleIdAsync(employee.role);
                employee.role = role_id;
                status = await Task.Run(() => Crm_Wrapper.AddEmployee(employee, error));
            }

            if (typeof(T) == typeof(Opportunities_product))
            {
                Opportunities_product opportunities_Product = (Opportunities_product)(object)entity;
                status = await Task.Run(() => Crm_Wrapper.AddOpportunity(opportunities_Product.Opportunities, error));
                if (status == 0)
                {
                    int numberOfProducts = opportunities_Product.product_id.Length;
                    status = await Task.Run(() => Crm_Wrapper.AddOpportunitie_detail(opportunities_Product.Opportunities.opportunity_id,
                        opportunities_Product.product_id, numberOfProducts, error));
                    string z = System.Text.Encoding.ASCII.GetString(error);
                    z.Remove(z.IndexOf('\0'));
                    if (status != 0)
                    {

                        return status;
                    }

                }
            }

            return status;
            
        }

        //public void Delete(T entity)
        //{

        //}

        //public IEnumerable<T> Find(Func<T, bool> predicate)
        //{

        //}

        //public IEnumerable<T> GetAll()
        //{

        //}

        public async Task <T> GetById(string id, byte[] error)
        {
            if (typeof(T) == typeof(Customer))
            {
                IntPtr customerPtr;
                int statusPtr = 0;
                Customer customer = null;
                await Task.Run(() =>
                {
                    Crm_Wrapper.getCustomerById(id, out customerPtr, out statusPtr, error);
                    customer = (Customer)Marshal.PtrToStructure(customerPtr, typeof(Customer));
                    Marshal.FreeCoTaskMem(customerPtr);
                });
                int status = statusPtr;
                Console.WriteLine("status = " + status);
                return (T)(object)customer ;
            }
            return null ;
            
        }

        //public void Update(T entity)
        //{

        //}
    }
}
