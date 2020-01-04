USE [KMT] 
GO 
CREATE TABLE 	[dbo].[KMTMenu](
[Menuld] [int] NOT NULL,
[MenuName] [nvarchar](50) NOT NULL,
[MenuURL] [nvarchar](50) NULL, 
[ParentId] [int] NOT NULL,
[ActionName] [nvarchar](50) NULL,
[ControllerName] [nvarchar](50) NULL,
[IsActive] [bit] NOT NULL,
[CreatedBy] [nvarchar](50) NOT NULL, 
[CreatedOn] [datetime] NOT NULL, 
[ModifiedBy] [nvarchar](50) NULL,
[ModifiedOn] [datetime] NULL)