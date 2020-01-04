USE [KMT]
GO
CREATE PROCEDURE [DBO].[uspGetBadge]
AS
BEGIN
SELECT
C.Id,
C.BadgeId,
C.BadgeName,
B.BadgeImage,
C.BadgePoint,
C.BadgePointTo,
C.IsActive,
R.username AS CreatedBy,
R.username,
C.CreatedOn,
C.ModifiedBy,
C.ModifiedOn
FROM [KMTBadgeDetail] C JOIN [KMTUserRegistration] R ON CREATEDBY = R.userNtid
JOIN [KMTBadge] B ON B.BadgeId = C.Id
END
GO