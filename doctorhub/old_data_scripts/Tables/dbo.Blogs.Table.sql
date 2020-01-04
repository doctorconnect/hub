USE [KMT]
GO
CREATE TABLE [dbo].[Blogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[Message] [varchar](max) NOT NULL,
	[BlogBy] [int] NOT NULL,
	[BlogDate] [datetime2](7) NOT NULL, 
	[Status] [int] NULL, 
	[Remarks] [varchar] (250) NULL, 
	[ApprovedBy] [varchar] (50) NULL, 
	[Approveddate] [date] NULL, 
	[CreatedBy] [varchar] (50) NULL, 
	[CapId] [int] NULL)
GO
