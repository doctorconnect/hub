USE [KMT] 
GO 
CREATE TABLE [dbo].[KMTQuiz](
[QuizlD] [int] IDENTITY(1,1) NOT NULL, 
[QuiZName] [varchar](80) NULL, 
[FromDate] [datetime] NOT NOLL, 
[ToDate] [datetime] NOT NULL, 
[IsActive] [bit] NULL,
[CreatedBy] [varchar](50) NOT NULL, 
[Createdon] [datetime] NOT NULL, 
[ModifiedBy] [varchar](50) NULL,
[ModifiedOn] [datetime] NULL, 
[CapId] [int] null) 

 