CREATE TABLE [dbo].[UploadDocument] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Message]     VARCHAR (200) NOT NULL,
    [Name]        VARCHAR (50)  NULL,
    [type]        VARCHAR (50)  NULL,
    [UploadBy]    INT           NOT NULL,
    [UploadDate]  DATETIME2 (7) NOT NULL,
    [Status]      INT           NULL,
    [FilePath]    INT           NULL,
    [Remarks]     VARCHAR (200) NULL,
    [ApproveBy]   VARCHAR (50)  NULL,
    [Approvedate] DATE          NULL,
    [Title]       VARCHAR (50)  NULL,
    [Category]    VARCHAR (50)  NULL,
    [CreatedBy]   VARCHAR (50)  NULL,
    [Capld]       INT           NULL
);

