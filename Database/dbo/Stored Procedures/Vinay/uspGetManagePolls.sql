USE [KMT]
GO
CREATE PROCEDURE [dbo].[uspGetManagePolls]
(
	@CapabilitiesId int,
	@IsAdmin int
)
AS
BEGIN
IF(@IsAdmin=0)
BEGIN
SELECT PollID, Title, Options, convert(nvarchar,FromDate,101) as FromDate, convert(nvarchar, ToDate, 101) AS ToDate, P.IsActive 
FROM Poll P
WHERE P.CREATEDBY IN (SELECT UserNTID FROM [KMTUserRegistration] R WHERE R.CapabilitiesId = @CapabilitiesId)
ORDER BY PollID Desc
END
IF(@IsAdmin=1)
BEGIN
SELECT PollID, Title, Options, convert(nvarchar,FromDate,101) as FromDate, convert(nvarchar, ToDate, 101) AS ToDate, P.IsActive 
FROM Poll P
ORDER BY PollID Desc
END
END
GO