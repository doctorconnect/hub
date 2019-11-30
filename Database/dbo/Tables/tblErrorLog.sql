CREATE TABLE [dbo].[tblErrorLog] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [Method]         NVARCHAR (255) NULL,
    [Exception]      NVARCHAR (MAX) NULL,
    [UserNTID]       NVARCHAR (50)  NULL,
    [ErrorTimeStamp] DATETIME       NULL
);

