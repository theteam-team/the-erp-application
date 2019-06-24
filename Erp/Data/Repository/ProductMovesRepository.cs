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
    public class ProductMovesRepository : Repository<ProductMoves, DataDbContext>, IProductMovesRepository
    {
        public ProductMovesRepository(IConfiguration config, ILogger<Repository.Repository<ProductMoves, DataDbContext>> ilogger, IHttpContextAccessor httpContextAccessor, AccountDbContext accountDbContext, Management management, DataDbContext datadbContext, UserManager<ApplicationUser> userManager) : base(config, ilogger, httpContextAccessor, management, datadbContext, accountDbContext, userManager)
        {

        }

        public async Task<List<ProductMoves>> GetProductsMoves(byte[] error)
        {
            List<ProductMoves> products = new List<ProductMoves>();
            IntPtr ProductPtr;
            await Task.Run(() =>
            {

                int number_fields = Warehouse_Wrapper.getProductsMoves(out ProductPtr, error, _ConnectionString);

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
