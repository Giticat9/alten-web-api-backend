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
	DECLARE @user_external_guid UNIQUEIDENTIFIER;

	SELECT @user_external_guid = ad.[user_external_guid]
	FROM [dbo].[account_data] ad
	WHERE ad.[guid] = @guid

	DELETE FROM [dbo].[account_data] WHERE [guid] = @guid;
	DELETE FROM [dbo].[users] WHERE [external_guid] = @user_external_guid;
END

GO

COMMIT TRAN