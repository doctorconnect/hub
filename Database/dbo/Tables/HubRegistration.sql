CREATE TABLE [dbo].[HubRegistration] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]     VARCHAR (100) NULL,
    [EmailId]  VARCHAR (100) NULL,
    [Gender]   CHAR (1)      NULL,
    [Dob]      DATE          NULL,
    [Password] VARCHAR (20)  NULL,
    [Address]  VARCHAR (250) NULL
);

