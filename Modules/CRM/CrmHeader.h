
#define ERP_API __declspec(dllexport)

#include <objbase.h>
#include <windows.h>
#include <stdio.h>
#include <iostream>
#include <sstream> 
#include <string>
#include <mysql.h>

unsigned int stringToInt(char* c);
bool checkQuery(int qstate,  char* error, MYSQL* conn);
