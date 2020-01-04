USE [KMT]
GO 
CREATE TABLE [dbo].[tblEmailTemplates](
[Title] [nvarchar](255) NULL, 
[Value] [nvarchar](max) NULL, 
[Scenario] [nvarchar](255) NULL,	1 
[To] [nvarchar](255) NULL, 
[CC] [nvarchar](255) NULL, 
[Subject] [nvarchar](255) NULL)
GO 