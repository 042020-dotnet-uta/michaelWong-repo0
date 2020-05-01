USE CustomerApplicationDb;
GO

INSERT INTO [UserTypes] ([Description], [Name]) VALUES (N'Administrator user. Access to tools to create new users, locations and products. Able to manage inventories.', N'Admin');
INSERT INTO [UserTypes] ([Description], [Name]) VALUES (N'Customer user. Access to viewing locations and placing orders', N'Customer');
GO

INSERT INTO [Users] ([FirstName], [LastName], [Password], [UserTypeId]) VALUES (N'AdminFirstName', N'AdminLastName', N'password', 1);
GO