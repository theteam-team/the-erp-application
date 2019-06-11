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
#define PASSWORD "root" //your password for mysql
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