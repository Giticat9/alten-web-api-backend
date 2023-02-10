BEGIN TRAN
GO

IF OBJECT_ID('[dbo].[spUsersAddOrUpdate]') IS NOT NULL
    DROP PROCEDURE [dbo].[spUsersAddOrUpdate]
GO

CREATE PROCEDURE [dbo].[spUsersAddOrUpdate]
(
    @id INT = NULL,
    @user [dbo].[UserModelType] READONLY
)
AS
BEGIN

    MERGE [dbo].[users] base
    USING @user source
        ON (@id IS NULL OR base.[id] = @id)
    WHEN MATCHED THEN
        UPDATE
            SET [lastname] = source.[lastname]
                ,[firstname] = source.[firstname]
                ,[middlename] = source.[middlename]
                ,[email] = source.[email]
                ,[login] = source.[login]
                ,[password] = source.[password]
    WHEN NOT MATCHED THEN
        INSERT (
            [lastname]
            ,[firstname]
            ,[middlename]
            ,[email]
            ,[login]
            ,[password]
        )
        VALUES (
            source.[lastname]
            ,source.[firstname]
            ,source.[middlename]
            ,source.[email]
            ,source.[login]
            ,source.[password]
        );

    IF @id = NULL
    BEGIN
        SELECT [guid] 
        FROM SCOPE_IDENTITY();
    END
    ELSE
    BEGIN
        SELECT [guid] 
        FROM [dbo].[users]
        WHERE [id] = @id; 
    END
END

GO

COMMIT TRAN