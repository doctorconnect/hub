CREATE TABLE [dbo].[KMTRSSFeed] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Title]      NVARCHAR (50)  NOT NULL,
    [Url]        NVARCHAR (250) NOT NULL,
    [IsActive]   BIT            NOT NULL,
    [CreatedBy]  NVARCHAR (50)  NOT NULL,
    [CreatedOn]  DATETIME       NOT NULL,
    [ModifiedBy] NVARCHAR (50)  NULL,
    [ModifiedOn] DATETIME       NULL,
    [Capld]      INT            NULL
);

