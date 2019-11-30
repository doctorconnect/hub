CREATE TABLE [dbo].[KMTBadgeDetail] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [BadgeId]      INT           NOT NULL,
    [BadgeName]    NVARCHAR (50) NOT NULL,
    [BadgePoint]   INT           NOT NULL,
    [BadgePointTo] INT           NOT NULL,
    [IsActive]     BIT           NOT NULL,
    [CreatedBy]    NVARCHAR (50) NOT NULL,
    [CreatedOn]    DATETIME      NOT NULL,
    [ModifiedBy]   NVARCHAR (50) NULL,
    [ModifiedOn]   DATETIME      NULL
);

