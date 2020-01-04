USE [KMT]
GO 
CREATE proc [dbo].[PROCSUBMITPOLL](
@Title varchar(max),
@Options varchar(100), 
@FromDate datetime , 
@ToDate dateTime , 
@sActive bit null =0,
@CreatedBy varchar(50), 
@CreatedOn datetime , 
@ModifiedBy varchar(50)=null,
@ModifiedOn datetime=null
)
AS
BEGIN
	INSERT INTO [dbo].[Poll] (Title, Options, FromDate, ToDate, IsActive, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn) 
	values(@Title, @Options, @FromDate, @ToDate, @sActive, @CreatedBy, @CreatedOn, @ModifiedBy, @ModifiedOn)
END
GO
 
