USE [KMT]
GO
CREATE PROCEDURE [dbo].[uspGetQuizAnswer]
AS
BEGIN
SELECT 
A.AnswerID,
A.AnswerText,
A.QuestionID,
Q.QuestionText
FROM [KMTAnswers] A JOIN [KMTQuestion] Q ON A.QuestionID=Q.QuestionID
JOIN [KMTQUIZ] B B.QuizID=Q.QuizID
WHERE B.IsActive=1'
END
GO