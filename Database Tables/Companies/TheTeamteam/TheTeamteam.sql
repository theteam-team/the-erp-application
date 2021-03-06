-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema TheTeamteam
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema TheTeamteam
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `TheTeamteam` DEFAULT CHARACTER SET utf8 ;
USE `TheTeamteam` ;

-- -----------------------------------------------------
-- Table `TheTeamteam`.`employee`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`employee` (
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
  UNIQUE INDEX `id_UNIQUE` (`Employee_ID` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`customer`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`customer` (
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
-- Table `TheTeamteam`.`opportunities`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`opportunities` (
  `Opportunity_ID` VARCHAR(45) NOT NULL,
  `Opportunity_Status` INT NULL,
  `Opportunity_Expected_Revenue` DECIMAL NULL,
  `Opportunity_Notes` VARCHAR(100) NULL,
  `Opportunity_Start_Date` DATE NULL,
  `Opportunity_End_Date` DATE NULL,
  `Customer_Customer_ID` VARCHAR(45) NOT NULL,
  `Employee_Employee_ID` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`Opportunity_ID`),
  UNIQUE INDEX `opportunity_id_UNIQUE` (`Opportunity_ID` ASC) VISIBLE,
  INDEX `fk_opportunities_Customer1_idx` (`Customer_Customer_ID` ASC) VISIBLE,
  INDEX `fk_opportunities_Employee1_idx` (`Employee_Employee_ID` ASC) VISIBLE,
  CONSTRAINT `fk_opportunities_Customer1`
    FOREIGN KEY (`Customer_Customer_ID`)
    REFERENCES `TheTeamteam`.`customer` (`Customer_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_opportunities_Employee1`
    FOREIGN KEY (`Employee_Employee_ID`)
    REFERENCES `TheTeamteam`.`employee` (`Employee_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`customer_address`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`customer_address` (
  `Address_ID` VARCHAR(45) NOT NULL,
  `City` VARCHAR(45) NULL,
  `Governate` VARCHAR(45) NULL,
  `Street` VARCHAR(45) NULL,
  `Zip_Code` INT NULL,
  `Customer_Customer_ID` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Address_ID`, `Customer_Customer_ID`),
  INDEX `fk_Customer_Address_Customer1_idx` (`Customer_Customer_ID` ASC) VISIBLE,
  CONSTRAINT `fk_Customer_Address_Customer1`
    FOREIGN KEY (`Customer_Customer_ID`)
    REFERENCES `TheTeamteam`.`customer` (`Customer_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`supplier`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`supplier` (
  `Supplier_ID` VARCHAR(45) NOT NULL,
  `Supplier_Name` VARCHAR(45) NULL,
  `Supplier_Phone_Number` DECIMAL NULL,
  `Supplier_Email` VARCHAR(100) NULL,
  PRIMARY KEY (`Supplier_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`product`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`product` (
  `Product_ID` VARCHAR(45) NOT NULL,
  `Product_Name` VARCHAR(45) NULL,
  `Product_Description` VARCHAR(200) NULL,
  `Product_Price` DOUBLE NULL,
  `Product_Weight` DOUBLE NULL,
  `length` DOUBLE NULL,
  `width` DOUBLE NULL,
  `height` DOUBLE NULL,
  `Units_In_Stock` INT NULL,
  PRIMARY KEY (`Product_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`shipment`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`shipment` (
  `Shipment_ID` VARCHAR(45) NOT NULL,
  `Shipment_Method` VARCHAR(45) NULL,
  `Shipment_Start` DATETIME NULL,
  `Shipment_End` DATETIME NULL,
  PRIMARY KEY (`Shipment_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`payment`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`payment` (
  `Payment_ID` VARCHAR(45) NOT NULL,
  `Payment_Method` VARCHAR(45) NULL,
  `Payment_Date` DATETIME NULL,
  `Payment_Amount` DOUBLE NULL,
  PRIMARY KEY (`Payment_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`category`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`category` (
  `Category_ID` VARCHAR(45) NOT NULL,
  `Category_Name` VARCHAR(45) NULL,
  `Category_Description` VARCHAR(200) NULL,
  PRIMARY KEY (`Category_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`account`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`account` (
  `Account_ID` VARCHAR(45) NOT NULL,
  `Account_Money` DOUBLE NULL,
  `Account_Creation_Date` DATETIME NULL,
  `Account_Debt` DOUBLE NULL,
  `Customer_Customer_ID` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Account_ID`, `Customer_Customer_ID`),
  INDEX `fk_Account_Customer1_idx` (`Customer_Customer_ID` ASC) VISIBLE,
  CONSTRAINT `fk_Account_Customer1`
    FOREIGN KEY (`Customer_Customer_ID`)
    REFERENCES `TheTeamteam`.`customer` (`Customer_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`product_has_category`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`product_has_category` (
  `Product_Product_ID` VARCHAR(45) NOT NULL,
  `Category_Category_ID` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Product_Product_ID`, `Category_Category_ID`),
  INDEX `fk_Product_has_Category1_Category1_idx` (`Category_Category_ID` ASC) INVISIBLE,
  INDEX `fk_Product_has_Category1_Product1_idx` (`Product_Product_ID` ASC) VISIBLE,
  CONSTRAINT `fk_Product_has_Category1_Product1`
    FOREIGN KEY (`Product_Product_ID`)
    REFERENCES `TheTeamteam`.`product` (`Product_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Product_has_Category1_Category1`
    FOREIGN KEY (`Category_Category_ID`)
    REFERENCES `TheTeamteam`.`category` (`Category_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`interest`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`interest` (
  `Interest_ID` INT NOT NULL,
  `Category_Category_ID` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Interest_ID`, `Category_Category_ID`),
  INDEX `fk_Interest_Category1_idx` (`Category_Category_ID` ASC) VISIBLE,
  CONSTRAINT `fk_Interest_Category1`
    FOREIGN KEY (`Category_Category_ID`)
    REFERENCES `TheTeamteam`.`category` (`Category_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`customer_interest`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`customer_interest` (
  `Level` INT NULL,
  `Customer_Customer_ID` VARCHAR(45) NOT NULL,
  `Interest_Interest_ID` INT NOT NULL,
  INDEX `fk_Customer_Interest_Interest1_idx` (`Interest_Interest_ID` ASC) VISIBLE,
  PRIMARY KEY (`Interest_Interest_ID`, `Customer_Customer_ID`),
  CONSTRAINT `fk_Customer_Interest_Customer1`
    FOREIGN KEY (`Customer_Customer_ID`)
    REFERENCES `TheTeamteam`.`customer` (`Customer_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Customer_Interest_Interest1`
    FOREIGN KEY (`Interest_Interest_ID`)
    REFERENCES `TheTeamteam`.`interest` (`Interest_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`opportunity_product`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`opportunity_product` (
  `Opportunities_Opportunity_ID` VARCHAR(45) NOT NULL,
  `Product_Product_ID` VARCHAR(45) NOT NULL,
  `Units` INT NULL,
  PRIMARY KEY (`Opportunities_Opportunity_ID`, `Product_Product_ID`),
  INDEX `fk_Opportunity_Product_Product1_idx` (`Product_Product_ID` ASC) VISIBLE,
  CONSTRAINT `fk_Opportunity_Product_Opportunities1`
    FOREIGN KEY (`Opportunities_Opportunity_ID`)
    REFERENCES `TheTeamteam`.`opportunities` (`Opportunity_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Opportunity_Product_Product1`
    FOREIGN KEY (`Product_Product_ID`)
    REFERENCES `TheTeamteam`.`product` (`Product_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`order_table`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`order_table` (
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
  INDEX `fk_order_table_Customer1_idx` (`Customer_Customer_ID` ASC) VISIBLE,
  INDEX `fk_order_table_Supplier1_idx` (`Supplier_Supplier_ID` ASC) VISIBLE,
  INDEX `fk_order_table_Payment1_idx` (`Payment_Payment_ID` ASC) VISIBLE,
  INDEX `fk_order_table_Shipment1_idx` (`Shipment_Shipment_ID` ASC) VISIBLE,
  CONSTRAINT `fk_order_table_Customer1`
    FOREIGN KEY (`Customer_Customer_ID`)
    REFERENCES `TheTeamteam`.`customer` (`Customer_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_order_table_Supplier1`
    FOREIGN KEY (`Supplier_Supplier_ID`)
    REFERENCES `TheTeamteam`.`supplier` (`Supplier_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_order_table_Payment1`
    FOREIGN KEY (`Payment_Payment_ID`)
    REFERENCES `TheTeamteam`.`payment` (`Payment_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_order_table_Shipment1`
    FOREIGN KEY (`Shipment_Shipment_ID`)
    REFERENCES `TheTeamteam`.`shipment` (`Shipment_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`inventory`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`inventory` (
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
-- Table `TheTeamteam`.`order_has_product`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`order_has_product` (
  `Product_Product_ID` VARCHAR(45) NOT NULL,
  `order_table_Order_ID` VARCHAR(45) NOT NULL,
  `Inventory_Inventory_ID` VARCHAR(45) NULL,
  `Units_In_Order` INT NULL,
  `Units_Done` INT NULL,
  PRIMARY KEY (`Product_Product_ID`, `order_table_Order_ID`),
  INDEX `fk_Order_has_Product1_Product1_idx` (`Product_Product_ID` ASC) VISIBLE,
  INDEX `fk_Order_has_Product_Inventory1_idx` (`Inventory_Inventory_ID` ASC) VISIBLE,
  INDEX `fk_Order_has_Product_order_table1_idx` (`order_table_Order_ID` ASC) VISIBLE,
  CONSTRAINT `fk_Order_has_Product1_Product1`
    FOREIGN KEY (`Product_Product_ID`)
    REFERENCES `TheTeamteam`.`product` (`Product_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Order_has_Product_Inventory1`
    FOREIGN KEY (`Inventory_Inventory_ID`)
    REFERENCES `TheTeamteam`.`inventory` (`Inventory_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Order_has_Product_order_table1`
    FOREIGN KEY (`order_table_Order_ID`)
    REFERENCES `TheTeamteam`.`order_table` (`Order_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`inventory_has_product`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`inventory_has_product` (
  `Inventory_Inventory_ID` VARCHAR(45) NOT NULL,
  `Product_Product_ID` VARCHAR(45) NOT NULL,
  `position` VARCHAR(45) NULL,
  `Units_In_Inventory` INT NULL,
  PRIMARY KEY (`Inventory_Inventory_ID`, `Product_Product_ID`),
  INDEX `fk_Inventory_has_Product_Product1_idx` (`Product_Product_ID` ASC) VISIBLE,
  INDEX `fk_Inventory_has_Product_Inventory1_idx` (`Inventory_Inventory_ID` ASC) VISIBLE,
  CONSTRAINT `fk_Inventory_has_Product_Inventory1`
    FOREIGN KEY (`Inventory_Inventory_ID`)
    REFERENCES `TheTeamteam`.`inventory` (`Inventory_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Inventory_has_Product_Product1`
    FOREIGN KEY (`Product_Product_ID`)
    REFERENCES `TheTeamteam`.`product` (`Product_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`employee_address`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`employee_address` (
  `Address_ID` VARCHAR(45) NOT NULL,
  `City` VARCHAR(45) NULL,
  `Governate` VARCHAR(45) NULL,
  `Street` VARCHAR(45) NULL,
  `Zip_Code` INT NULL,
  `Employee_Employee_ID` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`Address_ID`, `Employee_Employee_ID`),
  INDEX `fk_Employee_Address_Employee1_idx` (`Employee_Employee_ID` ASC) VISIBLE,
  CONSTRAINT `fk_Employee_Address_Employee1`
    FOREIGN KEY (`Employee_Employee_ID`)
    REFERENCES `TheTeamteam`.`employee` (`Employee_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`supplier_address`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`supplier_address` (
  `Address_ID` VARCHAR(45) NOT NULL,
  `City` VARCHAR(45) NULL,
  `Governate` VARCHAR(45) NULL,
  `Street` VARCHAR(45) NULL,
  `Zip_Code` INT NULL,
  `Supplier_Supplier_ID` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Address_ID`, `Supplier_Supplier_ID`),
  INDEX `fk_Inventory_Address_Supplier1_idx` (`Supplier_Supplier_ID` ASC) VISIBLE,
  CONSTRAINT `fk_Inventory_Address_Supplier1`
    FOREIGN KEY (`Supplier_Supplier_ID`)
    REFERENCES `TheTeamteam`.`supplier` (`Supplier_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`billMaterials`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`billMaterials` (
  `BillMaterials_ID` VARCHAR(45) NOT NULL,
  `Component_Name` INT NULL,
  `Valid_From` DATETIME NULL,
  `Valid_Until` DATETIME NULL,
  `Price` FLOAT NULL,
  PRIMARY KEY (`BillMaterials_ID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`inventory_has_billMaterials`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`inventory_has_billMaterials` (
  `Inventory_ID` VARCHAR(45) NOT NULL,
  `BillMaterials_ID` VARCHAR(45) NOT NULL,
  `position` VARCHAR(45) NULL,
  `Units_In_Inventory` INT NULL,
  PRIMARY KEY (`Inventory_ID`, `BillMaterials_ID`),
  INDEX `FK_BillMaterials_ID` (`BillMaterials_ID` ASC) INVISIBLE,
  INDEX `FK_Inventory_ID` (`Inventory_ID` ASC) VISIBLE,
  CONSTRAINT `fk_Inventory_has_BillMaterials_Inventory`
    FOREIGN KEY (`Inventory_ID`)
    REFERENCES `TheTeamteam`.`inventory` (`Inventory_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Inventory_has_BillMaterials_BillMaterials`
    FOREIGN KEY (`BillMaterials_ID`)
    REFERENCES `TheTeamteam`.`billMaterials` (`BillMaterials_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`product_has_billMaterials`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`product_has_billMaterials` (
  `Product_ID` VARCHAR(45) NOT NULL,
  `BillMaterials_ID` VARCHAR(45) NOT NULL,
  `Component_Name` VARCHAR(45) NULL,
  `Quantity` INT NULL,
  PRIMARY KEY (`Product_ID`, `BillMaterials_ID`),
  INDEX `FK_BillMaterials_ID_idx` (`BillMaterials_ID` ASC) INVISIBLE,
  INDEX `FK_Product_ID` (`Product_ID` ASC) VISIBLE,
  CONSTRAINT `fk_Product_has_BillMaterialst_Product`
    FOREIGN KEY (`Product_ID`)
    REFERENCES `TheTeamteam`.`product` (`Product_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Product_has_BillMaterialst_BillMaterials`
    FOREIGN KEY (`BillMaterials_ID`)
    REFERENCES `TheTeamteam`.`billMaterials` (`BillMaterials_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `TheTeamteam`.`manufacturingOrder`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`manufacturingOrder` (
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
-- Table `TheTeamteam`.`manufacturingOrder_has_materials`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TheTeamteam`.`manufacturingOrder_has_materials` (
  `ManufacturingOrder_ID` VARCHAR(45) NOT NULL,
  `Product_ID` VARCHAR(45) NOT NULL,
  `Units_In_order` INT NULL,
  `Units_Done` INT NULL,
  `Status` VARCHAR(45) NULL,
  PRIMARY KEY (`ManufacturingOrder_ID`, `Product_ID`),
  INDEX `Product_ID_idx` (`Product_ID` ASC) VISIBLE,
  INDEX `FK_ManufacturingOrder_ID` (`ManufacturingOrder_ID` ASC) VISIBLE,
  CONSTRAINT `ManufacturingOrder_has_Materials-Manufacturing`
    FOREIGN KEY (`ManufacturingOrder_ID`)
    REFERENCES `TheTeamteam`.`manufacturingOrder` (`ManufacturingOrder_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `ManufacturingOrder_has_Materials-Product`
    FOREIGN KEY (`Product_ID`)
    REFERENCES `TheTeamteam`.`product` (`Product_ID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
