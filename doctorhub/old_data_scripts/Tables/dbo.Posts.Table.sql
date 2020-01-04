CREATE TABLE [dbo].[Posts](
[Postld] [int] IDENTITY(1,1) NOT NULL,
[Message] [varchar](1000) NOT NULL, 
[PostedBy] [int] NOT NULL, 
[PostedDate] [datetime2](7) NOT NULL,
[status] [int] NULL, 
[Remarks] [varchar](250) NULL,
[ApproveBy] [varchar](50) NULL,
[ApprovedDate] [date] NULL, 
[CreatedBy] [varchar](50) NULL, 
[Capld] [int] NULL)