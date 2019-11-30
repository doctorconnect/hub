CREATE TABLE [dbo].[Blogs] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Title]        VARCHAR (100) NOT NULL,
    [Message]      VARCHAR (MAX) NOT NULL,
    [BlogBy]       INT           NOT NULL,
    [BlogDate]     DATETIME2 (7) NOT NULL,
    [Status]       INT           NULL,
    [Remarks]      VARCHAR (250) NULL,
    [ApprovedBy]   VARCHAR (50)  NULL,
    [Approveddate] DATE          NULL,
    [CreatedBy]    VARCHAR (50)  NULL,
    [CapId]        INT           NULL
);

