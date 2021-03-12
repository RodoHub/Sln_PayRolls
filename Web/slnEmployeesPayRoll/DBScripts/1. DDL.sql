/**************************************************************************************************
		 Author:	Rodolfo Bernal Sánchez
	Description:	Scripts for exam: "Employees PayRoll"
 ***************************************************************************************************/
CREATE DATABASE PayrollEmployees

GO

USE PayrollEmployees

GO

CREATE TABLE Role_Cat
(
	RoleID INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(100),
	Description VARCHAR(300)
)

GO

CREATE TABLE Employees_Tab
(
	EmployeeID INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(100),
	LastNames VARCHAR(200),
	AdmissionDate DATETIME,
	RoleID INT FOREIGN KEY(RoleID) REFERENCES Role_Cat(RoleID),
	Email VARCHAR(200),
	Password VARCHAR(10),
	Active BIT
)

GO

CREATE TABLE PayRollInfo_Tab
(
	PayRollInfoID INT IDENTITY(1,1) PRIMARY KEY,
	BaseSalary DECIMAL(8,2),
	BreakfastDeduction DECIMAL(8,2),
	SavingDeduction DECIMAL(8,2),
	Active BIT,
	CreationDate DATETIME,
	EmployeeID INT FOREIGN KEY(EmployeeID) REFERENCES Employees_Tab(EmployeeID)
)

GO

CREATE TABLE PayRoll_Tab
(
	PayRollID INT IDENTITY(1,1) PRIMARY KEY,
	InitialPeriod DATETIME,
	EndPeriod DATETIME,
	PayRollDate DATETIME,
	PayRollInfoID INT FOREIGN KEY(PayRollInfoID) REFERENCES PayRollInfo_Tab(PayRollInfoID)
)
