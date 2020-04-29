USE DemoDatabase;
GO
CREATE TABLE [Countries]
(
	[countryid] int NOT NULL PRIMARY KEY,
	[countryname] nvarchar(60) NOT NULL
);
GO

CREATE TABLE [States]
(
	[stateid] int NOT NULL PRIMARY KEY,
	[statename] nvarchar(60) NOT NULL,
	[countryid] int NOT NULL FOREIGN KEY REFERENCES [Countries]([countryid])
);
GO

INSERT INTO [Countries] ([countryid], [countryname]) VALUES (1213, N'United States');
INSERT INTO [Countries] ([countryid], [countryname]) VALUES (2315, N'Canada');
INSERT INTO [Countries] ([countryid], [countryname]) VALUES (9073, N'Mexico');
INSERT INTO [Countries] ([countryid], [countryname]) VALUES (4891, N'France');
INSERT INTO [Countries] ([countryid], [countryname]) VALUES (1001, N'Australia');

INSERT INTO [States] ([stateid], [statename], [countryid]) VALUES (12, N'Louisiana', 1213);
INSERT INTO [States] ([stateid], [statename], [countryid]) VALUES (31, N'Michigan', 1213);
INSERT INTO [States] ([stateid], [statename], [countryid]) VALUES (42, N'Hawaii', 1213);
INSERT INTO [States] ([stateid], [statename], [countryid]) VALUES (32, N'Nova Scotia', 2315);
INSERT INTO [States] ([stateid], [statename], [countryid]) VALUES (123, N'Prince Edward Island', 2315);
INSERT INTO [States] ([stateid], [statename], [countryid]) VALUES (342, N'New South Wales', 1001);

SELECT * FROM [Countries] AS [C]
CROSS JOIN [States] AS [S];
SELECT * FROM [Countries] AS [C]
JOIN [States] AS [S] ON [C].[countryid] = [S].[countryid];
SELECT * FROM [Countries] AS [C]
RIGHT JOIN [States] AS [S] ON [C].[countryid] = [S].[countryid];
SELECT * FROM [Countries] AS [C]
LEFT JOIN [States] AS [S] ON [C].[countryid] = [S].[countryid];
SELECT * FROM [Countries] AS [C]
FULL JOIN [States] AS [S] ON [C].[countryid] = [S].[countryid];