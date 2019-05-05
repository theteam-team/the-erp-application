﻿using Erp.Models;
using Erp.ModulesWrappers;
using Erp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Erp.ViewModels.CRN_Tabels;
using Erp.ModulesWrappers;
using System.Text;
using Erp.Data;
using Erp.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Erp.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private AccountDbContext _accountdbContext;
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly DataDbContext _datadbContext;
        private Management _managment;

        public ClaimsPrincipal User { get;  set; }

       
        public Repository(Management management, DataDbContext datadbContext, AccountDbContext accountdbContext
            , UserManager<ApplicationUser> userManager)
        {
            _accountdbContext = accountdbContext;
            _usermanager = userManager;
            _datadbContext = datadbContext;
            _managment = management;
        }
        public async Task<int> Create(T entity, byte[] error)
        {
            int status = 0;
            if (typeof(T) == typeof(NodeLangWorkflow))
            {
                
            }
            if (typeof(T) == typeof(Product))
            {
                Product product = (Product)(object)(entity);
                status = await Task.Run(() => Warehouse_Wrapper.addProduct(product, error));
            }
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
                Console.WriteLine("status= " +  status);
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

        protected void InitiateConnection()
        {
            var user = _accountdbContext.ErpUsers.Where(us => us.UserName == User.Identity.Name).First();
            if (!CommonNeeds.checkdtb(_datadbContext, user.DatabaseName))
            {
                throw new Exception("Error Please DataBase Does Not Exist");
            }
        }

        public async Task Create(T entity)
        {
            await Task.Run(()=>InitiateConnection());
            _datadbContext.Add(entity);
            _datadbContext.SaveChanges();
        }
        public async Task<int> Delete(string id, byte[] error)
        {
            int status = 10;
            if (typeof(T) == typeof(Product))
            {
                
                status = await Task.Run(() => Warehouse_Wrapper.deleteProduct(id, error));
            }
            return status;


        }public async Task<int> Delete(T entity, byte[] error)
        {
            int status = 10;
            if (typeof(T) == typeof(Product))
            {
                Product product = (Product)(object)(entity);
                status = await Task.Run(() => Warehouse_Wrapper.deleteProduct(product.id , error));
            }
            return status;

        }
        public async Task<List<T>> GetAll(byte[] error)
        {
            if (typeof(T) == typeof(Product))
            {
                List<Product> products = new List<Product>();
                IntPtr ProductPtr;
                await Task.Run(() =>
                {   
                    
                    int number_fields = Warehouse_Wrapper.showProducts(out ProductPtr, error);
                   
                    IntPtr current = ProductPtr;
                    for (int i = 0; i < number_fields; ++i)
                    {
                        Product product = (Product)Marshal.PtrToStructure(current, typeof(Product));
                       
                        current = (IntPtr)((long)current + Marshal.SizeOf(product));
                        products.Add(product);
                    }
                    Marshal.FreeCoTaskMem(ProductPtr);
                });
                return (List<T>)(object)products;
            }
            
            return null;
        }
        public async Task<List<T>> GetAll()
        {
            if (User.Identity.IsAuthenticated)
            {
                await Task.Run(() => InitiateConnection());
                return _datadbContext.Set<T>().ToList();
            }
            return null;
        }


        public async Task <T> GetById(string id, byte[] error)
        {
            if (typeof(T) == typeof(Customer))
            { 
                Customer customer = null;
                await Task.Run(() =>
                {
                    IntPtr customerPtr =  Crm_Wrapper.getCustomerById(id,  error);
                    customer = (Customer)Marshal.PtrToStructure(customerPtr, typeof(Customer));
                    Marshal.FreeCoTaskMem(customerPtr);
                });
                //Console.WriteLine("status = " + status);
                return (T)(object)customer ;
            }
            if (typeof(T) == typeof(Order))
            {
                IntPtr orderPtr = await Task.Run(() => Warehouse_Wrapper.getOrderInfo(id, error));
                Order order = (Order)Marshal.PtrToStructure(orderPtr, typeof(Order));
                Marshal.FreeCoTaskMem(orderPtr);
                return (T)(object)(order);
            }
            if (typeof(T) == typeof(Product))
            {
                IntPtr prodductPtr;
                int status = 0;
                Product product = null;
                await Task.Run(() =>
                {
                    status = Warehouse_Wrapper.getAllProductInfo(id, out prodductPtr, error);
                    product = (Product)Marshal.PtrToStructure(prodductPtr, typeof(Product));
                    Marshal.FreeCoTaskMem(prodductPtr);
                });
                Console.WriteLine("status = " + status);
                return (T)(object)product;
            }
            return null ;
            
        }
        public async Task<T> GetById(object id)
        {
            await Task.Run(() => InitiateConnection());
            return _datadbContext.Find<T>(id);
        }
        public async Task<int> UpdateInfo(string id, string key, string value, byte[] error)
        {
            int status = 0;
            if (typeof(T) == typeof(Product))
            {
                return await Task.Run(() => Warehouse_Wrapper.updateProductInfo(id, key, value, error));
            }
            return status;
        }
        public async Task<string> getInfo(string id, string key, byte[] error)
        {
            
            if (typeof(T) == typeof(Product))
            {
                
                StringBuilder sb = new StringBuilder(256);
                IntPtr z = await Task.Run(() => Warehouse_Wrapper.getProductInfo(id, key, sb,error));
                string x = Marshal.PtrToStringAnsi(z);
                Console.Write(x);
                return x;
            }
            return null;
        }
    }
}
