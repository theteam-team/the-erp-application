#include"Header.h"
#include "Database_tables.h"
#include <stdio.h>
#include<iostream>
#include <sstream> 
#include <string>
#include<mysql.h>
int status;
using namespace std;
int qstate;
extern "C"	ERP_API int AddCustomer(Customer* customer, char* error)
{
	
	int status;
	MYSQL* conn;
	MYSQL_ROW row;
	MYSQL_RES *res;
	conn = mysql_init(0);
	conn = mysql_real_connect(conn, "localhost", "root", "123456789pp", "erp_crm", 3306, NULL, 0);
	if (conn) {
		puts("Successful connection to database!");

		string query = "INSERT INTO customers VALUES";
		query += "('";
		query += customer->customer_id;
		query += "','";
		query += customer->first_name;
		query += "','";
		query += customer->middle_name;
		query += "','";
		query += customer->last_name;
		query += "','";
		query += customer->email;
		query += "',";
		query += to_string(customer->phone_number);
		query += ",";
		query += to_string(customer->year_birth);
		query += ",";
		query += to_string(customer->month_birth);
		query += ",";
		query += to_string(customer->day_birth);
		query += ",'";
		query += customer->gender;
		query += "',";
		query += to_string(customer->loyality_points);
		query += ",";
		query += to_string(customer->type);
		query += ",'";
		query += customer->company;
		query += "','";
		query += customer->company_email;
		query += "',";
		query += to_string(customer->is_lead);
		query += ");";

		cout << query << endl;
		const char* q = query.c_str();
		qstate = mysql_query(conn, q);
		if (qstate)
		{
			cout << "Query failed: " << mysql_error(conn) << endl;
			status = 2;
			error = (char*)mysql_error(conn);
		}
		else 
		{
			cout << "Query successded";
			status = 0;
			error = (char*)mysql_error(conn);

		}
	}
	else {
		puts("Connection to database has failed!");
		string err = (string)"Connection to database has failed!" + mysql_error(conn);
		error = (char*)err.c_str();
		status = 3;
	}
	
	mysql_close(conn);
	return status;
}
extern "C"	ERP_API int AddEmployee(crm_employee* crm_employee, char* error)
{
	int status;
	MYSQL* conn;
	MYSQL_ROW row;
	MYSQL_RES* res;
	conn = mysql_init(0);
	conn = mysql_real_connect(conn, "localhost", "root", "123456789pp", "erp_crm", 3306, NULL, 0);
	if(conn)
	{
		string query = (string)"INSERT INTO crm_employee VALUES('" + crm_employee->id + "','" + crm_employee->first_name + "','"
			"" + crm_employee->middle_name + "','" + crm_employee->last_name + "','" + crm_employee->email + "',"
			"" + to_string(crm_employee->phone_number) + ",'" + crm_employee->gender + "'," + to_string(crm_employee->points) + ","
			"" + to_string(crm_employee->year_birth) + "," + to_string(crm_employee->month_birth) +
			"" + "," + to_string(crm_employee->day_birth) + ",'" + crm_employee->role_id + "'," +
			"" + to_string(crm_employee->is_available) + ");";
		cout << query << endl;
		const char* q = query.c_str();
		qstate = mysql_query(conn, q);
		if (qstate)
		{
			cout << "Query failed: " << mysql_error(conn) << endl;
			status = 2;
			error = (char*)mysql_error(conn);
		}
		else
		{
			cout << "Query successded";
			status = 0;
			error = (char*)mysql_error(conn);

		}
	}
	else {
		puts("Connection to database has failed!");
		string err = (string)"Connection to database has failed!" + mysql_error(conn);
		error = (char*)err.c_str();
		status = 3;
	}
	 mysql_close(conn);
	 return status;
}

			
extern "C"	ERP_API void AddRole(Crm_roles* crm_roles,  char* error)
{
	int status;
	MYSQL* conn;
	MYSQL_ROW row;
	MYSQL_RES *res;
	conn = mysql_init(0);
	conn = mysql_real_connect(conn, "localhost", "root", "123456789pp", "erp_crm", 3306, NULL, 0);
	if (conn)
	{
		string query = string("INSERT INTO Crm_roles VALUES('") + crm_roles->role_id + "','" + crm_roles->role+ "');";
		cout << query << endl;
		const char* q = query.c_str();
		qstate = mysql_query(conn, q);
		if (qstate)
		{
			cout << "Query failed: " << mysql_error(conn) << endl;
			status = 2;
			error = (char*)mysql_error(conn);
		}
		else
		{
			cout << "Query successded";
			status = 0;
			error = (char*)mysql_error(conn);

		}
	}
	else {
		puts("Connection to database has failed!");
		string err = (string)"Connection to database has failed!" + mysql_error(conn);
		error = (char*)err.c_str();
		status = 3;
	}
	mysql_close(conn);
}
extern "C"	ERP_API int AddOpportunity(Customer_Opportunities* opportunities,  char* error)
{

	int status;
	MYSQL* conn;
	MYSQL_ROW row;
	MYSQL_RES *res;
	conn = mysql_init(0);
	conn = mysql_real_connect(conn, "localhost", "root", "123456789pp", "erp_crm", 3306, NULL, 0);
	if (conn) 
	{
		string query = string("INSERT INTO customer_opportunities VALUES('") + opportunities->opportunity_id + "','" + opportunities->customer_id + "',"
			""  + to_string(opportunities->status) +  "," + to_string(opportunities->expected_revenue)+ ",'" + opportunities->notes + "','" + 
			"" +opportunities->start_date +  "','" + opportunities->end_data +  "'";
		query += ");";
		cout << query << endl;
		const char* q = query.c_str();
		qstate = mysql_query(conn, q);
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
	else {
		puts("Connection to database has failed!");
		string err = (string)"Connection to database has failed!" + mysql_error(conn);
		string s = mysql_error(conn);
		strcpy_s(error, s.length() + 1, mysql_error(conn));
		status = 3;
	}
	mysql_close(conn);
	return status;
}

extern "C"	ERP_API int AddOpportunitie_detail(char* opportunity_id, char* product_id, char* error)
{
	int status;
	int _qstate;
	MYSQL* conn;
	MYSQL_ROW row;
	MYSQL_RES *res;
	conn = mysql_init(0);
	conn = mysql_real_connect(conn, "localhost", "root", "123456789pp", "erp_crm", 3306, NULL, 0);
	if (conn)
	{
		
		string query = string("INSERT INTO opportunities_details VALUES('") + opportunity_id + "','" + product_id + "');";
		cout << query << endl;
		const char* q = query.c_str();
		_qstate = mysql_query(conn, q);
		if (_qstate)
		{
			cout << "Query failed: " << mysql_error(conn) << endl;
			status = 2;
			string s = mysql_error(conn);
			strcpy_s(error, s.length() + 1, mysql_error(conn));
		}
		else
		{
			cout << "Query successded" << endl;
			status = 0;
			string s = mysql_error(conn);
			strcpy_s(error, s.length() + 1, mysql_error(conn));

		}
	}
	else {
		puts("Connection to database has failed!");
		string err = (string)"Connection to database has failed!" + mysql_error(conn);		
		strcpy_s(error, err.length() + 1, err.c_str());
		status = 3;
	}
	mysql_close(conn);
	return status;
}
extern "C"	ERP_API void getCustomerById(char* customer_id, Customer** customer, int** _status, char* error)
{
		
		*_status = (int*)CoTaskMemAlloc(sizeof(int));
		int* status = *_status;
		MYSQL* conn;
		MYSQL_ROW row;
		MYSQL_RES *res;
		conn = mysql_init(0);
		conn = mysql_real_connect(conn, "localhost", "root", "123456789pp", "erp_crm", 3306, NULL, 0);
		if (conn)
		{
			string query = "SELECT * FROM customers WHERE customer_id = '";
			query += customer_id;
			query += "';";
			cout << query << endl;
			const char* q = query.c_str();
			qstate = mysql_query(conn, q);
			
			

			if (!qstate)
			{
				
				res = mysql_store_result(conn);
				if (res->row_count > 0)
				{
					
					*customer = (Customer*)CoTaskMemAlloc(sizeof(Customer));
					Customer* p = *customer;
					*status = 0;
					string x;
					cout << "query succeedd" << endl;
					while (row = mysql_fetch_row(res))
					{

						cout << row[5];

						p->customer_id = row[0];
						p->first_name = row[1];
						p->middle_name = row[2];
						p->last_name = row[3];
						p->email = row[4];
						p->phone_number = stringToInt(row[5]);
						p->year_birth = stringToInt(row[6]);
						p->month_birth = stringToInt(row[7]);
						p->day_birth = stringToInt(row[8]);
						p->gender = row[9];
						p->loyality_points = stringToInt(row[10]);
						p->type = stringToInt(row[11]);
						p->company = row[12];
						p->company_email = row[13];
						p->is_lead = row[14];
					}
				}
				else
				{
					string s = "Error This customer id does not exixt";
					strcpy_s(error, s.length() + 1, s.c_str());
					*status = 4;
				}
			}
			else
			{
				cout << "Query failed: " << mysql_error(conn) << endl;
				*status = 2;
				string s = mysql_error(conn);
				strcpy_s(error, s.length() + 1, mysql_error(conn));
			}
		}
		else {
			puts("Connection to database has failed!");
			string err = (string)"Connection to database has failed!" + mysql_error(conn);;
			strcpy_s(error, err.length() + 1, err.c_str());
			*status = 3;
		}
		mysql_close(conn);
}
unsigned int stringToInt(char * c) 
{
	unsigned int number;
	stringstream q(c);
	q >> number;
	return number;
}
