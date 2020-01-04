USE [KMT]
GO
CREATE PROCEDURE [dbo].[uspVoteResultCount](
@Question VARCHAR (MAX)
)
AS
BEGIN
SELECT Question AS Question,
Options AS Options,
(COUNT(Options)*(100))/((SELECT COUNT(Options) FROM [dbo].[UsersVote] WHERE Question=@Question)) AS VotePerc
FROM [dbo].[UsersVote] WHERE Question =@Question GROUP BY Options, Question
END
GO