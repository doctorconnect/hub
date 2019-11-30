CREATE TABLE [dbo].[KMTQuiz] (
    [QuizlD]     INT          IDENTITY (1, 1) NOT NULL,
    [QuiZName]   VARCHAR (80) NULL,
    [FromDate]   DATETIME     NOT NULL,
    [ToDate]     DATETIME     NOT NULL,
    [IsActive]   BIT          NULL,
    [CreatedBy]  VARCHAR (50) NOT NULL,
    [Createdon]  DATETIME     NOT NULL,
    [ModifiedBy] VARCHAR (50) NULL,
    [ModifiedOn] DATETIME     NULL,
    [CapId]      INT          NULL
);

