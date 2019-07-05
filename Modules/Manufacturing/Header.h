#pragma once

#define ERP_API __declspec(dllexport)
#include "Header.h"
#include <iostream>
#include <cstdio>
#include <fstream>
#include <sstream>
#include <string.h>
#include <cstdlib>
#include <conio.h>
#include <mysql.h>
#include <vector>
#include <map>
#include <time.h>
#include <algorithm>
using namespace std;

bool checkQuery(int qstate, MYSQL*Conn , char* error);


