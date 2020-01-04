USE [KMT]
GO
CREATE PROCEDURE [dbo].[uspGetFaqList]
AS
BEGIN
SELECT 
F.Id,
F.FaqQuestion,
F.FaqAnswer,
F.IsActive,
R.usercode,
R.username as CREATEDBY,
F.CreatedOn,
F.ModifiedBy,
F.ModifiedOn fromn KMTFAQ F JOIN KMTUSERREGISTRATION R ON R.userNtid=F.CREATEDBY
ORDER BY F.CreatedOn desc
END
GO