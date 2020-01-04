CREATE TABLE [dbo].[KMTAssessmentResult](
[id] [int] IDENTITY(1,1) NOT NULL, 
[Quizld] [int] NOT NULL, 
[Marks] [int] NOT NULL, 
[Status] [nvarcharJ(50) NOT NULL, 
[CreatedBy] [nvarchar] (50) NOT NULL, 
[CreatedOn] [datetime] NOT NULL)