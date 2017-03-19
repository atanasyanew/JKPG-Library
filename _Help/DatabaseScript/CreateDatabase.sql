use master;
go

/* Exchange DBxxxx with your database name, 4 places*/ 
IF (EXISTS (SELECT * from sysdatabases WHERE name = 'DBBOOK'))
BEGIN
	DROP DATABASE DBBOOK
END
GO

IF (NOT EXISTS (SELECT * from sysdatabases WHERE name = 'DBBOOK'))
BEGIN
	CREATE DATABASE DBBOOK
END
