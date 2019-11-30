CREATE TABLE [dbo].[KMTWebExTraing] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Traingld]    INT           NOT NULL,
    [TraingTitle] NVARCHAR (50) NOT NULL,
    [CreatedBy]   NVARCHAR (50) NOT NULL,
    [CreatedOn]   DATETIME      NOT NULL
);

