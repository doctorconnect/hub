CREATE TABLE [dbo].[dbo.[UsersVote] (
    [VoteID]       INT           IDENTITY (1, 1) NOT NULL,
    [Question]     VARCHAR (MAX) NOT NULL,
    [Options]      VARCHAR (MAX) NOT NULL,
    [CreatedBy]    VARCHAR (50)  NOT NULL,
    [CreatedDate]  DATETIME      NOT NULL,
    [ModifiedBy]   VARCHAR (50)  NOT NULL,
    [ModifiedDate] DATETIME      NOT NULL,
    [IsActive]     BIT           NOT NULL
);

