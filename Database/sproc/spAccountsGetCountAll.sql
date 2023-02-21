BEGIN TRAN

GO

IF OBJECT_ID('[dbo].[spAccountsGetCountAll]') IS NOT NULL
	DROP PROCEDURE [dbo].[spAccountsGetCountAll]
GO

CREATE PROCEDURE [dbo].[spAccountsGetCountAll]
AS
BEGIN
	SELECT COUNT(*) AS count_all
	FROM [dbo].[users]
END

GO

COMMIT TRAN