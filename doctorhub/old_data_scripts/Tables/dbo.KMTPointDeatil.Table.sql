USE [KMT]
GO
CREATE TABLE [dbo].[KMTPointDeatil]( 
[id] [int]  IDENTITY(1,1) NOT NULL,
[InteractionType] [nvarchar](50) NOT NULL, 
[Point] [int] NOT NULL, 
[lsActive] [bit] NOT NULL, 
[CreatedBy] [nvarchar](50) NOT NULL,
[CreatedOn] [datetime] NOT NULL, 
[ModifiedBy] [nvarchar](50) NULL, 
[ModifiedOn] [datetime] NULL)