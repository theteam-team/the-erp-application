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
	char* orderId;
	char* productId;
	unsigned int units;
	unsigned int unitsDone;
};