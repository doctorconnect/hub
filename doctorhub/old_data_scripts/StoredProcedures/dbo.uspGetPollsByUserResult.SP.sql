USE [KMT]
GO
CREATE PROCEDURE [dbo].[uspGetPollsByUserResult](
@NTID varchar(50)
)
AS
BEGIN
SELECT Title, Options, CreatedBy FROM Poll WHERE CreatedBy=@NTID
END
GO