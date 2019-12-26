USE [KMT]
GO
CREATE PROCEDURE [dbo].[uspGetComment]
@Identifier VARCHAR(10)
AS
BEGIN
SELECT P.CommentId, P.PostId, Message, P.CommentedBy, R.username as CommentedByName, R.UserCode, P.CommentedDate, P.Identifier 
FROM PostComments P JOIN KMTUserRegistration R ON R.Id = P.CommenterdBy
WHERE Identifier = @Identifier ORDER BY P.CommentedDate desc
END
GO