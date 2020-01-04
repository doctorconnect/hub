USE KMT
GO
CREATE TABLE [dbo].[KMTBadgeDetail](
[Id] [int] IDENTITY(1,1) NOT NULL,
[Badgeld] [int] NOT NULL, 
[BadgeName] [nvarchar](50) NOT NULL, 
[BadgePoint] [int] NOT NULL, 
[BadgePointTo] [int] NOT NULL, 
[IsActive] [bit] NOT NULL,
[CreatedBy] [nvarchar] (50) NOT NULL, 
[CreatedOn] [datetime] NOT NULL,
[ModifiedBy] [nvarchar] (50) NULL, 
[ModifiedOn] [datetime] NULL)