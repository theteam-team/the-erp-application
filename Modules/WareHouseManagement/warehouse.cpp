#include "Header.h"

#pragma warning(disable : 4996)

using namespace std;

int calculateDeliveriesCycleTime(char* error, ConnectionString con) {

	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	vector<int> daysDiff;
	int total = 0, avg;
	int y1, y2, m1, m2, d1, d2;
	int increment[12] = { 1, -2, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1 };

	if (conn)
	{
		mysql_free_result(res);

		string query = (string)"select Order_Required_Date, Order_Completed_Date from order_table where outgoing = 1 and Order_Status = 'Done'";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);

		if (checkQuery(qstate, error, conn)) {
			res = mysql_store_result(conn);

			if (res->row_count > 0)
			{
				cout << res->row_count << endl;
				unsigned int i = 0;
				char* str1;
				char* str2;

				while (row = mysql_fetch_row(res)) {

					str1 = row[0];
					str2 = row[1];

					replace(str1, str1 + strlen(str1), '-', ' ');
					istringstream(str1) >> y1 >> m1 >> d1;

					replace(str2, str2 + strlen(str2), '-', ' ');
					istringstream(str2) >> y2 >> m2 >> d2;

					int daysInc = 0;
					if (d2 - d1 < 0)
					{
						int month = m2 - 2; // -1 from zero, -1 previous month.
						if (month < 0)
							month = 11; // Previous month is December.
						daysInc = increment[month];
						if ((month == 1) && (y2 % 4 == 0))
							daysInc++; // Increment days for leap year.
					}

					int total1 = y1 * 360 + m1 * 30 + d1;
					int total2 = y2 * 360 + m2 * 30 + d2;
					int diff = total2 - total1;
					int years = diff / 360;
					int months = (diff - years * 360) / 30;
					int days = diff - years * 360 - months * 30 + daysInc;

					// Extra calculation when we can pass one month instead of 30 days.
					if (d1 == 1 && d2 == 31) {
						months--;
						days = 30;
					}

					daysDiff.push_back(days);
				}
			}
			else
			{
				string s = "No Deliveries";
				cout << s << endl;
				status = 2;
			}
		}
		//
	}

	for (unsigned int i = 0; i < daysDiff.size(); i++)
		total += daysDiff[i];

	avg = total / daysDiff.size();

	return avg;
}

int calculateReceiptsCycleTime(char* error, ConnectionString con) {

	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	vector<int> daysDiff;
	int total = 0, avg;
	int y1, y2, m1, m2, d1, d2;
	int increment[12] = { 1, -2, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1 };

	if (conn)
	{
		mysql_free_result(res);

		string query = (string)"select Order_Required_Date, Order_Completed_Date from order_table where incoming = 1 and Order_Status = 'Done'";
		//////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);

		if (checkQuery(qstate, error, conn)) {
			res = mysql_store_result(conn);

			if (res->row_count > 0)
			{
				cout << res->row_count << endl;
				unsigned int i = 0;
				char* str1;
				char* str2;

				while (row = mysql_fetch_row(res)) {

					str1 = row[0];
					str2 = row[1];

					replace(str1, str1 + strlen(str1), '-', ' ');
					istringstream(str1) >> y1 >> m1 >> d1;

					replace(str2, str2 + strlen(str2), '-', ' ');
					istringstream(str2) >> y2 >> m2 >> d2;

					int daysInc = 0;
					if (d2 - d1 < 0)
					{
						int month = m2 - 2; // -1 from zero, -1 previous month.
						if (month < 0)
							month = 11; // Previous month is December.
						daysInc = increment[month];
						if ((month == 1) && (y2 % 4 == 0))
							daysInc++; // Increment days for leap year.
					}

					int total1 = y1 * 360 + m1 * 30 + d1;
					int total2 = y2 * 360 + m2 * 30 + d2;
					int diff = total2 - total1;
					int years = diff / 360;
					int months = (diff - years * 360) / 30;
					int days = diff - years * 360 - months * 30 + daysInc;

					// Extra calculation when we can pass one month instead of 30 days.
					if (d1 == 1 && d2 == 31) {
						months--;
						days = 30;
					}

					daysDiff.push_back(days);
				}
			}
			else
			{
				string s = "No Receipts";
				cout << s << endl;
				status = 2;
			}
		}
		//
	}

	for (unsigned int i = 0; i < daysDiff.size(); i++)
		total += daysDiff[i];

	avg = total / daysDiff.size();

	return avg;
}

double calculateInventoryValue(char* error, ConnectionString con) {

	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	int total = 0;

	if (conn)
	{
		mysql_free_result(res);

		string query = (string)"select Product_Price, Units_In_Stock from product";
		//////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);

		if (checkQuery(qstate, error, conn)) {
			res = mysql_store_result(conn);

			if (res->row_count > 0)
			{
				cout << res->row_count << endl;

				while (row = mysql_fetch_row(res))
					total += stod(row[0])*stoi(row[1]);
			}
			else
			{
				string s = "Inventory is empty";
				cout << s << endl;
				status = 2;
			}
		}
		//
	}
	return total;
}


double calculateOutgoingValue(char* error, ConnectionString con) {

	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	double total = 0;

	if (conn)
	{
		mysql_free_result(res);

		string query = (string)"select product.Product_Price, order_has_product.Units_In_Order from product, order_has_product, order_table where order_table.incoming = 1 and order_has_product.order_table_Order_ID = order_table.Order_ID and order_has_product.Product_Product_ID = product.Product_ID";
		//////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);

		if (checkQuery(qstate, error, conn)) {
			res = mysql_store_result(conn);

			if (res->row_count > 0)
			{
				cout << res->row_count << endl;

				while (row = mysql_fetch_row(res)) {

					total += stod(row[1])*stoi(row[0]);
				}
			}
			else
			{
				string s = "No outgoig Orders";
				cout << s << endl;
				status = 2;
			}
		}
		//
	}

	return total;
}


double calculateIncomingValue(char* error, ConnectionString con) {

	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	double total = 0;

	if (conn)
	{
		mysql_free_result(res);

		string query = (string)"select product.Product_Price, order_has_product.Units_In_Order from product, order_has_product, order_table where order_table.outgoing = 1 and order_has_product.order_table_Order_ID = order_table.Order_ID and order_has_product.Product_Product_ID = product.Product_ID";
		//////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);

		if (checkQuery(qstate, error, conn)) {
			res = mysql_store_result(conn);

			if (res->row_count > 0)
			{
				cout << res->row_count << endl;

				while (row = mysql_fetch_row(res)) {

					total += stod(row[1])*stoi(row[0]);
				}
			}
			else
			{
				string s = "No outgoig Orders";
				cout << s << endl;
				status = 2;
			}
		}
		//
	}

	return total;
}


int addToCategory(char* pid, char* cid, char* error, ConnectionString con) {

	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn)
	{
		string query = (string)"insert into product_has_category values ('" + pid + "', '" + cid + "')";
		//////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

int deleteFromCategory(char* pid, char* error, ConnectionString con) {

	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn)
	{
		string query = (string)"delete from product_has_category where Product_Product_ID = '" + pid + "'";
		//////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

int checkUnitsInStock(char* id, char* error, ConnectionString con) {

	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn)
	{
		string query = (string)"select Units_In_Stock from product where Product_ID = '" + id + "'";
		//////cout << query << endl;
		char const *q = query.c_str();
		mysql_free_result(res);
		qstate = mysql_query(conn, q);

		if (checkQuery(qstate, error, conn))
		{
			res = mysql_store_result(conn);
			if (res->row_count > 0)
			{
				row = mysql_fetch_row(res);
				status = 0;
				return stoi(row[0]);
			}
			else
			{
				string s = "Error This Product id does not exixt";
				cout << s << endl;
				strcpy_s(error, s.length() + 1, s.c_str());
				status = 2;
			}
		}
	}
	return status;
}


extern "C"	ERP_API int addToOrderTotal(Order* order, char* error, ConnectionString con) {

	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	double price = 0;

	if (conn)
	{
		string query = (string)"select total from order_table where Order_ID = '" + order->id + "'";
		//////cout << query << endl;
		char const *q = query.c_str();

		mysql_free_result(res);
		qstate = mysql_query(conn, q);

		if (checkQuery(qstate, error, conn))
		{
			res = mysql_store_result(conn);
			if (res->row_count > 0)
			{
				row = mysql_fetch_row(res);
				status = 0;
				row[0] ? price = stod(row[0]) : price = 0;
			}
		}

		price += order->totalPrice;
		query = (string)"update order_table set total = " + to_string(price) + " where Order_ID = '" + order->id + "'";
		////cout << query << endl;
		q = query.c_str();

		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C"	ERP_API int removeFromOrderTotal(Order* order, char* error, ConnectionString con) {

	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	double price = 0;

	if (conn)
	{
		string query = (string)"select total from order_table where Order_ID = '" + order->id + "'";
		////cout << query << endl;
		char const *q = query.c_str();

		mysql_free_result(res);
		qstate = mysql_query(conn, q);

		if (checkQuery(qstate, error, conn))
		{
			res = mysql_store_result(conn);
			if (res->row_count > 0)
			{
				row = mysql_fetch_row(res);
				status = 0;
				row[0] ? price = stod(row[0]) : price = 0;
			}
		}

		price -= order->totalPrice;
		query = (string)"update order_table set total = " + to_string(price) + " where Order_ID = '" + order->id + "'";
		////cout << query << endl;
		q = query.c_str();

		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C"	ERP_API int addToStock(char* id, int newUnits, char* error, ConnectionString con) {

	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	int units = 0;
	units = checkUnitsInStock(id, error, con);

	if (status == 0)
	{
		cout << newUnits << endl;
		units += newUnits;
		string query = "update product set Units_In_Stock = " + to_string(units) + " where Product_ID = '" + id + "'";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C"	ERP_API int removeFromStock(ProductInOrder* product, char* error, ConnectionString con) {

	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	int units;
	units = checkUnitsInStock(product->productID, error, con);
	units -= product->unitsOrdered;
	
	if (status == 0)
	{
		string query = "update product set Units_In_Stock = " + to_string(units) + " where Product_ID = '" + product->productID + "'";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C"	ERP_API int addInventory(Inventory* inventory, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn) {

		string query = (string) "insert into inventory values ('" + inventory->id + "', '" + inventory->governorate + "', '" + inventory->city + "', '" + inventory->street + "', " + to_string(inventory->length) + ", " + to_string(inventory->width) + ", " + to_string(inventory->height) + ")";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C"	ERP_API int addProduct(Product* product, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn) {
		
		string query = (string) "insert into product values ('" + product->id + "', '" + product->name + "', '" + product->description + "', " + to_string(product->price) + ", " + to_string(product->weight) + ", " + to_string(product->length) + ", " + to_string(product->width) + ", " + to_string(product->height) + ", " + to_string(product->unitsInStock) + ")";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);

		if (product->sold == 1)
			addToCategory(product->id, "1", error, con);
		if (product->purchased == 1)
			addToCategory(product->id, "2", error, con);
	}
	return status;
}

extern "C"	ERP_API int addOrder(Order* order, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn) {

		string query = (string) "insert into order_table values ('" + order->id + "', " + to_string(order->incoming) + ", " + to_string(order->outgoing) + ", '" + order->requiredDate + "', '" + order->completedDate + "', '" + order->orderStatus + "', " + to_string(order->totalPrice) + ", '" + order->customerID + "', '" + order->supplierID + "','" + order->paymentID + "', '" + order->shipmentID + "')";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C"	ERP_API int addPotentialOrder(Order* order, char* error, ConnectionString con)
{
	
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn) {

		string query = (string) "insert into order_table (Order_ID, incoming, outgoing, Order_Required_Date, Order_Status, Customer_Customer_ID) values ('" + order->id + "', " + to_string(order->incoming) + ", " + to_string(order->outgoing) + ", '" + order->requiredDate + "', '" + order->orderStatus + "', '" + order->customerID + "')";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C" ERP_API int addPotentialProduct(ProductInOrder* product, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn) {

		string query = (string) "insert into order_has_product (order_table_Order_ID, Product_Product_ID, Units_In_Order) values ('" + product->orderID + "', '" + product->productID + "', " + to_string(product->unitsOrdered) + ")";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C" ERP_API int addPayment(Payment* payment, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn) {

		string query = (string) "insert into payment (Payment_ID, Payment_Method, Payment_Amount) values ('" + payment->id + "', '" + payment->method + "', " + to_string(payment->amount) + ")";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C" ERP_API int addOrderPayment(Order* order, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn) {

		string query = (string) "update order_table set Payment_Payment_ID = '" + order->paymentID + "' where Order_ID = '" + order->id + "'";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C" ERP_API int addProductToOrder(ProductInOrder* product, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn) {

		string query = (string) "insert into order_has_product values ('" + product->orderID + "', '" + product->productID + "', '" + product->inventoryID + "', " + to_string(product->unitsOrdered) + ", " + to_string(product->unitsDone) + ")";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C" ERP_API int addProductToInventory(ProductInInventory* product, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn) {

		string query = (string) "insert into inventory_has_product values ('" + product->inventoryID + "', '" + product->productID + "', '" + product->position + "', " + to_string(product->unitsInInventory) + ")";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C" ERP_API int editProduct(Product* product, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn) {
        string query = (string) "update product set Product_Name = '" + product->name + "', Product_Description = '" + product->description + "', " + "', Product_Price = " + to_string(product->price) + ", Product_Weight = " + to_string(product->weight) + ", length = " + to_string(product->length) + ", width = " + to_string(product->width) + ", Product_height = " + to_string(product->height) + ", Units_In_Stock = " + to_string(product->unitsInStock) + " where Product_ID = '" + product->id + "'";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);

		deleteFromCategory(product->id, error, con);

		if (product->sold == 1)
			addToCategory(product->id, "1", error, con);
		if (product->purchased == 1)
			addToCategory(product->id, "2", error, con);
	}
	return status;
}

extern "C" ERP_API int editOrder(Order* order, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn) {
		string query = (string) "update order_table set incoming = " + to_string(order->incoming) + ", outgoing = " + to_string(order->outgoing) + ", Order_Required_Date = '" + order->requiredDate + "', Order_Status = '" + order->orderStatus + "', total = " + to_string(order->totalPrice) + ", Customer_Customer_ID = '" + order->customerID + "', Supplier_Supplier_ID = " + order->supplierID + "', Payment_Payment_ID = '" + order->paymentID + "', Shipment_Shipment_ID = '" + order->shipmentID + "' where Order_ID = '" + order->id + "'";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C" ERP_API int editProductInOrder(ProductInOrder* product, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn) {
		string query = (string) "update order_has_product set Units_In_Order = " + to_string(product->unitsOrdered) + ", Units_Done = " + to_string(product->unitsDone)  + " where order_table_Order_ID = '" + product->orderID + "' and Product_Product_ID = '" + product->productID + "'";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C" ERP_API int editProductInInventory(ProductInInventory* product, char* error, ConnectionString con)
{
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn) {
		string query = (string) "update inventory_has_product set position = '" + product->position + "', Units_In_Inventory = " + to_string(product->unitsInInventory) + " where Product_Product_ID = '" + product->productID + "'";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C"	ERP_API int deleteProduct(char* id, char* error, ConnectionString con) {

	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	deleteFromCategory(id, error, con);

	if (conn)
	{
		string query = (string)"delete from inventory_has_product where Product_Product_ID = '" + id + "';delete from product where Product_ID = '" + id + "'";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C"	ERP_API int deleteOrder(char* id, char* error, ConnectionString con) {

	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn)
	{
		string query = (string)"delete from order_has_product where order_table_Order_ID = '" + id + "';delete from order_table where Order_ID = '" + id + "'";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C"	ERP_API int deleteInventory(char* id, char* error, ConnectionString con) {

	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn)
	{
		string query = (string)"delete from inventory_has_product where Inventory_Inventory_ID = '" + id + "';delete from inventory where Inventory_ID = " + id + "';";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C"	ERP_API int deleteProductFromOrder(char* oID, char* pID, char* error, ConnectionString con) {
	
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn)
	{
		string query = (string)"delete from order_has_product where order_table_Order_ID = '" + oID + "' and Product_Product_ID = '" + pID + "'";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C"	ERP_API int deleteProductFromInventory(char* iID, char* pID, char* error, ConnectionString con) {
	
	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn)
	{
		string query = (string)"delete from inventory_has_product where Inventory_Inventory_ID = '" + iID + "' and Product_Product_ID = '" + pID + "'";
		////cout << query << endl;
		char const *q = query.c_str();
		qstate = mysql_query(conn, q);
		checkQuery(qstate, error, conn);
	}
	return status;
}

extern "C"	ERP_API int searchByCategory(Product** product, char* id, char* error, ConnectionString con)
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

		string query = (string) "select * from product, product_has_category where product.Product_ID = product_has_category.Product_Product_ID and product_has_category.Category_Category_ID = '" + id + "'";
		////cout << query << endl;
		char const *q = query.c_str();

		qstate = mysql_query(conn, q);

		if (checkQuery(qstate, error, conn)) {

			res = mysql_store_result(conn);

			if (res->row_count > 0)
			{
				*product = (Product*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Product));
				Product *_product = *product;

				while (row = mysql_fetch_row(res)) {

					_product->id = row[0];
					row[1] ? _product->name = row[1] : _product->name = nullptr;
					row[2] ? _product->description = row[2] : _product->description = nullptr;
					row[3] ? _product->price = stod(row[3]) : _product->price = 0;
					row[4] ? _product->weight = stod(row[4]) : _product->weight = 0;
					row[5] ? _product->length = stod(row[5]) : _product->length = 0;
					row[6] ? _product->width = stod(row[6]) : _product->width = 0;
					row[7] ? _product->height = stod(row[7]) : _product->height = 0;
					row[8] ? _product->unitsInStock = stoi(row[8]) : _product->unitsInStock = 0;

					if (id == "1") {
						_product->sold = 1;
						_product->purchased = 0;
					}
					else if (id = "2"){
						_product->sold = 0;
						_product->purchased = 1;
					}
					else {
						_product->sold = 0;
						_product->purchased = 0;
					}

					numberOfRows++;
					_product++;
				}
			}
		}
	    else
		{
			string s = "Category does not Exist";
			cout << s << endl;
			strcpy_s(error, s.length() + 1, s.c_str());
			status = 2;
		}

	}
	return numberOfRows;
}

extern "C"	ERP_API int searchProducts(Product** product, char* key, char* value, char* error, ConnectionString con)
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

		 string query = (string) "select * from product, product_has_category where " + key + " = '" + value + "' and product.Product_ID = product_has_category.Product_Product_ID";
		 ////cout << query << endl;
		 char const *q = query.c_str();

		 qstate = mysql_query(conn, q);

		 if (checkQuery(qstate, error, conn)) {

			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *product = (Product*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Product));
				 cout << res->row_count << endl;
				 numOfFields = mysql_num_fields(res);

				 Product *_product = *product;

				 while (row = mysql_fetch_row(res)) {

					 _product->id = row[0];
					 row[1] ? _product->name = row[1] : _product->name = nullptr;
					 row[2] ? _product->description = row[2] : _product->description = nullptr;
					 row[3] ? _product->price = stod(row[3]) : _product->price = 0;
					 row[4] ? _product->weight = stod(row[4]) : _product->weight = 0;
					 row[5] ? _product->length = stod(row[5]) : _product->length = 0;
					 row[6] ? _product->width = stod(row[6]) : _product->width = 0;
					 row[7] ? _product->height = stod(row[7]) : _product->height = 0;
					 row[8] ? _product->unitsInStock = stoi(row[8]) : _product->unitsInStock = 0;

					 if (row[10] == "1") {
						 _product->sold = 1;
						 _product->purchased = 0;
					 }
					 else if (row[10] = "2"){
						 _product->sold = 0;
						 _product->purchased = 1;
					 }
					 else {
						 _product->sold = 0;
						 _product->purchased = 0;
					 }

					 numberOfRows++;
					 _product++;
				 }
			 }
			 else
			 {
				 string s = "No Products Found";
				 cout << s << endl;
				 strcpy_s(error, s.length() + 1, s.c_str());
				 status = 2;
			 }
		 }
	 }
	 return numberOfRows;
 }

extern "C"	ERP_API int searchOrders(Order** order, char* key, char* value, char* error, ConnectionString con)
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

		 string query = (string) "select * from order_table where " + key + " = '" + value + "'";
		 ////cout << query << endl;
		 char const *q = query.c_str();

		 qstate = mysql_query(conn, q);

		 if (checkQuery(qstate, error, conn)) {

			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *order = (Order*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Order));
				 cout << res->row_count << endl;
				 numOfFields = mysql_num_fields(res);

				 Order *_order = *order;
				 while (row = mysql_fetch_row(res)) {

					 _order->id = row[0];
					 row[1] ? _order->incoming = stoi(row[1]) : _order->incoming = 2;
					 row[2] ? _order->outgoing = stoi(row[2]) : _order->outgoing = 2;
					 row[3] ? _order->requiredDate = row[3] : _order->requiredDate = nullptr;
					 row[4] ? _order->completedDate = row[4] : _order->completedDate = nullptr;
					 row[5] ? _order->orderStatus = row[5] : _order->orderStatus = nullptr;
					 row[6] ? _order->totalPrice = stod(row[6]) : _order->totalPrice = 0;
					 row[7] ? _order->customerID = row[7] : _order->customerID = nullptr;
					 row[8] ? _order->supplierID = row[8] : _order->supplierID = nullptr;
					 row[9] ? _order->paymentID = row[9] : _order->paymentID = nullptr;
					 row[10] ? _order->shipmentID = row[10] : _order->shipmentID = nullptr;

					 numberOfRows++;
					 _order++;
				 }
			 }
			 else
			 {
				 string s = "No Orders Found";
				 cout << s << endl;
				 strcpy_s(error, s.length() + 1, s.c_str());
				 status = 2;
			 }
		 }
	 }
	 return numberOfRows;
 }

extern "C"	ERP_API int searchInventories(Inventory** inventory, char* key, char* value, char* error, ConnectionString con)
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

		 string query = (string) "select * from inventory where " + key + " = '" + value + "'";
		 ////cout << query << endl;
		 char const *q = query.c_str();

		 qstate = mysql_query(conn, q);

		 if (checkQuery(qstate, error, conn)) {

			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *inventory = (Inventory*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Inventory));
				 cout << res->row_count << endl;
				 numOfFields = mysql_num_fields(res);

				 Inventory *_inventory = *inventory;
				 while (row = mysql_fetch_row(res)) {

					 _inventory->id = row[0];
					 row[1] ? _inventory->governorate = row[1] : _inventory->governorate = 0;
					 row[2] ? _inventory->city = row[2] : _inventory->city = 0;
					 row[3] ? _inventory->street = row[3] : _inventory->street = 0;
					 row[4] ? _inventory->length = stod(row[4]) : _inventory->length = 0;
					 row[5] ? _inventory->width = stod(row[5]) : _inventory->width = 0;
					 row[6] ? _inventory->height = stod(row[6]) : _inventory->height = 0;

					 numberOfRows++;
					 _inventory++;
				 }
			 }
			 else
			 {
				 string s = "No Inventories Found";
				 cout << s << endl;
				 strcpy_s(error, s.length() + 1, s.c_str());
				 status = 2;
			 }
		 }
	 }
	 return numberOfRows;
 }


 extern "C"	ERP_API int reporting(Report** report, char* error, ConnectionString con) {

	 int status = 0;

	 double a, b, c, d, e, f;
	 *report = (Report*)CoTaskMemAlloc(sizeof(Report));
	 Report *_report = *report;

	 a = calculateDeliveriesCycleTime(error, con);
	 b = calculateReceiptsCycleTime(error, con);
	 c = calculateInventoryValue(error, con);
	 d = calculateOutgoingValue(error, con);
	 e = calculateIncomingValue(error, con);
	 f = d / c;
	 f = round(f * 1000.0) / 1000.0;

	 _report->deliveriesCycleTime = a;
	 _report->receiptsCycleTime = b;
	 _report->inventoryValue = c;
	 _report->outgoingValue = d;
	 _report->incomingValue = e;
	 _report->inventoryTurnover = f;

	 return status;
 }

 extern "C"	ERP_API int getProductsMoves(ProductMoves** product, char* error, ConnectionString con) {

	 int status = 0;
	 int qstate;

	 MYSQL* conn;
	 MYSQL_ROW row = nullptr;
	 MYSQL_RES *res = nullptr;

	 conn = mysql_init(0);
	 conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	 int numberOfRows = 0;

	 if (conn)
	 {
		 unsigned int numOfFields;
		 string query = (string)"select order_table.Order_Required_Date, product.Product_ID, product.Product_Name, order_table.incoming, order_has_product.Units_In_Order from product, order_table, order_has_product where product.Product_ID = order_has_product.Product_Product_ID and order_has_product.order_table_Order_ID = order_table.Order_ID";
		 ////cout << query << endl;
		 char const *q = query.c_str();

		 mysql_free_result(res);
		 qstate = mysql_query(conn, q);

		 if (checkQuery(qstate, error, conn)) {

			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 cout << res->row_count << endl;
				 status = 0;
				 numOfFields = mysql_num_fields(res);
				 
				 *product = (ProductMoves*)CoTaskMemAlloc((int)(res->row_count) * sizeof(ProductMoves));
				 ProductMoves *_product = *product;

				 while (row = mysql_fetch_row(res)) {

					 _product->time = row[0];
					 _product->id = row[1];
					 _product->name = row[2];

					 if (stoi(row[3]) == 1) {
						 _product->status = "Incoming";
					 }
					 else {
						 _product->status = "Outgoing";
					 }
					 
					 _product->quantity = stoi(row[4]);

					 numberOfRows++;
					 _product++;
				 }
			 }
			 else
			 {
				 string s = "No product Moves";
				 cout << s << endl;
				 strcpy_s(error, s.length() + 1, s.c_str());
				 status = 2;
			 }
		 }
	 }
	 return numberOfRows;
 }


extern "C"	ERP_API int getAllProductInfo(char* id, Product** product, char* error, ConnectionString con) {

	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	if (conn)
	{
		unsigned int numOfFields;
		string query = (string)"select * from product, product_has_category where product.Product_ID = '" + id + "' and product.Product_ID = product_has_category.Product_Product_ID";
		////cout << query << endl;
		char const *q = query.c_str();

		mysql_free_result(res);

		qstate = mysql_query(conn, q);
		if (checkQuery(qstate, error, conn)) {
			res = mysql_store_result(conn);
			if (res->row_count > 0)
			{
				status = 0;
				numOfFields = mysql_num_fields(res);
				row = mysql_fetch_row(res);

				*product = (Product*)CoTaskMemAlloc(sizeof(Product));
				Product *_product = *product;
				
				_product->id = row[0];
				row[1] ? _product->name = row[1] : _product->name = nullptr;
				row[2] ? _product->description = row[2] : _product->description = nullptr;
				row[3] ? _product->price = stod(row[3]) : _product->price = 0;
				row[4] ? _product->weight = stod(row[4]) : _product->weight = 0;
				row[5] ? _product->length = stod(row[5]) : _product->length = 0;
				row[6] ? _product->width = stod(row[6]) : _product->width = 0;
				row[7] ? _product->height = stod(row[7]) : _product->height = 0;
				row[8] ? _product->unitsInStock = stoi(row[8]) : _product->unitsInStock = 0;

				if (row[10] == "1") {
					_product->sold = 1;
					_product->purchased = 0;
				}
				else if (row[10] = "2") {
					_product->sold = 0;
					_product->purchased = 1;
				}
				else {
					_product->sold = 0;
					_product->purchased = 0;
				}
			}
			else
			{
				string s = "Error This Product ID does not exist";
				cout << s << endl;
				strcpy_s(error, s.length() + 1, s.c_str());
				status = 2;
			}
		}
	}
	return status;
}


extern "C"	ERP_API int getOrderInfo(char* id, Order** order, char* error, ConnectionString con) {

	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	 if (conn)
	 {
		 unsigned int numOfFields;
		 string query = (string)"select * from order_table where Order_ID = '" + id + "'";

		 ////cout << query << endl;

		 char const *q = query.c_str();
		 mysql_free_result(res);
		 qstate = mysql_query(conn, q);

		 if (checkQuery(qstate, error, conn))
		 {
			 res = mysql_store_result(conn);
			 if (res->row_count > 0)
			 {
				 cout << "here";
				 status = 0;
				 numOfFields = mysql_num_fields(res);
				 row = mysql_fetch_row(res);

				 *order = (Order*)CoTaskMemAlloc(sizeof(Order));
				 Order* _order = *order;

				 _order->id = row[0];
				 row[1] ? _order->incoming = stoi(row[1]) : _order->incoming = 2;
				 row[2] ? _order->outgoing = stoi(row[2]) : _order->outgoing = 2;
				 row[3] ? _order->requiredDate = row[3] : _order->requiredDate = nullptr;
				 row[4] ? _order->completedDate = row[4] : _order->completedDate = nullptr;
				 row[5] ? _order->orderStatus = row[5] : _order->orderStatus = nullptr;
				 row[6] ? _order->totalPrice = stod(row[6]) : _order->totalPrice = 0;
				 row[7] ? _order->customerID = row[7] : _order->customerID = nullptr;
				 row[8] ? _order->supplierID = row[8] : _order->supplierID = nullptr;
				 row[9] ? _order->paymentID = row[9] : _order->paymentID = nullptr;
				 row[10] ? _order->shipmentID = row[10] : _order->shipmentID = nullptr;

				 cout << "here" << endl;

			 }
			 else
			 {
				 string s = "Error This order ID does not exixt";
				 cout << s << endl;
				 strcpy_s(error, s.length() + 1, s.c_str());
				 status = 2;
			 }
		 }

	 }
	 return status;
 }

extern "C"	ERP_API int showProducts(Product** product, char* error, ConnectionString con)
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
		 string query = "select * from product, product_has_category where product.Product_ID = product_has_category.Product_Product_ID";

		 qstate = mysql_query(conn, query.c_str());
		 ////cout << query << endl;
		 if (checkQuery(qstate, error, conn)) {
			 if (conn) {
				 res = mysql_store_result(conn);

				 if (res->row_count > 0)
				 {
					 *product = (Product*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Product));
					 cout << res->row_count << endl;
					 numOfFields = mysql_num_fields(res);

					 Product* _product = *product;

					 while (row = mysql_fetch_row(res)) {

						 _product->id = row[0];
						 row[1] ? _product->name = row[1] : _product->name = nullptr;
						 row[2] ? _product->description = row[2] : _product->description = nullptr;
						 row[3] ? _product->price = stod(row[3]) : _product->price = 0;
						 row[4] ? _product->weight = stod(row[4]) : _product->weight = 0;
						 row[5] ? _product->length = stod(row[5]) : _product->length = 0;
						 row[6] ? _product->width = stod(row[6]) : _product->width = 0;
						 row[7] ? _product->height = stod(row[7]) : _product->height = 0;
						 row[8] ? _product->unitsInStock = stoi(row[8]) : _product->unitsInStock = 0;

						 if (row[10] == "1") {
							 _product->sold = 1;
							 _product->purchased = 0;
						 }
						 else if (row[10] = "2") {
							 _product->sold = 0;
							 _product->purchased = 1;
						 }
						 else {
							 _product->sold = 0;
							 _product->purchased = 0;
						 }

						 numberOfRows++;
						 _product++;
					 }
					 cout << "ShowProducFiniished" << endl;
				 }
				 
			 }
			 else
			 {
				 string s = "No Products Found";
				 cout << s << endl;
				 strcpy_s(error, s.length() + 1, s.c_str());
				 status = 2;
			 }
		 }
	 }
	 return numberOfRows;
}

extern "C"	ERP_API int showAvailableProducts(Product** product, char* error, ConnectionString con)
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

		qstate = mysql_query(conn, "select * from product inner join product_has_category on product.Product_ID = product_has_category.Product_Product_ID and product_has_category.Category_Category_ID = '1'");
		
		if (checkQuery(qstate, error, conn)) {

			res = mysql_store_result(conn);

			if (res->row_count > 0)
			{
				*product = (Product*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Product));
				cout << res->row_count << endl;
				numOfFields = mysql_num_fields(res);

				Product *_product = *product;

				while (row = mysql_fetch_row(res)) {

					_product->id = row[0];
					row[1] ? _product->name = row[1] : _product->name = nullptr;
					row[2] ? _product->description = row[2] : _product->description = nullptr;
					row[3] ? _product->price = stod(row[3]) : _product->price = 0;
					row[4] ? _product->weight = stod(row[4]) : _product->weight = 0;
					row[5] ? _product->length = stod(row[5]) : _product->length = 0;
					row[6] ? _product->width = stod(row[6]) : _product->width = 0;
					row[7] ? _product->height = stod(row[7]) : _product->height = 0;
					row[8] ? _product->unitsInStock = stoi(row[8]) : _product->unitsInStock = 0;

					_product->sold = 1;
					_product->purchased = 0;

					numberOfRows++;
					_product++;
				}
			}
			else
			{
				string s = "No Products Found";
				cout << s << endl;
				strcpy_s(error, s.length() + 1, s.c_str());
				status = 2;
			}
		}
	}
	return numberOfRows;
}

extern "C"	ERP_API int showInventories(Inventory** inventory, char* error, ConnectionString con)
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

		 qstate = mysql_query(conn, "select * from inventory");
		 cout << "select * from inventory" << endl;
		 if (checkQuery(qstate, error, conn)) {

			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *inventory = (Inventory*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Inventory));
				 cout << res->row_count << endl;
				 numOfFields = mysql_num_fields(res);

				 Inventory *_inventory = *inventory;

				 while (row = mysql_fetch_row(res)) {

					 _inventory->id = row[0];
					 row[1] ? _inventory->governorate = row[1] : _inventory->governorate = 0;
					 row[2] ? _inventory->city = row[2] : _inventory->city = 0;
					 row[3] ? _inventory->street = row[3] : _inventory->street = 0;
					 row[4] ? _inventory->length = stod(row[4]) : _inventory->length = 0;
					 row[5] ? _inventory->width = stod(row[5]) : _inventory->width = 0;
					 row[6] ? _inventory->height = stod(row[6]) : _inventory->height = 0;

					 numberOfRows++;
					 _inventory++;
				 }
			 }
			 else
			 {
				 string s = "No Inventories yet";
				 cout << s << endl;
				 strcpy_s(error, s.length() + 1, s.c_str());
				 status = 2;
			 }
		 }
	 }
	 return numberOfRows;
 }

extern "C"	ERP_API int showProductsInInventory(char* id, ProductInInventory** product, char* error, ConnectionString con) {

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

		 string query = (string) "select * from product, inventory_has_product where inventory_has_product.Inventory_Inventory_ID = '" + id + "' and inventory_has_product.Product_Product_ID = product.Product_ID;";
		 ////cout << query << endl;
		 char const *q = query.c_str();
		 qstate = mysql_query(conn, q);

		 if (checkQuery(qstate, error, conn)) {

			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *product = (ProductInInventory*)CoTaskMemAlloc((int)(res->row_count) * sizeof(ProductInInventory));
				 ProductInInventory *_product = *product;

				 while (row = mysql_fetch_row(res)) {

					 _product->inventoryID = row[9];
					 _product->productID = row[0];
					 _product->name = row[1];
					 _product->position = row[11];
					 _product->weight = stod(row[4]);
					 _product->length = stod(row[5]);
					 _product->width = stod(row[6]);
					 _product->height = stod(row[7]);
					 _product->unitsInInventory = stoi(row[12]);

					 numberOfRows++;
					 _product++;
				 }
			 }
			 else
			 {
				 string s = "No products in inventory";
				 cout << s << endl;
				 strcpy_s(error, s.length() + 1, s.c_str());
				 status = 2;
			 }
		 }
	 }
	 return numberOfRows;
 }

extern "C"	ERP_API int showAllOrders(Order** order, char* error, ConnectionString con) {

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

		string query = (string)"select * from order_table where incoming = 1";
		const char* q = query.c_str();
		qstate = mysql_query(conn, q);
		////cout << query << endl;

		if (checkQuery(qstate, error, conn))
		{
			res = mysql_store_result(conn);

			if (res->row_count > 0)
			{
				*order = (Order*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Order));
				numOfFields = mysql_num_fields(res);
				cout << res->row_count << endl;

				Order *_order = *order;

				while (row = mysql_fetch_row(res)) {

					_order->id = row[0];
					row[1] ? _order->incoming = stoi(row[1]) : _order->incoming = 2;
					row[2] ? _order->outgoing = stoi(row[2]) : _order->outgoing = 2;
					row[3] ? _order->requiredDate = row[3] : _order->requiredDate = nullptr;
					row[4] ? _order->completedDate = row[4] : _order->completedDate = nullptr;
					row[5] ? _order->orderStatus = row[5] : _order->orderStatus = nullptr;
					row[6] ? _order->totalPrice = stod(row[6]) : _order->totalPrice = 0;
					row[7] ? _order->customerID = row[7] : _order->customerID = nullptr;
					row[8] ? _order->supplierID = row[8] : _order->supplierID = nullptr;
					row[9] ? _order->paymentID = row[9] : _order->paymentID = nullptr;
					row[10] ? _order->shipmentID = row[10] : _order->shipmentID = nullptr;

					numberOfRows++;
					_order++;
				}
				cout << "here" << endl;
			}
		}
	}
	return numberOfRows;
}

extern "C"	ERP_API int showReceipts(Order** order, char* error, ConnectionString con) {

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

		string query = (string)"select * from order_table where outgoing = 1";
		const char* q = query.c_str();
		qstate = mysql_query(conn, q);
		////cout << query << endl;

		if (checkQuery(qstate, error, conn))
		{
			res = mysql_store_result(conn);

			if (res->row_count > 0)
			{
				*order = (Order*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Order));
				numOfFields = mysql_num_fields(res);
				cout << res->row_count << endl;

				Order *_order = *order;

				while (row = mysql_fetch_row(res)) {

					_order->id = row[0];
					row[1] ? _order->incoming = stoi(row[1]) : _order->incoming = 2;
					row[2] ? _order->outgoing = stoi(row[2]) : _order->outgoing = 2;
					row[3] ? _order->requiredDate = row[3] : _order->requiredDate = nullptr;
					row[4] ? _order->completedDate = row[4] : _order->completedDate = nullptr;
					row[5] ? _order->orderStatus = row[5] : _order->orderStatus = nullptr;
					row[6] ? _order->totalPrice = stod(row[6]) : _order->totalPrice = 0;
					row[7] ? _order->customerID = row[7] : _order->customerID = nullptr;
					row[8] ? _order->supplierID = row[8] : _order->supplierID = nullptr;
					row[9] ? _order->paymentID = row[9] : _order->paymentID = nullptr;
					row[10] ? _order->shipmentID = row[10] : _order->shipmentID = nullptr;

					numberOfRows++;
					_order++;
				}
				cout << "here" << endl;
			}
		}
	}
	return numberOfRows;
}

 extern "C"	ERP_API int showCompletedOrders(Order** order, char* error, ConnectionString con) {

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
		 string query = (string)"select * from order_table where Order_Status = 'Done' and incoming = 1";
		 const char* q = query.c_str();
		 qstate = mysql_query(conn, q);
		 cout << "select * from order_table where Order_Status = 'Done' and incoming = 1" << endl;

		 if (checkQuery(qstate, error, conn))
		 {
			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *order = (Order*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Order));
				 numOfFields = mysql_num_fields(res);
				 cout << res->row_count << endl;

				 Order *_order = *order;

				 while (row = mysql_fetch_row(res)) {

					 _order->id = row[0];
					 row[1] ? _order->incoming = stoi(row[1]) : _order->incoming = 2;
					 row[2] ? _order->outgoing = stoi(row[2]) : _order->outgoing = 2;
					 row[3] ? _order->requiredDate = row[3] : _order->requiredDate = nullptr;
					 row[4] ? _order->completedDate = row[4] : _order->completedDate = nullptr;
					 row[5] ? _order->orderStatus = row[5] : _order->orderStatus = nullptr;
					 row[6] ? _order->totalPrice = stod(row[6]) : _order->totalPrice = 0;
					 row[7] ? _order->customerID = row[7] : _order->customerID = nullptr;
					 row[8] ? _order->supplierID = row[8] : _order->supplierID = nullptr;
					 row[9] ? _order->paymentID = row[9] : _order->paymentID = nullptr;
					 row[10] ? _order->shipmentID = row[10] : _order->shipmentID = nullptr;

					 numberOfRows++;
					 _order++;
				 }
				 cout << "here" << endl;
			 }
		 }
	 }
	 return numberOfRows;
}

 extern "C"	ERP_API int showCompletedReceipts(Order** order, char* error, ConnectionString con) {

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
		 qstate = mysql_query(conn, "select * from order_table where Order_Status = 'Done' and outgoing = 1");
		 cout << "select * from order_table where Order_Status = 'Done' and outgoing = 1" << endl;

		 if (checkQuery(qstate, error, conn))
		 {
			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *order = (Order*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Order));
				 numOfFields = mysql_num_fields(res);
				 cout << res->row_count << endl;

				 Order *_order = *order;

				 while (row = mysql_fetch_row(res)) {

					 _order->id = row[0];
					 row[1] ? _order->incoming = stoi(row[1]) : _order->incoming = 2;
					 row[2] ? _order->outgoing = stoi(row[2]) : _order->outgoing = 2;
					 row[3] ? _order->requiredDate = row[3] : _order->requiredDate = nullptr;
					 row[4] ? _order->completedDate = row[4] : _order->completedDate = nullptr;
					 row[5] ? _order->orderStatus = row[5] : _order->orderStatus = nullptr;
					 row[6] ? _order->totalPrice = stod(row[6]) : _order->totalPrice = 0;
					 row[7] ? _order->customerID = row[7] : _order->customerID = nullptr;
					 row[8] ? _order->supplierID = row[8] : _order->supplierID = nullptr;
					 row[9] ? _order->paymentID = row[9] : _order->paymentID = nullptr;
					 row[10] ? _order->shipmentID = row[10] : _order->shipmentID = nullptr;

					 numberOfRows++;
					 _order++;
				 }
				 cout << "here" << endl;
			 }
		 }
	 }
	 return numberOfRows;
 }

extern "C"	ERP_API int showReadyOrders(Order** order, char* error, ConnectionString con) {

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
		 string query = (string)"select * from order_table where Order_Status = 'Ready'";
		 qstate = mysql_query(conn, query.c_str());
		 ////cout << query << endl;
		 
		 if (checkQuery(qstate, error, conn))
		 {
			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *order = (Order*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Order));
				 numOfFields = mysql_num_fields(res);
				 cout << res->row_count << endl;

				 Order *_order = *order;

				 while (row = mysql_fetch_row(res)) {

					 _order->id = row[0];
					 row[1] ? _order->incoming = stoi(row[1]) : _order->incoming = 2;
					 row[2] ? _order->outgoing = stoi(row[2]) : _order->outgoing = 2;
					 row[3] ? _order->requiredDate = row[3] : _order->requiredDate = nullptr;
					 row[4] ? _order->completedDate = row[4] : _order->completedDate = nullptr;
					 row[5] ? _order->orderStatus = row[5] : _order->orderStatus = nullptr;
					 row[6] ? _order->totalPrice = stod(row[6]) : _order->totalPrice = 0;
					 row[7] ? _order->customerID = row[7] : _order->customerID = nullptr;
					 row[8] ? _order->supplierID = row[8] : _order->supplierID = nullptr;
					 row[9] ? _order->paymentID = row[9] : _order->paymentID = nullptr;
					 row[10] ? _order->shipmentID = row[10] : _order->shipmentID = nullptr;

					 numberOfRows++;
					 _order++;
				 }
				 cout << "here" << endl;
			 }
		 }
	 }
	 return numberOfRows;
 }

extern "C"	ERP_API int showOrdersInProgress(Order** order, char* error, ConnectionString con) {

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
		
		 string query = (string)"select * from order_table where Order_Status = 'In Progress'";
		 qstate = mysql_query(conn, query.c_str());
		 ////cout << query << endl;

		 if (checkQuery(qstate, error, conn))
		 {
			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *order = (Order*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Order));
				 numOfFields = mysql_num_fields(res);
				 cout << res->row_count << endl;

				 Order *_order = *order;

				 while (row = mysql_fetch_row(res)) {

					 _order->id = row[0];
					 row[1] ? _order->incoming = stoi(row[1]) : _order->incoming = 2;
					 row[2] ? _order->outgoing = stoi(row[2]) : _order->outgoing = 2;
					 row[3] ? _order->requiredDate = row[3] : _order->requiredDate = nullptr;
					 row[4] ? _order->completedDate = row[4] : _order->completedDate = nullptr;
					 row[5] ? _order->orderStatus = row[5] : _order->orderStatus = nullptr;
					 row[6] ? _order->totalPrice = stod(row[6]) : _order->totalPrice = 0;
					 row[7] ? _order->customerID = row[7] : _order->customerID = nullptr;
					 row[8] ? _order->supplierID = row[8] : _order->supplierID = nullptr;
					 row[9] ? _order->paymentID = row[9] : _order->paymentID = nullptr;
					 row[10] ? _order->shipmentID = row[10] : _order->shipmentID = nullptr;

					 numberOfRows++;
					 _order++;
				 }
				 cout << "here" << endl;
			 }
		 }
	 }
	 return numberOfRows;
}

 extern "C"	ERP_API int showWaitingOrders(Order** order, char* error, ConnectionString con) {

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
		 qstate = mysql_query(conn, "select * from order_table where Order_Status = 'Waiting' and incoming = 1");
		// cout << "select * from order_table where Order_Status = 'Waiting' and incoming = 1" << endl;

		 if (checkQuery(qstate, error, conn))
		 {
			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *order = (Order*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Order));
				 numOfFields = mysql_num_fields(res);
				 cout << res->row_count << endl;

				 Order *_order = *order;

				 while (row = mysql_fetch_row(res)) {

					 _order->id = row[0];
					 row[1] ? _order->incoming = stoi(row[1]) : _order->incoming = 2;
					 row[2] ? _order->outgoing = stoi(row[2]) : _order->outgoing = 2;
					 row[3] ? _order->requiredDate = row[3] : _order->requiredDate = nullptr;
					 row[4] ? _order->completedDate = row[4] : _order->completedDate = nullptr;
					 row[5] ? _order->orderStatus = row[5] : _order->orderStatus = nullptr;
					 row[6] ? _order->totalPrice = stod(row[6]) : _order->totalPrice = 0;
					 row[7] ? _order->customerID = row[7] : _order->customerID = nullptr;
					 row[8] ? _order->supplierID = row[8] : _order->supplierID = nullptr;
					 row[9] ? _order->paymentID = row[9] : _order->paymentID = nullptr;
					 row[10] ? _order->shipmentID = row[10] : _order->shipmentID = nullptr;;

					 numberOfRows++;
					 _order++;
				 }
				 cout << "here" << endl;
			 }
		 }
	 }
	 return numberOfRows;
 }

 extern "C"	ERP_API int showWaitingReceipts(Order** order, char* error, ConnectionString con) {

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
		 qstate = mysql_query(conn, "select * from order_table where Order_Status = 'Waiting' and outgoing = 1");
		 cout << "select * from order_table where Order_Status = 'Waiting' and outgoing = 1" << endl;

		 if (checkQuery(qstate, error, conn))
		 {
			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *order = (Order*)CoTaskMemAlloc((int)(res->row_count) * sizeof(Order));
				 numOfFields = mysql_num_fields(res);
				 cout << res->row_count << endl;

				 Order *_order = *order;

				 while (row = mysql_fetch_row(res)) {

					 _order->id = row[0];
					 row[1] ? _order->incoming = stoi(row[1]) : _order->incoming = 2;
					 row[2] ? _order->outgoing = stoi(row[2]) : _order->outgoing = 2;
					 row[3] ? _order->requiredDate = row[3] : _order->requiredDate = nullptr;
					 row[4] ? _order->completedDate = row[4] : _order->completedDate = nullptr;
					 row[5] ? _order->orderStatus = row[5] : _order->orderStatus = nullptr;
					 row[6] ? _order->totalPrice = stod(row[6]) : _order->totalPrice = 0;
					 row[7] ? _order->customerID = row[7] : _order->customerID = nullptr;
					 row[8] ? _order->supplierID = row[8] : _order->supplierID = nullptr;
					 row[9] ? _order->paymentID = row[9] : _order->paymentID = nullptr;
					 row[10] ? _order->shipmentID = row[10] : _order->shipmentID = nullptr;

					 numberOfRows++;
					 _order++;
				 }
				 cout << "here" << endl;
			 }
		 }
	 }
	 return numberOfRows;
 }

extern "C"	ERP_API int showProductsInOrder(char* id, ProductInOrder** product, char* error, ConnectionString con) {

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

		 string query = (string)"select * from order_has_product where order_table_Order_ID = '" + id + "'";
		 qstate = mysql_query(conn, query.c_str());
		 ////cout << query << endl;
		 if (checkQuery(qstate, error, conn)) {

			 res = mysql_store_result(conn);

			 if (res->row_count > 0)
			 {
				 *product = (ProductInOrder*)CoTaskMemAlloc((int)(res->row_count) * sizeof(ProductInOrder));
				 cout << res->row_count << endl;
				 numOfFields = mysql_num_fields(res);

				 ProductInOrder *_product = *product;
				 while (row = mysql_fetch_row(res)) {

					 _product->productID = row[0];
					 _product->orderID = row[1];
					 row[2] ? _product->inventoryID = row[2] : _product->inventoryID = nullptr;
					 _product->unitsOrdered = stoi(row[3]);
					 row[4] ? _product->unitsDone = stoi(row[4]) : _product->unitsDone = 0;
					 numberOfRows++;
					 _product++;
				 }
				 cout << "here" << endl;
			 }
		 }
	 }
	 return numberOfRows;
}

extern "C"	ERP_API int showCustomerProducts(char* id, CustomerProduct** product, char* error, ConnectionString con) {

	int status = 0;
	int qstate;

	MYSQL* conn;
	MYSQL_ROW row = nullptr;
	MYSQL_RES *res = nullptr;

	conn = mysql_init(0);
	conn = mysql_real_connect(conn, con.SERVER, con.USER, con.PASSWORD, con.DATABASE, 3306, NULL, 0);

	int cost = 0;
	int numberOfRows = 0;
	unsigned int numOfFields;

	if (conn) {

		mysql_free_result(res);

		string query = (string)"select order_has_product.order_table_Order_ID, order_has_product.Product_Product_ID, product.Product_Name, order_has_product.Units_In_Order, product.Product_Price from product, order_has_product, order_table where order_table.Customer_Customer_ID = '" + id + "' and order_has_product.order_table_Order_ID = order_table.Order_ID and order_has_product.Product_Product_ID = product.Product_ID";
		qstate = mysql_query(conn, query.c_str());
		////cout << query << endl;
		if (checkQuery(qstate, error, conn)) {

			res = mysql_store_result(conn);

			if (res->row_count > 0)
			{
				*product = (CustomerProduct*)CoTaskMemAlloc((int)(res->row_count) * sizeof(CustomerProduct));
				cout << res->row_count << endl;
				numOfFields = mysql_num_fields(res);

				CustomerProduct *_product = *product;
				while (row = mysql_fetch_row(res)) {

					_product->customerID = id;
					_product->orderID = row[0];
					_product->productID = row[1];
					_product->name = row[2];
					_product->unitsOrdered = stoi(row[3]);
					_product->price = stod(row[4]);

					cost += stoi(row[3])*stod(row[4]);
					_product->total = cost;

					numberOfRows++;
					_product++;
				}
				cout << "here" << endl;
			}
		}
	}
	return numberOfRows;
}


 bool checkQuery(int qstate,  char* error, MYSQL* conn)
 {
	 int status;

	 if (qstate)
	 {
		 cout << "Query failed: " << mysql_error(conn) << endl;
		 status = 2;
		 string s = mysql_error(conn);
		 strcpy_s(error, s.length() + 1, mysql_error(conn));
		 return false;
	 }
	 else
	 {
		 cout << "Query succeeded" << endl;
		 status = 0;
		 string s = mysql_error(conn);
		 strcpy_s(error, s.length() + 1, mysql_error(conn));
		 return true;

	 }
 }