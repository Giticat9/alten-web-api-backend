BEGIN TRAN

GO

IF OBJECT_ID('[dbo].[spAccountsCheckExists]') IS NOT NULL
	DROP PROCEDURE [dbo].[spAccountsCheckExists]
GO

CREATE PROCEDURE [dbo].[spAccountsCheckExists]
(
	@login VARCHAR(255) NULL,
	@email VARCHAR(100) NULL
)
AS
BEGIN

	SELECT ad.[guid]
	INTO #temp_accounts
	FROM [dbo].[account_data] ad
	LEFT JOIN [dbo].[users] u 
		ON u.[external_guid] = ad.[user_external_guid]
	WHERE u.[login] = @login
		OR ad.[email] = @email

	DECLARE @count BIGINT
	SELECT @count = COUNT(*) FROM #temp_accounts

	SELECT 
		CASE
			WHEN @count > 1 THEN CAST(1 AS BIT)
			WHEN @count = 0 THEN CAST(0 AS BIT)
			ELSE CAST(0 AS BIT)
		END
	
END

GO

COMMIT TRAN