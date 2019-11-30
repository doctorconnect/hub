CREATE TABLE [dbo].[Posts] (
    [Postld]       INT            IDENTITY (1, 1) NOT NULL,
    [Message]      VARCHAR (1000) NOT NULL,
    [PostedBy]     INT            NOT NULL,
    [PostedDate]   DATETIME2 (7)  NOT NULL,
    [status]       INT            NULL,
    [Remarks]      VARCHAR (250)  NULL,
    [ApproveBy]    VARCHAR (50)   NULL,
    [ApprovedDate] DATE           NULL,
    [CreatedBy]    VARCHAR (50)   NULL,
    [Capld]        INT            NULL
);

