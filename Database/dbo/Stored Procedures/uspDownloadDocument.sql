﻿CREATE PROCEDURE [DBO].[uspDownloadDocument]
(@Id INT)
AS
BEGIN
SELECT MESSAGE, NAME, TYPE, UPLOADBY, STATUS, FilePath FROM UploadDocument WHERE id=@id
END