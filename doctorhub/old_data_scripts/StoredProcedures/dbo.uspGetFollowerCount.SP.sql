USE [KMT]
GO
CREATE PROCEDURE [DBO].[uspGetFollowerCount]
@Id VARCHAR(20),
@UserId VARCHAR(20)
AS
BEGIN
SELECT COUNT(*) count FROM [Follwer] WHERE FollowrBy = @UserId AND FollowingBy = @Id
END
GO