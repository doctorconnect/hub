CREATE TABLE [dbo].[Follower](
[Id] [int] IDENTITY(1,1) NOT NULL,
[FollowerBy] [int] NOT NOLL, 
[FollowingBy] [int] NOT NULL, 
[FollowerDate] [datetime2](7) NOT NULL)
