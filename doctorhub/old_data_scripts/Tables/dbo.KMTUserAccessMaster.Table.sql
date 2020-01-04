USE [KMT] 
GO 
CREATE TABLE [dbo].[KMTUserAccessMaster](
[Id] [int] IDENTITY(1,1) NOT NULL,[UserCode] [nvarcharl(50) NOT NULL, [UserNTIDl [nvarchar](50) NOT NULL, [UserName] [nvarchar](50) NOT NULL, 
[UserType] [nvarchar](50) NOT NULL, [RoleId] [int] NOT NULL, [LOBId] [int] NOT NULL, [Status] [int] NOT NULL, [EditPostID] [int] NOT NULL, 
[DeleteOwnPostID] [int] NOT NULL, [ApproveAccessID] [int] NOT NULL, [DeleteAccesslD] [int] NOT NULL, [UploadAccessIDl [int] NOT NULL, 
[ApproveUploadedPicsID] [int] NOT NULL, [CreatePostID] [int] NOT NULL, [DeleteOtherPostID] [int] NOT NULL, [ApprovePostID] [int] NOT NULL, 
[FlagPostsID] [int] NOT NULL, [CreateBlogsID] [int] NOT NULL,[EditBlogsID] [int] NOT NULL, [DeleteOwnBlogsID] [int] NOT NULL, 
[DeleteOtherBlogsID] [int] NOT NULL, [ApproveBlogsID] [int] NOT NULL, [MoveDocumentsID] [int] NOT NULL, [CreateEvaluationslD] [int] NOT NULL, 
[EditEvaluationsID] [int] NOT NULL, [ExportEvaluationReportsID] [int] NOT NULL, [DeactivateOwnEvaluationsID] [int] NOT NULL, 
[DeactivateOtherEvaluationsID] [int] NOT NULL, [UploadVideoslD] [int] NOT NULL, [HostWebexSessionsID] [int] NOT NULL, 
[HostPollsID] [int] NOT NULL, [DeactivatePollsID] [int] NOT NULL, [UploadWebexLinksID] [int] NOT NULL, [IsActive] [bit] NOT NULL,
[CreatedBy] [nvarchar](50) NOT NULL, [CreatedOn] [datetime] NOT NULL, [ModifiedBy] [nvarchar](50) NULL, [ModifiedOn] [datetime] NULL)