CREATE TABLE [dbo].[KMTFaq] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [FaqQuestion] NVARCHAR (MAX) NOT NULL,
    [FaqAnswer]   NVARCHAR (MAX) NOT NULL,
    [IsActive]    BIT            NOT NULL,
    [CreatedBy]   NVARCHAR (50)  NOT NULL,
    [CreatedOn]   DATETIME       NOT NULL,
    [ModifiedBy]  NVARCHAR (50)  NULL,
    [ModifiedOn]  DATETIME       NULL
);

