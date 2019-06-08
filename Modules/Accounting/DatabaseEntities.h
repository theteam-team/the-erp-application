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