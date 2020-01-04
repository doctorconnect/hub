USE [KMT]
GO
CREATE PROCEDURE [dbo].[uspGetListOfUserFeedback]
AS
BEGIN
SELECT F.Id id, F.UserCode UserCode, F.UserName UserName, F.UserNTID UserNTID, F.UserEmail UserEmail, F.AdminCode AdminCode,
F.AdminEmail AdminEmail, F.AdminName AdminName, F.AdminNTID AdminNTID, F.UserLOB UserLOB, F.FeedbackId FeedbackId, 
F.UserFeedBack UserFeedBack, F.AdminReply AdminReply, F.CreatedOn CreatedOn, F.ModifiedOn ModifiedOn, G.[Name] LOBName 
FROM KMTFeedback F INNER JOIN KMTGeneric G ON F.UserLOB = G.Id
END

GO