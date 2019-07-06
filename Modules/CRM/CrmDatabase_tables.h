#pragma once

struct Customer
{
	char* customer_id;
	char* name;
	unsigned int phone_number;	
	char* email;
	char* dateOfBirth;
	char* gender;
	unsigned int loyality_points;
	unsigned int type;
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
	unsigned int status;
	double expected_revenue;
	char* notes;
	char* start_date;
	char* end_data;
	char* customer_id;
	char* employee_id;
};

struct OpportunityProduct 
{
	char* opportunityID;
	char* productID;
	unsigned int units;
};

struct Employee
{
	char* id;
	char* name;	
	unsigned int phone_number;
	char *email;
	char* dateOfBirth;	
	char* gender;
	unsigned int points;
	bool is_available;
	char* role_id;
	char* department;
};

struct  OpportunityDetails
{
	char* opportunityID;
	char* productID;
};

struct Crm_roles
{
	char* role_id;
	char* role;
};

struct ConnectionString
{
	char* SERVER;
	char* USER;
	char* PORT;
	char* PASSWORD;
	char* DATABASE;

};