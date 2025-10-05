CREATE TABLE [dbo].[OperationConfig]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [OperationName] NCHAR(10) NOT NULL, 
    [Type] NCHAR(10) NOT NULL, 
    [NumElements] INT NOT NULL, 
    [Digits] INT NOT NULL, 
    [Range] VARCHAR(20) NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreatedDate] DATETIME NOT NULL DEFAULT getdate(), 
    [UpdatedDate] DATETIME NULL, 
    [CreatedUserId] INT NOT NULL DEFAULT 1, 
)
