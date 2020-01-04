USE [KMT]
GO
CREATE TABLE [dbo].[Poll](
[PollID] [int] IDENTITY(1,1) NOT NULL,
[Title] [varchar](max) NOT NULL,
[Options] [varchar](max) NULL, 
[FromDate] [datetime] NOT NULL, 
[ToDate] [datetime] NOT NULL, 
[IsActive] [bit] NULL, 
[CreatedBy] [varchar](50) NOT NULL, 
[Createdon] [datetime] NOT NULL, 
[ModifiedBy] [varchar](50) NULL, 
[ModifiedOn] [datetime] NULL)
GO