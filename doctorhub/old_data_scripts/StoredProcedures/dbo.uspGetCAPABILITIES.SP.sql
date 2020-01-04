USE [KMT]
GO
CREATE PROCEDURE [dbo].[uspGetCAPABILITIES]
AS
BEGIN
SELECT c.ID,
c.BsId,
B.Name As BsName,
c.Name,
c.IDENTIFIER, c.ISACTIVE,
(select username from [dbo].[KMTUserRegistration] where userNtid=c.CREATEDBY) as CREATEDBY,
c.CREATEDON,
c.MODIFIEDBY,
c.MODIFIEDON FROM [KMTCAPABILITIES c JOIN [KMTBusinessSegment] B ON B.id=c.BsId
END
GO