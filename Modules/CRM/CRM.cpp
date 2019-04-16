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

#define SERVER "localhost"
#define USER "root" //your username
#define PASSWORD "123456789pp" //your password for mysql
#define DATABASE "erp" //database name
MYSQL* conn;
MYSQL_ROW row;
MYSQL_RES* res;
class db_response {

public:
	static void ConnectionFunction(char * error) {

		conn = mysql_init(0);
		conn = mysql_real_connect(conn, SERVER, USER, PASSWORD, DATABASE, 3306, NULL, 0);
		if (conn) {
			cout << "Database Connected To MySql" << conn << endl;

		}
		else
		{
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
		checkQuery(qstate, &status ,error);
		mysql_close(conn);
	}
	return status;
}
extern "C"	ERP_API int AddEmployee(Employee* crm_employee, char* error)
{
	db_response::ConnectionFunction(error);
	if(conn)
	{
		string query = (string)"INSERT INTO employee VALUES('" + crm_employee->id + "','" + crm_employee->first_name + "','"
			"" + crm_employee->middle_name + "','" + crm_employee->last_name + "','" + crm_employee->email + "',"
			"" + to_string(crm_employee->phone_number) +  "," + to_string(crm_employee->year_birth) + "," + to_string(crm_employee->month_birth) +
			"" + "," + to_string(crm_employee->day_birth) + ",'" + crm_employee->gender + "'," + to_string(crm_employee->points)+ "," + to_string(crm_employee->is_available) + "," +
			"'" + crm_employee->role_id + "','" + crm_employee->department + "');";
		cout << query << endl;
		const char* q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, &status, error);
		mysql_close(conn);
	}
	 return status;
}

			

extern "C"	ERP_API int AddOpportunity(Opportunity* opportunities,  char* error)
{
	db_response::ConnectionFunction(error);
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
		string query = string("INSERT INTO opportunities VALUES('") + opportunities->opportunity_id + "','" + opportunities->customer_id + "',"
			"" + x + "," + to_string(opportunities->status) +  "," + to_string(opportunities->expected_revenue)+ ",'" + opportunities->notes + "','" +
			"" +opportunities->start_date +  "','" + opportunities->end_data +  "'";
		query += ");";
		cout << query << endl;
		const char* q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, &status, error);
		mysql_close(conn);
	}
	return status;
}

extern "C"	ERP_API int AddOpportunitie_detail(char* opportunity_id, char** product_id,  int numOfProducts, char* error)
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
			checkQuery(qstate, &status, error);
		}
		mysql_close(conn);
	}
	
	return status;
}
extern "C"	ERP_API void getCustomerById(char* customer_id, Customer** customer, int* _status, char* error)
{
		
		int* mstatus = _status;
		db_response::ConnectionFunction(error);
		if (conn)
		{
			string query = "SELECT * FROM customers WHERE customer_id = '";
			query += customer_id;
			query += "';";
			cout << query << endl;
			const char* q = query.c_str();
			qstate = mysql_query(conn, q);
			checkQuery(qstate, mstatus, error);
				
				res = mysql_store_result(conn);
				if (res->row_count > 0)
				{
					
					*customer = (Customer*)CoTaskMemAlloc(sizeof(Customer));
					Customer* p = *customer;
					*mstatus = 0;
					cout << "query succeedd" << endl;
					while (row = mysql_fetch_row(res))
					{
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
					*mstatus = 2;
				}
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
void checkQuery(int qstate, int * status,char * error) 
{
	if (qstate)
	{
		cout << "Query failed: " << mysql_error(conn) << endl;
		*status = 1;
		string s = mysql_error(conn);
		strcpy_s(error, s.length() + 1, mysql_error(conn));
	}
	else
	{
		cout << "Query successded";
		*status = 0;
		string s = mysql_error(conn);
		strcpy_s(error, s.length() + 1, mysql_error(conn));

	}
}
