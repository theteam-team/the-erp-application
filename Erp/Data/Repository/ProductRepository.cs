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
    public class ProductRepository : Repository<Product, DataDbContext>, IProductRepository
    {
        public ProductRepository(AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, accountDbContext, userManager)
        {

        }

        public async Task<List<Product>> SearchProducts(string key, string value, byte[] error)
        {
            List<Product> products = new List<Product>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.searchProducts(out ProductPtr, key, value, error);

                IntPtr current = ProductPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    Product product = (Product)Marshal.PtrToStructure(current, typeof(Product));

                    current = (IntPtr)((long)current + Marshal.SizeOf(product));
                    products.Add(product);
                }
                Marshal.FreeCoTaskMem(ProductPtr);
            });
            return (List<Product>)(object)products;
        }

        public async Task<int> EditProduct(Product entity, byte[] error)
        {
            int status = 0;
            Product product = (Product)(object)(entity);
            status = await Task.Run(() => Warehouse_Wrapper.editProduct(product, error));
            return status;
        }

        public async Task<int> addToStock(string id, int newUnits, byte[] error)
        {
            return await Task.Run(() => Warehouse_Wrapper.addToStock(id, newUnits, error));
        }

        public async Task<List<ProductSold>> getSoldProduct(byte[] error)
        {
            List<ProductSold> products = new List<ProductSold>();
            IntPtr ProductPtr;

            await Task.Run(() =>
            {
                int number_fields = Accounting_Wrapper.getProfit(out ProductPtr, error);
                IntPtr current = ProductPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    ProductSold product = (ProductSold)Marshal.PtrToStructure(current, typeof(ProductSold));

                    current = (IntPtr)((long)current + Marshal.SizeOf(product));
                    products.Add(product);
                }
                Marshal.FreeCoTaskMem(ProductPtr);
            });
            return products;

        }

        public async Task<List<Invoice>> getInvoice(byte[] error)
        {
            List<Invoice> invoices = new List<Invoice>();
            IntPtr InvoicePtr;

            await Task.Run(() =>
            {
                int number_fields = Accounting_Wrapper.getInvoice(out InvoicePtr, error);
                IntPtr current = InvoicePtr;

                for (int i = 0; i < number_fields; ++i)
                {
                    Invoice invoice = (Invoice)Marshal.PtrToStructure(current, typeof(Invoice));

                    current = (IntPtr)((long)current + Marshal.SizeOf(invoice));
                    invoices.Add(invoice);
                }
                Marshal.FreeCoTaskMem(InvoicePtr);
            });
            return invoices;

        }

        //public async Task<int> updateProductInfo(string id, string key, string value, byte[] error)
        //{
        //    //byte[] id = Encoding.ASCII.GetBytes(id);
        //    return await Task.Run(() => Warehouse_Wrapper.updateProductInfo(id, key, value, error));
        //}

    }
}
