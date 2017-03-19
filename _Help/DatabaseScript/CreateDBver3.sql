
/* Exchange DBxxxx with the name of your database! 1 place */
 
USE DBBOOK
GO


/**************************************************************************************/
/****** Remove tables *****************************************************************/
/****** Object:  Table [dbo].[BOOK_AUTHOR]    Script Date: 2014-08-25 15:25:54 ******/
IF (EXISTS (SELECT * from sysobjects WHERE name = 'FK_BOOK_AUTHOR_BOOK'))
BEGIN
	ALTER TABLE [dbo].[BOOK_AUTHOR] DROP CONSTRAINT [FK_BOOK_AUTHOR_BOOK]
END
GO

IF (EXISTS (SELECT * from sysobjects WHERE name = 'FK_BOOK_AUTHOR_AUTHOR'))
BEGIN
	ALTER TABLE [dbo].[BOOK_AUTHOR] DROP CONSTRAINT [FK_BOOK_AUTHOR_AUTHOR]
END
GO

IF (EXISTS (SELECT * from sysobjects WHERE name = 'BOOK_AUTHOR'))
BEGIN
	DROP TABLE [dbo].[BOOK_AUTHOR]
END
GO

/****** Object:  Table [dbo].[AUTHOR]    Script Date: 2014-08-25 15:27:41 ******/
IF (EXISTS (SELECT * from sysobjects WHERE name = 'AUTHOR'))
BEGIN
	DROP TABLE [dbo].[AUTHOR]
END
GO

/****** Object:  Table [dbo].[BOOK]    Script Date: 2014-08-25 15:28:14 ******/
IF (EXISTS (SELECT * from sysobjects WHERE name = 'FK_BOOK_CLASSIFICATION'))
BEGIN
	ALTER TABLE [dbo].[BOOK] DROP CONSTRAINT [FK_BOOK_CLASSIFICATION]
END
GO

IF (EXISTS (SELECT * from sysobjects WHERE name = 'BOOK'))
BEGIN
	DROP TABLE [dbo].[BOOK]
END
GO

/****** Object:  Table [dbo].[CLASSIFICATION]    Script Date: 2014-08-25 15:28:58 ******/
IF (EXISTS (SELECT * from sysobjects WHERE name = 'CLASSIFICATION'))
BEGIN
	DROP TABLE [dbo].[CLASSIFICATION]
END
GO



/**************************************************************************************/
/****** Create tables *****************************************************************/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[CLASSIFICATION]    Script Date: 2014-08-25 15:28:58 ******/
CREATE TABLE [dbo].[CLASSIFICATION](
	[SignId] [int] NOT NULL,
	[Signum] [nvarchar](50) NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [PK_CLASSIFICATION] PRIMARY KEY CLUSTERED 
(
	[SignId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[BOOK]    Script Date: 2014-08-25 15:28:14 ******/
CREATE TABLE [dbo].[BOOK](
	[ISBN] [nvarchar](15) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[SignId] [int] NULL,
	[PublicationYear] [nvarchar](10) NULL,
	[publicationinfo] [nvarchar](255) NULL,
	pages [smallint] NULL,
 CONSTRAINT [PK_BOOK] PRIMARY KEY CLUSTERED 
(
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

IF (EXISTS (SELECT * from sysobjects WHERE name = 'CLASSIFICATION'))
BEGIN
	ALTER TABLE [dbo].[BOOK]  WITH CHECK ADD  CONSTRAINT [FK_BOOK_CLASSIFICATION] FOREIGN KEY([SignId])
	REFERENCES [dbo].[CLASSIFICATION] ([SignId])
END
ELSE
BEGIN
	PRINT 'Could not create foreign key FK_BOOK_CLASSIFICATION due to missing table CLASSIFICATION';
END
GO

IF (EXISTS (SELECT * from sysobjects WHERE name = 'CLASSIFICATION'))
BEGIN
	ALTER TABLE [dbo].[BOOK] CHECK CONSTRAINT [FK_BOOK_CLASSIFICATION]
END
GO

/****** Object:  Table [dbo].[AUTHOR]    Script Date: 2014-08-25 15:27:41 ******/
CREATE TABLE [dbo].[AUTHOR](
	[Aid] [int] IDENTITY(100,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[BirthYear] [nvarchar](10) NULL,
 CONSTRAINT [PK_AUTHOR] PRIMARY KEY CLUSTERED 
(
	[Aid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[BOOK_AUTHOR]    Script Date: 2014-08-25 15:25:54 ******/
CREATE TABLE [dbo].[BOOK_AUTHOR](
	[ISBN] [nvarchar](15) NOT NULL,
	[Aid] [int] NOT NULL,
 CONSTRAINT [PK_BOOK_AUTHOR] PRIMARY KEY CLUSTERED 
(
	[ISBN] ASC,
	[Aid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

IF (EXISTS (SELECT * from sysobjects WHERE name = 'AUTHOR'))
BEGIN
	ALTER TABLE [dbo].[BOOK_AUTHOR]  WITH CHECK ADD  CONSTRAINT [FK_BOOK_AUTHOR_AUTHOR] FOREIGN KEY([Aid])
	REFERENCES [dbo].[AUTHOR] ([Aid])
END
ELSE
BEGIN
	PRINT 'Could not create foreign key FK_BOOK_AUTHOR_AUTHOR due to missing table AUTHOR';
END
GO

IF (EXISTS (SELECT * from sysobjects WHERE name = 'AUTHOR'))
BEGIN
	ALTER TABLE [dbo].[BOOK_AUTHOR] CHECK CONSTRAINT [FK_BOOK_AUTHOR_AUTHOR]
END
GO

IF (EXISTS (SELECT * from sysobjects WHERE name = 'BOOK'))
BEGIN
	ALTER TABLE [dbo].[BOOK_AUTHOR]  WITH CHECK ADD  CONSTRAINT [FK_BOOK_AUTHOR_BOOK] FOREIGN KEY([ISBN])
	REFERENCES [dbo].[BOOK] ([ISBN])
END
ELSE
BEGIN
	PRINT 'Could not create foreign key FK_BOOK_AUTHOR_BOOK due to missing table BOOK';
END
GO

IF (EXISTS (SELECT * from sysobjects WHERE name = 'BOOK'))
BEGIN
	ALTER TABLE [dbo].[BOOK_AUTHOR] CHECK CONSTRAINT [FK_BOOK_AUTHOR_BOOK]
END
GO



