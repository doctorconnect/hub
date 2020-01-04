USE [KMT] 
GO 
CREATE TABLE [dbo].[PostComments]( 
[CommentId] [int] IDENTITY(1,1) NOT NULL, 
[Postld] [int] NOT NULL, 
[Message] [varchar](max) NOT NULL, 
[CommentedBy] [int] NOT NULL, 
[CommentedDate] [datetime2](7) NOT NULL, 
[Identifier] [varchar](10) NULL)
GO