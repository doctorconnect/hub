USE [KMT]
GO
CREATE PROCEDURE [dbo].[uspGetUserVoteResult](
@UserNTID VARCHAR(50),
@Question VARCHAR(max)
)
AS
BEGIN
SELECT VoteID, Question, Options, CreatedBy FROM UsersVote WHERE CreatedBy=@UserNTID AND Question=@Question
END
GO