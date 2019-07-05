#include"databaseModels.h"
#include"Header.h"
bool checkQuery(int qstate, MYSQL*conn  ,char* error)
{
	if (qstate)
	{
		cout << "Query failed: " << mysql_error(conn) << endl;
		string s = mysql_error(conn);
		strcpy_s(error, s.length() + 1, mysql_error(conn));
		return false;
	}
	else
	{
		cout << "Query succeeded" << endl;
		string s = mysql_error(conn);
		return true;

	}
}