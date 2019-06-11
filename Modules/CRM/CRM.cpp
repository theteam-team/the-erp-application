#include"CrmHeader.h"
#include "CrmDatabase_tables.h"
#include <stdio.h>
#include<iostream>
#include <sstream> 
#include <string>
#include<mysql.h>
int status;
using namespace std;
int qstate;

#define SERVER "localhost"
#define USER "root" //your username
#define PASSWORD "rana"//your password for mysql
#define DATABASE "erp" //d	atabase name
MYSQL* conn;
MYSQL_ROW row;
MYSQL_RES* res;
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
extern "C"	ERP_API int AddCustomer(Customer* customer, char* error)
{


	db_response::ConnectionFunction(error);
	if (conn) {
		puts("Successful connection to database!");
		string query = "INSERT INTO Customer VALUES";
		query += "('";
		query += customer->customer_id;
		query += "','";
		query += customer->name;
		query += "',";
		query += to_string(customer->phone_number);
		query += ",'";
		query += customer->email;
		query += "','";
		query += customer->dateOfBirth;
		query += "','";
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
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}
extern "C"	ERP_API int AddEmployee(Employee* crm_employee, char* error)
{
	db_response::ConnectionFunction(error);
	if (conn)
	{
		string query = (string)"INSERT INTO employee VALUES('" + crm_employee->id + "','" + crm_employee->name + "','"
			"" + to_string(crm_employee->phone_number) + "','" + crm_employee->email + "','" + crm_employee->dateOfBirth +
			"','" + crm_employee->gender + "'," + to_string(crm_employee->points) + "," + to_string(crm_employee->is_available) + "," +
			"'" + crm_employee->role_id + "','" + crm_employee->department + "');";
		cout << query << endl;
		const char* q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}



extern "C"	ERP_API int AddOpportunity(Opportunity* opportunities, char* error)
{
	db_response::ConnectionFunction(error);
	status = 0;
	if (conn)
	{
		string x = opportunities->employee_id;
		if (x == "")
		{
			x = "Null";
		}
		else
		{
			x = "'" + x + "'";
		}
		string query = string("INSERT INTO opportunities VALUES('") + opportunities->opportunity_id + "',"
			+ to_string(opportunities->status) + "," + to_string(opportunities->expected_revenue) + ",'" + opportunities->notes + "','" +
			"" + opportunities->start_date + "','" + opportunities->end_data + "','" + opportunities->customer_id + "'," + x + "";
		query += ");";
		cout << query << endl;
		const char* q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}

extern "C"	ERP_API int AddOpportunitie_detail(char* opportunity_id, char** product_id, int numOfProducts, char* error)
{
	db_response::ConnectionFunction(error);
	if (conn)
	{
		for (int i = 0; i < numOfProducts; ++i)
		{

			string query = string("INSERT INTO opportunities_details VALUES('") + opportunity_id + "','" + product_id[i] + "');";
			cout << query << endl;
			const char* q = query.c_str();
			qstate = mysql_query(conn, q);
			checkQuery(qstate, error);
		}
		mysql_close(conn);
	}

	return status;
}
extern "C"	ERP_API Customer* getCustomerById(char* customer_id, char* error)
{
	status = 0;
	db_response::ConnectionFunction(error);
	if (conn)
	{
		string query = "SELECT * FROM Customer WHERE customer_id = '";
		query += customer_id;
		query += "';";
		cout << query << endl;
		const char* q = query.c_str();
		qstate = mysql_query(conn, q);
		if (checkQuery(qstate, error))
		{

			res = mysql_store_result(conn);
			if (res->row_count > 0)
			{

				Customer* customer = (Customer*)CoTaskMemAlloc(sizeof(Customer));
				Customer* p = customer;
				status = 0;
				cout << "query succeedd" << endl;
				while (row = mysql_fetch_row(res))
				{
					p->customer_id = row[0];
					p->name = row[1];
					p->phone_number = stoi(row[2]);
					p->email = row[3];
					p->dateOfBirth = row[4];
					p->gender = row[5];
					p->loyality_points = stringToInt(row[6]);
					p->type = stringToInt(row[7]);
					p->company = row[8];
					p->company_email = row[9];
					p->is_lead = row[10];
				}
				return customer;
			}
			else
			{
				string s = "Error This customer id does not exixt";
				strcpy_s(error, s.length() + 1, s.c_str());
				status = 2;
			}
		}
	}
	mysql_close(conn);

	return nullptr;
}
unsigned int stringToInt(char* c)
{
	unsigned int number;
	stringstream q(c);
	q >> number;
	return number;
}
bool checkQuery(int qstate, char* error)
{
	if (qstate)
	{
		cout << "Query failed: " << mysql_error(conn) << endl;
		status = 1;
		string s = mysql_error(conn);
		strcpy_s(error, s.length() + 1, mysql_error(conn));
		return false;
	}
	else
	{
		cout << "Query successded" << endl;
		status = 0;
		string s = mysql_error(conn);
		strcpy_s(error, s.length() + 1, mysql_error(conn));
		return true;

	}
}