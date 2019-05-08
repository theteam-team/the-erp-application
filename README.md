# the-erp-application
An EPR System for Medium Businesses and Start-Ups

## Requirements
* .Net core 2.2
* Node.js 8.12.0
* Angular CLI 7.3.8
* mysql server

## Database
* Create **erp** database, diagram found in *Database_schemas* folder
* In *Modules\WareHouseManagement\warehouse.cpp*  and  *Modules\CRM\CRM.cpp*  change the password in the line `#define PASSWORD "123456789pp"` to your mysql server password

## Pre-Build
In *Solution Explorer*, in *Modules* folder, right click on any module => *Properties* => *Configuration manager* and make sure each module is set to **Release x64** and *Erp* is set to **Debug Any CPU**


## Build
* `cd Erp`
* `npm install`
* `ng build` 
* In *Solution Explorer* Right-Click on the Solution File **the-erp-app** Choose **Build Solution**    

## Run
* Start your mysql server
* Open project in VS
* From *View*, open *SQL Server Object Explorer*, if **(localdb)\ProjectsV13** is not found:
   1. Right click on *SQL Server* => *Add SQL Server*
   2. In *Server Name* type **(localdb)\ProjectsV13** and click **Connect**
* In Command line, type `ng build`
* Run from VS using **f5**
* Log in using the *database name:* **kemo** and any username and password

