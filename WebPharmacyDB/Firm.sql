CREATE TABLE [dbo].[Firm]
(
	[Id] INT Identity(1,1) NOT NULL PRIMARY KEY,
	Name nvarchar(100) not null,
	Country nvarchar(100) not null
)
