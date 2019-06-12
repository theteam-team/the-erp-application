// MySqlTest.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
#include "pch.h"
#include <string>
#include <string.h>
#include <mysql.h>
#include <iostream>
#include <sstream> 
#include"DatabaseEntities.h"
#include"Header.h"
using namespace std;
#define SERVER "localhost"
#define USER "root" //your username
#define PASSWORD "0198484014###" //your password for mysql
#define DATABASE "erp" //database name

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
string input;
string accId;
string accMoney;
//void finish_with_error(MYSQL *conn)
//{
//	fprintf(stderr, "%s\n", mysql_error(conn));
//	mysql_close(conn);
//	exit(1);
//}
/*
extern "C"	ERP_API int soldProducts(ProductSold** product, char* error) {
	status = 0;
	int numberOfRows = 0;
	unsigned int numOfFields;
	db_response::ConnectionFunction(error);
	if (conn) {
		mysql_free_result(res);
		query = "SELECT Product_Product_ID, sum(Units_In_Order) as Units_In_Order FROM erp.order_has_product group by Product_Product_ID";
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
*/
extern "C"	ERP_API int profit(ProductSold** product, char* error) {
	status = 0;
	int numberOfRows = 0;
	unsigned int numOfFields;
	db_response::ConnectionFunction(error);
	if (conn) {
		mysql_free_result(res);
		//for each sold product, calculate its profit --> Units_In_Order *( product_price - Produt_cost),,,,,, then get summation of all profits
		query = "SELECT Product_ID, Product_Price, Product_Cost, sum(Units_In_Order) as Units_In_Order, sum(Units_In_Order * (Product_Price - Product_Cost)) AS Profit FROM erp.product, erp.order_has_product WHERE(erp.product.product_id = erp.order_has_product.Product_Product_ID) group by Product_ID";
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
					row[2] ? _product->price = stoi(row[2]) : _product->price = 0;
					row[3] ? _product->cost = stoi(row[3]) : _product->cost = 0;
					row[4] ? _product->profit = stoi(row[4]) : _product->profit = 0;

					/*row[1] ? _product->name = row[1] : _product->name = nullptr;
					row[2] ? _product->description = row[2] : _product->description = nullptr;
					row[3] ? _product->position = row[3] : _product->position = nullptr;
					row[4] ? _product->price = stod(row[4]) : _product->price = 0;
					row[5] ? _product->size = stod(row[5]) : _product->size = 0;
					row[6] ? _product->weight = stod(row[6]) : _product->weight = 0;
					row[7] ? _product->unitsInStock = stoi(row[7]) : _product->unitsInStock = 0;*/
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
extern "C"	ERP_API int invoice(Invoice** invoice, char* error) {
	status = 0;
	int numberOfRows = 0;
	unsigned int numOfFields;
	db_response::ConnectionFunction(error);
	if (conn) {
		mysql_free_result(res);
		//for each sold product, calculate its profit --> Units_In_Order *( product_price - Produt_cost),,,,,, then get summation of all profits
		query = "SELECT  Supplier_ID, Supplier_Name, Supplier_Phone_Number, Supplier_Email, Supplier_Payment_method, sum(Units_Supplied) as num_of_supplied_units, sum(Paid_up) as total_paid, sum(Units_Supplied * (select Product_Cost from erp.product where product_id = product_has_supplier.Product_Product_ID)) as total_cost, sum(Units_Supplied * (select Product_Cost from erp.product where product_id = product_has_supplier.Product_Product_ID)- Paid_up) As depts from erp.product_has_supplier, erp.supplier, erp.product where Supplier_ID = Supplier_Supplier_ID AND product_id = product_has_supplier.Product_Product_ID group by supplier_id order by supplier_id;";
		qstate = mysql_query(conn, query.c_str());
		cout << query << endl;
		if (checkQuery(qstate, error)) {
			res = mysql_store_result(conn);
			if (res->row_count > 0)
			{
				*invoice = (Invoice*)CoTaskMemAlloc((int)(res->row_count) * sizeof(ProductSold));
				cout << res->row_count << endl;
				numOfFields = mysql_num_fields(res);
				Invoice* _invoice = *invoice ;
				while (row = mysql_fetch_row(res)) {
					_invoice->id = row[0];
					row[1] ? _invoice-> suppName = row[1] : _invoice->suppName = 0;
					row[2] ? _invoice-> suppPhone = stoi(row[2]) : _invoice->suppPhone = 0;
					row[3] ? _invoice->suppMail = row[3] : _invoice->suppMail = 0;
					row[4] ? _invoice->payment_method = stoi(row[4]) : _invoice->payment_method = 0;
					row[5] ? _invoice->suppUnits = stod(row[5]) : _invoice->suppUnits = 0;
					row[6] ? _invoice->totalPaid= stod(row[6]) : _invoice->totalPaid = 0;
					row[7] ? _invoice->totalCost = stod(row[7]) : _invoice->totalCost = 0; 
					row[8] ? _invoice->depts = stod(row[7]) : _invoice->depts = 0; 
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
//void init() {
//	conn = mysql_init(0);
//	conn = mysql_real_connect(conn, "localhost", "root", "rana", "erp", 3306, NULL, 0);
//	if (conn) {
//		puts("Successful connection to database!");
//		printf("MySQL client version: %s\n", mysql_get_client_info());
//		soldProducts();
//	}
//
//	else
//		puts("Connection to database has failed!");
//}
//int main()
//{
//	init();
//	return 0;
//}

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