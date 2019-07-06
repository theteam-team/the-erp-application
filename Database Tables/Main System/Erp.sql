-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema ERP
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema ERP
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `ERP` DEFAULT CHARACTER SET utf8 ;
USE `ERP` ;

-- -----------------------------------------------------
-- Table `ERP`.`employee`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`employee` (
  `Employee_ID` VARCHAR(50) NOT NULL,
  `Employee_Name` VARCHAR(45) NULL,
  `Employee_Phone_Number` DECIMAL NULL,
  `Employee_Email` VARCHAR(100) NULL,
  `Employee_Date_Of_Birth` DATE NULL,
  `Employee_Gender` VARCHAR(10) NULL,
  `Employee_Points` INT NULL,
  `Is_Available` TINYINT NULL,
  `Role_ID` VARCHAR(45) NOT NULL,
  `Employee_Department` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Employee_ID`),
  UNIQUE INDEX `id_UNIQUE` (`Employee_ID` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`customer`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`customer` (
  `Customer_ID` VARCHAR(45) NOT NULL,
  `Customer_Name` VARCHAR(45) NULL,
  `Customer_Phone_Number` DECIMAL NULL,
  `Customer_Email` VARCHAR(100) NULL,
  `Customer_Date_Of_Birth` DATE NULL,
  `Customer_Gender` VARCHAR(10) NULL,
  `Customer_Loyality_Points` INT NULL,
  `Customer_Type` INT NULL,
  `Customer_Company` VARCHAR(45) NULL,
  `Company_Email` VARCHAR(100) NULL,
  `Is_Lead` TINYINT NULL,
  PRIMARY KEY (`Customer_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`opportunities`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`opportunities` (
  `Opportunity_ID` VARCHAR(45) NOT NULL,
  `Opportunity_Status` INT NULL,
  `Opportunity_Expected_Revenue` DECIMAL NULL,
  `Opportunity_Notes` VARCHAR(100) NULL,
  `Opportunity_Start_Date` DATE NULL,
  `Opportunity_End_Date` DATE NULL,
  `Customer_Customer_ID` VARCHAR(45) NOT NULL,
  `Employee_Employee_ID` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`Opportunity_ID`),
  UNIQUE INDEX `opportunity_id_UNIQUE` (`Opportunity_ID` ASC),
  INDEX `fk_opportunities_Customer1_idx` (`Customer_Customer_ID` ASC),
  INDEX `fk_opportunities_Employee1_idx` (`Employee_Employee_ID` ASC),
  CONSTRAINT `fk_opportunities_Customer1`
    FOREIGN KEY (`Customer_Customer_ID`)
    REFERENCES `ERP`.`customer` (`Customer_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_opportunities_Employee1`
    FOREIGN KEY (`Employee_Employee_ID`)
    REFERENCES `ERP`.`employee` (`Employee_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`customer_address`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`customer_address` (
  `Address_ID` VARCHAR(45) NOT NULL,
  `City` VARCHAR(45) NULL,
  `Governate` VARCHAR(45) NULL,
  `Street` VARCHAR(45) NULL,
  `Zip_Code` INT NULL,
  `Customer_Customer_ID` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Address_ID`, `Customer_Customer_ID`),
  INDEX `fk_Customer_Address_Customer1_idx` (`Customer_Customer_ID` ASC),
  CONSTRAINT `fk_Customer_Address_Customer1`
    FOREIGN KEY (`Customer_Customer_ID`)
    REFERENCES `ERP`.`customer` (`Customer_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`supplier`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`supplier` (
  `Supplier_ID` VARCHAR(45) NOT NULL,
  `Supplier_Name` VARCHAR(45) NULL,
  `Supplier_Phone_Number` DECIMAL NULL,
  `Supplier_Email` VARCHAR(100) NULL,
  PRIMARY KEY (`Supplier_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`product`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`product` (
  `Product_ID` VARCHAR(45) NOT NULL,
  `Product_Name` VARCHAR(45) NULL,
  `Product_Description` VARCHAR(200) NULL,
  `Product_Price` DOUBLE NULL,
  `Product_Weight` DOUBLE NULL,
  `length` DOUBLE NULL,
  `width` DOUBLE NULL,
  `height` DOUBLE NULL,
  `Units_In_Stock` INT NULL,
  `Product_Cost` DOUBLE NULL,
  PRIMARY KEY (`Product_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`shipment`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`shipment` (
  `Shipment_ID` VARCHAR(45) NOT NULL,
  `Shipment_Method` VARCHAR(45) NULL,
  `Shipment_Start` DATETIME NULL,
  `Shipment_End` DATETIME NULL,
  PRIMARY KEY (`Shipment_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`payment`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`payment` (
  `Payment_ID` VARCHAR(45) NOT NULL,
  `Payment_Method` VARCHAR(45) NULL,
  `Payment_Date` DATETIME NULL,
  `Payment_Amount` DOUBLE NULL,
  PRIMARY KEY (`Payment_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`category`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`category` (
  `Category_ID` VARCHAR(45) NOT NULL,
  `Category_Name` VARCHAR(45) NULL,
  `Category_Description` VARCHAR(200) NULL,
  PRIMARY KEY (`Category_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`account`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`account` (
  `Account_ID` VARCHAR(45) NOT NULL,
  `Account_Money` DOUBLE NULL,
  `Account_Creation_Date` DATETIME NULL,
  `Account_Debt` DOUBLE NULL,
  `Customer_Customer_ID` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Account_ID`, `Customer_Customer_ID`),
  INDEX `fk_Account_Customer1_idx` (`Customer_Customer_ID` ASC),
  CONSTRAINT `fk_Account_Customer1`
    FOREIGN KEY (`Customer_Customer_ID`)
    REFERENCES `ERP`.`customer` (`Customer_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`product_has_category`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`product_has_category` (
  `Product_Product_ID` VARCHAR(45) NOT NULL,
  `Category_Category_ID` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Product_Product_ID`, `Category_Category_ID`),
  INDEX `fk_Product_has_Category1_Category1_idx` (`Category_Category_ID` ASC),
  INDEX `fk_Product_has_Category1_Product1_idx` (`Product_Product_ID` ASC),
  CONSTRAINT `fk_Product_has_Category1_Product1`
    FOREIGN KEY (`Product_Product_ID`)
    REFERENCES `ERP`.`product` (`Product_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Product_has_Category1_Category1`
    FOREIGN KEY (`Category_Category_ID`)
    REFERENCES `ERP`.`category` (`Category_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`interest`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`interest` (
  `Interest_ID` INT NOT NULL,
  `Category_Category_ID` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Interest_ID`, `Category_Category_ID`),
  INDEX `fk_Interest_Category1_idx` (`Category_Category_ID` ASC),
  CONSTRAINT `fk_Interest_Category1`
    FOREIGN KEY (`Category_Category_ID`)
    REFERENCES `ERP`.`category` (`Category_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`customer_interest`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`customer_interest` (
  `Level` INT NULL,
  `Customer_Customer_ID` VARCHAR(45) NOT NULL,
  `Interest_Interest_ID` INT NOT NULL,
  INDEX `fk_Customer_Interest_Interest1_idx` (`Interest_Interest_ID` ASC),
  PRIMARY KEY (`Interest_Interest_ID`, `Customer_Customer_ID`),
  CONSTRAINT `fk_Customer_Interest_Customer1`
    FOREIGN KEY (`Customer_Customer_ID`)
    REFERENCES `ERP`.`customer` (`Customer_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Customer_Interest_Interest1`
    FOREIGN KEY (`Interest_Interest_ID`)
    REFERENCES `ERP`.`interest` (`Interest_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`opportunity_product`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`opportunity_product` (
  `Opportunities_Opportunity_ID` VARCHAR(45) NOT NULL,
  `Product_Product_ID` VARCHAR(45) NOT NULL,
  `Units` INT NULL,
  PRIMARY KEY (`Opportunities_Opportunity_ID`, `Product_Product_ID`),
  INDEX `fk_Opportunity_Product_Product1_idx` (`Product_Product_ID` ASC),
  CONSTRAINT `fk_Opportunity_Product_Opportunities1`
    FOREIGN KEY (`Opportunities_Opportunity_ID`)
    REFERENCES `ERP`.`opportunities` (`Opportunity_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Opportunity_Product_Product1`
    FOREIGN KEY (`Product_Product_ID`)
    REFERENCES `ERP`.`product` (`Product_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`order_table`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`order_table` (
  `Order_ID` VARCHAR(45) NOT NULL,
  `incoming` INT NULL,
  `outgoing` INT NULL,
  `Order_Required_Date` DATE NULL,
  `Order_Completed_Date` DATE NULL,
  `Order_Status` VARCHAR(20) NULL,
  `total` DOUBLE NULL,
  `Customer_Customer_ID` VARCHAR(45) NULL,
  `Supplier_Supplier_ID` VARCHAR(45) NULL,
  `Payment_Payment_ID` VARCHAR(45) NULL,
  `Shipment_Shipment_ID` VARCHAR(45) NULL,
  PRIMARY KEY (`Order_ID`),
  INDEX `fk_order_table_Customer1_idx` (`Customer_Customer_ID` ASC),
  INDEX `fk_order_table_Supplier1_idx` (`Supplier_Supplier_ID` ASC),
  INDEX `fk_order_table_Payment1_idx` (`Payment_Payment_ID` ASC),
  INDEX `fk_order_table_Shipment1_idx` (`Shipment_Shipment_ID` ASC),
  CONSTRAINT `fk_order_table_Customer1`
    FOREIGN KEY (`Customer_Customer_ID`)
    REFERENCES `ERP`.`customer` (`Customer_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_order_table_Supplier1`
    FOREIGN KEY (`Supplier_Supplier_ID`)
    REFERENCES `ERP`.`supplier` (`Supplier_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_order_table_Payment1`
    FOREIGN KEY (`Payment_Payment_ID`)
    REFERENCES `ERP`.`payment` (`Payment_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_order_table_Shipment1`
    FOREIGN KEY (`Shipment_Shipment_ID`)
    REFERENCES `ERP`.`shipment` (`Shipment_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`inventory`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`inventory` (
  `Inventory_ID` VARCHAR(45) NOT NULL,
  `Governorate` VARCHAR(20) NULL,
  `City` VARCHAR(20) NULL,
  `Street` VARCHAR(20) NULL,
  `length` DOUBLE NULL,
  `width` DOUBLE NULL,
  `height` DOUBLE NULL,
  PRIMARY KEY (`Inventory_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`order_has_product`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`order_has_product` (
  `Product_Product_ID` VARCHAR(45) NOT NULL,
  `order_table_Order_ID` VARCHAR(45) NOT NULL,
  `Inventory_Inventory_ID` VARCHAR(45) NULL,
  `Units_In_Order` INT NULL,
  `Units_Done` INT NULL,
  PRIMARY KEY (`Product_Product_ID`, `order_table_Order_ID`),
  INDEX `fk_Order_has_Product1_Product1_idx` (`Product_Product_ID` ASC),
  INDEX `fk_Order_has_Product_Inventory1_idx` (`Inventory_Inventory_ID` ASC),
  INDEX `fk_Order_has_Product_order_table1_idx` (`order_table_Order_ID` ASC),
  CONSTRAINT `fk_Order_has_Product1_Product1`
    FOREIGN KEY (`Product_Product_ID`)
    REFERENCES `ERP`.`product` (`Product_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Order_has_Product_Inventory1`
    FOREIGN KEY (`Inventory_Inventory_ID`)
    REFERENCES `ERP`.`inventory` (`Inventory_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Order_has_Product_order_table1`
    FOREIGN KEY (`order_table_Order_ID`)
    REFERENCES `ERP`.`order_table` (`Order_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`inventory_has_product`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`inventory_has_product` (
  `Inventory_Inventory_ID` VARCHAR(45) NOT NULL,
  `Product_Product_ID` VARCHAR(45) NOT NULL,
  `position` VARCHAR(45) NULL,
  `Units_In_Inventory` INT NULL,
  PRIMARY KEY (`Inventory_Inventory_ID`, `Product_Product_ID`),
  INDEX `fk_Inventory_has_Product_Product1_idx` (`Product_Product_ID` ASC),
  INDEX `fk_Inventory_has_Product_Inventory1_idx` (`Inventory_Inventory_ID` ASC),
  CONSTRAINT `fk_Inventory_has_Product_Inventory1`
    FOREIGN KEY (`Inventory_Inventory_ID`)
    REFERENCES `ERP`.`inventory` (`Inventory_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Inventory_has_Product_Product1`
    FOREIGN KEY (`Product_Product_ID`)
    REFERENCES `ERP`.`product` (`Product_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`employee_address`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`employee_address` (
  `Address_ID` VARCHAR(45) NOT NULL,
  `City` VARCHAR(45) NULL,
  `Governate` VARCHAR(45) NULL,
  `Street` VARCHAR(45) NULL,
  `Zip_Code` INT NULL,
  `Employee_Employee_ID` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`Address_ID`, `Employee_Employee_ID`),
  INDEX `fk_Employee_Address_Employee1_idx` (`Employee_Employee_ID` ASC),
  CONSTRAINT `fk_Employee_Address_Employee1`
    FOREIGN KEY (`Employee_Employee_ID`)
    REFERENCES `ERP`.`employee` (`Employee_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`supplier_address`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`supplier_address` (
  `Address_ID` VARCHAR(45) NOT NULL,
  `City` VARCHAR(45) NULL,
  `Governate` VARCHAR(45) NULL,
  `Street` VARCHAR(45) NULL,
  `Zip_Code` INT NULL,
  `Supplier_Supplier_ID` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Address_ID`, `Supplier_Supplier_ID`),
  INDEX `fk_Inventory_Address_Supplier1_idx` (`Supplier_Supplier_ID` ASC),
  CONSTRAINT `fk_Inventory_Address_Supplier1`
    FOREIGN KEY (`Supplier_Supplier_ID`)
    REFERENCES `ERP`.`supplier` (`Supplier_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`billMaterials`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`billMaterials` (
  `BillMaterials_ID` VARCHAR(45) NOT NULL,
  `Component_Name` INT NULL,
  `Valid_From` DATETIME NULL,
  `Valid_Until` DATETIME NULL,
  `Price` FLOAT NULL,
  PRIMARY KEY (`BillMaterials_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`inventory_has_billMaterials`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`inventory_has_billMaterials` (
  `Inventory_ID` VARCHAR(45) NOT NULL,
  `BillMaterials_ID` VARCHAR(45) NOT NULL,
  `position` VARCHAR(45) NULL,
  `Units_In_Inventory` INT NULL,
  PRIMARY KEY (`Inventory_ID`, `BillMaterials_ID`),
  INDEX `FK_BillMaterials_ID` (`BillMaterials_ID` ASC),
  INDEX `FK_Inventory_ID` (`Inventory_ID` ASC),
  CONSTRAINT `fk_Inventory_has_BillMaterials_Inventory`
    FOREIGN KEY (`Inventory_ID`)
    REFERENCES `ERP`.`inventory` (`Inventory_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Inventory_has_BillMaterials_BillMaterials`
    FOREIGN KEY (`BillMaterials_ID`)
    REFERENCES `ERP`.`billMaterials` (`BillMaterials_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`product_has_billMaterials`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`product_has_billMaterials` (
  `Product_ID` VARCHAR(45) NOT NULL,
  `BillMaterials_ID` VARCHAR(45) NOT NULL,
  `Component_Name` VARCHAR(45) NULL,
  `Quantity` INT NULL,
  PRIMARY KEY (`Product_ID`, `BillMaterials_ID`),
  INDEX `FK_BillMaterials_ID_idx` (`BillMaterials_ID` ASC),
  INDEX `FK_Product_ID` (`Product_ID` ASC),
  CONSTRAINT `fk_Product_has_BillMaterialst_Product`
    FOREIGN KEY (`Product_ID`)
    REFERENCES `ERP`.`product` (`Product_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Product_has_BillMaterialst_BillMaterials`
    FOREIGN KEY (`BillMaterials_ID`)
    REFERENCES `ERP`.`billMaterials` (`BillMaterials_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`manufacturingOrder`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`manufacturingOrder` (
  `ManufacturingOrder_ID` VARCHAR(45) NOT NULL,
  `Start` DATETIME NULL,
  `End` DATETIME NULL,
  `Status` VARCHAR(45) NULL,
  `Total_Hours` INT NULL,
  `Total_Cycles` INT NULL,
  `Responsible` VARCHAR(45) NULL,
  PRIMARY KEY (`ManufacturingOrder_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`manufacturingOrder_has_materials`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`manufacturingOrder_has_materials` (
  `ManufacturingOrder_ID` VARCHAR(45) NOT NULL,
  `Product_ID` VARCHAR(45) NOT NULL,
  `Units_In_order` INT NULL,
  `Units_Done` INT NULL,
  `Status` VARCHAR(45) NULL,
  PRIMARY KEY (`ManufacturingOrder_ID`, `Product_ID`),
  INDEX `Product_ID_idx` (`Product_ID` ASC),
  INDEX `FK_ManufacturingOrder_ID` (`ManufacturingOrder_ID` ASC),
  CONSTRAINT `ManufacturingOrder_has_Materials-Manufacturing`
    FOREIGN KEY (`ManufacturingOrder_ID`)
    REFERENCES `ERP`.`manufacturingOrder` (`ManufacturingOrder_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `ManufacturingOrder_has_Materials-Product`
    FOREIGN KEY (`Product_ID`)
    REFERENCES `ERP`.`product` (`Product_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`product_has_supplier`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`product_has_supplier` (
  `product_Product_ID` VARCHAR(45) NOT NULL,
  `supplier_Supplier_ID` VARCHAR(45) NOT NULL,
  `Units_Supplied` INT NULL,
  `date` DATE NULL,
  `paid_up` DOUBLE NULL,
  `supplied_department` VARCHAR(45) NULL,
  `employee_name` VARCHAR(45) NULL,
  `employee_ID` VARCHAR(45) NULL,
  PRIMARY KEY (`product_Product_ID`, `supplier_Supplier_ID`),
  INDEX `fk_product_has_supplier_supplier1_idx` (`supplier_Supplier_ID` ASC),
  INDEX `fk_product_has_supplier_product1_idx` (`product_Product_ID` ASC),
  CONSTRAINT `fk_product_has_supplier_product1`
    FOREIGN KEY (`product_Product_ID`)
    REFERENCES `ERP`.`product` (`Product_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_product_has_supplier_supplier1`
    FOREIGN KEY (`supplier_Supplier_ID`)
    REFERENCES `ERP`.`supplier` (`Supplier_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `ERP`.`customer_copy1`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `ERP`.`customer_copy1` (
  `Customer_ID` VARCHAR(45) NOT NULL,
  `Customer_Name` VARCHAR(45) NULL,
  `Customer_Phone_Number` DECIMAL NULL,
  `Customer_Email` VARCHAR(100) NULL,
  `Customer_Date_Of_Birth` DATE NULL,
  `Customer_Gender` VARCHAR(10) NULL,
  `Customer_Loyality_Points` INT NULL,
  `Customer_Type` INT NULL,
  `Customer_Company` VARCHAR(45) NULL,
  `Company_Email` VARCHAR(100) NULL,
  `Is_Lead` TINYINT NULL,
  PRIMARY KEY (`Customer_ID`))
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
