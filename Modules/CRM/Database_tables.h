#pragma once
struct Customer
{
	char* customer_id;
	char* first_name;
	char* middle_name;
	char* last_name;
	char* email;
	unsigned int phone_number;
	char* gender;
	unsigned int loyality_points;
	unsigned int type;
	unsigned int year_birth;
	unsigned int month_birth;
	unsigned int day_birth;
	char* company;
	char* company_email;
	bool is_lead;
};
struct Address
{
	char* id;
	char* city;
	char* governate;
	char* street;
	unsigned int zip_code;
	char* customer_id;
	char* Crm_employee_id;
};
struct Interest
{
	unsigned int interest_id;
	char* customer_id;
};
struct Customer_interest
{
	unsigned int interest_id;
	unsigned char* customer_id;
	unsigned int level_of_interest;
};
struct Opportunity
{
	char* opportunity_id;
	char* customer_id;
	char* employee_id;
	unsigned int status;
	double expected_revenue;
	char* notes;
	char* start_date;
	char* end_data;
};
struct Employee
{
	char* id;
	char* first_name;
	char* middle_name;
	char* last_name;
	char *email;
	unsigned int phone_number;
	unsigned int year_birth;
	unsigned int month_birth;
	unsigned int day_birth;
	char* gender;
	unsigned int points;
	bool is_available;
	char* role_id;
	char* department;
};
struct  Opportunities_details
{
	char* opportunity_id;
	char* product_id;
};
struct Crm_roles
{
	char* role_id;
	char* role;
};
