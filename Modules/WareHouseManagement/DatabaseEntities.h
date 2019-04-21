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
	char* Order_ID;
	char* Order_Required_Date;
	char* Order_Completed_Date;
	char* Payment_Payment_ID;
	char* Shipment_Shipment_ID;
	char* Opportunities_Opportunity_ID;
};
struct Product_In_Order 
{
	char* productId;
	unsigned int Units;

};