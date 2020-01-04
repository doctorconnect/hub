USE [KMT] 
GO 
CREATE TABLE [dbo].[KMTRolePermission](
[RolePermissionld] [int] IDENTITY(1,1) NOT NULL, 
[Roleld] [int] NOT NULL, 
[Menuld] [int] NOT NULL, 
[ActonName] [nvarchar](50) NULL, 
[ControllerName] [nvarchar](50) NULL, 
[IsActive] [bit] NOT NULL, 
[CreatedBy] [nvarchar](50) NOT NULL, 
[CreatedOn] [datetime] NOT NULL, 
[ModifiedBy] [nvarchar](50) NULL, 
[ModifiedOn] [datetime] NULL)
GO