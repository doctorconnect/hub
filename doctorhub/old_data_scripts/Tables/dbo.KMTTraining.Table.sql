USE [KMT]
GO 
CREATE TABLE [dbo].[KMTTraining](
[Id] [int] identity(1,1) NOT NULL, 
[Title] [varchar](200) NOT NULL,
[Url] [varchar](200) NULL,
[FromDate] [datetime] NOT NULL, 
[ToDate] [datetime] NOT NULL, 
[IsActive] [bit] NULL, 
[CreatedBy] [varchar](50) NOT NULL, 
[Createdon] [datetime] NOT NULL],
[ModifiedBy] [varchar](50) NULL,
[ModifiedOn] [datetime] NULL,
[CapId] [int] NULL)
GO 