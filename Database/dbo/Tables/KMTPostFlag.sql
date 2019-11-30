CREATE TABLE [dbo].[KMTPostFlag] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Postld]     INT           NOT NULL,
    [Status]     INT           NOT NULL,
    [CreatedBy]  NVARCHAR (50) NOT NULL,
    [CreatedOn]  DATETIME      NOT NULL,
    [ModifiedBy] NVARCHAR (50) NULL,
    [ModifiedOn] DATETIME      NULL,
    [Capld]      INT           NULL
);

