USE [KMT] 
GO 
CREATE TABLE [dbo].[KMTPostFlag](
[Id] [int] IDENTITY(1,1) NOT NULL,
[Postld] [int] NOT NULL, 
[Status] [int] NOT NULL,
[CreatedBy] [nvarchar](50) NOT NULL, 
[CreatedOn] [datetime] NOT NULL, 
[ModifiedBy] [nvarchar](50) NULL, 
[ModifiedOn] [datetime] NULL, 
[Capld] [int] NULL)
GO