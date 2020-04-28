CREATE TABLE Department
(
	DepartmentId INT NOT NULL PRIMARY KEY,
	Location NVARCHAR(80) NOT NULL
);
GO
CREATE TABLE Employee
(
	[EmployeeId] INT NOT NULL PRIMARY KEY,
	[FirstName] NVARCHAR(40) NOT NULL,
	[LastName] NVARCHAR(40) NOT NULL,
	[SSN] NVARCHAR(9) NOT NULL,
	[DeptID] INT FOREIGN KEY REFERENCES [Department]([DepartmentId])
);
GO
CREATE TABLE EmpDetails
(
	[EmployeeId] INT NOT NULL FOREIGN KEY REFERENCES [Employee]([EmployeeId]),
	[Salary] MONEY NOT NULL,
	[Address1] NVARCHAR(80) NOT NULL,
	[Address2] NVARCHAR(80) NOT NULL,
	[City] NVARCHAR(40) NOT NULL,
	[State] NVARCHAR(30) NOT NULL,
	[Country] NVARCHAR(30) NOT NULL
);
GO

--add at least 3 records into each table
INSERT INTO [Department] ([DepartmentId], [Location]) VALUES (1, N'HR');
INSERT INTO [Department] ([DepartmentId], [Location]) VALUES (2, N'PR');
INSERT INTO [Department] ([DepartmentId], [Location]) VALUES (3, N'Medical');
INSERT INTO [Employee] ([DeptID], [EmployeeId], [FirstName], [LastName], [SSN]) VALUES (1, 1, N'James', N'Jones', N'429381432');
INSERT INTO [Employee] ([DeptID], [EmployeeId], [FirstName], [LastName], [SSN]) VALUES (1, 2, N'Mary', N'King', N'123872309');
INSERT INTO [Employee] ([DeptID], [EmployeeId], [FirstName], [LastName], [SSN]) VALUES (3, 3, N'Jessie', N'Marton', N'123980909');
INSERT INTO [EmpDetails] ([Address1], [Address2], [City], [Country], [EmployeeId], [Salary], [State]) VALUES (N'1983 Some Str', N'', N'Detroit', N'US', 1, 100000, N'Michigan');
INSERT INTO [EmpDetails] ([Address1], [Address2], [City], [Country], [EmployeeId], [Salary], [State]) VALUES (N'1930 Some Str', N'', N'Detroit', N'US', 2, 130000, N'Michigan');
INSERT INTO [EmpDetails] ([Address1], [Address2], [City], [Country], [EmployeeId], [Salary], [State]) VALUES (N'1182 Some Dr', N'', N'Detroit', N'US', 3, 90000, N'Michigan');
--add employee Tina Smith and department Marketing
INSERT INTO [Department] ([DepartmentId], [Location]) VALUES (4, N'Marketing');
INSERT INTO [Employee] ([DeptID], [EmployeeId], [FirstName], [LastName], [SSN]) VALUES (4, 4, N'Tina', N'Smith', N'842129933');
INSERT INTO [EmpDetails] ([Address1], [Address2], [City], [Country], [EmployeeId], [Salary], [State]) VALUES (N'1542 Some Dr', N'', N'Detroit', N'US', 4, 70000, N'Michigan');
--list all employees in Marketing
SELECT [Employee].[EmployeeId], [Employee].[LastName], [Employee].[FirstName], [Department].[Location]
FROM [Employee] JOIN [Department] ON [Employee].[DeptID] = [Department].[DepartmentId]
WHERE [Department].[Location] = N'Marketing';
--report total salary of Marketing
SELECT SUM([EmpDetails].[Salary]) AS MarketingSalary
FROM [EmpDetails] JOIN [Employee] ON [Employee].[EmployeeId] = [EmpDetails].[EmployeeId]
JOIN [Department] ON [Department].[DepartmentId] = [Employee].[DeptID]
WHERE [Department].[Location] = N'Marketing'
GROUP BY [Department].[DepartmentId];
--report total employees by department
SELECT [Department].[Location], COUNT([Employee].[EmployeeId]) AS EmployeeCount
FROM [Employee] JOIN [Department] ON [Employee].[DeptID] = [Department].[DepartmentId]
GROUP BY [Department].[Location];
--increase salary of Tina Smith to $90,000
UPDATE [EmpDetails]
SET [Salary] = 90000
FROM [Employee] JOIN [EmpDetails] on [Employee].[EmployeeId] = [EmpDetails].[EmployeeId]
WHERE [Employee].[FirstName] = N'Tina' AND [Employee].[LastName] = N'Smith';