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
        public static extern int addInventory(Inventory inventory, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int addProduct(Product product, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int addOrder(Order order, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int addPotentialOrder(Order order, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int addPotentialProduct(ProductInOrder product, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int addPayment(Payment payment, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int addOrderPayment(Order order, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int addProductToOrder(ProductInOrder product, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int addProductToInventory(ProductInInventory product, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int editProduct(Product product, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int editOrder(Order order, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int editProductInOrder(ProductInOrder product, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int editProductInInventory(ProductInInventory product, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int deleteInventory(string id, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int deleteProduct(string id, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int deleteOrder(string id, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int deleteProductFromOrder(string oID, string pID, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int deleteProductFromInventory(string iID, string pID, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int addToStock(string id, int newUnits, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int addToOrderTotal(Order order, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int removeFromOrderTotal(Order order, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int removeFromStock(ProductInOrder product, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int showInventories(out IntPtr Inventory, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
         public static extern  int showProducts(out IntPtr Product, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int showAvailableProducts(out IntPtr Product, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int searchByCategory(out IntPtr Product, string value, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int searchInventories(out IntPtr Inventory, string key, string value, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int searchProducts(out IntPtr Product, string key, string value, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int searchOrders(out IntPtr Order, string key, string value, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int reporting(out IntPtr reportPtr, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getProductsMoves(out IntPtr ProductMoves, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int getAllProductInfo(string id, out IntPtr productPtr, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern  int getOrderInfo(string id, out IntPtr orderPtr, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int showAllOrders(out IntPtr order, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int showReceipts(out IntPtr order, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern  int showCompletedOrders(out IntPtr order, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int showCompletedReceipts(out IntPtr order, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int showReadyOrders(out IntPtr order, byte[] error, ConnectionString connectionString);
        
        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern  int showOrdersInProgress(out IntPtr order, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int showWaitingOrders(out IntPtr order, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int showWaitingReceipts(out IntPtr order, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern  int showProductsInOrder(string id, out IntPtr productInOrder, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int showCustomerProducts(string id, out IntPtr productInOrder, byte[] error, ConnectionString connectionString);

        [DllImport("Modules//WareHouseManagement//WareHouseManagement.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int showProductsInInventory(string id, out IntPtr productInInventory, byte[] error, ConnectionString connectionString);
    }
}
