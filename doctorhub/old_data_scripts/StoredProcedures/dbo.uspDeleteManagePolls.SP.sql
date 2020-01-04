USE [KMT]
GO
create proc [dbo].[uspDeleteManagePolls]
@PollID int
AS 
BEGIN 
delete from Poll where PollID=@PollID
END
GO 