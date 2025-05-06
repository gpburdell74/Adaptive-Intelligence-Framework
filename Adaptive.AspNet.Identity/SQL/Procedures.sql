CREATE PROCEDURE [dbo].[GetUserByEmailAddress]   @EmailAddress NVARCHAR(255)  AS    BEGIN     SELECT    [Users].[UserId],    [Users].[LoginName],    [Users].[PasswordHash],    [Users].[Deleted],    [Users].[CreatedDate],    [Users].[LastModifiedDate],    [Users].[LockedOut],    [Users].[Expiry],      [Persons].[PersonId],    [Persons].[FirstName],    [Persons].[MiddleName],    [Persons].[LastName],    [Persons].[Suffix],    [Persons].[Title],    [Persons].[Nickname],    [Persons].[EmailAddress],    [Persons].[UserId],    [Persons].[Deleted],    [Persons].[CreatedDate],    [Persons].[LastModifiedDate],      [Roles].[RoleId],    [Roles].[RoleName],    [Roles].[IsAdmin],    [Roles].[Deleted],    [Roles].[CreatedDate],    [Roles].[LastModifiedDate]     FROM    [dbo].[Users]     INNER JOIN [dbo].[Persons]      ON [Users].[UserId] = [Persons].[UserId]       INNER JOIN [dbo].[UserRoles]      ON [Users].[UserId] = [UserRoles].[UserId]       INNER JOIN [dbo].[Roles]      ON [UserRoles].[RoleId] = [Roles].[RoleId]   WHERE    [Users].[LoginName] = @EmailAddress    END  
GO

CREATE PROCEDURE [dbo].[CreateUserRole]   @RoleId  UNIQUEIDENTIFIER,   @UserId  UNIQUEIDENTIFIER  AS    BEGIN     DECLARE @Id UNIQUEIDENTIFIER     SELECT @Id = [UserRoleId]   FROM    [dbo].[UserRoles]   WHERE     [UserRoles].[UserId] = @UserId    AND [UserRoles].[RoleId] = @RoleId     IF (@Id IS NULL)     BEGIN    SET @Id = newid()      INSERT INTO [dbo].[UserRoles]     ([UserRoleId], [RoleId], [UserId], [Deleted], [CreatedDate], [LastModifiedDate])    VALUES     (@Id, @RoleId, @UserId, 0, SYSUTCDATETIME(), SYSUTCDATETIME())        END   SELECT @Id    END  
GO

CREATE PROCEDURe [dbo].[GetPasswordHash]   @UserID  UNIQUEIDENTIFIER  AS    BEGIN     SELECT [PasswordHash] FROM [dbo].[Users] WHERE [Users].[UserId] = @UserID      END  
GO

CREATE PROCEDURe [dbo].[GetLoginName]   @UserID  UNIQUEIDENTIFIER  AS    BEGIN     SELECT [LoginName] FROM [dbo].[Users] WHERE [Users].[UserId] = @UserID      END  
GO

CREATE PROCEDURE [dbo].[UpdateEmailAddress]   @UserID   UNIQUEIDENTIFIER,   @EmailAddress NVARCHAR(255)  AS    BEGIN     UPDATE [dbo].[Users]     SET    [Users].[LoginName] = @EmailAddress   WHERE    [Users].[UserId] = @UserId      UPDATE [dbo].[Persons]     SET       [Persons].[EmailAddress] = @EmailAddress   WHERE    [Persons].[UserId] = @UserId      END  
GO

CREATE PROCEDURE [dbo].[UpdateSecurity]   @UserId  UNIQUEIDENTIFIER,   @Data  NVARCHAR(255)  AS    BEGIN     UPDATE [dbo].[Users]     SET    [PasswordHash] = @Data   WHERE    [Users].[UserId] = @UserId    END  
GO

CREATE PROCEDURE [dbo].[UsersDelete]   @UserId   UNIQUEIDENTIFIER  AS    BEGIN     UPDATE [dbo].[Users]     SET    [Deleted] = 1   WHERE    [Users].[UserId] = @UserId      END  
GO

CREATE PROCEDURE [dbo].[UsersGetById]   @UserId   UNIQUEIDENTIFIER  AS    BEGIN     SELECT    [UserId],    [LoginName],    [Deleted],    [LockedOut],    [Expiry],    [CreatedDate],    [LastModifiedDate]     FROM     [dbo].[Users]   WHERE    [Users].[UserId] = @UserId      END  
GO

CREATE PROCEDURE [dbo].[UsersUpdate]   @UserId   UNIQUEIDENTIFIER,   @LoginName  NVARCHAR(255),   @PasswordHash NVARCHAR(255),   @LockedOut  BIT,   @Expiry   DATETIME NULL  AS    BEGIN     UPDATE [dbo].[Users]     SET    LoginName = @LoginName,    PasswordHash = @PasswordHash,    LastModifiedDate = SYSUTCDATETIME(),    LockedOut = @LockedOut,    Expiry = @Expiry   WHERE    [UserId] = @UserId     SELECT    [UserId],    [LoginName],    [Deleted],    [LockedOut],    [Expiry],    [CreatedDate],    [LastModifiedDate]     FROM     [dbo].[Users]   WHERE    [Users].[UserId] = @UserId      END  
GO

CREATE PROCEDURE [dbo].[UsersInsert]   @LoginName  NVARCHAR(255),   @PasswordHash NVARCHAR(255)  AS    BEGIN     DECLARE @Id UNIQUEIDENTIFIER = newid()     INSERT INTO [dbo].[Users]       (UserId, LoginName, PasswordHash, Deleted, CreatedDate, LastModifiedDate, LockedOut, Expiry)   VALUES    (@Id, @LoginName, @PasswordHash, 0, SYSUTCDATETIME(), SYSUTCDATETIME(), 0, NULL)     SELECT @Id      END  
GO

CREATE PROCEDURE [dbo].[LoginHistoryUpdate]   @LoginHistoryId   UNIQUEIDENTIFIER,   @UserId     UNIQUEIDENTIFIER,   @LoginDate    DATETIME  AS    BEGIN     UPDATE [dbo].[LoginHistory]     SET    UserId = @UserId,    LoginDate = @LoginDate,    LastModifiedDate = SYSUTCDATETIME()   WHERE    [LoginHistoryId] = @LoginHistoryId     SELECT    [LoginHistoryId],    [UserId],    [LoginDate],    [Deleted],    [CreatedDate],    [LastModifiedDate]     FROM     [dbo].[LoginHistory]   WHERE    [LoginHistoryId] = @LoginHistoryId      END  
GO

CREATE PROCEDURE [dbo].[LoginHistoryInsert]   @UserId   UNIQUEIDENTIFIER  AS    BEGIN     DECLARE @Id UNIQUEIDENTIFIER = newid()     INSERT INTO [dbo].[LoginHistory]       (LoginHistoryId, UserId, LoginDate, Deleted, CreatedDate, LastModifiedDate)   VALUES    (@Id, @UserId, SYSUTCDATETIME(), 0, SYSUTCDATETIME(), SYSUTCDATETIME())     SELECT @Id      END  
GO

CREATE PROCEDURE [dbo].[LoginHistoryGetById]   @LoginHistoryId   UNIQUEIDENTIFIER  AS    BEGIN     SELECT    [LoginHistoryId],    [UserId],    [LoginDate],    [Deleted],    [CreatedDate],    [LastModifiedDate]     FROM     [dbo].[LoginHistory]   WHERE    [LoginHistory].[LoginHistoryId] = @LoginHistoryId      END  
GO

CREATE PROCEDURE [dbo].[LoginHistoryDelete]   @LoginHistoryId   UNIQUEIDENTIFIER  AS    BEGIN     UPDATE [dbo].[LoginHistory]     SET    [Deleted] = 1   WHERE    [LoginHistory].[LoginHistoryId] = @LoginHistoryId      END  
GO

CREATE PROCEDURE [dbo].[PersonsDelete]   @PersonId   UNIQUEIDENTIFIER  AS    BEGIN     UPDATE [dbo].[Persons]     SET    [Deleted] = 1   WHERE    [Persons].[PersonId] = @PersonId      END  
GO

CREATE PROCEDURE [dbo].[PersonsUpdate]   @PersonId   UNIQUEIDENTIFIER,   @FirstName   NVARCHAR(100) NULL,   @MiddleName   NVARCHAR(100) NULL,   @LastName   NVARCHAR(100) NULL,   @Suffix    NVARCHAR(100) NULL,   @Title    NVARCHAR(100) NULL,   @Nickname   NVARCHAR(100) NULL,   @EmailAddress  NVARCHAR(255) NULL,   @UserId    UNIQUEIDENTIFIER  AS    BEGIN     UPDATE [dbo].[Persons]     SET    [FirstName] = @FirstName,    [MiddleName] = @MiddleName,    [LastName] = @LastName,    [Suffix] = @Suffix,    [Title] = @Title,    [Nickname] = @Nickname,    [EmailAddress] = @EmailAddress,    [UserId] = @UserId,    [Deleted] = 0,    [LastModifiedDate] = SYSUTCDATETIME()   WHERE    [Persons].[PersonId] = @PersonId     SELECT    [PersonId],    [FirstName],    [MiddleName],    [LastName],    [Suffix],    [Title],    [Nickname],    [EmailAddress],    [UserId],    [Deleted],    [CreatedDate],    [LastModifiedDate]     FROM     [dbo].[Persons]   WHERE    [Persons].[PersonId] = @PersonId      END  
GO

CREATE PROCEDURE [dbo].[PersonsInsert]   @FirstName   NVARCHAR(100) NULL,   @MiddleName   NVARCHAR(100) NULL,   @LastName   NVARCHAR(100) NULL,   @Suffix    NVARCHAR(100) NULL,   @Title    NVARCHAR(100) NULL,   @Nickname   NVARCHAR(100) NULL,   @EmailAddress  NVARCHAR(255) NULL,   @UserId    UNIQUEIDENTIFIER  AS    BEGIN     DECLARE @Id UNIQUEIDENTIFIER = newid()     INSERT INTO [dbo].[Persons]    ([PersonId],[FirstName],[MiddleName],[LastName],[Suffix],[Title],[Nickname],[EmailAddress],     [UserId],[Deleted],[CreatedDate],[LastModifiedDate])     VALUES    (@Id, @FirstName,  @MiddleName, @LastName, @Suffix, @Title, @Nickname, @EmailAddress,    @UserId, 0, SYSUTCDATETIME(), SYSUTCDATETIME())     SELECT @Id      END  
GO

CREATE PROCEDURE [dbo].[PersonsGetById]   @PersonId   UNIQUEIDENTIFIER  AS    BEGIN     SELECT    [PersonId],    [FirstName],    [MiddleName],    [LastName],    [Suffix],    [Title],    [Nickname],    [EmailAddress],    [UserId],    [Deleted],    [CreatedDate],    [LastModifiedDate]     FROM     [dbo].[Persons]   WHERE    [Persons].[PersonId] = @PersonId      END  
GO

CREATE PROCEDURE [dbo].[UserRolesInsert]   @RoleId   UNIQUEIDENTIFIER,   @UserId   UNIQUEIDENTIFIER  AS    BEGIN     DECLARE @Id UNIQUEIDENTIFIER = newid()     INSERT INTO [dbo].[UserRoles]    ([UserRoleId],[RoleId],[UserId], [Deleted],[CreatedDate],[LastModifiedDate])     VALUES    (@Id, @RoleId,  @UserId, 0, SYSUTCDATETIME(), SYSUTCDATETIME())     SELECT @Id      END  
GO

CREATE PROCEDURE [dbo].[UserRolesGetById]   @UserRoleId   UNIQUEIDENTIFIER  AS    BEGIN     SELECT    [UserRoleId],    [RoleId],    [UserId],    [Deleted],    [CreatedDate],    [LastModifiedDate]     FROM     [dbo].[UserRoles]   WHERE    [UserRoles].[UserRoleId] = @UserRoleId      END  
GO

CREATE PROCEDURE [dbo].[UserRolesDelete]   @UserRoleId   UNIQUEIDENTIFIER  AS    BEGIN     UPDATE [dbo].[UserRoles]     SET    [Deleted] = 1   WHERE    [UserRoles].[UserRoleId] = @UserRoleId      END  
GO

CREATE PROCEDURE [dbo].[UserRolesUpdate]   @UserRoleId  UNIQUEIDENTIFIER,   @RoleId   UNIQUEIDENTIFIER,   @UserId   UNIQUEIDENTIFIER  AS    BEGIN     UPDATE [dbo].[UserRoles]     SET    [RoleId] = @RoleId,    [UserId] = @UserId,    [Deleted] = 0,    [LastModifiedDate] = SYSUTCDATETIME()   WHERE    [UserRoles].[UserRoleId] = @UserRoleId     SELECT    [UserRoleId],    [RoleId],    [UserId],    [Deleted],    [CreatedDate],    [LastModifiedDate]     FROM     [dbo].[UserRoles]   WHERE    [UserRoles].[UserRoleId] = @UserRoleId      END  
GO

CREATE PROCEDURE [dbo].[RolesUpdate]   @RoleId  UNIQUEIDENTIFIER,   @RoleName NVARCHAR(255),   @IsAdmin BIT  AS    BEGIN     UPDATE [dbo].[Roles]     SET    [RoleId] = @RoleId,    [RoleName] = @RoleName,    [Deleted] = 0,    [IsAdmin] = @IsAdmin,    [LastModifiedDate] = SYSUTCDATETIME()   WHERE    [Roles].[RoleId] = @RoleId     SELECT    [RoleId],    [RoleName],    [IsAdmin],    [Deleted],    [CreatedDate],    [LastModifiedDate]     FROM     [dbo].[Roles]   WHERE    [Roles].[RoleId] = @RoleId      END  
GO

CREATE PROCEDURE [dbo].[RolesInsert]   @RoleName  NVARCHAR(255),   @IsAdmin  BIT  AS    BEGIN     DECLARE @Id UNIQUEIDENTIFIER = newid()     INSERT INTO [dbo].[Roles]    ([RoleId],[RoleName], [IsAdmin], [Deleted],[CreatedDate],[LastModifiedDate])     VALUES    (@Id, @RoleName, @IsAdmin, 0, SYSUTCDATETIME(), SYSUTCDATETIME())     SELECT @Id      END  
GO

CREATE PROCEDURE [dbo].[RolesGetById]   @RoleId   UNIQUEIDENTIFIER  AS    BEGIN     SELECT    [RoleId],    [RoleName],    [IsAdmin],    [Deleted],    [CreatedDate],    [LastModifiedDate]     FROM     [dbo].[Roles]   WHERE    [Roles].[RoleId] = @RoleId      END  
GO
CREATE PROCEDURE [dbo].[RolesDelete]   @RoleId   UNIQUEIDENTIFIER  AS    BEGIN     UPDATE [dbo].[Roles]     SET    [Deleted] = 1   WHERE    [Roles].[RoleId] = @RoleId      END  
GO

