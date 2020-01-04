USE [KMT] 
GO 
CREATE TABLE [dbo].[tblMobile]( 
[Id] [int] IDENTITY(1,1) NOT NULL, 
[MobileName] [nvarchar](50) NOT NULL, 
[MobileType] [nvarchar](50) NOT NULL, 
[MobileDescripation] [nvarchar](100) NOT NULL, 
[MobileIMEINo] [bigint] NOT NULL, 
[MobilePrice] [float] NULL, 
[MobileManufactureDate] [date] NULL)
GO 