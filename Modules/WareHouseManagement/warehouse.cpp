#include "pch.h"
#include "Header.h"
#include <iostream>
#include <cstdio>
#include <fstream>
#include <sstream>
#include <string.h>
#include <cstdlib>
#include <conio.h>
#include <windows.h>
#include"DatabaseEntities.h"
#include <mysql.h>

using namespace std;

#define SERVER "localhost"
#define USER "root" //your username
#define PASSWORD "rana" //your password for mysql
#define DATABASE "warehouse" //database name

int qstate;
MYSQL* conn;
MYSQL_ROW row;
MYSQL_RES* res;


class db_response {

public:
	static void ConnectionFunction() {

		conn = mysql_init(0);

		if (conn) {
			cout << "Database Connected" << endl;
		}
		else
			cout << "Failed To Connect!" << mysql_errno(conn) << endl;

		conn = mysql_real_connect(conn, SERVER, USER, PASSWORD, DATABASE, 3306, NULL, 0);
		if (conn) {
			cout << "Database Connected To MySql" << conn << endl;
		}
		else
			cout << "Failed To Connect!" << mysql_errno(conn) << endl;
	}
};


void addProduct(string, string, string, string, string, string, string, string);
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
double makeOrder(char*);


int main() {

	db_response::ConnectionFunction();
}


 void addProduct(Product * _Product) 
{
	
	db_response::ConnectionFunction();
	string query = (string) "insert into product (Product_ID, Product_Name, Product_Price, Units_In_Stock, Product_Description, Product_Position, Product_Size, Product_Weight) values ('" + _Product->id  + "', '" + _Product->name + "', '" + _Product->price + "', '" + _Product->unitsInStock + "', '" + _Product->description + "', '" + _Product->position + "', '" + _Product->size + "', '" + _Product->weight + "')";
	char const *q = query.c_str();
	qstate = mysql_query(conn, q);
}


 extern "C"	ERP_API void deleteProduct(char * id) {

	string query = (string)"delete from product where Product_ID = " + id;
	char const *q = query.c_str();
	qstate = mysql_query(conn, q);
}


 extern "C"	ERP_API int checkUnitsInStock(char* id) {

	string query = (string)"select Units_In_Stock from product where Product_ID = " + id;
	char const *q = query.c_str();

	mysql_free_result(res);

	qstate = mysql_query(conn, q);
	res = mysql_store_result(conn);
	row = mysql_fetch_row(res);

	return stoi(row[0]);
}


 extern "C"	ERP_API void addToStock(char* id, int newUnits) {

	int units;
	units = checkUnitsInStock(id);
	units += newUnits;

	string query = "update product set Units_In_Stock = '" + to_string(units) + "' where Product_ID = " + id;
	char const *q = query.c_str();
	qstate = mysql_query(conn, q);
}


 extern "C"	ERP_API void removeFromStock(char* id, int newUnits) {

	int units;
	units = checkUnitsInStock(id);
	units -= newUnits;

	string query = "update product set Units_In_Stock = '" + to_string(units) + "' where Product_ID = " + id;
	char const *q = query.c_str();
	qstate = mysql_query(conn, q);
}


 extern "C"	ERP_API void updateProductInfo(char* id, char* key, char* value) {

	string query = (string)"update product set " + key + " = '" + value + "' where Product_ID = " + id;
	char const *q = query.c_str();
	qstate = mysql_query(conn, q);
}


 extern "C"	ERP_API char* getProductInfo(char* id, char * key) {

	string query = (string)"select " + key + " from product where Product_ID = " + id;
	char const *q = query.c_str();
	
	mysql_free_result(res);

	qstate = mysql_query(conn, q);
	res = mysql_store_result(conn);
	row = mysql_fetch_row(res);

	return row[0];
}


 extern "C"	ERP_API void getAllProductInfo(char* id) {

	unsigned int i, numOfFields;
	string query = (string)"select * from product where Product_ID = " + id;
	char const *q = query.c_str();

	mysql_free_result(res);

	qstate = mysql_query(conn, q);
	res = mysql_store_result(conn);

	numOfFields = mysql_num_fields(res);
	row = mysql_fetch_row(res);

	cout << "id   name   description   position   price   size   weight   units in stock\n";

	for (i = 0; i < numOfFields; i++) {

		if (row[i] != NULL)
			cout << row[i] << "   ";
		else
			cout << "NULL" << "   ";
	}
}


 extern "C"	ERP_API void showProducts() {

	unsigned int i, numOfFields;

	mysql_free_result(res);

	qstate = mysql_query(conn, "select * from product");
	res = mysql_store_result(conn);

	numOfFields = mysql_num_fields(res);

	while (row = mysql_fetch_row(res)) {

		/*cout << "id   name   description   position   price   size   weight   units in stock\n";

		for (i = 0; i < numOfFields; i++) {

			if (row[i] != NULL)
				cout << row[i] << "   ";
			else
				cout << "NULL" << "   ";
		}
		cout << "\n\n";*/


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
		char* _productId;
		strcpy_s(_productId, productID.length() + 1, productID.c_str());
		removeFromStock(_productId, unitsInOrder);
	}

	return total;
}