#include "pch.h"

#include <iostream>
#include <cstdio>
#include <fstream>
#include <sstream>
#include <string.h>
#include <cstdlib>
#include <conio.h>
#include <windows.h>

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
void deleteProduct(string);
int checkUnitsInStock(string);
void addToStock(string, int);
void removeFromStock(string, int);
void updateProductInfo(string, string, string);
string getProductInfo(string, string);
void getAllProductInfo(string);
void showProducts();
void getOrderInfo(string);
void showCompletedOrders();
void showOrdersInProgress();
void showProductsInOrder(string);
double makeOrder(string);


int main() {

	db_response::ConnectionFunction();
}


void addProduct(string id, string name, string price, string unitsInStock, string description, string position, string size, string weight) {

	string query = "insert into product (Product_ID, Product_Name, Product_Price, Units_In_Stock, Product_Description, Product_Position, Product_Size, Product_Weight) values ('" + id + "', '" + name + "', '" + price + "', '" + unitsInStock + "', '" + description + "', '" + position + "', '" + size + "', '" + weight + "')";
	char const *q = query.c_str();
	qstate = mysql_query(conn, q);
}


void deleteProduct(string id) {

	string query = "delete from product where Product_ID = " + id;
	char const *q = query.c_str();
	qstate = mysql_query(conn, q);
}


int checkUnitsInStock(string id) {

	string query = "select Units_In_Stock from product where Product_ID = " + id;
	char const *q = query.c_str();

	mysql_free_result(res);

	qstate = mysql_query(conn, q);
	res = mysql_store_result(conn);
	row = mysql_fetch_row(res);

	return stoi(row[0]);
}


void addToStock(string id, int newUnits) {

	int units;
	units = checkUnitsInStock(id);
	units += newUnits;

	string query = "update product set Units_In_Stock = '" + to_string(units) + "' where Product_ID = " + id;
	char const *q = query.c_str();
	qstate = mysql_query(conn, q);
}


void removeFromStock(string id, int newUnits) {

	int units;
	units = checkUnitsInStock(id);
	units -= newUnits;

	string query = "update product set Units_In_Stock = '" + to_string(units) + "' where Product_ID = " + id;
	char const *q = query.c_str();
	qstate = mysql_query(conn, q);
}


void updateProductInfo(string id, string key, string value) {

	string query = "update product set " + key + " = '" + value + "' where Product_ID = " + id;
	char const *q = query.c_str();
	qstate = mysql_query(conn, q);
}


string getProductInfo(string id, string key) {

	string query = "select " + key + " from product where Product_ID = " + id;
	char const *q = query.c_str();
	
	mysql_free_result(res);

	qstate = mysql_query(conn, q);
	res = mysql_store_result(conn);
	row = mysql_fetch_row(res);

	return row[0];
}


void getAllProductInfo(string id) {

	unsigned int i, numOfFields;
	string query = "select * from product where Product_ID = " + id;
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


void showProducts() {

	unsigned int i, numOfFields;

	mysql_free_result(res);

	qstate = mysql_query(conn, "select * from product");
	res = mysql_store_result(conn);

	numOfFields = mysql_num_fields(res);

	while (row = mysql_fetch_row(res)) {

		cout << "id   name   description   position   price   size   weight   units in stock\n";

		for (i = 0; i < numOfFields; i++) {

			if (row[i] != NULL)
				cout << row[i] << "   ";
			else
				cout << "NULL" << "   ";
		}
		cout << "\n\n";
	}
}


void getOrderInfo(string id) {

	unsigned int i, numOfFields;
	string query = "select * from order where Order_ID = " + id;
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


void showCompletedOrders() {

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


void showOrdersInProgress() {

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


void showProductsInOrder(string id) {

	string query = "select Product_Product_ID, Units_In_Order from order_has_product where Order_Order_ID = " + id;
	char const *q = query.c_str();

	mysql_free_result(res);

	qstate = mysql_query(conn, q);
	res = mysql_store_result(conn);

	cout << "product id     units in order";

	while (row = mysql_fetch_row(res))
		cout << row[0] << "   " << row[1] << "\n";
}


double makeOrder(string id) {

	MYSQL_ROW r;
	double price, total = 0;
	int units, unitsInOrder;
	string productID;

	string query = "select Product_Product_ID, Units_In_Order from order_has_product where Order_Order_ID = " + id;
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

		removeFromStock(productID, unitsInOrder);
	}

	return total;
}