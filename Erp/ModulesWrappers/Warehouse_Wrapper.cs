using Erp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Erp.ModulesWrappers
{
    public class Warehouse_Wrapper
    {
        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int addProduct(Product product, byte[] error);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int deleteProduct(string id, byte[] error);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int checkUnitsInStock(string id, byte[] error);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int addToStock(string id, int newUnits, byte[] error);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int removeFromStock(string id, int newUnits, byte[] error);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int updateProductInfo(string id, string key, string value,  byte[] error);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getAllProductInfo(string id, out IntPtr productPtr, byte[] error);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
         public static extern  IntPtr getProductInfo(string id, string key, StringBuilder result ,byte[] error);


        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
         public static extern  int showProducts(out IntPtr Product, byte[] error);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern  IntPtr getOrderInfo(string id, byte[] error);
        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern  int showCompletedOrders(out IntPtr order, byte[] error);
        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern  int showOrdersInProgress(out IntPtr order, byte[] error);
        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern  int showProductsInOrder(string id, out IntPtr productInOrder, byte[] error);


    }
}
