// MySqlTest.cpp : This file contains the 'main' function. Program execution begins and ends there.
//
#include "pch.h"
#include <string>
#include <string.h>
#include <mysql.h>
#include <iostream>
#include <sstream> 
using namespace std;
using namespace std;
MYSQL* conn;
MYSQL_ROW row;
MYSQL_RES *res;
int qstate;
string query;
string input;
string accId;
string accMoney;
void finish_with_error(MYSQL *conn)
{
	fprintf(stderr, "%s\n", mysql_error(conn));
	mysql_close(conn);
	exit(1);
}
void soldProducts() {
	query = "SELECT Order_has_Product FROM Order_has_Product";
	const char* q = query.c_str();
	qstate = mysql_query(conn, q);
	if (!qstate) {
		res = mysql_store_result(conn);
		while (row = mysql_fetch_row(res)) {
			int j = 0;
			while(1)
			{
				// Make sure row[i] is valid!
				if (row[j] != NULL)
					cout << "\t" << row[j] << "\t";
				else
					cout << "NULL" << endl;
					break;
				j++;
				// Also, you can use ternary operator here instead of if-else
				// cout << row[i] ? row[i] : "NULL" << endl;
			}
			cout << endl;
		}
	}
	else
		cout << "Query failed: " << mysql_error(conn) << endl;
}
void init() {
	conn = mysql_init(0);
	conn = mysql_real_connect(conn, "localhost", "root", "rana", "erp", 3306, NULL, 0);
	if (conn) {
		puts("Successful connection to database!");
		printf("MySQL client version: %s\n", mysql_get_client_info());
		soldProducts();
	}

	else
		puts("Connection to database has failed!");
}
int main()
{
	init();
	return 0;
}
