USE [KMT] GO 
CREATE PROCEDURE [dbo].[uspCreateUserProfile]
(@UserCode VARCHAR(50), @UserNTID VARCHAR(50), @UserEmail VARCHAR(50), @UserName VARCHAR(50), @ManagerCode VARCHAR(50), @ManagerEmail VARCHAR(50), @ManagerName VARCHAR(50), @ManagerNTID VARCHAR(50), @RoleId INT, @BusinessSegmentld INT, @CapabilitiesId INT, @LOBId INT, @Status INT, @IsActive BIT, @CreatedBy VARCHAR(5O), @CreatedOn DATETIME) 
AS BEGIN BEGIN TRANSACTION; SAVE TRANSACTION MySavePoint; BEGIN TRY if(@RoleId = 1) BEGIN 
INSERT INTO KMTUserAccessMaster(UserCode, UserNTID, UserName, UserType, RoleId, LOBId, [Status], EditPostID, DeleteOwnPostID, ApproveAccessID, DeleteAccessID, UploadAccessID, 
ApproveUploadedPicsID, CreatePostID, DeleteOtherPostID, ApprovePostID, FlagPostsID, CreateBlogslD, EditBlogsID, DeleteOwnBlogsID, DeleteOtherBlogsID, ApproveBlogsID, 
MoveDocumentsID, CreateEvaluationsID, EditEvaluationsID, ExportEvaluationReportsID, DeactivateOwnEvaluationsID, DeactivateOtherEvaluationsID, UploadVideosID, HostWebexSessionsID,
HostPollsID, DeactivatePollsID, UploadWebexLinksID, IsActive, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn) 
VALUES (@UserCode, @UserNTID, @UserName, 'ADMIN', @RoleId, @LOBId, @Status, '1', '1', '1', '1', '1', '1', '1', '1','1', '1', '1', '1','1', '1', '1', '1','1', '1', '1', '1','1', '1', '1', '1','1', '1', @IsActive, @CreatedBy, @CreatedOn, NULL,NULL) 
END if(@RoleId = 2) BEGIN 
INSERT INTO KMTUserAccessMaster(UserCode, UserNTID, UserName, UserType, RoleId, LOBId, [Status], EditPostID, DeleteOwnPostID, ApproveAccessID, DeleteAccessID, 
UploadAccessID, ApproveUploadedPicsID, CreatePostID, DeleteOtherPostID, ApprovePostID, FlagPostsID, CreateBlogsID, EditBlogsID, DeleteOwnBlogsID, DeleteOtherBlogsID,
ApproveBlogsID, MoveDocumentsID, CreateEvaluationsID, EditEvaluationsID, ExportEvaluationReportsID, DeactivateOwnEvaluationsID, DeactivateOtherEvaluationsID,
UploadVideosID, HostWebexSessionsID, HostPollsID, DeactivatePollsID, UploadWebexLinksID, IsActive, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn) 
VALUES (@UserCode, @UserNTID, @UserName, 'FACILITATOR', @RoleId, @LOBId, @Status, '0',  '0', '0', '1', '0', '1', '0', '1', '0','0','0','1','1','1','0', '1', '0', '1','1','1','1','1',
'1','1','1','1', @IsActive, @CreatedBy, @CreatedOn, NULL,NULL )
END if(@RoleId = 2) BEGIN  
INSERT INTO KMTUserAccessMaster(UserCode, UserNTID, UserName, UserType, RoleId, LOBId, [Status], EditPostID, DeleteOwnPostID, ApproveAccessID, DeleteAccessID, UploadAccessID, 
ApproveUploadedPicsID, CreatePostID, DeleteOtherPostID, ApprovePostID, FlagPostsID, CreateBlogsID, EditBlogsID, DeleteOwnBlogsID, DeleteOtherBlogsID, ApproveBlogsID, MoveDocumentsID, 
CreateEvaluationsID, EditEvaluationsID, ExportEvaluationReportsID, DeactivateOwnEvaluationsID, DeactivateOtherEvaluationsID, UploadVideosID, HostWebexSessionsID, HostPollsID, -----Vinay
DeactivatePollsID, UploadWebexLinksID, IsActive, CreatedBy, CreatedOn, ModifiedBy, ModifiedOn) 
VALUES (@UserCode, @UserNTID, @UserName, 'USER', @RoleId, @LOBId, @Status, '0','0','0','1','0','1','0','1','0','0','0','1','1','1','0','0','0','0','0','0','0','0','0','0','0','0',
@IsActive, @CreatedBy, @CreatedOn, NULL,NULL ) 
END END TRY	BEGIN CATCH	IF @@TRANCOUNT>0 BEGIN	ROLLBACK TRANSACTION MySavePoint;	END	END CATCH COMMIT TRANSACTION END GO 



