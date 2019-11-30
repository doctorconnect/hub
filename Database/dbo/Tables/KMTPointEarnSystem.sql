CREATE TABLE [dbo].[KMTPointEarnSystem] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Points]   INT           NOT NULL,
    [Pes_ld]   INT           NOT NULL,
    [Pes_type] NVARCHAR (50) NOT NULL,
    [Pes_by]   NVARCHAR (50) NOT NULL,
    [Pes_On]   DATETIME      NOT NULL
);

