USE [KMT] GO /****** Object: StoredProcedure [dbo].[uspGetInteractionsReports] script Date: 8/23/2019 6:11:46AM ******/
CREATE PROCEDURE [dbo].[uspGetInteractionsReports] ( @StartDate DateTime, @EndDate DateTime, @Criteria Varchar(50),@Capid int,@CapText Varchar(1O)) AS IF @CapText IS NULL BEGIN  BEGIN
IF @Criteria='Month-wise' BEGIN SELECT CONCAT(DATEPART(MONTH,Pes_on),'/',DATEPART(YEAR,Pes_on)) AS Interval, COUNT(*) AS Utilization FROM [KMTPointEarnSystem] P join KMTUserRegistration R
on P.Pes_by = R.UserNTID WHERE Pes_on>=@StartDate AND Pes_on<=@EndDate +1 and R.CapabilitiesId= @Capid GROUP BY DATEPART(MONTH, Pes_on), DATEPART(YEAR,Pes_on)
ORDER BY DATEPART (YEAR,Pes_on), DATEPART(MONTH, Pes_on) END 
ELSE IF @Criteria='Week-wise' BEGIN SELECT CONCAT(DATEPART(MONTH, Pes_on),'/',DATEPART(YEAR,Pes_on), 'Week', DATEPART(WEEK, Pes_on)) AS Interval, COUNT(*) AS Utilization 
FROM [KMTPointEarnSystem] P join KMTUserRegistration R on P.Pes_by = R.UserNTID WHERE Pes_on>=@StartDate AND Pes_on<=@EndDate +1 and R.CapabilitiesId= @Capid 
GROUP BY DATEPART(WEEK,Pes_on),DATEPART(MONTH,Pes_on),DATEPART(YEAR,Pes_on) ORDER BY DATEPART(YEAR,Pes_on),DATEPART(MONTH,Pes_on),DATEPART(WEEK,Pes_on) END
ELSE IF @Criteria='Day-wise' BEGIN SELECT CONCAT(DATEPART(MONTH,Pes_on),'/',DATEPART(DAY,Pes_on),'/',DATEPART(YEAR,Pes_on)) AS Interval,COUN(*) AS Utilization
FROM [KMTPointEarnSystem] P join KMTUserRegistration R on P.Pes_by= R.UserNTID WHERE Pes_on>=@StartDate AND Pes_on<=@EndDate +1 and R.CapabilitiesId=@Capid
GROUP BY DATEPART(DAY,Pes_on), DATEPART(MONTH,Pes_on), DATEPART(YEAR,Pes_on) ORDER BY DATEPART(YEAR,Pes_on), DATEPART(MONTH,Pes_on), DATEPART(DAY,Pes_on) END
ELSE BEGIN SELECT CONCAT(DATEPART(MONTH,Pes_on),'/',DATEPART(DAY,Pes_on),'/',DATEPART(YEAR,Pes_on),'',DATEPART(HOUR,Pes_on),':00') AS Interval, COUNT(*) AS Utilization
FROM [KMTPointEarnSystem] P join KMTUserRegistration R on P.Pes_by=R.UserNTID WHERE Pes_on>=@StartDate AND Pes_on<=@EndDate +1 and R.CapabilitiesId=@Capid
GROUP BY DATEPART(HOUR,Pes_on), DATEPART(DAY, Pes_on), DATEPART(MONTH, Pes_on), DATEPART(YEAR, Pes_on)
ORDER BY DATEPART(YEAR, Pes_on), DATEPART(MONTH, Pes_on), DATEPART(DAY,Pes_on) DATEPART(HOUR,Pes_on) END END END ELSE IF @CapText='ALL'BEGIN
IF @Criteria='Month-wise' BEGIN SELECT CONCAT(DATEPART(MONTH,Pes_on),'/',DATEPART(YEAR,Pes_on)) AS Interval, COUNT(*) AS Utilization FROM [KMTPointEarnSystem] P join KMTUserRegistration R 
on P.Pes_by = R.UserNTID WHERE Pes_on>=@StartDate AND Pes_on<=@EndDate +1 GROUP BY DATEPART(MONTH,Pes_on), DATEPART(YEAR, Pes_on)
ORDER BY DATEPART(YEAR, Pes_on), DATEPART(MONTH, Pes_on) END 
ELSE IF @Criteria='Week-wise' BEGIN SELECT CONCAT(DATEPART(MONTH, Pes_on),'/',DATEPART(YEAR,Pes_on),'Week',DATEPART(WEEK,Pes_on)) AS Interval, COUNT (*) AS Utilization 
FROM [KMTPointEarnSystem] P join KMTUserRegistration R on P.Pes_by = R.UserNTID WHERE Pes_on>=@StartDate AND Pes_on<=@EndDate +1
GROUP BY DATEPART(WEEK, Pes_on), DATEPART(MONTH, Pes_on), DATEPART(YEAR, Pes_on), DATEPART(YEAR, Pes_on) ORDER BY DATEPART(YEAR, Pes_on), DATEPART(MONTH, Pes_on), DATEPART(WEEK,Pes_on)END 
ELSE IF @Criteria='Day-wise' BEGIN SELECT CONCAT(DATEPART(MONTH,Pes_on),'/',DATEPART(DAY,Pes_on),'/',DATEPART(YEAR,Pes_on)) AS Interval, COUNT(*) AS Utilization FROM [KMTPointEarnSystem] P
join KMTUserRegistration R on P.Pes_by = R.UserNTID WHERE pes_on>=@StartDate AND Pes_on<=@EndDate +1
GROUP BY DATEPART(DAY,Pes_on),DATEPART(MONTH,Pes_on),DATEPART(YEAR,Pes_on) ORDER BY DATEPART(YEAR, Pes_on),DATEPART(MONTH,Pes_on),DATEPART(DAY,Pes_on) END
ELSE BEGIN SELECT CONCAT(DATEPART(MONTH, Pes_on),'/',DATEPART(DAY,Pes_on),'/',DATEPART(YEAR,Pes_on),'',DATEPART(HOUR,Pes_on),':OO') AS Interval, COUNT(*) AS Utilization 
FROM [KMTPointEarnSystem] P join KMTUserRegistration R on P.Pes_by = R.UserNTID WHERE Pes_on>=@StartDate AND Pes_on<=@EndDate +1
GROUP BY DATEPART(HOUR,Pes_on),DATEPART(DAY, Pes_on), DATEPART(MONTH, Pes_on), DATEPART(YEAR,Pes_on) 
ORDER BY DATEPART(YEAR,Pes_on),DATEPART(MONTH, Pes_on),DATEPART(DAY, Pes_on), DATEPART(HOUR, Pes_on) END END GO 