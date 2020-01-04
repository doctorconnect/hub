USE [KMT]
GO
CREATE PROCEDURE [dbo].[uspGetQuizChoices]
AS
BEGIN
SELECT 
A.ChoiceID,
A.ChoiceText,
A.QuestionID,
Q.QuestionText
FROM [KMTChoices] A join [KMTQuestion] Q on A.QuestionID=Q.QuestionID
JOIN [KMTQuiz] B on B.QuizID=Q.QuizID
WHERE B.ISActive='1' and A.ChoiceText !='!X'
END
GO