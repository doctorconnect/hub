USE [KMT]
GO
CREATE TABLE [dbo.[UsersVote](
[VoteID] [int] IDENTITY(1,1) NOT NULL,
[Question] [varchar](max) NOT NULL, 
[Options] [varchar](max) NOT NULL, 
[CreatedBy] [varchar](50) NOT NULL, 
[CreatedDate] [datetime] NOT NULL, 
[ModifiedBy] [varchar](50) NOT NULL, 
[ModifiedDate] [datetime] NOT NULL, 
[IsActive] [bit] NOT NULL)
GO 