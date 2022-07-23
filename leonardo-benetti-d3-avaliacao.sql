CREATE DATABASE [leonardo-benetti-d3-avaliacao];

USE [leonardo-benetti-d3-avaliacao];

CREATE TABLE Users (
	IdUser		INT IDENTITY(1,1) PRIMARY KEY,
	[Name]		VARCHAR(255) NOT NULL,
	[Email]		VARCHAR(255) NOT NULL,
	[Password]	VARCHAR(255) NOT NULL
);

SELECT * FROM Users;

INSERT INTO Users ([Name], Email, [Password])
VALUES ('admin', 'admin@email.com', 'admin123');