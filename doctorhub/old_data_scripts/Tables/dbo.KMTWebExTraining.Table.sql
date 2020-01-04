USE [KMT]
GO 
CREATE TABLE [dbo].[KMTWebExTraing](
[Id] [int] IDENTITY(1,1) NOT NULL,
[Traingld] [int] NOT NULL, 
[TraingTitle] [nvarchar](50) NOT NULL, 
[CreatedBy] [nvarchar](50) NOT NULL, 
[CreatedOn] [datetime] NOT NULL)
GO