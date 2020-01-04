USE [KMT]
GO
CREATE PROCEDURE [dbo].[uspGetLustOfNotificationDetails]
AS
BEGIN
SELECT UR.UserName UserName, N.Identifier Identifier 
FROM KMTUserRegistration UR INNER JOIN KMTNotification N ON UR.UserCode=N.UserCode WHERE UR.[STATUS]5
END
GO