USE [KMT] 
GO 
CREATE TABLE [dbo].[KMTSystemLink]( 
[Id] [int] IDENTITY(1,1) NOT NULL, 
[Title][nvarchar](50) NOT NULL, 
[Url] [nvarchar](250) NOT NULL, 
[IsActive] [bit] NOT NULL, 
[CreatedBy] [nvarchar](50) NOT NULL, 
[CreatedOn] [datetime] NOT NULL, 
[ModifiedBy] [nvarchar](50) NULL, 
[ModifiedOn] [datetime] NULL, 
[Capld] [int] NULL)