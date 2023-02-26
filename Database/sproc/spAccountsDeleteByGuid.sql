BEGIN TRAN

GO

IF OBJECT_ID('[dbo].[spAccountsDeleteByGuid]') IS NOT NULL
	DROP PROCEDURE [dbo].[spAccountsDeleteByGuid]
GO

CREATE PROCEDURE [dbo].[spAccountsDeleteByGuid]
(
	@guid UNIQUEIDENTIFIER
)
AS
BEGIN
	DECLARE @account_guid UNIQUEIDENTIFIER
	DECLARE @user_external_guid UNIQUEIDENTIFIER

	SELECT @account_guid = ad.[guid]
		,@user_external_guid = ad.[user_external_guid]
	FROM [dbo].[account_data] ad
	WHERE ad.[guid] = @guid

	-- удаление информации об учетной записи
	DELETE FROM [dbo].[account_data] WHERE [guid] = @guid;
	-- удаление учетной записи
	DELETE FROM [dbo].[users] WHERE [external_guid] = @user_external_guid;
	-- удаление прав доступа для учетной записи
	DELETE FROM [dbo].[sections_access_right] WHERE [account_guid] = @account_guid;
END

GO

COMMIT TRAN