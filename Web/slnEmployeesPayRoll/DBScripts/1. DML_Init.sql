/**************************************************************************************************
		 Author:	Rodolfo Bernal Sánchez
	Description:	Scripts for exam: "Employees PayRoll"
 ***************************************************************************************************/

USE PayrollEmployees

GO

INSERT INTO Role_Cat (Name, Description) VALUES ('Admin', 'Payrolls administrator')
INSERT INTO Role_Cat (Name, Description) VALUES ('Employee', 'Employee with restricted privileges')

GO

INSERT INTO Employees_Tab (Name, LastNames, AdmissionDate, RoleID, Email, Password, Active)
SELECT 'Rarimo' AS Name, 'Barrera Torres' AS LastNames, '2020-01-01' AS AdmissionDate,
		1 AS RoleID, 'ramiro.barrera@arkusnexus.com' AS Email, 'Adm1n1s$' Password,
		1 AS Active 
UNION 
SELECT 'Alma' AS Name, 'Madero Cisneros' AS LastNames, '2021-01-01' AS AdmissionDate,
		2 AS RoleID, 'alma.madero@arkusnexus.com' AS Email, 'Almit4$' Password,
		1 AS Active 

GO

INSERT INTO PayRollInfo_Tab (BaseSalary, BreakfastDeduction, SavingDeduction, Active, CreationDate, EmployeeID)
					 VALUES (10000, 0.10, 0.05, 1, '2020-01-01', 1)

INSERT INTO PayRollInfo_Tab (BaseSalary, BreakfastDeduction, SavingDeduction, Active, CreationDate, EmployeeID)
					 VALUES (7000, 0.10, 0.07, 1, '2021-01-01', 2)

GO


select * from Role_Cat
select * from Employees_Tab
select * from PayRollInfo_Tab
select * from PayRoll_Tab
