CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Nickname] VARCHAR(50) NOT NULL, 
    [FullName] NVARCHAR(80) NOT NULL, 
    [Email] NCHAR(10) NOT NULL, 
    [Institution] VARCHAR(100) NOT NULL, 
    [UserTypeId] INT NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [CreatedDate] DATETIME NOT NULL DEFAULT getdate(), 
    [UpdatedDate] DATETIME NULL, 
    [CreatedUserId] INT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_Users_ToUserType] FOREIGN KEY (UserTypeId) REFERENCES [UserType]([Id]) 
)
