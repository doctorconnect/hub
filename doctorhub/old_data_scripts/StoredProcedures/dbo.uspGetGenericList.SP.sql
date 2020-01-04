USE [KMT]
GO
CREATE PROCEDURE [dbo].[uspGetGenericList]
(
@Identifier VARCHAR(50)
)
AS
BEGIN
SELECT * FROM KMTGeneric WHERE Identifier=@Identifier
END
GO