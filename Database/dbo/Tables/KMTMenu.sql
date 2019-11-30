CREATE TABLE [dbo].[KMTMenu] (
    [Menuld]         INT           NOT NULL,
    [MenuName]       NVARCHAR (50) NOT NULL,
    [MenuURL]        NVARCHAR (50) NULL,
    [ParentId]       INT           NOT NULL,
    [ActionName]     NVARCHAR (50) NULL,
    [ControllerName] NVARCHAR (50) NULL,
    [IsActive]       BIT           NOT NULL,
    [CreatedBy]      NVARCHAR (50) NOT NULL,
    [CreatedOn]      DATETIME      NOT NULL,
    [ModifiedBy]     NVARCHAR (50) NULL,
    [ModifiedOn]     DATETIME      NULL
);

