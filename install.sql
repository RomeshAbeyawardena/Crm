USE master

CREATE LOGIN [AppUser]
WITH 
	PASSWORD = 'n4Yg8BeW5aGY2bPn', 
	CHECK_EXPIRATION = OFF,
	CHECK_POLICY = OFF
GO

ALTER DATABASE Crm
SET SINGLE_USER WITH ROLLBACK IMMEDIATE

DROP DATABASE Crm
GO
CREATE DATABASE Crm
GO
USE Crm

CREATE TABLE [dbo].[Category] (
	[Id] INT NOT NULL IDENTITY(1,1)
		CONSTRAINT PK_Category PRIMARY KEY
	,[Name] VARCHAR(200) NOT NULL
		CONSTRAINT IQ_Category UNIQUE
	,[Created] DATETIMEOFFSET NOT NULL
	,[Modified] DATETIMEOFFSET NOT NULL
    ,INDEX IX_Category_Name NONCLUSTERED ([Name])
)

CREATE TABLE [dbo].[Preference] (
	[Id] INT NOT NULL IDENTITY(1,1)
		CONSTRAINT PK_Preference PRIMARY KEY
	,[CategoryId] INT NULL
		CONSTRAINT FK_Preference_Category
		REFERENCES [dbo].[Category]
	,[Name] VARCHAR(200) NOT NULL
	,[Created] DATETIMEOFFSET NOT NULL
	,[Modified] DATETIMEOFFSET NOT NULL
	,CONSTRAINT IQ_Preference_Category
		UNIQUE ([CategoryId], [Name])
	,INDEX IX_Preference_Name NONCLUSTERED ([CategoryId],[Name])
)

CREATE TABLE [dbo].[Customer](
	[Id] INT NOT NULL IDENTITY(1,1)
		CONSTRAINT PK_Customer PRIMARY KEY
	,[EmailAddress] VARBINARY(MAX) NOT NULL
	,[FirstName] VARBINARY(MAX) NOT NULL
	,[MiddleName] VARBINARY(MAX) NULL
	,[LastName] VARBINARY(MAX) NOT NULL
	,[Active] BIT NOT NULL
	,[Password] VARBINARY(MAX) NOT NULL
	,[Created] DATETIMEOFFSET NOT NULL
	,[Modified] DATETIMEOFFSET NOT NULL
	,[LastIndexed] DATETIMEOFFSET NULL
)

CREATE TABLE [dbo].[CustomerPreference] (
	[Id] INT NOT NULL IDENTITY(1,1)
		CONSTRAINT PK_CustomerPreference PRIMARY KEY
	,[PreferenceId] INT NOT NULL
		CONSTRAINT FK_CustomerPreference_Preference
		REFERENCES [dbo].[Preference]
	,[CustomerId] INT NOT NULL
		CONSTRAINT FK_CustomerPreference_Customer
		REFERENCES [dbo].[Customer]
	,[OptInDate] DATETIMEOFFSET NULL
	,[NextCheckInDate] DATETIMEOFFSET NOT NULL
	,[Created] DATETIMEOFFSET NOT NULL
	,[Modified] DATETIMEOFFSET NULL
)

CREATE TABLE [dbo].[Attribute] (
	[Id] INT NOT NULL IDENTITY(1,1)
		CONSTRAINT PK_Attribute PRIMARY KEY 
	,[Key] VARCHAR(200) NOT NULL
		CONSTRAINT IQ_Attribute UNIQUE
	,[Created] DATETIMEOFFSET NOT NULL
	,[Modified] DATETIMEOFFSET NOT NULL
	,INDEX IDX_Attribute NONCLUSTERED ([Key])
)

CREATE TABLE [dbo].[CustomerAttribute] (
	[Id] INT NOT NULL IDENTITY(1,1)
		CONSTRAINT PK_CustomerAttribute PRIMARY KEY 
	,[AttributeId] INT NOT NULL
		CONSTRAINT FK_CustomerAttribute_Attribute
		REFERENCES [dbo].[Attribute]
	,[CustomerId] INT NOT NULL
		CONSTRAINT FK_CustomerAttribute_Customer
		REFERENCES [dbo].[Customer]
	,[Value] VARBINARY(MAX) NOT NULL
	,[Created] DATETIMEOFFSET NOT NULL
	,[Modified] DATETIMEOFFSET NOT NULL
	,CONSTRAINT IQ_CustomerAttribute_Attribute_Customer
		UNIQUE ([AttributeId], [CustomerId])
	,INDEX IDX_CustomerAttribute
		NONCLUSTERED (AttributeId, CustomerId)
)
CREATE TABLE [dbo].[CustomerHash](
	[Id] INT NOT NULL IDENTITY(1,1)
		CONSTRAINT PK_CustomerHash PRIMARY KEY
	,[Hash] BINARY(6) NOT NULL
	,[Index] INT NOT NULL
	,[CustomerId] INT NOT NULL
		CONSTRAINT FK_CustomerHash_Customer
		REFERENCES [dbo].[Customer]
)

CREATE TYPE [dbo].[Hash] AS TABLE
([Value] VARBINARY(MAX) NOT NULL, [Index] INT NOT NULL)
GO

CREATE PROC [dbo].[ContainsHash]
	@hashes [dbo].[Hash] readonly
AS BEGIN
	SELECT DISTINCT([Customer].[Id]), [dbo].[Customer].[EmailAddress],
			[dbo].[Customer].[FirstName], [dbo].[Customer].[MiddleName],
			[dbo].[Customer].[LastName], [dbo].[Customer].[Password],
			[dbo].[Customer].[Active], [dbo].[Customer].[Created],
			[dbo].[Customer].[Modified],[dbo].[Customer].[LastIndexed] 
		FROM @hashes h
		INNER JOIN [dbo].[CustomerHash]
		ON [dbo].[CustomerHash].[Hash] = h.[Value]
		INNER JOIN [dbo].[Customer]
		ON [Customer].[Id] = [CustomerHash].[CustomerId]
		WHERE [h].[Index] = [dbo].[CustomerHash].[Index]
END
GO

CREATE USER [AppUser]
FOR LOGIN [AppUser]

ALTER ROLE db_datareader
ADD MEMBER AppUser

ALTER ROLE db_datawriter
ADD MEMBER AppUser

GRANT EXECUTE ON [dbo].[ContainsHash]
TO [AppUser]

GRANT EXECUTE ON TYPE::[dbo].[Hash]
TO [AppUser]
GO

USE [master]

ALTER DATABASE [Hangfire]
SET SINGLE_USER WITH ROLLBACK IMMEDIATE

DROP DATABASE [Hangfire]

CREATE DATABASE [Hangfire]
GO

USE Hangfire



CREATE USER [AppUser]
FOR LOGIN [AppUser]

ALTER ROLE db_datareader
ADD MEMBER AppUser

ALTER ROLE db_datawriter
ADD MEMBER AppUser

ALTER ROLE db_owner
ADD MEMBER AppUser
	