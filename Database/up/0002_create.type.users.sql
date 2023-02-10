BEGIN TRAN

IF OBJECT_ID('[dbo].[spUsersAddOrUpdate]') IS NOT NULL
    DROP PROCEDURE [dbo].[spUsersAddOrUpdate];
GO

IF TYPE_ID('[dbo].[UserModelType]') IS NOT NULL
    DROP TYPE [dbo].[UserModelType]
GO

CREATE TYPE [dbo].[UserModelType] AS TABLE
(
    [lastname] VARCHAR(255) NOT NULL,
    [firstname] VARCHAR(255) NOT NULL,
    [middlename] VARCHAR(255) NULL,
    [email] VARCHAR(255) NULL,
    [login] VARCHAR(255) NOT NULL,
    [password] VARCHAR(255) NOT NULL
)

COMMIT TRAN