CREATE TABLE [dbo].[Follower] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [FollowerBy]   INT           NOT NULL,
    [FollowingBy]  INT           NOT NULL,
    [FollowerDate] DATETIME2 (7) NOT NULL
);

