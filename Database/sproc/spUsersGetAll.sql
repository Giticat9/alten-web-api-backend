BEGIN TRAN

IF EXISTS (SELECT * FROM [sys].[objects] WHERE [type] = 'P' AND [name] = 'spUsersGetAll')
BEGIN
    DROP PROCEDURE [dbo].[spUsersGetAll]
END

GO

CREATE PROCEDURE [dbo].[spUsersGetAll]
(
    @search VARCHAR(255) = NULL
)
AS
BEGIN

    SELECT u.[id]
        ,u.[guid]
        ,u.[lastname]
        ,u.[firstname]
        ,u.[middlename]
        ,u.[email]
        ,u.[login]
        ,u.[password]
        ,u.[created_at]
    FROM [dbo].[users] u
    WHERE (@search IS NULL 
        OR CONCAT(u.[lastname], ' ', u.[firstname], ' ', u.[middlename]) LIKE '%' + @search + '%'
        OR u.[email] LIKE '%' + @search + '%'
        OR u.[login] LIKE '%' + @search + '%')
    ORDER BY u.[created_at] DESC
END

GO 

COMMIT TRAN