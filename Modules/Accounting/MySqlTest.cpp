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
#pragma warning(disable : 4996)
using namespace std;

#define SERVER "localhost"
#define USER "root" //your username
#define PASSWORD "0198484014###" //your password for mysql
#define DATABASE "company1" //database name

MYSQL* conn;
MYSQL_ROW row;
MYSQL_RES *res;
int status;
int qstate;
class db_response {

public:
	static void ConnectionFunction(char* error) {

		conn = mysql_init(0);
		conn = mysql_real_connect(conn, SERVER, USER, PASSWORD, DATABASE, 3306, NULL, 0);
		if (!conn) {

			cout << "Failed To Connect!" << mysql_errno(conn) << endl;
			string err = (string)"Connection to database has failed!" + mysql_error(conn);;
			strcpy_s(error, err.length() + 1, err.c_str());
			status = 3;
		}
	}
};
string query;

extern "C" ERP_API int getProfit(ProductSold** product, char* error) {
	status = 0;
	int numberOfRows = 0;
	unsigned int numOfFields;
	db_response::ConnectionFunction(error);
	if (conn) {
		mysql_free_result(res);
		//for each sold product, calculate its profit --> Units_In_Order *( product_price - Produt_cost),,,,,, then get summation of all profits
		query = "SELECT Product_ID, sum(Units_In_Order) as Units_In_Order, Product_Cost, Product_Price,sum(Units_In_Order * (Product_Price - Product_Cost)) AS Profit FROM product, order_has_product WHERE(product.product_id = order_has_product.Product_Product_ID) group by Product_ID";
		qstate = mysql_query(conn, query.c_str());
		cout << query << endl;
		if (checkQuery(qstate, error)) {
			res = mysql_store_result(conn);
			if (res->row_count > 0)
			{
				*product = (ProductSold*)CoTaskMemAlloc((int)(res->row_count) * sizeof(ProductSold));
				cout << res->row_count << endl;
				numOfFields = mysql_num_fields(res);
				ProductSold* _product = *product;
				while (row = mysql_fetch_row(res)) {
					_product->id = row[0];
					row[1] ? _product->unitsSold = stoi(row[1]) : _product->unitsSold = 0;
					row[2] ? _product->cost = stod(row[2]) : _product->cost = 0.0;
					row[3] ? _product->price = stod(row[3]) : _product->price = 0.0;
					row[4] ? _product->profit = stod(row[4]) : _product->profit = 0.0;
					numberOfRows++;
					_product++;
				}
			}
			else
			{
				string s = "No Product Exist";
				cout << s << endl;
				strcpy_s(error, s.length() + 1, s.c_str());
				status = 2;
			}
		}
	}
	return numberOfRows;
}
extern "C" ERP_API int getInvoice(Invoice** invoice, char* error) {
	status = 0;
	int numberOfRows = 0;
	unsigned int numOfFields;
	db_response::ConnectionFunction(error);
	if (conn) {
		mysql_free_result(res);
		//for each sold product, calculate its profit --> Units_In_Order *( product_price - Produt_cost),,,,,, then get summation of all profits
		query = "SELECT  Supplier_ID, Supplier_Name,  Supplier_Phone_Number,Supplier_Email, Supplier_Payment_method, Product_Name,Product_Cost, Units_Supplied, (Units_Supplied * (select Product_Cost from product where product_id = product_has_supplier.Product_Product_ID)) as total_cost,paid_up, (Units_Supplied * (select Product_Cost from product where product_id = product_has_supplier.Product_Product_ID) - Paid_up) As depts from supplier, product_has_supplier, product where (Supplier_ID = product_has_supplier.Supplier_Supplier_ID AND product_id = product_has_supplier.Product_Product_ID) order by supplier_id ";
		qstate = mysql_query(conn, query.c_str());
		cout << query << endl;
		if (checkQuery(qstate, error)) {
			res = mysql_store_result(conn);
			if (res->row_count > 0)
			{
				*invoice = (Invoice*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Invoice));
				cout << res->row_count << endl;
				numOfFields = mysql_num_fields(res);
				Invoice* _invoice = *invoice ;
				while (row = mysql_fetch_row(res)) {
					_invoice->suppId = row[0];
					row[1] ? _invoice-> suppName = row[1] : _invoice->suppName = 0;
					row[2] ? _invoice-> suppPhone = stoi(row[2]) : _invoice->suppPhone = 0;
					row[3] ? _invoice->suppMail = row[3] : _invoice->suppMail = 0;
					row[4] ? _invoice->payment_method = row[4] : _invoice->payment_method = 0;
					row[5] ? _invoice->productName = row[5] : _invoice->productName = 0;
					row[6] ? _invoice->productCost = stod(row[6]) : _invoice->productCost = 0;
					row[7] ? _invoice->suppUnits = stoi(row[7]) : _invoice->suppUnits = 0;
					row[8] ? _invoice->totalCost= stod(row[8]) : _invoice->totalCost = 0;
					row[9] ? _invoice->totalPaid = stod(row[9]) : _invoice->totalPaid= 0;
					row[10] ? _invoice->depts = stod(row[10]) : _invoice->depts = 0; 
					/*row[1] ? _product->name = row[1] : _product->name = nullptr;
					row[2] ? _product->description = row[2] : _product->description = nullptr;
					row[3] ? _product->position = row[3] : _product->position = nullptr;
					row[4] ? _product->price = stod(row[4]) : _product->price = 0;
					row[5] ? _product->size = stod(row[5]) : _product->size = 0;
					row[6] ? _product->weight = stod(row[6]) : _product->weight = 0;
					row[7] ? _product->unitsInStock = stoi(row[7]) : _product->unitsInStock = 0;*/
					numberOfRows++;
					_invoice++;
				}
			}
			else
			{
				string s = "No invoices Exist";
				cout << s << endl;
				strcpy_s(error, s.length() + 1, s.c_str());
				status = 2;
			}
		}
	}
	return numberOfRows;
}
extern "C" ERP_API int getCustomerById(char* id, Customer** customer, char* error) {
	status = 0;
	int numberOfRows = 0;
	unsigned int numOfFields;
	db_response::ConnectionFunction(error);
	if (conn) {
		mysql_free_result(res);
		string query = (string)"select * from customer where customer_ID = '" + id + "'";
		qstate = mysql_query(conn, query.c_str());
		cout << query << endl;
		if (checkQuery(qstate, error)) {
			res = mysql_store_result(conn);
			if (res->row_count > 0)
			{
				*customer = (Customer*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Customer));
				cout << res->row_count << endl;
				numOfFields = mysql_num_fields(res);
				Customer* _customer= *customer;
				while (row = mysql_fetch_row(res)) {
					_customer ->customer_id = row[0];
					row[1] ? _customer->name = row[1] : _customer->name = nullptr;
					row[2] ? _customer->phone_number = stoi(row[2]) : _customer->phone_number = 0;
					row[3] ? _customer->email = row[3] : _customer->email = nullptr;
					row[4] ? _customer->dateOfBirth = row[4] : _customer->dateOfBirth = nullptr;
					row[5] ? _customer->gender = row[5] : _customer->gender = nullptr;
					row[6] ? _customer->loyality_points = stoi(row[6]) : _customer->loyality_points = 0;
					row[7] ? _customer->type = stoi(row[7]) : _customer->type = 0;
					row[8] ? _customer->company = row[8] : _customer->company = nullptr;
					row[9] ? _customer->company_email = row[9] : _customer->company_email = nullptr;
					row[10] ? _customer->is_lead = row[10] : _customer->is_lead = true;
					numberOfRows++;
					_customer++;
				}
			}
			else
			{
				string s = "No Customer has that id";
				cout << s << endl;
				strcpy_s(error, s.length() + 1, s.c_str());
				status = 2;
			}
		}
	}
	return numberOfRows;
}
extern "C" ERP_API int getCustomerOrders(char* id, Order** order, char* error) {
	status = 0;
	int numberOfRows = 0;
	unsigned int numOfFields;
	db_response::ConnectionFunction(error);
	if (conn) {
		mysql_free_result(res);
		query = (string) "SELECT Order_ID,Order_Required_Date,Order_Completed_Date,Order_Status,Payment_Payment_ID,sum(Units_In_Order*(select product_price from product where product_product_id = product_id)) as tP FROM  order , order_has_product where order.Customer_Customer_id  = '" + id + "' And order_id = order_order_id";
		qstate = mysql_query(conn, query.c_str());
		cout << query << endl;
		if (checkQuery(qstate, error)) {
			res = mysql_store_result(conn);
			if (res->row_count > 0)
			{
				*order = (Order*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Order));
				cout << res->row_count << endl;
				numOfFields = mysql_num_fields(res);
				Order* _order = *order;
				while (row = mysql_fetch_row(res)) {
					_order->id = row[0];
					row[1] ? _order->requiredDate = row[1] : _order->requiredDate = nullptr;
					row[2] ? _order->completedDate = row[2] : _order->completedDate = "Not Completed";
					row[3] ? _order->orderStatus = row[3] : _order->orderStatus = nullptr;
					row[4] ? _order->customerID = row[4] : _order->customerID = nullptr;
					row[5] ? _order->paymentID = row[5] : _order->paymentID = nullptr;
					row[6] ? _order->totalPrice = stod(row[6]) : _order->totalPrice = 0;

					numberOfRows++;
					_order++;
				}
			}
			else
			{
				string s = "No orders Exist for this customer";
				cout << s << endl;
				strcpy_s(error, s.length() + 1, s.c_str());
				status = 2;
			}
		}
	}
	return numberOfRows;
}
extern "C" ERP_API int getOrderProducts(char* id, Product** product_order, char* error) {
	status = 0;
	int numberOfRows = 0;
	unsigned int numOfFields;
	db_response::ConnectionFunction(error);
	if (conn) {
		mysql_free_result(res);
		query = (string) "select Product_Product_ID,Product_Name, Units_In_Order, Product_Price, (Product_Price*Units_In_Order) as Pcost from order_has_product, product where Order_Order_ID ='" + id + "'AND Product_Product_ID = Product_ID group by Product_Product_ID";
		qstate = mysql_query(conn, query.c_str());
		cout << query << endl;
		if (checkQuery(qstate, error)) {
			res = mysql_store_result(conn);
			if (res->row_count > 0)
			{
				*product_order = (Product*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Product));
				cout << res->row_count << endl;
				numOfFields = mysql_num_fields(res);
				Product* _product = *product_order;
				while (row = mysql_fetch_row(res)) {
					_product->id = row[0];
					row[1] ? _product->name = row[1] : _product->name = nullptr;
					row[2] ? _product->unitsInOrder = stoi(row[2]) : _product->unitsInOrder = 0.0;
					row[3] ? _product->price = stod(row[3]) : _product->price = 0.0;
					row[4] ? _product->totalPrice = stod(row[4]) : _product->totalPrice = 0.0;
					numberOfRows++;
					_product++;
				}
			}
			else
			{
				string s = "No Product Exist";
				cout << s << endl;
				strcpy_s(error, s.length() + 1, s.c_str());
				status = 2;
			}
		}
	}
	return numberOfRows;
}
bool checkQuery(int qstate, char* error)
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