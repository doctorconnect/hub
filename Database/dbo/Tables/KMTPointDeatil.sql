CREATE TABLE [dbo].[KMTPointDeatil] (
    [id]              INT           IDENTITY (1, 1) NOT NULL,
    [InteractionType] NVARCHAR (50) NOT NULL,
    [Point]           INT           NOT NULL,
    [lsActive]        BIT           NOT NULL,
    [CreatedBy]       NVARCHAR (50) NOT NULL,
    [CreatedOn]       DATETIME      NOT NULL,
    [ModifiedBy]      NVARCHAR (50) NULL,
    [ModifiedOn]      DATETIME      NULL
);

