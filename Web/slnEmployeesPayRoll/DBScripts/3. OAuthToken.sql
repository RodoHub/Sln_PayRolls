/**************************************************************************************************
		 Author:	Rodolfo Bernal Sánchez
	Description:	Scripts for exam: "Employees PayRoll"
					- OAUT Token
		   DATE:    24/MAR/2021
 ***************************************************************************************************/

USE PayrollEmployees

GO

--BEGIN TRAN

ALTER TABLE Employees_Tab ADD Token UNIQUEIDENTIFIER NULL

GO

GO
BEGIN TRAN
ROLLBACK
CREATE TABLE TokenStatus
(
	TokenStatusID TINYINT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(10),
	Description VARCHAR(100),
	CreationDate DATETIME
)

GO

INSERT INTO TokenStatus (Name, Description, CreationDate)
SELECT 'Active' AS Name, 'Token Active' AS Description, GETDATE() AS CreationDate
UNION
SELECT 'Inactive' AS Name, 'Token Inactive' AS Description, GETDATE() AS CreationDate

GO

CREATE TABLE TokenAuth
(
	TokenAuthID INT IDENTITY(1,1) PRIMARY KEY,
	Token UNIQUEIDENTIFIER NULL,
	CreationDate DATETIME,
	TokenStatusID TINYINT FOREIGN KEY(TokenStatusID) REFERENCES TokenStatus(TokenStatusID)
)

GO


--ROLLBACK
--COMMIT
SELECT * FROM PayRoll_Tab
SELECT * FROM PayRollInfo_Tab
SELECT * FROM Employees_Tab
SELECT * FROM TokenAuth
SELECT * FROM TokenStatus
--BEGIN TRAN
--DELETE FROM PayRollInfo_Tab WHERE PayRollInfoID >= 5
--DELETE FROM Employees_Tab  WHERE EmployeeID >= 5
--COMMIT

--BEGIN TRAN
--UPDATE TokenAuth SET TokenStatusID = 2 
--COMMIT


