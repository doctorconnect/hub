USE [KMT]
GO
CREATE PROCEDURE [dbo].[uspGetListAssessmentAttend]
(@CreatedBy VARCHAR(50))
AS
BEGIN
SELECT 
QuizId,
COUNT(QuizId) As Attend,
status,
(SELECT MAX(Marks) FROM KMTAssessmentResult WHERE QuizId = a.QuizId and CreatedBy = @CreatedBy) AS Marks
FROM KMTAssessmentResult a
WHERE createdby = @CreatedBy
GROUP BY QuizId, status
ORDER BY status DESC
END
GO