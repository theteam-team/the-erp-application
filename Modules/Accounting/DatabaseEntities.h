#pragma once
struct Product
{
	char* id;
	char* name;
	char* description;
	char* position;
	double price;
	double size;
	double weight;
	unsigned int unitsInStock;
};
struct ProductSold
{
	char* id;
	unsigned int unitsSold;
	unsigned int profit;
	unsigned int price;
	unsigned int cost;
};
struct Invoice
{
	char* id;
	char* suppName;
	unsigned int suppPhone;
	char* suppMail;
	unsigned int payment_method;
	unsigned int suppUnits;
	unsigned int totalPaid;
	unsigned int totalCost;
	unsigned int depts;
};
struct Order 
{
	char* id;
	char* requiredDate;
	char* completedDate;
	char* orderStatus;
	char* customerID;
	char* paymentID;
};

struct ProductInOrder
{
	char* orderID;
	char* productID;
	unsigned int unitsOrdered;
	unsigned int unitsDone;
};