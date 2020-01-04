USE [KMT]
GO 
CREATE TABLE [dbo].[kmtManageBulletinBoard](
[Id] [int] IDENTITY(1,1) NOT NULL,
[Name] [varchar](50) NOT NULL,
[Title] [varchar](50) NOT NULL,
[Description][varchar](100) NOT NULL, 
[Article] [varchar](max) NOT NULL, 
[type] [varchar](50) NOT NULL, 
[IsActive][bit] NOT NULL, 
[CreatedBy][nvarchar](50) NOT NULL, 
[CreatedOn] [datetime] NOT NULL, 
[ModifiedBy][nvarchar](50) NULL,
[ModifiedOn][datetime] NULL, 
[Capld][int] NULL)