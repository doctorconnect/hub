USE [KMT]
GO
CREATE TABLE [dbo].[PostLikes](
[Likeld] [int] IDENTITY(I.I) NOT NULL, 
[Postld] [int] NOT NULL, 
[Message] [varchar](max) NOT NULL,
[LikeBy] [int] NOT NULL,
[LikeDate] [datetime2](7) NOT NULL, 
[Identifier] [varchar](10) NULL)
GO 