
#define ERP_API __declspec(dllexport)
#include<objbase.h>
#include <windows.h>
unsigned int stringToInt(char * c);
void checkQuery(int qstate, int * status, char * error);
