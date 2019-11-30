﻿CREATE  PROCEDURE PrcSubmitUserrequest
(
 @Name VARCHAR(50),
 @EmailId  VARCHAR(50),
 @Password  VARCHAR(50)
)
AS
BEGIN 
BEGIN TRANSACTION
SAVE TRANSACTION MYSAVEPOINT
BEGIN TRY
INSERT INTO HUBREGISTRATION(Name,EmailId,Password) VALUES( @Name, @EmailId,@Password);
END TRY
BEGIN CATCH
IF @@TRANCOUNT >0
BEGIN ROLLBACK TRANSACTION MYSAVEPOINT;
END;
END CATCH;
COMMIT TRANSACTION;
END;