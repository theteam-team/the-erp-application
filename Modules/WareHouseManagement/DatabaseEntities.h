#pragma once

struct Inventory
{
	char* id;
	char* governorate;
	char* city;
	char* street;
	double length;
	double width;
	double height;
};

struct Product
{
	char* id;
	char* name;
	char* description;
	double price;
	double weight;
	double length;
	double width;
	double height;
	unsigned int unitsInStock;
	unsigned int sold;
	unsigned int purchased;
};

struct ProductInInventory
{
	char* inventoryID;
	char* productID;
	char* name;
	char* position;
	double weight;
	double length;
	double width;
	double height;
	unsigned int unitsInInventory;
};

struct ProductInOrder
{
	char* orderID;
	char* productID;
	char* inventoryID;
	unsigned int unitsOrdered;
	unsigned int unitsDone;
};

struct CustomerProduct
{
	char* customerID;
	char* orderID;
	char* productID;
	char* name;
	unsigned int unitsOrdered;
	double price;
};

struct Order 
{
	char* id;
	unsigned int incoming;
	unsigned int outgoing;
	char* requiredDate;
	char* completedDate;
	char* orderStatus;
	double totalPrice;
	char* customerID;
	char* supplierID;
	char* paymentID;
	char* shipmentID;
};

struct Report
{
	unsigned int deliveriesCycleTime;
	unsigned int receiptsCycleTime;
	double inventoryValue;
	double outgoingValue;
	double incomingValue;
	double inventoryTurnover;
};

struct ProductMoves
{
	char* time;
	char* id;
	char* name;
	char* status;
	unsigned int quantity;
};

struct Payment
{
	char* id;
	char* method;
	char* date;
	double amount;
};

struct ConnectionString
{
	char* SERVER;
	char* USER;
	char* PORT;
	char* PASSWORD;
	char* DATABASE;

};