BEGIN TRAN

GO

SELECT * FROM [dbo].[users] 
IF @@ROWCOUNT = 0
BEGIN
	--	Добавление super пользователя
	INSERT INTO [dbo].[users] ([login], [password], [is_default])
	VALUES (
		'admin',
		HASHBYTES('sha1', CAST('admin' AS VARBINARY(20))),
		1
	)

	DECLARE @user_external_guid UNIQUEIDENTIFIER;
	SELECT @user_external_guid = u.[external_guid] 
	FROM [dbo].[users] u 
	WHERE u.[id] = @@IDENTITY

	--	Добавление информации об super пользователе
	INSERT INTO [dbo].[account_data] ([first_name], [last_name], [permission], [user_external_guid])
	VALUES (
		'admin',
		'admin',
		1,
		@user_external_guid
	)

	DECLARE @account_guid UNIQUEIDENTIFIER
	SELECT @account_guid = ad.[guid]
	FROM [dbo].[account_data] ad 
	WHERE ad.[id] = @@IDENTITY

	--	Добавление плав доступа для super для пользователя
	INSERT INTO [dbo].[sections_access_right] ([account_guid], [section_guid])
	SELECT @account_guid,
		s.[guid]
	FROM [dbo].[sections] s
END

GO

COMMIT TRAN