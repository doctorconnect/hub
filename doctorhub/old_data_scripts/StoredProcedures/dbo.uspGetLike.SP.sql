USE [KMT]
GO
CREATE PROCEDURE [DBO].[uspGetLike]
@Identifier VARCHAR(10)
AS
BEGIN
SELECT Postid, LikeBy, Identifier FROM PostLikes WHERE Identifier = @Identifier
END
GO