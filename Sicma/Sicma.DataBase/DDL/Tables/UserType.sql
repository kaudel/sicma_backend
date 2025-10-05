CREATE TABLE [dbo].[UserType]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserTypeName] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(100) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreatedDate] DATETIME NOT NULL DEFAULT getdate(), 
    [UpdatedDate] DATETIME NULL, 
    [CreatedUserId] INT NOT NULL DEFAULT 1, 
)
