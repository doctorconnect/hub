USE [KMT] 
CREATE TABLE [dbo].[KMTAnswers](
[AnswerID] [int] IDENTITY(1,1) NOT NULL, 
[AnswerText] [varchar](max) NULL, 
[QuestionID] [int] NOT NULL)