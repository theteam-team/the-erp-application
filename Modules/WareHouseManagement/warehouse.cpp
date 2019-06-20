#include "pch.h"
#include "Header.h"
#include <iostream>
#include <cstdio>
#include <fstream>
#include <sstream>
#include <string.h>
#include <cstdlib>
#include <conio.h>
#include "DatabaseEntities.h"
#include <mysql.h>
#include <vector>
#include <map>

#pragma warning(disable : 4996)
using namespace std;

/*#define SERVER "localhost"
#define USER "root" //your username
#define PASSWORD "rana" //your password for mysql
#define DATABASE "erp" //database name
#define SERVER "mysqldbaws.cwhgjrqrh1zu.us-east-2.rds.amazonaws.com"
#define USER "mySQLadmin" //your username
#define PASSWORD "mySQLpass123" //your password for mysql
#define DATABASE "ERP" //database name*/

int status;
int qstate;

MYSQL* conn;
MYSQL_ROW row;
MYSQL_RES *res;

MYSQL_ROW tempRow;
MYSQL_RES* tempRes;

MYSQL_ROW tempRow2;
MYSQL_RES* tempRes2;

class db_response {

public:
	static void ConnectionFunction(char * error, ConnectionString con) {

		conn = mysql_init(0);
		cout << con.DATABASE;

		conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);
		if (!conn) {
			
			cout << "Failed To Connect!" << mysql_errno(conn) << endl;
			string err = (string)"Connection to database has failed!" + mysql_error(conn);;
			strcpy_s(error, err.length() + 1, err.c_str());
			status = 3;
		}
	}
};

int addToCategory(char* pid, char* cid, char* error, ConnectionString con) {

	status = 0;
	db_response::ConnectionFunction(error, con);

	if (conn)
	{
		string query = (string)"insert into category values ('" + pid + "', '" + cid + "')";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}

int deleteFromCategory(char* pid, char* error, ConnectionString con) {

	status = 0;
	db_response::ConnectionFunction(error, con);

	if (conn)
	{
		string query = (string)"delete from product_has_category where Product_Product_ID = '" + pid + "'";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}

map<char*, int> checkCategory(char* pid, char* error) {

	status = 0;
	int numberOfRows = 0;
	unsigned int numOfFields;
	map<char*, int> categories;

	categories["sold"] = 0;
	categories["purchased"] = 0;

	//db_response::ConnectionFunction(error);

	if (conn) {

		mysql_free_result(tempRes2);

		string query = (string)"select Category_Category_ID from product_has_category where Product_Product_ID = '" + pid + "'";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);

		if (checkQuery(qstate, error)) {

			tempRes2 = mysql_store_result(conn);

			if (tempRes2->row_count > 0)
			{
				unsigned int i = 0;
				while (tempRow2 = mysql_fetch_row(tempRes2)) {
					if (tempRow2[0] == "1")
						categories["sold"] = 1;
					if (tempRow2[0] == "2")
						categories["purchased"] = 1;
				}
			}
			else
			{
				string s = "No categories for this product";
				cout << s << endl;
				strcpy_s(error, s.length() + 1, s.c_str());
				status = 2;
			}
		}
	}
	return categories;
}

int checkUnitsInStock(char* id, char* error, ConnectionString con) {

	status = 0;
	db_response::ConnectionFunction(error, con);

	if (conn)
	{
		string query = (string)"select Units_In_Stock from product where Product_ID = '" + id + "'";
		cout << query << endl;
		char const *q = query.c_str();
		mysql_free_result(res);
		qstate = mysql_query(conn, q);

		if (checkQuery(qstate, error))
		{
			res = mysql_store_result(conn);
			if (res->row_count > 0)
			{
				row = mysql_fetch_row(res);
				status = 0;
				return stoi(row[0]);
			}
			else
			{
				string s = "Error This Product id does not exixt";
				cout << s << endl;
				strcpy_s(error, s.length() + 1, s.c_str());
				status = 2;
				mysql_close(conn);
			}
		}
	}
	return status;
}
int deleteAll(char* id, char* error, ConnectionString con) {

	status = 0;
	db_response::ConnectionFunction(error, con);

	if (conn)
	{
		string query = (string)"delete from order_has_product where Order_Order_ID = '" + id + "'";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}
extern "C"	ERP_API int addToStock(char* id, int newUnits, char* error, ConnectionString con) {

	
	int units = 0;
	status = 0;
	units = checkUnitsInStock(id, error, con);

	db_response::ConnectionFunction(error, con);

	if (status == 0)
	{
		cout << newUnits << endl;
		units += newUnits;
		string query = "update product set Units_In_Stock = " + to_string(units) + " where Product_ID = '" + id + "'";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}
extern "C"	ERP_API int removeFromStock(ProductInOrder* product, char* error, ConnectionString con) {
	int units;
	status = 0;
	units = checkUnitsInStock(product->productID, error, con);
	units -= product->unitsOrdered;

	db_response::ConnectionFunction(error, con);
	
	if (status == 0)
	{
		string query = "update product set Units_In_Stock = " + to_string(units) + " where Product_ID = '" + product->productID + "'";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}
extern "C"	ERP_API int addInventory(Inventory* inventory, char* error, ConnectionString con)
{
	status = 0;
	db_response::ConnectionFunction(error, con);

	if (conn) {

		string query = (string) "insert into inventory values ('" + inventory->id + "', '" + inventory->governorate + "', '" + inventory->city + "', '" + inventory->street + "', " + to_string(inventory->length) + ", " + to_string(inventory->width) + ", " + to_string(inventory->height) + ")";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}
extern "C"	ERP_API int addProduct(Product* product, char* error,  ConnectionString con)
{
	status = 0;
	db_response::ConnectionFunction(error, con);

	if (conn) {
		
		string query = (string) "insert into product values ('" + product->id + "', '" + product->name + "', '" + product->description + "', " + to_string(product->price) + ", " + to_string(product->weight) + ", " + to_string(product->length) + ", " + to_string(product->width) + ", " + to_string(product->height) + ", " + to_string(product->unitsInStock) + ")";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);

		if (product->sold == 1)
			addToCategory(product->id, "1", error, con);
		if (product->purchased == 1)
			addToCategory(product->id, "2", error, con);
	}
	return status;
}
extern "C"	ERP_API int addOrder(Order* order, char* error, ConnectionString con)
{
	status = 0;
	db_response::ConnectionFunction(error, con);

	if (conn) {

		string query = (string) "insert into"+ con.DATABASE+".order values ('" + order->id + "', '" + order->requiredDate + "', '" + order->completedDate + "', '" + order->orderStatus + "', '" + order->customerID + "', '" + order->paymentID + "')";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}
extern "C" ERP_API int addProductToOrder(ProductInOrder* product, char* error, ConnectionString con)
{
	status = 0;
	db_response::ConnectionFunction(error, con);

	if (conn) {

		string query = (string) "insert into order_has_product values ('" + product->orderID + "', '" + product->productID + "', " + to_string(product->unitsOrdered) + ", " + to_string(product->unitsDone) + ")";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}
extern "C" ERP_API int addProductToInventory(ProductInInventory* product, char* error, ConnectionString con)
{
	status = 0;
	db_response::ConnectionFunction(error, con);

	if (conn) {

		string query = (string) "insert into inventory_has_product values ('" + product->inventoryID + "', '" + product->productID + "', '" + product->position + "', " + to_string(product->unitsInInventory) + ")";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}
extern "C" ERP_API int editProduct(Product* product, char* error,  ConnectionString con)
{
	status = 0;
	db_response::ConnectionFunction(error, con);

	if (conn) {
        string query = (string) "update product set Product_Name = '" + product->name + "', Product_Description = '" + product->description + "', " + "', Product_Price = " + to_string(product->price) + ", Product_Weight = " + to_string(product->weight) + ", length = " + to_string(product->length) + ", width = " + to_string(product->width) + ", Product_height = " + to_string(product->height) + ", Units_In_Stock = " + to_string(product->unitsInStock) + " where Product_ID = '" + product->id + "'";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);

		deleteFromCategory(product->id, error, con);
		if (product->sold == 1)
			addToCategory(product->id, "1", error, con);
		if (product->purchased == 1)
			addToCategory(product->id, "2", error, con);
	}
	return status;
}
extern "C" ERP_API int editOrder(Order* order, char* error, ConnectionString con)
{
	status = 0;
	db_response::ConnectionFunction(error, con);

	if (conn) {
		string query = (string) "update"+ con.DATABASE+".order set Order_Required_Date = '" + order->requiredDate + "', Order_Status = '" + order->orderStatus + "', Customer_Customer_ID = '" + order->customerID + "', Payment_Payment_ID = '" + order->paymentID + "' where Order_ID = '" + order->id + "'";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}
extern "C" ERP_API int editProductInOrder(ProductInOrder* product, char* error, ConnectionString con)
{
	status = 0;
	db_response::ConnectionFunction(error, con);

	if (conn) {
		string query = (string) "update order_has_product set Units_In_Order = " + to_string(product->unitsOrdered) + ", Units_Done = " + to_string(product->unitsDone)  + " where Order_Order_ID = '" + product->orderID + "' and Product_Product_ID = '" + product->productID + "'";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}
extern "C" ERP_API int editProductInInventory(ProductInInventory* product, char* error, ConnectionString con)
{
	status = 0;
	db_response::ConnectionFunction(error, con);

	if (conn) {
		string query = (string) "update inventory_has_product set Units_In_Inventory = " + to_string(product->unitsInInventory) + " where Product_Product_ID = '" + product->productID + "'";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}
extern "C"	ERP_API int deleteProduct(char* id, char* error, ConnectionString con) {
	status = 0;
	deleteFromCategory(id, error, con);

	db_response::ConnectionFunction(error, con);

	if (conn)
	{
		string query = (string)"delete from inventory_has_product where Product_Product_ID = '" + id + "'";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}

	db_response::ConnectionFunction(error, con);

	if (conn) 
	{
		string query = (string)"delete from product where Product_ID = '" + id + "'";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}
extern "C"	ERP_API int deleteOrder(char* id, char* error,  ConnectionString con) {

	//deleteAll(id, error);

	status = 0;
	/*db_response::ConnectionFunction(error);

	if (conn)
	{
		string query = (string)"delete from"+ con.DATABASE+".order where Order_ID = '" + id + "'";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);

	}*/
	return status;
}
extern "C"	ERP_API int deleteInventory(char* id, char* error, ConnectionString con) {

	status = 0;
	db_response::ConnectionFunction(error, con);

	if (conn)
	{
		string query = (string)"delete from inventory_has_product where Inventory_Inventory_ID = '" + id + "'";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}

	status = 0;
	db_response::ConnectionFunction(error, con);

	if (conn)
	{
		string query = (string)"delete from inventory where Inventory_ID = '" + id + "'";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);

	}
	return status;
}
extern "C"	ERP_API int deleteProductFromOrder(char* oID, char* pID, char* error, ConnectionString con) {
	status = 0;
	db_response::ConnectionFunction(error, con);

	if (conn)
	{
		string query = (string)"delete from order_has_product where Order_Order_ID = '" + oID + "' and Product_Product_ID = '" + pID + "'";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}
extern "C"	ERP_API int deleteProductFromInventory(char* iID, char* pID, char* error, ConnectionString con) {
	status = 0;
	db_response::ConnectionFunction(error, con);

	if (conn)
	{
		string query = (string)"delete from inventory_has_product where Inventory_Inventory_ID = '" + iID + "' and Product_Product_ID = '" + pID + "'";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}
extern "C"	ERP_API int searchByCategory(Product** product, char* id, char* error, ConnectionString con)
{
	status = 0;
	int numberOfRows = 0;
	unsigned int numOfFields;
	//vector<string> ids;

	db_response::ConnectionFunction(error, con);

	if (conn) {

		mysql_free_result(res);

		string query = (string) "select Product_Product_ID from product_has_category where Category_Category_ID = '" + id + "'";
		cout << query << endl;
		char const *q = query.c_str();

		qstate = mysql_query(conn, q);

		if (checkQuery(qstate, error)) {

			res = mysql_store_result(conn);

			if (res->row_count > 0)
			{
				*product = (Product*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Product));
				Product *_product = *product;

				while (row = mysql_fetch_row(res)) {

					mysql_free_result(tempRes);

					query = (string)"select * from product where Product_ID = '" + row[0] + "'";
					cout << query << endl;
					q = query.c_str();

					qstate = mysql_query(conn, q);
					if (checkQuery(qstate, error)) {
						tempRes = mysql_store_result(conn);

						if (tempRes->row_count > 0)
						{
							cout << "here";
							status = 0;
							numOfFields = mysql_num_fields(tempRes);
							tempRow = mysql_fetch_row(tempRes);

							map<char*, int> categories;

							_product->id = tempRow[0];
							tempRow[1] ? _product->name = tempRow[1] : _product->name = nullptr;
							tempRow[2] ? _product->description = tempRow[2] : _product->description = nullptr;
							tempRow[3] ? _product->price = stod(tempRow[3]) : _product->price = 0;
							tempRow[4] ? _product->weight = stod(tempRow[4]) : _product->weight = 0;
							tempRow[5] ? _product->length = stod(tempRow[5]) : _product->length = 0;
							tempRow[6] ? _product->width = stod(tempRow[6]) : _product->width = 0;
							tempRow[7] ? _product->height = stod(tempRow[7]) : _product->height = 0;
							tempRow[8] ? _product->unitsInStock = stoi(tempRow[8]) : _product->unitsInStock = 0;

							categories = checkCategory(tempRow[0], error);
							_product->sold = categories["sold"];
							_product->purchased = categories["purchased"];

							numberOfRows++;
							_product++;
						}

					}
				}
			}
		}
	    else
		{
			string s = "Category does not Exist";
			cout << s << endl;
			strcpy_s(error, s.length() + 1, s.c_str());
			status = 2;
		}

	}
	return numberOfRows;
}
extern "C"	ERP_API int searchProducts(Product** product, char* key, char* value, char* error , ConnectionString con)
 {
	 status = 0;
	 int numberOfRows = 0;
	 unsigned int numOfFields;

	 db_response::ConnectionFunction(error, con);

	 if (conn) {

		 mysql_free_result(res);

		 string query = (string) "select * from product where " + key + " = '" + value + "'";
		 cout << query << endl;
		 char const *q = query.c_str();

		 qstate = mysql_query(conn, q);

		 if (checkQuery(qstate, error)) {

			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *product = (Product*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Product));
				 cout << res->row_count << endl;
				 numOfFields = mysql_num_fields(res);

				 Product *_product = *product;
				 map <char*, int> categories;

				 while (row = mysql_fetch_row(res)) {

					 _product->id = row[0];
					 row[1] ? _product->name = row[1] : _product->name = nullptr;
					 row[2] ? _product->description = row[2] : _product->description = nullptr;
					 row[3] ? _product->price = stod(row[3]) : _product->price = 0;
					 row[4] ? _product->weight = stod(row[4]) : _product->weight = 0;
					 row[5] ? _product->length = stod(row[5]) : _product->length = 0;
					 row[6] ? _product->width = stod(row[6]) : _product->width = 0;
					 row[7] ? _product->height = stod(row[7]) : _product->height = 0;
					 row[8] ? _product->unitsInStock = stoi(row[8]) : _product->unitsInStock = 0;

					 categories = checkCategory(row[0], error);
					 _product->sold = categories["sold"];
					 _product->purchased = categories["purchased"];

					 numberOfRows++;
					 _product++;
				 }
			 }
			 else
			 {
				 string s = "No Products Found";
				 cout << s << endl;
				 strcpy_s(error, s.length() + 1, s.c_str());
				 status = 2;
			 }
		 }
	 }
	 return numberOfRows;
 }
extern "C"	ERP_API int searchOrders(Order** order, char* key, char* value, char* error, ConnectionString con)

 {
	 status = 0;
	 int numberOfRows = 0;
	 unsigned int numOfFields;

	 db_response::ConnectionFunction(error, con);

	 if (conn) {

		 mysql_free_result(res);

		 string query = (string) "select * from " + con.DATABASE + ".order where " + key + " = '" + value + "'";
		 cout << query << endl;
		 char const *q = query.c_str();

		 qstate = mysql_query(conn, q);

		 if (checkQuery(qstate, error)) {

			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *order = (Order*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Order));
				 cout << res->row_count << endl;
				 numOfFields = mysql_num_fields(res);

				 Order *_order = *order;
				 while (row = mysql_fetch_row(res)) {

					 _order->id = row[0];
					 row[1] ? _order->requiredDate = row[1] : _order->requiredDate = nullptr;
					 row[2] ? _order->completedDate = row[2] : _order->completedDate = "Not Completed";
					 row[3] ? _order->orderStatus = row[3] : _order->orderStatus = nullptr;
					 row[4] ? _order->customerID = row[4] : _order->customerID = nullptr;
					 row[5] ? _order->paymentID = row[5] : _order->paymentID = nullptr;
					 row[6] ? _order->shipmentID = row[6] : _order->shipmentID = nullptr;

					 numberOfRows++;
					 _order++;
				 }
			 }
			 else
			 {
				 string s = "No Orders Found";
				 cout << s << endl;
				 strcpy_s(error, s.length() + 1, s.c_str());
				 status = 2;
			 }
		 }
	 }
	 return numberOfRows;
 }
extern "C"	ERP_API int searchInventories(Inventory** inventory, char* key, char* value, char* error, ConnectionString con)
 {
	 status = 0;
	 int numberOfRows = 0;
	 unsigned int numOfFields;

	 db_response::ConnectionFunction(error, con);

	 if (conn) {

		 mysql_free_result(res);

		 string query = (string) "select * from inventory where " + key + " = '" + value + "'";
		 cout << query << endl;
		 char const *q = query.c_str();

		 qstate = mysql_query(conn, q);

		 if (checkQuery(qstate, error)) {

			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *inventory = (Inventory*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Inventory));
				 cout << res->row_count << endl;
				 numOfFields = mysql_num_fields(res);

				 Inventory *_inventory = *inventory;
				 while (row = mysql_fetch_row(res)) {

					 _inventory->id = row[0];
					 row[2] ? _inventory->governorate = row[2] : _inventory->governorate = 0;
					 row[3] ? _inventory->city = row[3] : _inventory->city = 0;
					 row[4] ? _inventory->street = row[4] : _inventory->street = 0;
					 row[5] ? _inventory->length = stod(row[5]) : _inventory->length = 0;
					 row[6] ? _inventory->width = stod(row[6]) : _inventory->width = 0;
					 row[7] ? _inventory->height = stod(row[7]) : _inventory->height = 0;

					 numberOfRows++;
					 _inventory++;
				 }
			 }
			 else
			 {
				 string s = "No Inventories Found";
				 cout << s << endl;
				 strcpy_s(error, s.length() + 1, s.c_str());
				 status = 2;
			 }
		 }
	 }
	 return numberOfRows;
 }
extern "C"	ERP_API int getAllProductInfo(char* id, Product** product, char* error, ConnectionString con) {
//

	status = 0;
	db_response::ConnectionFunction(error, con);
	if (conn)
	{
		unsigned int numOfFields;
		string query = (string)"select * from product where Product_ID = '" + id + "'";
		cout << query << endl;
		char const *q = query.c_str();

		mysql_free_result(res);

		qstate = mysql_query(conn, q);
		if (checkQuery(qstate, error)) {
			res = mysql_store_result(conn);
			if (res->row_count > 0)
			{
				cout << "here";
				status = 0;
				numOfFields = mysql_num_fields(res);
				row = mysql_fetch_row(res);
				*product = (Product*)CoTaskMemAlloc(sizeof(Product));
				Product *_product = *product;
				map <char*, int> categories;
				
				_product->id = row[0];
				row[1] ? _product->name = row[1] : _product->name = nullptr;
				row[2] ? _product->description = row[2] : _product->description = nullptr;
				row[3] ? _product->price = stod(row[3]) : _product->price = 0;
				row[4] ? _product->weight = stod(row[4]) : _product->weight = 0;
				row[5] ? _product->length = stod(row[5]) : _product->length = 0;
				row[6] ? _product->width = stod(row[6]) : _product->width = 0;
				row[7] ? _product->height = stod(row[7]) : _product->height = 0;
				row[8] ? _product->unitsInStock = stoi(row[8]) : _product->unitsInStock = 0;

				categories = checkCategory(row[0], error);
				_product->sold = categories["sold"];
				_product->purchased = categories["purchased"];
			}
			else
			{
				string s = "Error This Product ID does not exist";
				cout << s << endl;
				strcpy_s(error, s.length() + 1, s.c_str());
				status = 2;
			}
		}
	}
	return status;
}
extern "C"	ERP_API int getOrderInfo(char* id, Order** order, char* error, ConnectionString con) {
//

	 status = 0;
	 db_response::ConnectionFunction(error, con);
	 if (conn)
	 {
		 unsigned int numOfFields;
		 string query = (string)"select * from " + con.DATABASE + ".order where Order_ID = '" + id + "'";

		 cout << query << endl;

		 char const *q = query.c_str();
		 mysql_free_result(res);
		 qstate = mysql_query(conn, q);

		 if (checkQuery(qstate, error))
		 {
			 res = mysql_store_result(conn);
			 if (res->row_count > 0)
			 {
				 cout << "here";
				 status = 0;
				 numOfFields = mysql_num_fields(res);
				 row = mysql_fetch_row(res);

				 *order = (Order*)CoTaskMemAlloc(sizeof(Order));
				 Order* _order = *order;

				 _order->id = row[0];
				 row[1] ? _order->requiredDate = row[1] : _order->requiredDate = nullptr;
				 row[2] ? _order->completedDate = row[2] : _order->completedDate = "Not Completed";
				 row[3] ? _order->orderStatus = row[3] : _order->orderStatus = nullptr;
				 row[4] ? _order->customerID = row[4] : _order->customerID = nullptr;
				 row[5] ? _order->paymentID = row[5] : _order->paymentID = nullptr;
				 row[6] ? _order->shipmentID = row[6] : _order->shipmentID = nullptr;

				 cout << "here" << endl;

			 }
			 else
			 {
				 string s = "Error This order ID does not exixt";
				 cout << s << endl;
				 strcpy_s(error, s.length() + 1, s.c_str());
				 status = 2;
			 }
		 }

	 }
	 return status;
 }
extern "C"	ERP_API int showProducts(Product** product, char* error, ConnectionString con)
//
 {
	 status = 0;
	 int numberOfRows = 0;
	 unsigned int numOfFields;

	 db_response::ConnectionFunction(error, con);

	 if (conn) {

		 mysql_free_result(res);

		 qstate = mysql_query(conn, "select * from product");
		 cout << "select * from product" << endl;
		 if (checkQuery(qstate, error)) {

			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *product = (Product*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Product));
				 cout << res->row_count << endl;
				 numOfFields = mysql_num_fields(res);

				 Product *_product = *product;
				 map <char*, int> categories;

				 while (row = mysql_fetch_row(res)) {
					 
					 _product->id = row[0];
					 row[1] ? _product->name = row[1] : _product->name = nullptr;
					 row[2] ? _product->description = row[2] : _product->description = nullptr;
					 row[3] ? _product->price = stod(row[3]) : _product->price = 0;
					 row[4] ? _product->weight = stod(row[4]) : _product->weight = 0;
					 row[5] ? _product->length = stod(row[5]) : _product->length = 0;
					 row[6] ? _product->width = stod(row[6]) : _product->width = 0;
					 row[7] ? _product->height = stod(row[7]) : _product->height = 0;
					 row[8] ? _product->unitsInStock = stoi(row[8]) : _product->unitsInStock = 0;

					 categories = checkCategory(row[0], error);
					 _product->sold = categories["sold"];
					 _product->purchased = categories["purchased"];

					 numberOfRows++;
					 _product++;
				 }
			 }
			 else
			 {
				 string s = "No Products Found";
				 cout << s << endl;
				 strcpy_s(error, s.length() + 1, s.c_str());
				 status = 2;
			 }
		 }
	 }
	 return numberOfRows;
}
extern "C"	ERP_API int showInventories(Inventory** inventory, char* error, ConnectionString con)
 {
	 status = 0;
	 int numberOfRows = 0;
	 unsigned int numOfFields;
	 db_response::ConnectionFunction(error, con);

	 if (conn) {

		 mysql_free_result(res);

		 qstate = mysql_query(conn, "select * from inventory");
		 cout << "select * from inventory" << endl;
		 if (checkQuery(qstate, error)) {

			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *inventory = (Inventory*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Inventory));
				 cout << res->row_count << endl;
				 numOfFields = mysql_num_fields(res);

				 Inventory *_inventory = *inventory;

				 while (row = mysql_fetch_row(res)) {

					 _inventory->id = row[0];
					 row[1] ? _inventory->governorate = row[1] : _inventory->governorate = 0;
					 row[2] ? _inventory->city = row[2] : _inventory->city = 0;
					 row[3] ? _inventory->street = row[3] : _inventory->street = 0;
					 row[4] ? _inventory->length = stod(row[4]) : _inventory->length = 0;
					 row[5] ? _inventory->width = stod(row[5]) : _inventory->width = 0;
					 row[6] ? _inventory->height = stod(row[6]) : _inventory->height = 0;

					 numberOfRows++;
					 _inventory++;
				 }
			 }
			 else
			 {
				 string s = "No Inventories yet";
				 cout << s << endl;
				 strcpy_s(error, s.length() + 1, s.c_str());
				 status = 2;
			 }
		 }
	 }
	 return numberOfRows;
 }
extern "C"	ERP_API int showProductsInInventory(char* id, ProductInInventory** product, char* error , ConnectionString con) {

	 status = 0;
	 int numberOfRows = 0;
	 unsigned int numOfFields;

	 db_response::ConnectionFunction(error, con);

	 if (conn) {

		 mysql_free_result(res);

		 string query = (string) "select * from inventory_has_product where Inventory_Inventory_ID = '" + id + "'";
		 cout << query << endl;
		 char const *q = query.c_str();
		 qstate = mysql_query(conn, q);

		 if (checkQuery(qstate, error)) {

			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *product = (ProductInInventory*)CoTaskMemAlloc((int)(res->row_count) * sizeof(ProductInInventory));
				 ProductInInventory *_product = *product;

				 while (row = mysql_fetch_row(res)) {

					 _product->productID = row[1];
					 row[2] ? _product->position = row[2] : _product->position = nullptr;
					 row[3] ? _product->unitsInInventory = stoi(row[3]) : _product->unitsInInventory = 0;

					 query = (string)"select * from product where Product_ID = '" + row[1] + "'";
					 cout << query << endl;
					 q = query.c_str();

					 qstate = mysql_query(conn, q);
					 if (checkQuery(qstate, error)) {
						 tempRes = mysql_store_result(conn);

						 if (tempRes->row_count > 0)
						 {
							 cout << "here";
							 status = 0;
							 numOfFields = mysql_num_fields(tempRes);
							 tempRow = mysql_fetch_row(tempRes);

							 tempRow[1] ? _product->name = tempRow[1] : _product->name = nullptr;
							 tempRow[4] ? _product->weight = stod(tempRow[4]) : _product->weight = 0;
							 tempRow[5] ? _product->length = stod(tempRow[5]) : _product->length = 0;
							 tempRow[6] ? _product->width = stod(tempRow[6]) : _product->width = 0;
							 tempRow[7] ? _product->height = stod(tempRow[7]) : _product->height = 0;

						 }
					 }
					 numberOfRows++;
					 _product++;
				 }
			 }
			 else
			 {
				 string s = "No products in inventory";
				 cout << s << endl;
				 strcpy_s(error, s.length() + 1, s.c_str());
				 status = 2;
			 }
		 }
	 }
	 return numberOfRows;
 }
extern "C"	ERP_API int showAllOrders(Order** order, char* error, ConnectionString con) {

	 status = 0;
	 int numberOfRows = 0;
	 unsigned int numOfFields;
	 db_response::ConnectionFunction(error, con);
	 if (conn) {

		 mysql_free_result(res);
		 string query = (string)"select * from " + con.DATABASE + ".order";
		 qstate = mysql_query(conn, query.c_str());
		 cout << query << endl;
		 if (checkQuery(qstate, error))
		 {
			 res = mysql_store_result(conn);
			 
			 if (res->row_count > 0)
			 {
				 *order = (Order*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Order));
				 numOfFields = mysql_num_fields(res);
				 cout << res->row_count << endl;

				 Order *_order = *order;

				 while (row = mysql_fetch_row(res)) {

					 _order->id = row[0];
					 row[1] ? _order->requiredDate = row[1] : _order->requiredDate = nullptr;
					 row[2] ? _order->completedDate = row[2] : _order->completedDate = nullptr;
					 row[3] ? _order->orderStatus = row[3] : _order->orderStatus = nullptr;
					 row[4] ? _order->customerID = row[4] : _order->customerID = nullptr;
					 row[5] ? _order->paymentID = row[5] : _order->paymentID = nullptr;
					 row[6] ? _order->shipmentID = row[6] : _order->shipmentID = nullptr;

					 numberOfRows++;
					 _order++; 
				 }
				 cout << "here" << endl;
			 }
		 }
	 }
	 return numberOfRows;
 }
extern "C"	ERP_API int showCompletedOrders(Order** order, char* error, ConnectionString con) {
//

	 status = 0;
	 int numberOfRows = 0;
	 unsigned int numOfFields;
	 db_response::ConnectionFunction(error, con);
	 if (conn) {

		 mysql_free_result(res);
		 string query = (string)"select * from " + con.DATABASE + ".order where Order_Status = \"Done\"";
		 qstate = mysql_query(conn, query.c_str());
		 cout << query << endl;

		 if (checkQuery(qstate, error))
		 {
			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *order = (Order*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Order));
				 numOfFields = mysql_num_fields(res);
				 cout << res->row_count << endl;

				 Order *_order = *order;

				 while (row = mysql_fetch_row(res)) {

					 _order->id = row[0];
					 row[1] ? _order->requiredDate = row[1] : _order->requiredDate = nullptr;
					 row[2] ? _order->completedDate = row[2] : _order->completedDate = "Not Completed";
					 row[3] ? _order->orderStatus = row[3] : _order->orderStatus = nullptr;
					 row[4] ? _order->customerID = row[4] : _order->customerID = nullptr;
					 row[5] ? _order->paymentID = row[5] : _order->paymentID = nullptr;
					 row[6] ? _order->shipmentID = row[6] : _order->shipmentID = nullptr;

					 numberOfRows++;
					 _order++;
				 }
				 cout << "here" << endl;
			 }
		 }
	 }
	 return numberOfRows;
}
extern "C"	ERP_API int showReadyOrders(Order** order, char* error, ConnectionString con) {
//

	 status = 0;
	 int numberOfRows = 0;
	 unsigned int numOfFields;
	 db_response::ConnectionFunction(error, con);
	 if (conn) {

		 mysql_free_result(res);
		 string query = (string)"select * from " + con.DATABASE + ".order where Order_Status = \"Ready\"";
		 qstate = mysql_query(conn, query.c_str());
		 cout << query << endl;
		 

		 if (checkQuery(qstate, error))
		 {
			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *order = (Order*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Order));
				 numOfFields = mysql_num_fields(res);
				 cout << res->row_count << endl;

				 Order *_order = *order;

				 while (row = mysql_fetch_row(res)) {

					 _order->id = row[0];
					 row[1] ? _order->requiredDate = row[1] : _order->requiredDate = nullptr;
					 row[2] ? _order->completedDate = row[2] : _order->completedDate = "Not Completed";
					 row[3] ? _order->orderStatus = row[3] : _order->orderStatus = nullptr;
					 row[4] ? _order->customerID = row[4] : _order->customerID = nullptr;
					 row[5] ? _order->paymentID = row[5] : _order->paymentID = nullptr;
					 row[6] ? _order->shipmentID = row[6] : _order->shipmentID = nullptr;

					 numberOfRows++;
					 _order++;
				 }
				 cout << "here" << endl;
			 }
		 }
	 }
	 return numberOfRows;
 }
extern "C"	ERP_API int showOrdersInProgress(Order** order, char* error, ConnectionString con) {
//

	 status = 0;
	 int numberOfRows = 0;
	 unsigned int numOfFields;
	 db_response::ConnectionFunction(error, con);
	 if (conn) {

		 mysql_free_result(res);
		
		 string query = (string)"select * from " + con.DATABASE + ".order where Order_Status = \"In Progress\"";
		 qstate = mysql_query(conn, query.c_str());
		 cout << query << endl;
		 

		 if (checkQuery(qstate, error))
		 {
			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *order = (Order*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Order));
				 numOfFields = mysql_num_fields(res);
				 cout << res->row_count << endl;

				 Order *_order = *order;

				 while (row = mysql_fetch_row(res)) {

					 _order->id = row[0];
					 row[1] ? _order->requiredDate = row[1] : _order->requiredDate = nullptr;
					 row[2] ? _order->completedDate = row[2] : _order->completedDate = "Not Completed";
					 row[3] ? _order->orderStatus = row[3] : _order->orderStatus = nullptr;
					 row[4] ? _order->customerID = row[4] : _order->customerID = nullptr;
					 row[5] ? _order->paymentID = row[5] : _order->paymentID = nullptr;
					 row[6] ? _order->shipmentID = row[6] : _order->shipmentID = nullptr;

					 numberOfRows++;
					 _order++;
				 }
				 cout << "here" << endl;
			 }
		 }
	 }
	 return numberOfRows;
}
extern "C"	ERP_API int showProductsInOrder(char* id, ProductInOrder** product, char* error, ConnectionString con) {
//

	 status = 0;
	 int numberOfRows = 0;
	 unsigned int numOfFields;

	 db_response::ConnectionFunction(error, con);
	 if (conn) {

		 mysql_free_result(res);

		 string query = (string)"select * from order_has_product where Order_Order_ID = '" + id + "'";
		 qstate = mysql_query(conn, query.c_str());
		 cout << query << endl;
		 if (checkQuery(qstate, error)) {

			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *product = (ProductInOrder*)CoTaskMemAlloc((int)(res->row_count) * sizeof(ProductInOrder));
				 cout << res->row_count << endl;
				 numOfFields = mysql_num_fields(res);

				 ProductInOrder *_product = *product;
				 while (row = mysql_fetch_row(res)) {

					 _product->orderID = row[0];
					 _product->productID = row[1];
					 _product->unitsOrdered = stoi(row[2]);
					 _product->unitsDone = stoi(row[3]);
					 numberOfRows++;
					 _product++;
				 }
				 cout << "here" << endl;
			 }
		 }
	 }
	 return numberOfRows;
}
/*
 extern "C"	ERP_API double makeOrder(char* id, char *error) {

	MYSQL_ROW r;
	double price, total = 0;
	int units, unitsInOrder;
	string productID;

	string query = (string)"select Product_Product_ID, Units_In_Order from order_has_product where Order_Order_ID = " + id;
	char const *q = query.c_str();

	mysql_free_result(res);

	qstate = mysql_query(conn, q);
	res = mysql_store_result(conn);

	while (row = mysql_fetch_row(res)) {

		productID = row[0];
		units = stoi(row[1]);

		string query = "select Units_In_Stock, Product_Price from product where Product_ID = " + productID;
		char const *q = query.c_str();

		qstate = mysql_query(conn, q);
		res = mysql_store_result(conn);
		r = mysql_fetch_row(res);

		price = stod(r[1]);

		if (stoi(r[0]) >= units) 
			unitsInOrder = units;

		else
			unitsInOrder = stoi(r[0]);

		total = price * unitsInOrder;
		char* _productId = "";
		strcpy_s(_productId, productID.length() + 1, productID.c_str());
		removeFromStock(_productId, unitsInOrder, error);
	}

	return total;
}

*/
bool checkQuery(int qstate,  char * error)
 {
	 if (qstate)
	 {
		 cout << "Query failed: " << mysql_error(conn) << endl;
		 status = 2;
		 string s = mysql_error(conn);
		 strcpy_s(error, s.length() + 1, mysql_error(conn));
		 return false;
	 }
	 else
	 {
		 cout << "Query succeeded" << endl;
		 status = 0;
		 string s = mysql_error(conn);
		 //strcpy_s(error, s.length() + 1, mysql_error(conn));
		 return true;

	 }
 }