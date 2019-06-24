# the-erp-application
An EPR System for Medium Businesses and Start-Ups

## Requirements
* .Net core 2.2
* Node.js 8.12.0
* Angular CLI 7.3.8
* mysql server

## Database
* 
* In *Erp\AppConfig.json*  change the Connection String to yours in  `Connectionstring["MysqL_Local"]` and `Mysql_c++`
## Build
*`Build your c++ modules`
* In Command line, type `ng build`
* `cd Erp`
* `npm install`

## Run
* Start your mysql server
* Open project in VS
* From *View*, open *SQL Server Object Explorer*, if **(localdb)\ProjectsV13** is not found:
   1. Right click on *SQL Server* => *Add SQL Server*
   2. In *Server Name* type **(localdb)\ProjectsV13** and click **Connect**
* From *Solution Explorer* right click on each module => *Build*

* Run from VS using **f5** or form comandLine **dotnet run** at Erp/ directory


