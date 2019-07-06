#pragma once
struct AProduct
{
	char* id;
	char* name;
	unsigned int unitsInOrder;
	double price;
	double totalPrice;
};
struct ProductSold
{
	char* id;
	unsigned int unitsSold;
	double cost;
	double price;
	double profit;
};
struct Invoice
{
	char* suppId;
	char* suppName;
	unsigned int suppPhone;
	char* suppMail;
	//char* payment_method;
	char* productName;
	double productCost;
	unsigned int suppUnits;
	double totalCost;
	double totalPaid;
	double debts;
};
struct AOrder 
{
	char* id;
	char* requiredDate;
	char* completedDate;
	char* orderStatus;
	//char* customerID;
	char* paymentID;
	double totalPrice;
};
struct ProductInOrder
{
	char* orderID;
	char* productID;
	unsigned int unitsOrdered;
	unsigned int unitsDone;
};
struct OrderDetails {
	char* orderID;
	char* productID;
	char* productName;
	unsigned int unitsOrdered;
	double productCost;
	double totalCost;
};
struct Customer
{
	char* customer_id;
	char* name;
	unsigned int phone_number;
	char *email;
	char* dateOfBirth;
	char* gender;
	unsigned int loyality_points;
	unsigned int type;
	char* company;
	char* company_email;
	bool is_lead;
};
struct Account {
	char* account_id;
	double account_money;
	char* creation_date;
	double account_debts;
};
struct ConnectionString
{
	char* SERVER;
	char* USER;
	char* PORT;
	char* PASSWORD;
	char* DATABASE;

};
struct Out{
	char* payment_id;
	char* payment_method;
	//char* payment_date;
	double payment_amount;
};
struct AnInvoice {
	char* supplied_department;
	char* employee_ID;
	char* employee_name;
	char* Supplier_Name;
	char* Supplier_ID;
	char* Supplier_Email;
	char* Supplier_Phone_Number;
	char* product_id;
	int Units_Supplied;
	double product_cost;
	
};