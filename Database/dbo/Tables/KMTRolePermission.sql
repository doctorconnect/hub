CREATE TABLE [dbo].[KMTRolePermission] (
    [RolePermissionld] INT           IDENTITY (1, 1) NOT NULL,
    [Roleld]           INT           NOT NULL,
    [Menuld]           INT           NOT NULL,
    [ActonName]        NVARCHAR (50) NULL,
    [ControllerName]   NVARCHAR (50) NULL,
    [IsActive]         BIT           NOT NULL,
    [CreatedBy]        NVARCHAR (50) NOT NULL,
    [CreatedOn]        DATETIME      NOT NULL,
    [ModifiedBy]       NVARCHAR (50) NULL,
    [ModifiedOn]       DATETIME      NULL
);

