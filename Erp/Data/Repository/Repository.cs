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
using Erp.Data;
using Erp.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Threading;

namespace Erp.Repository
{
    public class Repository<T, C> : IRepository<T, C> where T : class where C : DbContext
    {
        private AccountDbContext _accountdbContext;
        private readonly UserManager<ApplicationUser> _usermanager;
        private readonly DataDbContext _datadbContext;
        private Management _managment;
        protected ConnectionString _ConnectionString;
        private IConfiguration _config;
        private IHttpContextAccessor _httpContextAccessor;
        private IConfiguration config;
        private ILogger<InventoryRepository> ilogger;
        private IHttpContextAccessor httpContextAccessor;
        private Management management;
        private DataDbContext datadbContext;
        private AccountDbContext accountDbContext;
        private UserManager<ApplicationUser> userManager;

        public ClaimsPrincipal User { get; set; }


        public Repository(IConfiguration config , ILogger<Repository<T,C>> ilogger, IHttpContextAccessor httpContextAccessor, Management management, DataDbContext datadbContext, AccountDbContext accountdbContext
            , UserManager<ApplicationUser> userManager)
        {
           
            _config = config;
            _httpContextAccessor = httpContextAccessor;
            _accountdbContext = accountdbContext;
            _usermanager = userManager;
            _datadbContext = datadbContext;
            _managment = management;

            if (httpContextAccessor.HttpContext != null)
            {
                User = httpContextAccessor.HttpContext.User;
                ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
                
                if (User.Identity.IsAuthenticated)
                {
                    string databaseName = identity.FindFirst("database").Value;
                    User = httpContextAccessor.HttpContext.User;
                    setConnectionString(databaseName);
                }

            }
          
           

        }

        

        public async void setConnectionString(string databaseName)
        {
            Console.WriteLine("aadsad");
            _ConnectionString = new ConnectionString()
            {
                SERVER = _config["MySqlC++:server"],
                USER = _config["MySqlC++:user"],
                PORT = _config["MySqlC++:port"],
                PASSWORD = _config["MySqlC++:password"],
                DATABASE = databaseName,
            };
            
        }

        public async Task<int> Create(T entity, byte[] error)
        {
            int status = 0;

            if (typeof(T) == typeof(Inventory))
            {
                Inventory inventory = (Inventory)(object)(entity);
                //Console.WriteLine(_ConnectionString.DATABASE);
                status = await Task.Run(() => Warehouse_Wrapper.addInventory(inventory, error, _ConnectionString));
            }
            if (typeof(T) == typeof(ProductInInventory))
            {
                ProductInInventory product = (ProductInInventory)(object)(entity);
                status = await Task.Run(() => Warehouse_Wrapper.addProductToInventory(product, error, _ConnectionString));
            }
            if (typeof(T) == typeof(Product))
            {
                Product product = (Product)(object)(entity);
                status = await Task.Run(() => Warehouse_Wrapper.addProduct(product, error, _ConnectionString));
            }

            if (typeof(T) == typeof(Order))
            {
                Order order = (Order)(object)(entity);
                status = await Task.Run(() => Warehouse_Wrapper.addOrder(order, error, _ConnectionString));
            }

            if (typeof(T) == typeof(ProductInOrder))
            {
                ProductInOrder product = (ProductInOrder)(object)(entity);
                status = await Task.Run(() => Warehouse_Wrapper.addProductToOrder(product, error, _ConnectionString));
            }

            if (typeof(T) == typeof(Customer))
            {
                Customer customer = (Customer)(object)entity;
                if(_ConnectionString != null)
                    status = await Task.Run(() => Crm_Wrapper.AddCustomer(customer, error, _ConnectionString));
            }

            if (typeof(T) == typeof(Employee))
            {
                Employee employee = (Employee)(object)entity;
                string role_id = await _managment.getRoleIdAsync(employee.role);
                employee.role = role_id;
                status = await Task.Run(() => Crm_Wrapper.AddEmployee(employee, error, _ConnectionString));
            }

            if (typeof(T) == typeof(Opportunities_product))
            {
                Opportunities_product opportunities_Product = (Opportunities_product)(object)entity;
                status = await Task.Run(() => Crm_Wrapper.AddOpportunity(opportunities_Product.Opportunities, error, _ConnectionString));
                Console.WriteLine("status= " + status);
                if (status == 0)
                {
                    int numberOfProducts = opportunities_Product.product_id.Length;
                    status = await Task.Run(() => Crm_Wrapper.AddOpportunitie_detail(opportunities_Product.Opportunities.opportunity_id,
                        opportunities_Product.product_id, numberOfProducts, error, _ConnectionString));
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

        /*protected void InitiateConnection()
        {
            string username = "admin";
            if (User != null)
                username = User.Identity.Name;
            var user = _accountdbContext.ErpUsers.Where(us => us.UserName == username).FirstOrDefault();
            if (user != null)
            {
                if (!CommonNeeds.checkdtb(_datadbContext, user.DatabaseName))
                {
                    throw new Exception("Error Please DataBase Does Not Exist");
                }
            }
        }*/

        public async Task Create(T entity)
        {
            
            if (typeof(C) == (typeof(AccountDbContext)))
            {
                //await Task.Run(() => InitiateConnection());
                _accountdbContext.Add(entity);
                _accountdbContext.SaveChanges();
            }
        }

        public async Task<int> Delete(string id, byte[] error)
        {
            int status = 10;

            if (typeof(T) == typeof(Inventory))
            {
                status = await Task.Run(() => Warehouse_Wrapper.deleteInventory(id, error, _ConnectionString));
            }
            if (typeof(T) == typeof(Product))
            {
                status = await Task.Run(() => Warehouse_Wrapper.deleteProduct(id, error, _ConnectionString));
            }
            if (typeof(T) == typeof(Order))
            {
                status = await Task.Run(() => Warehouse_Wrapper.deleteOrder(id, error, _ConnectionString));
            }
            return status;

        }

        public async Task<List<T>> GetAll(byte[] error)
        {
            if (typeof(T) == typeof(Inventory))
            {
                List<Inventory> inventories = new List<Inventory>();
                IntPtr InventoryPtr;

                await Task.Run(() =>
                {
                    int number_fields = Warehouse_Wrapper.showInventories(out InventoryPtr, error, _ConnectionString);
                    IntPtr current = InventoryPtr;

                    for (int i = 0; i < number_fields; ++i)
                    {
                        Inventory inventory = (Inventory)Marshal.PtrToStructure(current, typeof(Inventory));

                        current = (IntPtr)((long)current + Marshal.SizeOf(inventory));
                        inventories.Add(inventory);
                    }
                    Marshal.FreeCoTaskMem(InventoryPtr);
                });
                return (List<T>)(object)inventories;
            }

            if (typeof(T) == typeof(Product))
            {
                List<Product> products = new List<Product>();
                IntPtr ProductPtr;

                await Task.Run(() =>
                {
                    int number_fields = Warehouse_Wrapper.showProducts(out ProductPtr, error, _ConnectionString);
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

            if (typeof(T) == typeof(Order))
            {
                List<Order> orders = new List<Order>();
                IntPtr OrderPtr;

                await Task.Run(() =>
                {
                    int number_fields = Warehouse_Wrapper.showAllOrders(out OrderPtr, error, _ConnectionString);
                    IntPtr current = OrderPtr;

                    for (int i = 0; i < number_fields; ++i)
                    {
                        Order order = (Order)Marshal.PtrToStructure(current, typeof(Order));

                        current = (IntPtr)((long)current + Marshal.SizeOf(order));
                        orders.Add(order);
                    }
                    Marshal.FreeCoTaskMem(OrderPtr);
                });
                return (List<T>)(object)orders;
            }
            return null;
        }
        public async Task<List<T>> GetAll()
        {
            if (typeof(C) == (typeof(AccountDbContext)))
            {
                //await Task.Run(() => InitiateConnection());
                return _accountdbContext.Set<T>().ToList();
            }
            return null;
        }


        public async Task<T> GetById(string id, byte[] error)
        {
            if (typeof(T) == typeof(Customer))
            {
                Customer customer = null;

                await Task.Run(() =>
                {
                    IntPtr customerPtr = Crm_Wrapper.getCustomerById(id, error, _ConnectionString);
                    customer = (Customer)Marshal.PtrToStructure(customerPtr, typeof(Customer));
                    Marshal.FreeCoTaskMem(customerPtr);
                });
                //Console.WriteLine("status = " + status);
                return (T)(object)customer;
            }

            if (typeof(T) == typeof(Order))
            {
                IntPtr orderPtr;
                int status = 0;
                Order order = null;

                await Task.Run(() =>
                {
                    status = Warehouse_Wrapper.getOrderInfo(id, out orderPtr, error, _ConnectionString);
                    order = (Order)Marshal.PtrToStructure(orderPtr, typeof(Order));
                    Marshal.FreeCoTaskMem(orderPtr);
                });
                Console.WriteLine("status = " + status);
                return (T)(object)order;
            }

            if (typeof(T) == typeof(Product))
            {
                IntPtr prodductPtr;
                int status = 0;
                Product product = null;

                await Task.Run(() =>
                {
                    status = Warehouse_Wrapper.getAllProductInfo(id, out prodductPtr, error, _ConnectionString);
                    product = (Product)Marshal.PtrToStructure(prodductPtr, typeof(Product));
                    Marshal.FreeCoTaskMem(prodductPtr);
                });
                Console.WriteLine("status = " + status);
                return (T)(object)product;
            }
            return null;

        }
        /*public virtual async Task<List<T>> GetAll()
        {
            if (typeof(C) == typeof(DataDbContext))
            {
                await Task.Run(() => InitiateConnection());
                return _datadbContext.Set<T>().ToList();
            }
            if (typeof(C) == typeof(AccountDbContext))
            {
                return _datadbContext.Set<T>().ToList();
            }
            return null;
        }*/
        public virtual async Task<T> GetById(object id)
        {
            if (typeof(C) == typeof(AccountDbContext))
            {
                return _accountdbContext.Find<T>(id);
            }
            return null;
        }

        public virtual async Task Update(T ob)
        {
            //if (typeof(C) == typeof(DataDbContext))
            //{
            //    //await Task.Run(() => InitiateConnection());
            //    _datadbContext.Update<T>(ob);
            //    _datadbContext.SaveChanges();
            //}
            if (typeof(C) == typeof(AccountDbContext))
            {
                _accountdbContext.Update<T>(ob);
                _accountdbContext.SaveChanges();
            }
        }

        public virtual async Task Insert(T ob)
        {
            //if (typeof(C) == typeof(DataDbContext))
            //{
            //    //await Task.Run(() => InitiateConnection());
            //    _datadbContext.Add(ob);
            //    _datadbContext.SaveChanges();
            //}
            if (typeof(C) == typeof(AccountDbContext))
            {
                _accountdbContext.Add(ob);
                _accountdbContext.SaveChanges();
            }
        }
    }
}

