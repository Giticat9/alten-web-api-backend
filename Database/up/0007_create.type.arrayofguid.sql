﻿BEGIN TRAN

GO

IF TYPE_ID('[dbo].[ArrayOfGuid]') IS NULL
BEGIN
	CREATE TYPE [dbo].[ArrayOfGuid] AS TABLE
	(
		[value] UNIQUEIDENTIFIER
	)
END

GO

COMMIT TRAN