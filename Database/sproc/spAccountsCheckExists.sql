BEGIN TRAN

GO

IF OBJECT_ID('[dbo].[spAccountsCheckExists]') IS NOT NULL
	DROP PROCEDURE [dbo].[spAccountsCheckExists]
GO

CREATE PROCEDURE [dbo].[spAccountsCheckExists]
(
	@is_existed_account_check BIT = 0,
	@account_guid UNIQUEIDENTIFIER,
	@login VARCHAR(255),
	@email VARCHAR(100)
)
AS
BEGIN

	IF @is_existed_account_check = 1 AND @account_guid IS NOT NULL
	BEGIN
		SELECT TOP 1 1
		FROM [dbo].[account_data] ad
		WHERE ad.[guid] != @account_guid
			AND ad.[email] = @email

		SELECT
			CASE @@ROWCOUNT
				WHEN 0 THEN CAST(0 AS BIT)
				WHEN 1 THEN CAST(1 AS BIT)
			END
	END
	ELSE
	BEGIN
		SELECT TOP 1 1
		FROM [dbo].[users] u
		LEFT JOIN [dbo].[account_data] ad ON ad.[user_external_guid] = u.[external_guid]
		WHERE u.[login] = @login
			OR ad.[email] = @email

		SELECT 
			CASE @@ROWCOUNT
				WHEN 0 THEN CAST(0 AS BIT)
				WHEN 1 THEN CAST(1 AS BIT)
			END
	END
END

GO

COMMIT TRAN