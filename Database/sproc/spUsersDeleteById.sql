BEGIN TRAN

IF EXISTS (SELECT * FROM [sys].[objects] WHERE [type] = 'P' AND [name] = 'spUsersDeleteById')
BEGIN
    DROP PROCEDURE [dbo].[spUsersDeleteById]
END

GO

CREATE PROCEDURE [dbo].[spUsersDeleteById]
(
    @id INT
)
AS
BEGIN

    IF @id = null
    BEGIN
        RAISERROR('Параметр @id не может быть пустым', 16, 1)
    END
    ELSE IF NOT EXISTS (SELECT * FROM [dbo].[users] WHERE [id] = @id)
    BEGIN
        DECLARE @ErrorMessage VARCHAR = CONCAT('Пользователь с id = ', @id, ' отсутствует');
        RAISERROR(@ErrorMessage, 16, 1)
    END
    ELSE
    BEGIN
        DELETE [dbo].[users] WHERE [id] = @id;
    END
    
END

GO

COMMIT TRAN