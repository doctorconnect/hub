USE [KMT]
GO 
CREATE FUNCTION [dbo].[udf_StripHTML] (@HTMLText VARCHAR(MAX)) 
RETURNS VARCHAR(MAX) 
AS
BEGIN 
DECLARE @Start INT
DECLARE @End INT 
DECLARE @Length INT 
SET @Start = CHARINDEX('<',@HTMLText) 
SET @End = CHARINDEX('>',@HTMLText, CHARINDEX('<',@HTMLText)) 
SET @Lengh =(@End-@Start) + 1 WHILE @Start > O 
AND @End > O 
AND @Length > O 
BEGIN
SET @HTMLText = STUFF(@HTMLText,@Start,@Length,'')
SET @Start = CHARINDEX('<',@HTMLText) SET @End = CHARINDEX('>',@HTMLText,CHARINDEX('<',@HTMLText))
SET @Length = (@End - @Start) + 1 
END 
RETURN LTRIM(RTRIM(@HTMLText))
END
GO 
