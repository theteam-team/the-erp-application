#include"CrmHeader.h"
#include "CrmDatabase_tables.h"

using namespace std;

extern "C"	ERP_API int AddCustomerAddress(Address* address, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn) {

		string query = (string) "insert into customer_address values ('" + address->id + "', '" + address->city + "', '" + address->governate + "', '" + address->street + "', " + to_string(address->zip_code) + ", '" + address->customer_id + "')";
		cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
		mysql_close(conn);
	}
	return status;
}

extern "C"	ERP_API int AddCustomer(Customer* customer, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn) {
		
		string query = (string)"INSERT INTO customer VALUES ('" + customer->customer_id + "', '" + customer->name + "', " + to_string(customer->phone_number) + ", '" + customer->email + "', '" + customer->dateOfBirth + "', '" + customer->gender + "', " + to_string(customer->loyality_points) + ", " + to_string(customer->type) + ", '" + customer->company + "', '" + customer->company_email + "', " + to_string(customer->is_lead) + ")";
	
		cout << query << endl;
		const char* q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
		mysql_close(conn);
	}
	return status;
}

extern "C"	ERP_API int AddOpportunity(Opportunity* opportunities, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

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
		string query = (string)"INSERT INTO opportunities VALUES('" + opportunities->opportunity_id + "', " + to_string(opportunities->status) + ", " + to_string(opportunities->expected_revenue) + ", '" + opportunities->notes + "', '" + opportunities->start_date + "', '" + opportunities->end_data + "', '" + opportunities->customer_id + "', " + x + ")";
		cout << query << endl;
		const char* q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
		mysql_close(conn);
	}
	return status;
}

extern "C"	ERP_API int AddOpportunityProduct(OpportunityProduct* product, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn)
	{
		string query = (string)"INSERT INTO opportunity_product VALUES('" + product->opportunityID + "', '" + product->productID + "', " + to_string(product->units) + ")";
		cout << query << endl;
		const char* q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
		mysql_close(conn);
	}
	return status;
}

extern "C"	ERP_API int AddOpportunitie_detail(char* opportunity_id, char** product_id, int numOfProducts, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn)
	{
		for (int i = 0; i < numOfProducts; ++i)
		{

			string query = (string)"INSERT INTO opportunities_details VALUES('" + opportunity_id + "','" + product_id[i] + "');";
			cout << query << endl;
			const char* q = query.c_str();
			qstate = mysql_query(conn, q);
			checkQuery(qstate, error, conn);
		}
		mysql_close(conn);
	}

	return status;
}

extern "C"	ERP_API Customer* getCustomerById(char* customer_id, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn)
	{
		string query = (string)"SELECT * FROM customer WHERE customer_id = '" + customer_id + "'";

		cout << query << endl;
		const char* q = query.c_str();
		qstate = mysql_query(conn, q);

		if (checkQuery(qstate, error, conn))
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


extern "C"	ERP_API int GetAllCustomers(Customer** customer, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	int numberOfRows = 0;
	unsigned int numOfFields;

	if (conn) {

		mysql_free_result(res);

		qstate = mysql_query(conn, "select * from customer");
		cout << "select * from customer" << endl;
		if (checkQuery(qstate, error, conn)) {

			res = mysql_store_result(conn);

			if (res->row_count > 0)
			{
				*customer = (Customer*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Customer));
				cout << res->row_count << endl;
				numOfFields = mysql_num_fields(res);

				Customer *_customer = *customer;

				while (row = mysql_fetch_row(res)) {

					_customer->customer_id = row[0];
					row[1] ? _customer->name = row[1] : _customer->name = nullptr;
					row[2] ? _customer->phone_number = stoi(row[2]) : _customer->phone_number = 0;
					row[3] ? _customer->email = row[3] : _customer->email = nullptr;
					row[4] ? _customer->dateOfBirth = row[4] : _customer->dateOfBirth = nullptr;
					row[5] ? _customer->gender = row[5] : _customer->gender = nullptr;
					row[6] ? _customer->loyality_points = stoi(row[6]) : _customer->loyality_points = 0;
					row[7] ? _customer->type = stoi(row[7]) : _customer->type = 0;
					row[8] ? _customer->company = row[8] : _customer->company = nullptr;
					row[9] ? _customer->company_email = row[9] : _customer->company_email = nullptr;
					row[10] ? _customer->is_lead = row[10] : _customer->is_lead = false;

					numberOfRows++;
					_customer++;
				}
			}
			else
			{
				string s = "No Customers yet";
				cout << s << endl;
				strcpy_s(error, s.length() + 1, s.c_str());
				status = 2;
			}
		}
	}
	return numberOfRows;
}

extern "C"	ERP_API int GetAllOpportunities(Opportunity** opportunity, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	int numberOfRows = 0;
	unsigned int numOfFields;

	if (conn) {

		mysql_free_result(res);

		qstate = mysql_query(conn, "select * from opportunities");
		cout << "select * from customer" << endl;
		if (checkQuery(qstate, error, conn)) {

			res = mysql_store_result(conn);

			if (res->row_count > 0)
			{
				*opportunity = (Opportunity*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Opportunity));
				cout << res->row_count << endl;
				numOfFields = mysql_num_fields(res);

				Opportunity *_opportunity = *opportunity;

				while (row = mysql_fetch_row(res)) {

					_opportunity->opportunity_id = row[0];
					row[1] ? _opportunity->status = stoi(row[1]) : _opportunity->status = 0;
					row[2] ? _opportunity->expected_revenue = stod(row[2]) : _opportunity->expected_revenue = 0;
					row[3] ? _opportunity->notes = row[3] : _opportunity->notes = nullptr;
					row[4] ? _opportunity->start_date = row[4] : _opportunity->start_date = nullptr;
					row[5] ? _opportunity->end_data = row[5] : _opportunity->end_data = nullptr;
					row[6] ? _opportunity->customer_id = row[6] : _opportunity->customer_id = nullptr;
					row[7] ? _opportunity->employee_id = row[7] : _opportunity->employee_id = nullptr;

					numberOfRows++;
					_opportunity++;
				}
			}
			else
			{
				string s = "No Opportunities yet";
				cout << s << endl;
				strcpy_s(error, s.length() + 1, s.c_str());
				status = 2;
			}
		}
	}
	return numberOfRows;
}

extern "C"	ERP_API int GetAllOpportunityProducts(char* id, OpportunityProduct** product, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	int numberOfRows = 0;
	unsigned int numOfFields;

	if (conn) {

		mysql_free_result(res);

		string query = (string)"select * from opportunity_product where Opportunities_Opportunity_ID = '" + id + "'";
		qstate = mysql_query(conn, query.c_str());
		cout << query << endl;

		if (checkQuery(qstate, error, conn)) {

			res = mysql_store_result(conn);

			if (res->row_count > 0)
			{
				*product = (OpportunityProduct*)CoTaskMemAlloc((int)(res->row_count) * sizeof(OpportunityProduct));
				cout << res->row_count << endl;
				numOfFields = mysql_num_fields(res);

				OpportunityProduct *_product = *product;

				while (row = mysql_fetch_row(res)) {

					_product->opportunityID = row[0];
					row[1] ? _product->productID = row[1] : _product->productID = nullptr;
					row[2] ? _product->units = stoi(row[2]) : _product->units = 0;
					

					numberOfRows++;
					_product++;
				}
			}
			else
			{
				string s = "No Opportunity Products yet";
				cout << s << endl;
				strcpy_s(error, s.length() + 1, s.c_str());
				status = 2;
			}
		}
	}
	return numberOfRows;
}


bool checkQuery(int qstate, char* error, MYSQL* conn)
{
	int status = 0;

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