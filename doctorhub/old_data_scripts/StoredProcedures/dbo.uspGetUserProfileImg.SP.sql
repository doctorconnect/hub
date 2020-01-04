USE [KMT]
GO
CREATE PROCEDURE [DBO].[uspGetUserProfileImg]
@Id INT
AS
BEGIN
SELECT id, UserPhoto FROM [dbo].[KMTUserRegistration] WHERE id = @Id
END
GO