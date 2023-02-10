BEGIN TRAN

IF EXISTS (SELECT * FROM [sys].[objects] WHERE [type] = 'p' AND [name] = 'spSectionsGet')
BEGIN
    DROP PROCEDURE [dbo].[spSectionsGet]
END

GO

CREATE PROCEDURE [dbo].[spSectionsGet]
(
    @id INT = NULL
)
AS
BEGIN
    SELECT s.[id]
        ,s.[guid]
        ,s.[name]
        ,s.[description]
        ,s.[created_at]
    FROM [dbo].[sections] s
    WHERE (@id IS NULL OR s.[id] = @id)
END

GO

COMMIT TRAN;