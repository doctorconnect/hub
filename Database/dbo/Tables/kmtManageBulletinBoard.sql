CREATE TABLE [dbo].[kmtManageBulletinBoard] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [Title]       VARCHAR (50)  NOT NULL,
    [Description] VARCHAR (100) NOT NULL,
    [Article]     VARCHAR (MAX) NOT NULL,
    [type]        VARCHAR (50)  NOT NULL,
    [IsActive]    BIT           NOT NULL,
    [CreatedBy]   NVARCHAR (50) NOT NULL,
    [CreatedOn]   DATETIME      NOT NULL,
    [ModifiedBy]  NVARCHAR (50) NULL,
    [ModifiedOn]  DATETIME      NULL,
    [Capld]       INT           NULL
);

