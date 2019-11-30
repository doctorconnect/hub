CREATE TABLE [dbo].[Poll] (
    [PollID]     INT           IDENTITY (1, 1) NOT NULL,
    [Title]      VARCHAR (MAX) NOT NULL,
    [Options]    VARCHAR (MAX) NULL,
    [FromDate]   DATETIME      NOT NULL,
    [ToDate]     DATETIME      NOT NULL,
    [IsActive]   BIT           NULL,
    [CreatedBy]  VARCHAR (50)  NOT NULL,
    [Createdon]  DATETIME      NOT NULL,
    [ModifiedBy] VARCHAR (50)  NULL,
    [ModifiedOn] DATETIME      NULL
);

