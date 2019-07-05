#include "pch.h"
#include <iostream>
#include <cstdio>
#include <fstream>
#include <sstream>
#include <string.h>
#include <cstdlib>
#include <conio.h>
#include "DatabaseEntities.h"
#include <mysql.h>
#include <vector>
#include <map>
#include <time.h>
#include <algorithm>


#define ERP_API __declspec(dllexport)

bool checkQuery(int qstate,  char* error, MYSQL* conn);


