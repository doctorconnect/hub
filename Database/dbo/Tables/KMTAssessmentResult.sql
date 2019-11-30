CREATE TABLE [dbo].[KMTAssessmentResult] (
    [id]        INT           IDENTITY (1, 1) NOT NULL,
    [Quizld]    INT           NOT NULL,
    [Marks]     INT           NOT NULL,
    [Status]    NVARCHAR (50) NOT NULL,
    [CreatedBy] NVARCHAR (50) NOT NULL,
    [CreatedOn] DATETIME      NOT NULL
);

