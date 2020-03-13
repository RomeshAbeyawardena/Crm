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
	,[CustomerId] INT NOT NULL
		CONSTRAINT FK_CustomerHash_Customer
		REFERENCES [dbo].[Customer]
)

CREATE USER [AppUser]
FOR LOGIN [AppUser]

ALTER ROLE db_datareader
ADD MEMBER AppUser

ALTER ROLE db_datawriter
ADD MEMBER AppUser

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
	
SELECT * FROM Crm.dbo.CustomerHash ch
WHERE (SELECT COUNT(*) Id FROM Crm.dbo.CustomerHash ch1 WHERE ch1.Hash = ch.Hash) > 1

SELECT * FROM Crm.dbo.CustomerHash