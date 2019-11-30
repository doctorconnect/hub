CREATE TABLE [dbo].[KMTAnswers] (
    [AnswerID]   INT           IDENTITY (1, 1) NOT NULL,
    [AnswerText] VARCHAR (MAX) NULL,
    [QuestionID] INT           NOT NULL
);

