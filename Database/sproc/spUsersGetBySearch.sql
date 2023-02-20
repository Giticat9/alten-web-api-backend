BEGIN TRAN

IF EXISTS (SELECT * FROM [sys].[objects] WHERE [type] = 'P' AND [name] = 'spUsersGetBySearch')
BEGIN
    DROP PROCEDURE [dbo].[spUsersGetBySearch]
END

GO

CREATE PROCEDURE [dbo].[spUsersGetBySearch]
(
    @id INT NULL,
    @guid UNIQUEIDENTIFIER NULL,
    @email VARCHAR(255) NULL,
    @login VARCHAR(255) NUll
)
AS
BEGIN

    IF @id = NULL AND @guid = NULL AND @email = NULL AND @login = NULL
        BEGIN
            RAISERROR('Необходимо указать хотя бы один параметр: guid, идентификатор, эл. почта или логин', 16, 1)
        END
    ELSE
    BEGIN
        SELECT u.[id]
            ,u.[guid]
            ,u.[lastname]
            ,u.[firstname]
            ,u.[middlename]
            ,u.[email]
            ,u.[login]
        FROM [dbo].[users] u
        WHERE u.[id] = @id
            OR u.[guid] = @guid
            OR u.[email] = @email
            OR u.[login] = @login
    END

END
GO

COMMIT TRAN