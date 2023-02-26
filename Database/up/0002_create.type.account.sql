BEGIN TRAN

IF OBJECT_ID('[dbo].[spAccountAddOrUpdate]') IS NOT NULL
    DROP PROCEDURE [dbo].[spAccountAddOrUpdate];
GO

IF TYPE_ID('[dbo].[AccountModelType]') IS NOT NULL
    DROP TYPE [dbo].[AccountModelType]
GO

CREATE TYPE [dbo].[AccountModelType] AS TABLE
(
    [guid] UNIQUEIDENTIFIER NULL,
	[login] VARCHAR(255) NOT NULL,
	[password] VARCHAR(150) NULL,
	[first_name] VARCHAR(150) NOT NULL,
	[last_name] VARCHAR(150) NOT NULL,
	[middle_name] VARCHAR(150) NULL,
	[position] VARCHAR(255) NULL,
	[email] VARCHAR(100) NULL,
	[permission] BIGINT NOT NULL
)

COMMIT TRAN