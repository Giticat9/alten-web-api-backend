BEGIN TRAN

GO

IF EXISTS (SELECT * FROM [sys].[objects] o WHERE o.[type] = 'p' AND o.[name] = 'spAccountsGetAll')
	DROP PROCEDURE [dbo].[spAccountsGetAll]
GO

CREATE PROCEDURE [dbo].[spAccountsGetAll]
(
	@term VARCHAR(255)
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
	INTO #temp_account_data
	FROM [dbo].[account_data] ad
	LEFT JOIN [dbo].[users] u ON u.[external_guid] = ad.[user_external_guid]
	WHERE (
		@term IS NULL
			OR LOWER(LTRIM(RTRIM(CONCAT(ad.[first_name], ' ', ad.[last_name], ' ', ad.[middle_name])))) LIKE '%' + LOWER(@term) + '%'
			OR LOWER(u.[login]) LIKE '%' + LOWER(@term) + '%'
	)

	SELECT tad.[guid]
		,tad.[login]
		,tad.[first_name]
		,tad.[last_name]
		,tad.[middle_name]
		,tad.[position]
		,tad.[email]
		,tad.[permission]
		,tad.[created_at]
	FROM #temp_account_data tad;

	SELECT DISTINCT
		s.[guid] AS section_guid,
		sar.[account_guid] AS account_guid,
		s.[description]
	FROM [dbo].[sections] s
	LEFT JOIN [dbo].[sections_access_right] sar ON sar.[section_guid] = s.[guid]
	LEFT JOIN #temp_account_data tad ON tad.[guid] = sar.[account_guid]
END

GO

COMMIT TRAN