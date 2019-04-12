#include"Header.h"
#include <stdio.h>
#include<iostream>
#include <mysqlx/xapi.h>
#include <mysqlx/xdevapi.h>


struct Customer
{
	char* ID;
	char* first_name;
	char* middle_name;
	char* last_name;
	char* email;
	unsigned int phone_number;
	char* gender;
	unsigned int loyality_points;
	unsigned type;
	char* birth_date;
	char* company;
	char* company_email;
	bool is_lead;
};

using ::std::cout;
using ::std::endl;
using namespace ::mysqlx;

extern "C"	ERP_API void AddCustomer(Customer* customer, char * buf)
{
	try
	{
		string url = "mysqlx://root:123456789pp@127.0.0.1";
		

		Session sess(url);
		// Get a list of all available schemas
		Schema db = sess.getSchema("erp_crm");
		Table employees = db.getTable("customers");
		employees.insert("customer_id", "first_name", "middle_name","last_name", "email", "phone_number",
			"gender", "loyality_points", "type"/*,"birth_date"*/,"company", "company_email", "is_lead")
			.values(customer->ID, customer->first_name, customer->middle_name,customer->last_name, 
				customer->email, customer->phone_number, customer->gender, customer->loyality_points, 
				customer->type/*, customer->birth_date*/, 
				customer->company, customer->company_email, customer->is_lead).execute();
		//char x[100] = "success";
		sess.close();
		std::string x= "success";
		strcpy_s(buf, x.length() + 1, x.c_str());
		
		
	}
	catch(const mysqlx::Error & err)
	{
		cout << "ERROR: " << err << endl;
	}
}
extern "C"	ERP_API int MultiplyNumbers(int x, int y)
{

	return x * y;
}
	
