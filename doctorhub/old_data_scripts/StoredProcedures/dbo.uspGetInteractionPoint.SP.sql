USE [KMT]
GO
CREATE PROCEDURE [dbo].[uspGetInteractionPoint]
AS
BEGIN
SELECT 
C.Id,
C.InteractionType,
C.Point,
C.IsActive,
R.username AS CreatedBy,
R.username,
C.CreatedOn,
C.ModifiedBy,
C.ModifiedOn 
FROM [KMTPointDeatil] C JOIN [KMTUserRegistration] R ON C.CREATEDBY=R.userNtid
END
GO