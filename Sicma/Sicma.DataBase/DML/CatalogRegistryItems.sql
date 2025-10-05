/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
DECLARE @UserTypeId INT =0;

INSERT INTO UserType ( UserTypeName, Description, IsActive, CreatedDate, UpdatedDate, CreatedUserId )
VALUES ('SuperAdmin','Super User Admin',1,GETDATE(), GETDATE(),1)

SET @UserTypeId = (SELECT @@IDENTITY)

INSERT INTO UserType ( UserTypeName, Description, IsActive, CreatedDate,UpdatedDate, CreatedUserId )
VALUES ('Admin','User Admin ',1,GETDATE(), GETDATE(),1),
       ('User','User student',1,GETDATE(), GETDATE(),1)

INSERT INTO Users (Nickname, FullName, email, Institution, UserTypeId,IsActive, CreatedDate, UpdatedDate, CreatedUserId )
VALUES ('admin','Super User Admin','test@test','root',@UserTypeId,1,GETDATE(), GETDATE(),1)
