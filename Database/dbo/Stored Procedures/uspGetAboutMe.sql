﻿CREATE PROCEDURE [DBO].[uspGetAboutMe]
@Id INT
AS
BEGIN
SELECT AboutMe FROM KMTUserRegistration WHERE id = @Id
END
