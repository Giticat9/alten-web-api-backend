BEGIN TRAN

GO

IF OBJECT_ID('[dbo].[spAccountAddOrUpdate]') IS NOT NULL
	DROP PROCEDURE [dbo].[spAccountAddOrUpdate]
GO

CREATE PROCEDURE [dbo].[spAccountAddOrUpdate]
(
	@account_data [dbo].[AccountModelType] READONLY,
	@account_sections_access [dbo].[ArrayOfGuid] READONLY
)
AS
BEGIN
	DECLARE @user_external_guid UNIQUEIDENTIFIER = NULL
	SELECT @user_external_guid = u.[external_guid]
	FROM @account_data model
	LEFT JOIN [dbo].[account_data] ad ON ad.[guid] = model.[guid]
	LEFT JOIN [dbo].[users] u ON u.[external_guid] = ad.[user_external_guid]
	WHERE model.[guid] IS NOT NULL

	MERGE [dbo].[users] u_target
	USING @account_data ad_source
	ON u_target.[external_guid] = @user_external_guid
	WHEN MATCHED THEN
		UPDATE SET [login] = ad_source.[login]
	WHEN NOT MATCHED THEN
		INSERT ([login]
			,[password]
			,[is_default])
		VALUES (ad_source.[login]
			,HASHBYTES('sha1', CAST(ad_source.[password] AS VARBINARY(20)))
			,0);
	
	IF @user_external_guid IS NULL
	BEGIN
		SELECT @user_external_guid = u.[external_guid] 
		FROM [dbo].[users] u
		WHERE u.id = @@IDENTITY
	END

	MERGE [dbo].[account_data] ad_target
	USING @account_data ad_source
	ON ad_target.[guid] = ad_source.[guid]
	WHEN MATCHED THEN
		UPDATE SET [first_name] = ad_source.[first_name]
			,[last_name] = ad_source.[last_name]
			,[middle_name] = ad_source.[middle_name]
			,[position] = ad_source.[position]
			,[email] = ad_source.[email]
			,[permission] = ad_source.[permission]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT ([first_name]
			,[last_name]
			,[middle_name]
			,[position]
			,[email]
			,[permission]
			,[user_external_guid])
		VALUES (ad_source.[first_name]
			,ad_source.[last_name]
			,ad_source.[middle_name]
			,ad_source.[position]
			,ad_source.[email]
			,ad_source.[permission]
			,@user_external_guid);

	DECLARE @account_guid UNIQUEIDENTIFIER;
	SELECT TOP 1 @account_guid = [guid] FROM @account_data

	IF @account_guid IS NOT NULL
	BEGIN
		DELETE FROM [dbo].[sections_access_right] WHERE [account_guid] = @account_guid;

		IF EXISTS (SELECT 1 FROM @account_sections_access)
		BEGIN
			INSERT INTO [dbo].[sections_access_right] ([account_guid], [section_guid])
			SELECT @account_guid
				,asa.[value]
			FROM @account_sections_access asa
		END
	END
	ELSE
	BEGIN
		SELECT @account_guid = [guid] 
		FROM [dbo].[account_data] ad 
		WHERE ad.[id] = @@IDENTITY

		IF EXISTS (SELECT 1 FROM @account_sections_access)
		BEGIN
			INSERT INTO [dbo].[sections_access_right] ([account_guid], [section_guid])
			SELECT @account_guid
				,asa.[value]
			FROM @account_sections_access asa
		END
	END
END

GO

COMMIT TRAN