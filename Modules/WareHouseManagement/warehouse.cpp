#include "pch.h"
#include "Header.h"
#include <iostream>
#include <cstdio>
#include <fstream>
#include <sstream>
#include <string.h>
#include <cstdlib>
#include <conio.h>
#include"DatabaseEntities.h"
#include <mysql.h>
#pragma warning(disable : 4996)
using namespace std;

#define SERVER "localhost"
#define USER "root" //your username
#define PASSWORD "123456789pp" //your password for mysql
#define DATABASE "erp" //database name
int status;
int qstate;
MYSQL* conn;
MYSQL_ROW row;
MYSQL_RES* res;


class db_response {

public:
	static void ConnectionFunction(char * error) {

		conn = mysql_init(0);

		/*if (conn) {
			cout << "Database Connected" << endl;
		}
		else
			cout << "Failed To Connect!" << mysql_errno(conn) << endl;*/

		conn = mysql_real_connect(conn, SERVER, USER, PASSWORD, DATABASE, 3306, NULL, 0);
		if (!conn) {
			
			cout << "Failed To Connect!" << mysql_errno(conn) << endl;
			string err = (string)"Connection to database has failed!" + mysql_error(conn);;
			strcpy_s(error, err.length() + 1, err.c_str());
			status = 3;
		}
	}
};


/*void addProduct(Product* product);
void deleteProduct(char*);
int checkUnitsInStock(char*);
void addToStock(char*, int);
void removeFromStock(char*, int);
void updateProductInfo(char*, char*, char*);
char* getProductInfo(char*, char*);
void getAllProductInfo(char*);
void showProducts();
void getOrderInfo(char*);
void showCompletedOrders();
void showOrdersInProgress();
void showProductsInOrder(char*);
double makeOrder(char*);*/





extern "C"	ERP_API int addProduct(Product * product, char * error)
{
	status = 0;
	db_response::ConnectionFunction(error);
	if (conn) {
		
		
		string query = (string) "insert into product (Product_ID, Product_Name, Product_Price, Units_In_Stock, Product_Description, Product_Position, Product_Size, Product_Weight) values ('" + product->id + "', '" + product->name + "', " + to_string(product->price) + ", " + to_string(product->unitsInStock) + ", '" + product->description + "', '" + product->position + "', " + to_string(product->size) + ", " + to_string(product->weight) + ")";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}


 extern "C"	ERP_API int deleteProduct(char * id, char* error) {
	 status = 0;
	 db_response::ConnectionFunction(error);
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


 extern "C"	ERP_API int checkUnitsInStock(char* id, char* error) {

	 status = 0;
	 db_response::ConnectionFunction(error);
	 if (conn)
	 {
		 string query = (string)"select Units_In_Stock from product where Product_ID = " + id;
		 cout << query << endl;
		 char const *q = query.c_str();
		 mysql_free_result(res);
		 qstate = mysql_query(conn, q);
		 checkQuery(qstate, error);
		 res = mysql_store_result(conn);
		 if (res->row_count > 0)
		 {
			 row = mysql_fetch_row(res);
			 status = 0;
			 return stoi(row[0]);
		 }
		 
		 string s = "Error This Product id does not exixt";
		 cout << s << endl;
		 strcpy_s(error, s.length() + 1, s.c_str());
		 status = 2;
		 mysql_close(conn);
	 }
	 return status;
}

 
 extern "C"	ERP_API int addToStock(char* id, int newUnits, char* error) {

	int units = 0;
	status = 0;
	db_response::ConnectionFunction(error);
	units = checkUnitsInStock(id, error);
	if (status == 0)
	{
		cout << newUnits<<endl;
		units += newUnits;
		string query = "update product set Units_In_Stock = " + to_string(units) + " where Product_ID = " + id;
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}

 
 extern "C"	ERP_API int removeFromStock(char* id, int newUnits, char* error) {

	 int units;
	 status = 0;
	 db_response::ConnectionFunction(error);
	 units = checkUnitsInStock(id, error);
	 units -= newUnits;
	 if (status == 0)
	 {
		 string query = "update product set Units_In_Stock = '" + to_string(units) + "' where Product_ID = " + id;
		 cout << query << endl;
		 char const *q = query.c_str();
		 qstate = mysql_query(conn, q);
		 checkQuery(qstate, error);
		 mysql_close(conn);
	 }
	return status;
}

 
 extern "C"	ERP_API int updateProductInfo(char* id, char* key,  char* value, char* error) {
	status = 0;
	db_response::ConnectionFunction(error);
	if (conn)
	{
		cout << key<<endl;
		string query = (string)"update product set " + key + " = '" + value + "' where Product_ID = " + "'" + id +"'";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}

 
 extern "C"	ERP_API char* getProductInfo(char* id, char * key, char* result, char* error) {

	 status = 0;
	 db_response::ConnectionFunction(error);
	 if (conn)
	 {
		 string query = (string)"select " + key + " from product where Product_ID = '" + id + "'";
		 cout << query << endl;
		 char const *q = query.c_str();

		 mysql_free_result(res);

		 qstate = mysql_query(conn, q);
		 checkQuery(qstate, error);
		 res = mysql_store_result(conn);
		 if (res->row_count > 0)
		 {
			 
			row = mysql_fetch_row(res);
			string s = row[0];
			strcpy_s(result, s.length() + 1, s.c_str());
			return row[0];
		 }
		 else 
		 {
			 string s = "Error This Product id does not exixt";
			 cout << s << endl;
			 strcpy_s(error, s.length() + 1, s.c_str());
			 status = 2;
		 }
		 mysql_close(conn);
	 }
	 return error;

}

 
 extern "C"	ERP_API int getAllProductInfo(char* id, Product** product, char* error) {

	status = 0;
	db_response::ConnectionFunction(error);
	if (conn)
	{
		unsigned int i, numOfFields;
		string query = (string)"select * from product where Product_ID = '" + id+"'";
		char const *q = query.c_str();

		mysql_free_result(res);

		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		res = mysql_store_result(conn);
		if (res->row_count > 0) 
		{
			cout << "here";
			status = 0;
			numOfFields = mysql_num_fields(res);
			row = mysql_fetch_row(res);
			*product = (Product*)CoTaskMemAlloc(sizeof(Product));
			Product *_product = *product;
			_product->id = row[0];
			_product->name = row[1];
			_product->description = row[2];
			_product->position = row[3];
			_product->price = stod(row[4]);
			_product->size = stod(row[5]);
			_product->weight = stod(row[6]);
			_product->unitsInStock = stoi(row[7]);
			
		}
		else
		{
			string s = "Error This Product id does not exixt";
			cout << s << endl;
			strcpy_s(error, s.length() + 1, s.c_str());
			status = 2;
		}
	}
	return status;
}

 /*
 extern "C"	ERP_API void showProducts() {

	unsigned int i, numOfFields;

	mysql_free_result(res);

	qstate = mysql_query(conn, "select * from product");
	res = mysql_store_result(conn);

	numOfFields = mysql_num_fields(res);

	while (row = mysql_fetch_row(res)) {

		


	}
}


 extern "C"	ERP_API void getOrderInfo(char* id) {

	unsigned int i, numOfFields;
	string query = (string)"select * from order where Order_ID = " + id;
	char const *q = query.c_str();

	mysql_free_result(res);

	qstate = mysql_query(conn, q);
	res = mysql_store_result(conn);

	numOfFields = mysql_num_fields(res);
	row = mysql_fetch_row(res);

	cout << "id   required data   completed data   customer id   payment id   shippment id\n";

	for (i = 0; i < numOfFields; i++) {

		if (row[i] != NULL)
			cout << row[i] << "   ";
		else
			cout << "NULL" << "   ";
	}
}


 extern "C"	ERP_API void showCompletedOrders() {

	unsigned int i, numOfFields;

	mysql_free_result(res);

	qstate = mysql_query(conn, "select * from order where Order_Completed_Date <> NULL");
	res = mysql_store_result(conn);

	numOfFields = mysql_num_fields(res);

	while (row = mysql_fetch_row(res)) {

		cout << "id   required data   completed data   customer id   payment id   shippment id\n";

		for (i = 0; i < numOfFields; i++) {

			if (row[i] != NULL)
				cout << row[i] << "   ";
			else
				cout << "NULL" << "   ";
		}
		cout << "\n\n";
	}
}


 extern "C"	ERP_API void showOrdersInProgress() {

	unsigned int i, numOfFields;

	mysql_free_result(res);

	qstate = mysql_query(conn, "select * from order where Order_Completed_Date = NULL");
	res = mysql_store_result(conn);

	numOfFields = mysql_num_fields(res);

	while (row = mysql_fetch_row(res)) {

		cout << "id   required data   completed data   customer id   payment id   shippment id\n";

		for (i = 0; i < numOfFields; i++) {

			if (row[i] != NULL)
				cout << row[i] << "   ";
			else
				cout << "NULL" << "   ";
		}
		cout << "\n\n";
	}
}


 extern "C"	ERP_API void showProductsInOrder(char* id) {

	string query = (string)"select Product_Product_ID, Units_In_Order from order_has_product where Order_Order_ID = " + id;
	char const *q = query.c_str();

	mysql_free_result(res);

	qstate = mysql_query(conn, q);
	res = mysql_store_result(conn);

	cout << "product id     units in order";

	while (row = mysql_fetch_row(res))
		cout << row[0] << "   " << row[1] << "\n";
}


 extern "C"	ERP_API double makeOrder(char* id) {

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
		removeFromStock(_productId, unitsInOrder);
	}

	return total;
}
*/

 void checkQuery(int qstate,  char * error)
 {
	 if (qstate)
	 {
		 cout << "Query failed: " << mysql_error(conn) << endl;
		 status = 2;
		 string s = mysql_error(conn);
		 strcpy_s(error, s.length() + 1, mysql_error(conn));
	 }
	 else
	 {
		 cout << "Query successded";
		 status = 0;
		 string s = mysql_error(conn);
		 strcpy_s(error, s.length() + 1, mysql_error(conn));

	 }
 }