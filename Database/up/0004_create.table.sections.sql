IF NOT EXISTS (SELECT * FROM [INFORMATION_SCHEMA].[TABLES] WHERE [TABLE_NAME] = 'sections')
BEGIN
    CREATE TABLE [dbo].[sections] (
        [id] INT PRIMARY KEY IDENTITY NOT NULL,
        [guid] UNIQUEIDENTIFIER DEFAULT NEWSEQUENTIALID(),
        [name] VARCHAR(255) NOT NULL,
        [description] VARCHAR(255) NOT NULL,
        [created_at] DATETIME NOT NULL DEFAULT GETDATE()
    );

    CREATE NONCLUSTERED INDEX IX_Sections_id
        ON [dbo].[sections] ([id])

    CREATE NONCLUSTERED INDEX IX_Sections_guid
        ON [dbo].[sections] ([guid])
END

IF NOT EXISTS (SELECT * FROM [dbo].[sections] s WHERE s.[name] = 'main')
BEGIN
    INSERT INTO [dbo].[sections] ([name], [description]) 
    VALUES ('main', 'Главная страница') 
END

IF NOT EXISTS (SELECT * FROM [dbo].[sections] s WHERE s.[name] = 'history')
BEGIN
    INSERT INTO [dbo].[sections] ([name], [description]) 
    VALUES ('history', 'Страница истории') 
END

IF NOT EXISTS (SELECT * FROM [dbo].[sections] s WHERE s.[name] = 'activity')
BEGIN
    INSERT INTO [dbo].[sections] ([name], [description]) 
    VALUES ('activity', 'Страница деятельности') 
END

IF NOT EXISTS (SELECT * FROM [dbo].[sections] s WHERE s.[name] = 'products')
BEGIN
    INSERT INTO [dbo].[sections] ([name], [description]) 
    VALUES ('products', 'Страница продукции') 
END

IF NOT EXISTS (SELECT * FROM [dbo].[sections] s WHERE s.[name] = 'primary_current_sources')
BEGIN
    INSERT INTO [dbo].[sections] ([name], [description]) 
    VALUES ('primary_current_sources', 'Страница первичных источников тока') 
END

IF NOT EXISTS (SELECT * FROM [dbo].[sections] s WHERE s.[name] = 'lithium-ion_batteries')
BEGIN
    INSERT INTO [dbo].[sections] ([name], [description]) 
    VALUES ('lithium-ion_batteries', 'Страница литий-ионных батарей') 
END

IF NOT EXISTS (SELECT * FROM [dbo].[sections] s WHERE s.[name] = 'chargers_and_discharge_devices')
BEGIN
    INSERT INTO [dbo].[sections] ([name], [description]) 
    VALUES ('chargers_and_discharge_devices', 'Страница зарядно-разрядных устройств') 
END

IF NOT EXISTS (SELECT * FROM [dbo].[sections] s WHERE s.[name] = 'chargers_and_discharge_devices')
BEGIN
    INSERT INTO [dbo].[sections] ([name], [description]) 
    VALUES ('chargers_and_discharge_devices', 'Страница зарядно-разрядных устройств') 
END

IF NOT EXISTS (SELECT * FROM [dbo].[sections] s WHERE s.[name] = 'company')
BEGIN
    INSERT INTO [dbo].[sections] ([name], [description]) 
    VALUES ('company', 'Страница предприятия') 
END

IF NOT EXISTS (SELECT * FROM [dbo].[sections] s WHERE s.[name] = 'leadership')
BEGIN
    INSERT INTO [dbo].[sections] ([name], [description]) 
    VALUES ('leadership', 'Страница руководства') 
END

IF NOT EXISTS (SELECT * FROM [dbo].[sections] s WHERE s.[name] = 'licenses')
BEGIN
    INSERT INTO [dbo].[sections] ([name], [description]) 
    VALUES ('licenses', 'Страница лицензий') 
END

IF NOT EXISTS (SELECT * FROM [dbo].[sections] s WHERE s.[name] = 'documents')
BEGIN
    INSERT INTO [dbo].[sections] ([name], [description]) 
    VALUES ('documents', 'Страница документов')
END

IF NOT EXISTS (SELECT * FROM [dbo].[sections] s WHERE s.[name] = 'gallery')
BEGIN
    INSERT INTO [dbo].[sections] ([name], [description]) 
    VALUES ('gallery', 'Страница галереи') 
END

IF NOT EXISTS (SELECT * FROM [dbo].[sections] s WHERE s.[name] = 'vacancies')
BEGIN
    INSERT INTO [dbo].[sections] ([name], [description]) 
    VALUES ('vacancies', 'Страница вакансий') 
END

IF NOT EXISTS (SELECT * FROM [dbo].[sections] s WHERE s.[name] = 'news')
BEGIN
    INSERT INTO [dbo].[sections] ([name], [description]) 
    VALUES ('news', 'Страница новостей') 
END