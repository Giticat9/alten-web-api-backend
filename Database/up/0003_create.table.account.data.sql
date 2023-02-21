BEGIN TRAN

GO

IF NOT EXISTS (SELECT * FROM [INFORMATION_SCHEMA].[TABLES] WHERE [TABLE_NAME] = 'account_data')
BEGIN
	CREATE TABLE [dbo].[account_data]
	(
		[id] BIGINT PRIMARY KEY IDENTITY(1,1),
		[guid] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
		[first_name] VARCHAR(150) NOT NULL,
		[last_name] VARCHAR(150) NOT NULL,
		[middle_name] VARCHAR(150) NULL,
		[position] VARCHAR(255) NULL,
		[email] VARCHAR(100) NULL,
		[permission] INT NOT NULL,
		[user_external_guid] UNIQUEIDENTIFIER NOT NULL,
		[created_at] DATETIME2 DEFAULT(SYSDATETIME())
	)
END

GO

IF NOT EXISTS (SELECT * FROM [sys].[indexes] WHERE [name] = 'IX_Account_Data_Guid' AND [object_id] = OBJECT_ID('[dbo].[account_data]'))
BEGIN
	CREATE NONCLUSTERED INDEX IX_Account_Data_Guid
	ON [dbo].[account_data] ([guid])
	INCLUDE ([first_name], [last_name], [user_external_guid])
END

GO

COMMIT TRAN