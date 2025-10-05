CREATE TABLE [dbo].[PracticeLevel]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(70) NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreatedDate] DATETIME NOT NULL DEFAULT getdate(), 
    [UpdatedDate] DATETIME NULL, 
    [CreatedUserId] INT NOT NULL DEFAULT 1, 
)
