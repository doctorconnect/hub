USE [KMT]
GO
CREATE TABLE [dbo].[KMTCapabilities](
[Id] [int] IDENTITY(1,1) NOT NULL,
[Bsld] [int] NOT NULL,
[Name] [nvarchar](50) NOT NULL, 
[Identifier] [nvarchar](50) NOT NULL, 
[IsActive] (bit) NOT NULL, 
[CreatedBy] [nvarchar](50) NOT NULL, 
[CreatedOn] [datetime] NOT NULL, 
[ModifiedBy] [nvarchar](50) NULL, 
[ModifiedOn] [datetime] NULL)
GO 

 