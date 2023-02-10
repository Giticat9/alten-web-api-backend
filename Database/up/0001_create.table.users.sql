IF NOT EXISTS (SELECT * FROM [INFORMATION_SCHEMA].[TABLES] WHERE [TABLE_NAME] = 'users')
BEGIN
    CREATE TABLE [dbo].[users] (
        [id] INT PRIMARY KEY IDENTITY NOT NULL,
        [guid] UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID(),
        [lastname] VARCHAR(255) NOT NULL,
        [firstname] VARCHAR(255) NOT NULL,
        [middlename] VARCHAR(255) NULL,
        [email] VARCHAR(255) NULL,
        [login] VARCHAR(255) NOT NULL,
        [password] VARCHAR(255) NOT NULL,
        [created_at] DATETIME NOT NULL DEFAULT GETDATE(),
    );

    CREATE NONCLUSTERED INDEX IX_Users_Id
        ON [dbo].[users] ([id]);

    CREATE NONCLUSTERED INDEX IX_Users_Guid
        ON [dbo].[users] ([guid])
END;