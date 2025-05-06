-- -----------------------------------------------------------
-- Roles Table
-- -----------------------------------------------------------
CREATE TABLE [dbo].[Roles]
(
	[RoleId]			UNIQUEIDENTIFIER	NOT NULL,
	[RoleName]			NVARCHAR(255)		NOT NULL,
	[IsAdmin]			BIT					NOT NULL,
	[RoleFlags]			INT					NOT NULL,
	[Deleted]			BIT					NOT NULL,
	[CreatedDate]		DATETIME			NOT NULL,
	[LastModifiedDate]	DATETIME			NULL,
	CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
	(
		[RoleId] ASC
	)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_IsAdmin]  DEFAULT ((0)) FOR [IsAdmin]
GO

ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO

ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_RoleFlags]  DEFAULT ((0)) FOR [RoleFlags]
GO

ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_CreatedDate]  DEFAULT (sysutcdatetime()) FOR [CreatedDate]
GO

-- -----------------------------------------------------------
-- Users Table
-- -----------------------------------------------------------
CREATE TABLE [dbo].[Users]
(
	[UserId]			UNIQUEIDENTIFIER	NOT NULL,
	[LoginName]			NVARCHAR(255)		NOT NULL,
	[PasswordHash]		NVARCHAR(255)		NULL,
	[Deleted]			BIT					NOT NULL,
	[CreatedDate]		DATETIME			NOT NULL,
	[LastModifiedDate]	DATETIME			NULL,
	[LockedOut]			BIT					NOT NULL,
	[Expiry]			DATETIME			NULL,
	CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
	(
		[UserId] ASC
	)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreatedDate]  DEFAULT (sysutcdatetime()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_LockedOut]  DEFAULT ((0)) FOR [LockedOut]
GO

-- -----------------------------------------------------------
-- User Roles Table
-- -----------------------------------------------------------
CREATE TABLE [dbo].[UserRoles]
(
	[UserRoleId]		UNIQUEIDENTIFIER	NOT NULL,
	[RoleId]			UNIQUEIDENTIFIER	NOT NULL,
	[UserId]			UNIQUEIDENTIFIER	NOT NULL,
	[Deleted]			BIT					NOT NULL,
	[CreatedDate]		DATETIME			NOT NULL,
	[LastModifiedDate]	DATETIME			NULL,
	CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
	(
		[UserRoleId] ASC
	)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserRoles] ADD  CONSTRAINT [DF_UserRoles_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO

ALTER TABLE [dbo].[UserRoles] ADD  CONSTRAINT [DF_UserRoles_CreatedDate]  DEFAULT (sysutcdatetime()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO

ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]
GO

ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO

-- -----------------------------------------------------------
-- Persons Table
-- -----------------------------------------------------------
CREATE TABLE [dbo].[Persons]
(
	[PersonId]			UNIQUEIDENTIFIER	NOT NULL,
	[FirstName]			NVARCHAR(100)		NULL,
	[MiddleName]		NVARCHAR(100)		NULL,
	[LastName]			NVARCHAR(100)		NULL,
	[Suffix]			NVARCHAR(100)		NULL,
	[Title]				NVARCHAR(100)		NULL,
	[Nickname]			NVARCHAR(100)		NULL,
	[EmailAddress]		NVARCHAR(255)		NULL,
	[UserId]			UNIQUEIDENTIFIER	NULL,
	[Deleted]			BIT					NOT NULL,
	[CreatedDate]		DATETIME			NOT NULL,
	[LastModifiedDate]	DATETIME			NULL,
	CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
	(
		[PersonId] ASC
	)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Persons] ADD  CONSTRAINT [DF_Persons_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO

ALTER TABLE [dbo].[Persons] ADD  CONSTRAINT [DF_Persons_CreatedDate]  DEFAULT (sysutcdatetime()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[Persons]  WITH CHECK ADD  CONSTRAINT [FK_Persons_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[Persons] CHECK CONSTRAINT [FK_Persons_Users]
GO

-- -----------------------------------------------------------
-- Login History Table
-- -----------------------------------------------------------
CREATE TABLE [dbo].[LoginHistory]
(
	[LoginHistoryId]	UNIQUEIDENTIFIER	NOT NULL,
	[UserId]			UNIQUEIDENTIFIER	NOT NULL,
	[LoginDate]			DATETIME			NOT NULL,
	[Deleted]			BIT					NOT NULL,
	[CreatedDate]		DATETIME			NOT NULL,
	[LastModifiedDate]	DATETIME			NULL,
	CONSTRAINT [PK_LoginHistory] PRIMARY KEY CLUSTERED 
	(
		[LoginHistoryId] ASC
	)
	WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[LoginHistory] ADD  CONSTRAINT [DF_LoginHistory_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO

ALTER TABLE [dbo].[LoginHistory] ADD  CONSTRAINT [DF_LoginHistory_CreatedDate]  DEFAULT (sysutcdatetime()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[LoginHistory]  WITH CHECK ADD  CONSTRAINT [FK_LoginHistory_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[LoginHistory] CHECK CONSTRAINT [FK_LoginHistory_Users]
GO

