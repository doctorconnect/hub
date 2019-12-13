USE [KMT]
GO
CREATE PROCEDURE [dbo].[uspGetRSSFeed]
(
@CapabilitiesId int,
@IsAdmin
)
AS
BEGIN
IF(@IsAdmin=0)
BEGIN
	SELECT C.Id, C.Title, C.Url, C.IsActive, R.username as CREATEDBY, C.CREATEDON, C.MODIFIEDBY, C.MODIFIEDON 
	FROM [KMTRSSFeed] C JOIN [KMTUserRegistration] R ON R.userNtid = c.CREATEDBY 
	WHERE C.CapId = @CapabilitiesId
END
IF(@IsAdmin=1)
BEGIN
	SELECT C.Id, C.Title, C.Url, C.IsActive, R.username as CREATEDBY, C.CREATEDON, C.MODIFIEDBY, C.MODIFIEDON 
	FROM [KMTRSSFeed] C JOIN [KMTUserRegistration] R ON R.userNtid = c.CREATEDBY
END
END
GO