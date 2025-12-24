CREATE TABLE [dbo].[Person]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastNme] NVARCHAR(50) NULL, 
    [Company] NVARCHAR(100) NULL, 
    [Email] NVARCHAR(100) NULL, 
    [Phone] NVARCHAR(50) NULL, 
    [Notes] NVARCHAR(MAX) NULL, 
    [ImageFilename] NVARCHAR(100) NULL
)
