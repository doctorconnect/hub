CREATE TABLE [dbo].[KMTNotification](
[Id] [int] IDENTITY(1,1) NOT NULL,
[Roleld] [varchar](50) NULL, 
[UserCode] [varchar](50) NULL, 
[IsAdminFlag] [bit] NULL, 
[IsUserFlag] [bit] NULL, 
[lsFacilitatorFlag] [bit] NULL, 
[Identifier] [nvarchar](50) NULL,
[AdminDescripation][nvarchar](max) NULL,
[UserDescripation][nvarchar](max) NULL,
[CreatedOn][datetime] NOT NULL)