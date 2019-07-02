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


MYSQL* conn;
MYSQL_ROW row;
MYSQL_RES* res;
class db_response {

public:
	static void ConnectionFunction(char* error, ConnectionString con) {

		conn = mysql_init(0);
		conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);
		if (!conn) {

			cout << "Failed To Connect!" << mysql_errno(conn) << endl;
			string err = (string)"Connection to database has failed!" + mysql_error(conn);;
			strcpy_s(error, err.length() + 1, err.c_str());
			status = 3;
		}
	}
};

extern "C"	ERP_API int AddCustomerAddress(Address* address, char* error, ConnectionString con)
{
	status = 0;
	db_response::ConnectionFunction(error, con);

	if (conn) {

		string query = (string) "insert into Customer_Address values ('" + address->id + "', '" + address->city + "', '" + address->governate + "', '" + address->street + "', " + to_string(address->zip_code) + ", '" + address->customer_id + "')";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error);
		mysql_close(conn);
	}
	return status;
}


extern "C"	ERP_API int AddCustomer(Customer* customer, char* error, ConnectionString con)
{


	db_response::ConnectionFunction(error, con);
	if (conn) {
		
		cout << customer->customer_id<<endl;
		cout << customer->name << endl;
		cout << customer->phone_number<<endl;
		cout << customer->email << endl;
		cout << customer->dateOfBirth << endl;
		//cout << customer->gender<< endl;
		cout << customer->loyality_points<< endl;
		cout << customer->type<< endl;
		cout << customer->company<< endl;
		cout << customer->company_email<< endl;
		cout << customer->is_lead<< endl;
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
extern "C"	ERP_API int AddEmployee(Employee* crm_employee, char* error, ConnectionString con)
{
	db_response::ConnectionFunction(error, con);
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
extern "C"	ERP_API int AddOpportunity(Opportunity* opportunities, char* error, ConnectionString con)
{
	db_response::ConnectionFunction(error, con);
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
extern "C"	ERP_API int AddOpportunitie_detail(char* opportunity_id, char** product_id, int numOfProducts, char* error, ConnectionString con)
{
	db_response::ConnectionFunction(error, con);
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
extern "C"	ERP_API Customer* getCustomerById(char* customer_id, char* error, ConnectionString con)
{
	status = 0;
	db_response::ConnectionFunction(error, con);
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
					row[1] ? p->name = row[1] : p->name = nullptr;
					row[2] ? p->phone_number = stoi(row[2]) : p->phone_number = 0;
					row[3] ? p->email = row[3] : p->email = nullptr;
					row[4] ? p->dateOfBirth = row[3] : p->dateOfBirth = nullptr;
					row[5] ? p->gender = row[5] : p->gender = nullptr;
					row[6] ? p->loyality_points = stoi(row[6]) : p->loyality_points = 0;
					row[7] ? p->type = stoi(row[7]) : p->type = 0;
					row[8] ? p->company = row[8] : p->company = nullptr;
					row[9] ? p->company_email = row[9] : p->company_email = nullptr;
					row[10] ? p->is_lead = row[3] : p->is_lead = false;
										
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