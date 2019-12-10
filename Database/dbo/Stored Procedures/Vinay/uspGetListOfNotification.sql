USE [KMT]
GO
CREATE PROCEDURE [dbo].[uspGetListOfNotification]
AS
BEGIN
SELECT KMT.Id Id, KMT.RoleId RoleId, KMT.UserCode UserCode, KMT.IsAdminFlag IsAdminFlag, KMT.IsUserFlag IsUserFlag, KMT.IsFacilitatorFlag IsFacilitatorFlag, KMT.Identifier Identifier,
KMT.AdminDescripation AdminDescripation, KMT.UserDescripation UserDescripation, [USER].CapabilitiesId CapabilityId, KMT.CreatedOn CreatedOn 
FROM KMTNotification KMT INNER JOIN KMTUserRegistration [USER] 
ON KMT.UserCode = [USER].UserCode ORDER BY KMT.CreatedOn Desc
END
GO