BEGIN TRAN
GO

IF NOT EXISTS (SELECT * FROM [INFORMATION_SCHEMA].[TABLES] WHERE [TABLE_NAME] = 'users')
BEGIN
	CREATE TABLE [dbo].[users]
	(
		[id] BIGINT PRIMARY KEY IDENTITY(1,1),
		[external_guid] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
		[login] VARCHAR(255) NOT NULL,
		[password] VARBINARY(20) NOT NULL,
		[is_default] BIT NOT NULL DEFAULT 0,
		[created_at] DATETIME2 DEFAULT(SYSDATETIME())
	)
END;

GO

IF NOT EXISTS (SELECT * FROM [sys].[indexes] WHERE [name] = 'IX_Users_External_Guid' AND [object_id] = OBJECT_ID('[dbo].[users]'))
BEGIN
	CREATE NONCLUSTERED INDEX IX_Users_External_Guid
	ON [dbo].[users] ([external_guid])
	INCLUDE ([login], [password], [created_at])
END

GO

IF NOT EXISTS (SELECT * FROM [sys].[indexes] WHERE [name] = 'UI_Users_Login' AND [object_id] = OBJECT_ID('[dbo].[users]'))
BEGIN
	CREATE UNIQUE INDEX UI_Users_Login
	ON [dbo].[users] ([login])
END

GO

COMMIT TRAN