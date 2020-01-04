USE [KMT]
GO
CREATE PROCEDURE [dbo].[uspGetBusinessSegment]
AS
BEGIN
SELECT Id,
Name,
Identifier,
IsActive,
(select username from [dbo].[KMTUserRegistration] where userNtid=c.CREATEDBY) as CREATEDBY,
CreatedOn,
ModifiedBy,
ModifiedOn 
FROM KMTBusinessSegment c
END
GO