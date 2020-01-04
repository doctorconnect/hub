CREATE TABLE [dbo].[KMTPointEarnSystem](
[Id] [int] IDENTITY(1,1) NOT NULL, 
[Points] [int] NOT NULL, 
[Pes_ld] [int] NOT NULL,
[Pes_type] [nvarchar](50) NOT NULL, 
[Pes_by] [nvarchar](50) NOT NULL,
[Pes_On] [datetime] NOT NULL)