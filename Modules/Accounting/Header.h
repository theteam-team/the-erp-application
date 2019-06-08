
#define ERP_API __declspec(dllexport)
#include<objbase.h>
#include <windows.h>
unsigned int stringToInt(char * c);
bool checkQuery(int qstate, char* error);
