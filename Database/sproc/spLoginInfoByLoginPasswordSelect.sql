BEGIN TRAN

GO

IF OBJECT_ID('[dbo].[spLoginInfoByLoginPasswordSelect]') IS NOT NULL
	DROP PROCEDURE [dbo].[spLoginInfoByLoginPasswordSelect];
GO

CREATE PROCEDURE [dbo].[spLoginInfoByLoginPasswordSelect]
(
	@login VARCHAR(255),
	@password VARCHAR(64)
)
AS
BEGIN
	SELECT LTRIM(RTRIM(CONCAT(ad.[last_name], ' ', ad.[first_name], ' ', ad.[middle_name]))) AS [full_name]
		,u.[external_guid]
		,ad.[email]
		,u.[login]
	FROM [dbo].[account_data] ad
	LEFT JOIN [dbo].[users] u ON u.[external_guid] = ad.[user_external_guid]
	WHERE u.[login] = @login
		AND u.[password] = HASHBYTES('sha1', CAST(@password AS VARBINARY(20)))
END

GO

COMMIT TRAN