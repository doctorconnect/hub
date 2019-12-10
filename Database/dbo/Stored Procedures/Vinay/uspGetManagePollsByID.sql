USE [KMT]
Go
CREATE PROC [dbo].[uspGetManagePollsByID]
@PollID int,
@CapabilitiesId int,
@IsAdmin int
AS
BEGIN
IF(@IsAdmin=0)
BEGIN
	SELECT PollId, Title, Options, CONVERT(NVARCHAR, FromDate, 101) AS FromDate, CONVERT(NVARCHAR, ToDate, 101) AS ToDate, IsActive FROM POLL P 
	WHERE P.CREATEDBY IN (SELECT UserNTID FROM [KMTUserRegistration] R WHERE R.CapabilitiesId = @CapabilitiesId) AND PollID = @PollID
	ORDER BY PollID Desc
END
ELSE IF(@IsAdmin=1)
BEGIN
	SELECT PollId, Title, Options, CONVERT(NVARCHAR, FromDate, 101) AS FromDate, CONVERT(NVARCHAR, ToDate, 101) AS ToDate, IsActive FROM POLL 
	WHERE PollID = @PollID
	ORDER BY PollID Desc
END
END
GO