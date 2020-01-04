USE [KMT]
GO
CREATE TABLE [dbo].[KMTLob]( 
[Id] [int] IDENTITY(1,1) NOT NULL,
[Capld] [int] NOT NULL,
[Name] [nvarchar](50) NOT NULL,
[Identifier] [nvarchar](50) NOT NULL,
[IsActive] [bit] NOT NULL,
[CreatedBy] [nvarchar](50) NOT NULL,
[CreatedOn] [datetime] NOT NULL, 
[ModifiedBy] [nvarchar](50) NULL,
[ModifiedOn] [datetime] NULL)