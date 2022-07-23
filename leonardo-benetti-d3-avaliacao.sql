CREATE DATABASE [leonardo-benetti-d3-avaliacao];

USE [leonardo-benetti-d3-avaliacao];

CREATE TABLE Users (
	IdUser		INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name]		VARCHAR(255) NOT NULL,
	[Email]		VARCHAR(255) NOT NULL,
	[Password]	BINARY(64) NOT NULL
);

SELECT * FROM Users;

INSERT INTO Users ([Name], Email, [Password])
VALUES ('admin', 'admin@email.com', HASHBYTES('SHA2_512', 'admin123'));

SELECT IdUser, Name, Email, Password FROM Users WHERE Email LIKE 'admin@email.com';