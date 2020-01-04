USE [KMT]
GO
CREATE PROCEDURE [dbo].[uspGetEmailTemplates]
AS
BEGIN
SELECT * FROM tblEmailTemplates
END
GO