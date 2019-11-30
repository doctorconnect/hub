CREATE TABLE [dbo].[KMTBusinessSegment] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50) NOT NULL,
    [Identifier] NVARCHAR (50) NOT NULL,
    [IsActive]   BIT           NOT NULL,
    [CreatedBy]  NVARCHAR (50) NOT NULL,
    [CreatedOn]  DATETIME      NOT NULL,
    [ModifiedBy] NVARCHAR (50) NULL,
    [ModifiedOn] DATETIME      NULL
);

