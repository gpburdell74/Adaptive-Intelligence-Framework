-- -----------------------------------------------------------
-- First User Information
-- -----------------------------------------------------------
--
-- Populate the data contents for the first user record, which 
-- will also be the Adminstrator User.
--
DECLARE @Title			NVARCHAR(100) = 'Mr.'
DECLARE @FirstName		NVARCHAR(100) = 'Samuel'
DECLARE @MiddleName		NVARCHAR(100) = 'Casey'
DECLARE @LastName		NVARCHAR(100) = 'Jones'
DECLARE @Suffix			NVARCHAR(100) = ''
DECLARE @Nickname		NVARCHAR(100) = 'Sam'

-- -----------------------------------------------------------
-- Create Administrator Role
-- -----------------------------------------------------------
INSERT INTO [dbo].[Roles]
	([RoleId], [RoleName], [IsAdmin], [RoleFlags], [Deleted], [CreatedDate], [LastModifiedDate])
VALUES
	(newid(), 'Administrator', 1, 0, 0, SYSUTCDATETIME(), SYSUTCDATETIME())

DECLARE @RoleId UNIQUEIDENTIFIER
SELECT @RoleId = [Roles].[RoleId] FROM [dbo].[Roles]

-- -----------------------------------------------------------
-- Create Standard User Role
-- -----------------------------------------------------------
INSERT INTO [dbo].[Roles]
	([RoleId], [RoleName], [IsAdmin], [RoleFlags], [Deleted], [CreatedDate], [LastModifiedDate])
VALUES
	(newid(), 'User', 0, 0, 0, SYSUTCDATETIME(), SYSUTCDATETIME())

-- -----------------------------------------------------------
-- Create the 1st / Administrator User
-- -----------------------------------------------------------
INSERT INTO [dbo].[Users] 
	([UserId], [LoginName], [PasswordHash], [Deleted], [CreatedDate], [LastModifiedDate])
  VALUES
	(newid(), @EmailAddress, @PasswordHash, 0, SYSUTCDATETIME(), SYSUTCDATETIME())

DECLARE @UserId  UNIQUEIDENTIFIER
SELECT @UserId = [Users].[UserId] FROM [dbo].[Users]

-- -----------------------------------------------------------
-- Create the User to Role relationship for the Admin user.
-- -----------------------------------------------------------
INSERT INTO [dbo].[UserRoles]
	([UserRoleId], [RoleId], [UserId], [Deleted], [CreatedDate], [LastModifiedDate])
VALUES
	(newid(), @RoleId, @UserId, 0, SYSUTCDATETIME(), SYSUTCDATETIME())

-- -----------------------------------------------------------
-- Create the Person record for the Admin User
-- -----------------------------------------------------------
INSERT INTO [dbo].[Persons]
	([PersonId], [FirstName], [MiddleName], [LastName], [Nickname], [Title], [Suffix],
	 [EmailAddress], [UserId], [Deleted], [CreatedDate], [LastModifiedDate])
VALUES
	(NEWID(), @FirstName, @MiddleName, @LastName, @Nickname, @Title, @Suffix,
	 @EmailAddress, @UserId, 0, SYSUTCDATETIME(), SYSUTCDATETIME())

-- -----------------------------------------------------------
-- See the new data.
-- -----------------------------------------------------------
SELECT * FROM [Users]
SELECT * FROM [Roles]
SELECT * FROM [UserRoles]
SELECT * FROM [Persons]
