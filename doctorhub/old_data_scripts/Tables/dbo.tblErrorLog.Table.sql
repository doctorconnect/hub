USE [KMT]
GO 
CREATE TABLE [dbo].[tblErrorLog](
[ID] [int] IDENTITY(1,1) NOT NULL,
[Method] [nvarchar(255) NULL, 
[Exception] [nvarchar](max) NULL, 
[UserNTID] [nvarchar](50) NULL, 
[ErrorTimeStamp] [datetime] NULL)
GO