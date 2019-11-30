CREATE TABLE [dbo].[KMTFeedback] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [UserCode]     NVARCHAR (50)  NOT NULL,
    [UserNTlD]     NVARCHAR (50)  NOT NULL,
    [UserName]     NVARCHAR (50)  NOT NULL,
    [UserEmail]    NVARCHAR (50)  NOT NULL,
    [UserLOB]      INT            NOT NULL,
    [AdminCode]    NVARCHAR (50)  NULL,
    [AdminNTID]    NVARCHAR (50)  NULL,
    [AdminName]    NVARCHAR (50)  NULL,
    [AdminEmail]   NVARCHAR (50)  NULL,
    [Feedbackld]   NVARCHAR (50)  NOT NULL,
    [UserFeedBack] NVARCHAR (500) NOT NULL,
    [AdminReply]   NVARCHAR (500) NULL,
    [CreatedBy]    NVARCHAR (50)  NOT NULL,
    [CreatedOn]    DATETIME       NOT NULL,
    [ModifiedBy]   NVARCHAR (50)  NULL,
    [ModifiedOn]   DATETIME       NULL
);

