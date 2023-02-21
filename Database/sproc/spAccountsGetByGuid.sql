BEGIN TRAN

GO

IF EXISTS (SELECT * FROM [sys].[objects] o WHERE o.[type] = 'p' AND o.[name] = 'spAccountsGetByGuid')
	DROP PROCEDURE [dbo].[spAccountsGetByGuid]
GO

CREATE PROCEDURE [dbo].[spAccountsGetByGuid]
(
	@guid UNIQUEIDENTIFIER
)
AS
BEGIN
	SELECT ad.[guid]
		,u.[login]
		,ad.[first_name]
		,ad.[last_name]
		,ad.[middle_name]
		,ad.[position]
		,ad.[email]
		,ad.[permission]
		,ad.[created_at]
	FROM [dbo].[account_data] ad
	LEFT JOIN [dbo].[users] u ON u.[external_guid] = ad.[user_external_guid]
	WHERE ad.[guid] = @guid

	SELECT DISTINCT 
		s.[guid] AS section_guid
		,s.[description]
	FROM [dbo].[sections] s
	LEFT JOIN [dbo].[sections_access_right] sar ON sar.[account_guid] = @guid
END

GO

COMMIT TRAN