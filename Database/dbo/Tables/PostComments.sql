CREATE TABLE [dbo].[PostComments] (
    [CommentId]     INT           IDENTITY (1, 1) NOT NULL,
    [Postld]        INT           NOT NULL,
    [Message]       VARCHAR (MAX) NOT NULL,
    [CommentedBy]   INT           NOT NULL,
    [CommentedDate] DATETIME2 (7) NOT NULL,
    [Identifier]    VARCHAR (10)  NULL
);

