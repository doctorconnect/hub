CREATE TABLE [dbo].[PostLikes] (
    [Likeld]     INT           IDENTITY (1, 1) NOT NULL,
    [Postld]     INT           NOT NULL,
    [Message]    VARCHAR (MAX) NOT NULL,
    [LikeBy]     INT           NOT NULL,
    [LikeDate]   DATETIME      NOT NULL,
    [Identifier] VARCHAR (10)  NULL
);

