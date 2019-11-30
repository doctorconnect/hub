CREATE TABLE [dbo].[KMTNotification] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [Roleld]            VARCHAR (50)   NULL,
    [UserCode]          VARCHAR (50)   NULL,
    [IsAdminFlag]       BIT            NULL,
    [IsUserFlag]        BIT            NULL,
    [lsFacilitatorFlag] BIT            NULL,
    [Identifier]        NVARCHAR (50)  NULL,
    [AdminDescripation] NVARCHAR (MAX) NULL,
    [UserDescripation]  NVARCHAR (MAX) NULL,
    [CreatedOn]         DATETIME       NOT NULL
);

