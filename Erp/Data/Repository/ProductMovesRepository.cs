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
    public class ProductMovesRepository : Repository<ProductMoves, DataDbContext>, IProductMovesRepository
    {
        public ProductMovesRepository(AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(management, datadbContext, accountDbContext, userManager)
        {

        }

        public async Task<List<ProductMoves>> GetProductsMoves(byte[] error)
        {
            List<ProductMoves> products = new List<ProductMoves>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.getProductsMoves(out ProductPtr, error);

                IntPtr current = ProductPtr;
                for (int i = 0; i < number_fields; ++i)
                {
                    ProductMoves product = (ProductMoves)Marshal.PtrToStructure(current, typeof(ProductMoves));

                    current = (IntPtr)((long)current + Marshal.SizeOf(product));
                    products.Add(product);
                }
                Marshal.FreeCoTaskMem(ProductPtr);
            });
            return (List<ProductMoves>)(object)products;
        }
    }
}
