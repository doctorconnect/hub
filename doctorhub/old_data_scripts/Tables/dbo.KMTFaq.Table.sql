USE KMT
GO
CREATE TABLE [dbo].[KMTFaq](
[Id] [int] IDENTITY(1,1) NOT NULL, 
[FaqQuestion] [nvarchar](max) NOT NULL, 
[FaqAnswer] [nvarchar](max) NOT NULL, 
[IsActive] [bit] NOT NULL, 
[CreatedBy] [nvarchar](50) NOT NULL, 
[CreatedOn] [datetime] NOT NULL,
[ModifiedBy] [nvarchar](50) NULL, 
[ModifiedOn] [datetime] NULL) 