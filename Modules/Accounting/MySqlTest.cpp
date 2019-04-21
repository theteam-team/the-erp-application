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

void showDatabase() {
	query = "SHOW DATABASES";
	const char* q = query.c_str();
	qstate = mysql_query(conn, q);
	if (!qstate)
	{
		cout << "Databases are" << endl;
		res = mysql_store_result(conn);
		while (row = mysql_fetch_row(res))
		{
			printf("  --> %s\n", row[0]);
		}
	}
	else
		cout << "Query failed: " << mysql_error(conn) << endl;
	cout << endl;
}
void showTables() {
	cout << "Choose Database , please" << endl;
	getline(cin, input);
	query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + input + "'";
	const char* q = query.c_str();
	qstate = mysql_query(conn, q);
	if (!qstate)
	{
		cout << "TABLES are" << endl;
		res = mysql_store_result(conn);
		while (row = mysql_fetch_row(res))
		{
			printf("	-->%s\n", row[0]);
		}
	}
	else
		cout << "Query failed: " << mysql_error(conn) << endl;
}
string chooseTable() {
	cout << "choose table (CUSTOMER_T, ACCOUNT_T, PRODUCTS_T, PAYMENT_T,INVOICE_T)" << endl;
	cout << "	-->";
	getline(cin, input);
	return input;
}
int numberOfColumns(string h) {
	//getline(cin, input);
	query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'mydb' AND TABLE_NAME = '" + h + "'";
	const char* q = query.c_str();
	qstate = mysql_query(conn, q);
	int y = 0;
	if (!qstate) {
		res = mysql_store_result(conn);
		y = mysql_num_rows(res);
	}
	else
		cout << "Query failed: " << mysql_error(conn) << endl;
	return y;
}
void showFieldName() {
	query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'mydb' AND TABLE_NAME = '" + input + "'";
	const char* q = query.c_str();
	qstate = mysql_query(conn, q);
	if (!qstate) {
		res = mysql_store_result(conn);
		printf("\t\t-->");
		while (row = mysql_fetch_row(res)) {
			printf("%s\t", row[0]);
		}
	}
	else
		cout << "Query failed: " << mysql_error(conn) << endl;
	cout << endl;
}
void finish_with_error(MYSQL *conn)
{
	fprintf(stderr, "%s\n", mysql_error(conn));
	mysql_close(conn);
	exit(1);
}
void insertion() {
	printf("let's insert\n");
	string z = chooseTable();
	showFieldName();
	int s = numberOfColumns(z);
	//cout << s << endl;
	//printf("Enter an account id : ");
	//query = "INSERT INTO  (ACCOUNT_ID, ACCOUNT_Money) VALUES('" + accId + "', '" + accMoney + "')";
	query = "INSERT INTO ";
	query += z;
	query += " VALUES('";
	cout << "Enter a/an "+z+" fields in the same order: ";
	for (int i = 0; i < s; i++) {
		string field;
		getline(cin, field);
		query += field;
		if (s - i == 1) {
			query += "'";
			break;
		}
		query += "','";
	}

	query += ")";
	cout << query;
	//printf("Enter an account money : ");
	//getline(cin, accMoney);
	//cout << "You have entered id : " << accId << ", Money: " << accMoney << endl;
	//query = "INSERT INTO ACCOUNT_T (ACCOUNT_ID, ACCOUNT_Money) VALUES('" + accId + "', '" + accMoney + "')";
	const char* q = query.c_str();
	if (mysql_query(conn, q)) {
		finish_with_error(conn);

	}
}
void search() {
	printf("let's search\n");
	//cout << "choose table (CUSTOMER_T, ACCOUNT_T, PRODUCTS_T, PAYMENT_T,INVOICE_T)" << endl;
	string z = chooseTable();
	showFieldName();
	int m = numberOfColumns(z);
	//cout << z << endl;
	cout << "search by ? .. " << endl << "write fieldName" << endl;
	string i;
	getline(cin, i);
	string search_string;
	cout << "search about" << endl;
	getline(cin, search_string);
	showFieldName();
	query = "SELECT * FROM "+z+" WHERE "+i+" = '"+ search_string +"'";
	const char* q = query.c_str();
	qstate = mysql_query(conn, q);
	if (!qstate) {
		res = mysql_store_result(conn);
		cout << "\t";
		while (row = mysql_fetch_row(res)) {
			//printf("\t\t-->%s\t\t-->%s\n", row[0], row[1]);
			for (int j = 0; j < m; j++)
			{
				// Make sure row[i] is valid!
				if (row[j] != NULL)
					cout << "\t" << row[j] << "\t";
				else
					cout << "NULL" << endl;
				
				// Also, you can use ternary operator here instead of if-else
				// cout << row[i] ? row[i] : "NULL" << endl;
			}
			cout << endl;
		}
	}
	else
		cout << "Query failed: " << mysql_error(conn) << endl;
}
void update() {
	printf("let's update\n");
	search();
}
void deletion() {
	printf("let's delete\n");
}
void createTable() {

}
void init() {
	conn = mysql_init(0);
	conn = mysql_real_connect(conn, "localhost", "root", "0198484014###", "mydb", 3306, NULL, 0);
	if (conn) {
		puts("Successful connection to database!");
		printf("MySQL client version: %s\n", mysql_get_client_info());
		showDatabase();
		showTables();
		printf("Choose one (insertion, update, search, deletion)\n");
		int i = 0;
		while (i < 2)
		{
			i++;
			getline(cin, input);
			if (input.compare("insertion") == 0) {
				insertion();
				break;
			}
			else if (input.compare("update") == 0) {
				update();
				break;
			}
			else if (input.compare("search") == 0) {
				search();
				break;
			}
			else if (input.compare("deletion") == 0) {
				deletion();
				break;
			}
			else
				printf("wrong input, please choose one operation from four mentioned\n");
		}
		printf("\nThanks for your work\n");
		
	}

	else 
		puts("Connection to database has failed!");
}
int main()
{
	init();	

	//string s = "Ali";
	//cout << s << endl;
	//s += ")";
	//cout << s << endl;
		/*
		string query = "SELECT * FROM ACCOUNT_T";
		const char* q = query.c_str();
		qstate = mysql_query(conn, q);
		if (!qstate)
		{
			res = mysql_store_result(conn);
			while (row = mysql_fetch_row(res))
			{
				printf("ID: %d, Money: %d\n", row[0], row[1]);
			}
		}
		
		else
		{
			cout << "Query failed: " << mysql_error(conn) << endl;
		}
		*/
		/* // last inserted id (for auto incremented id)
 		int id = mysql_insert_id(conn);
		printf("The last inserted row id is: %d\n", id);
		*/
	return 0;
}