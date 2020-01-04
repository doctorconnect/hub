USE [KMT] GO /****** Object: StoredProcedure [dbo].[uspGetDocumentsUploadedReport] Script Date: 8/23/2019 6:11:46 AM******/ 

CREATE PROCEDURE [dbo].[uspGetDocumentsUploadedReport] ( @StartDate DateTime, @EndDate DateTime, @Criteria Varchar(50), @Capid int, @CapText Varchar(10)) AS IF @CapText IS NULL BEGIN BEGIN  
IF @Criteria='Month-wise' BEGIN SELECT CONCAT(DATEPART(MONTH,UploadDate),'/',DATEPART(YEAR,UploadDate)) AS Interval,COUNT(*) AS Utilization FROM UploadDocument 
WHERE UploadDate>=@StartDate AND UploadDate<=@EndDate +1 AND CapId = @Capid GROUP BY DATEPART(MONTH,UploadDate), DATEPART(YEAR, UploadDate)
ORDER BY DATEPART(YEAR, UploadDate), DATEPART(MONTH,UploadDate) END
ELSE IF @Criteria='Week-wise' BEGIN SELECT CONCAT (DATEPART(MONTH,UploadDate),'/',DATEPART (YEAR ,UploadDate), ' Week', DATEPART(WEEK,UploadDate)) AS Interval, COUNT(*) AS Utilization 
FROM UploadDocument WHERE UploadDate>=@StartDate AND UploadDate<=@EndDate +1 AND CapId = @Capid
GROUP BY DATEPART(WEEK,UploadDate), DATEPART(MONTH, UploadDate), DATEPART(YEAR, UploadDate)
ORDER BY DATEPART(YEAR, UploadDate), DATEPART(MONTH, UploadDate), DATEPART(WEEK,UploadDate) END 
ELSE IF @Criteria='Day-wise' BEGIN SELECT CONCAT(DATEPART(MONTH,UploadDate),'/',DATEPART (DAY,UploadDate),'/', DATEPART (YEAR ,UploadDate)) AS Interval, COUNT(*) AS Utilization 
FROM UploadDocument WHERE UploadDate>=@StartDate AND UploadDate<=@EndDate= +1 AND Capld = @Capid 
GROUP BY DATEPART(DAY, UploadDate), DATEPART(MONTH, UploadDate), DATEPART(YEAR, UploadDate) 
ORDER BY DATEPART(YEAR, UploadDate), DATEPART(MONTH, UploadDate), DATEPART(DAY, UploadDate) END 
ELSE BEGIN SELECT CONCAT (DATEPART(MONTH, UploadDate),'/', DATEPART (DAY,UploadDate),'/',DATEPART(YEAR,UploadDate),'',DATEPART(HOUR, UploadDate), ':OO') AS Interval, COUNT(*) AS Utilization 
FROM UploadDocument WHERE UploadDate>=@StartDate AND UploadDate<=@EndDate +1 AND Capld= @Capid 
GROUP BY DATEPART(HOUR, UploadDate), DATEPART(DAY, UploadDate), DATEPART(MONTH, UploadDate), DATEPART(YEAR, UploadDate) 
ORDER BY DATEPART(YEAR, UploadDate), DATEPART(MONTH, UploadDate),DATEPART(DAY, UploadDate), DATEPART(HOUR, UploadDate) END END END IF @CapText='ALL' BEGIN BEGIN 
IF @Criteria='Month-wise' BEGIN SELECT CONCAT(DATEPART(MONTH,UploadDate),'/',DATEPART(YEAR,UploadDate)) AS Interval,COUNT(*) AS Utilization FROM UploadDocument 
WHERE UploadDate>=@StartDate AND UploadDate<=@EndDate +1 GROUP BY DATEPART(MONTH, UploadDate), DATEPART(YEAR, UploadDate)
ORDER BY DATEPART(YEAR, UploadDate), DATEPART(MONTH, UploadDate) END 
ELSE IF @Criteria='Week-wise' BEGIN SELECT CONCAT(DATEPART(MONTH, UploadDate),'/',DATEPART(YEAR,UploadDate), ' Week'. DATEPART(WEEK, uploadDate)) AS Interval,COUNT(*) AS Utilization
FROM UploadDocument WHERE UploadDate>=@StartDate  AND UploadDate<=@EndDate +1 GROUP BY DATEPART(WEEK, UploadDate), DATEPART(MONTH, UploadDate),DATEPART(YEAR,UploadDate) 
ORDER BY DATEPART(YEAR, UploadDate), DATEPART(MONTH, UploadDate), DATEPART(WEEK, UploadDate) END
ELSE IF @Criteria='Day-wise' BEGIN SELECT CONCAT(DATEPART(MONTH,UploadDate),'/',DATEPART(DAY,UploadDate,'/',DATEPART(YEAR,UploadDate)) AS Interval,COUNT(*) AS Utilization
FROM UploadDocument WHERE UploadDate>=@StartDate AND UploadDate<=@EndDate +1 GROUP BY DATEPART(DAY,UploadDate),DATEPART(MONTH,UploadDate),DATEPART(YEAR,UploadDate)
ORDER BY DATEPART(YEAR, UploadDate), DATEPART(MONTH, UploadDate), DATEPART(DAY, UploadDate) END 
ELSE BEGIN SELECT CONCAT(DATEPART(MONTH,UploadDate),'/',DATEPART(DAY,UploadDate),'/',DATEPART(YEAR,UploadDate),'',DATEPART(HOUR,UploadDate),':OO') AS Interval,COUNT(*) AS Utilization
FROM UploadDocument WHERE UploadDate>=@StartDate AND UploadDate<=@EndDate+1
GROUP BY DATEPART(HOUR,UploadDate),DATEPART(DAY,UploadDate),DATEPART(MONTH,UploadDate),DATEPART(YEAR,UploadDate)
ORDER BY DATEPART(YEAR,UploadDate),DATEPART(MONTH,UploadDate),DATEPART(DAY,UploadDate),DATEPART(HOUR,UploadDate) END END END 
GO 

