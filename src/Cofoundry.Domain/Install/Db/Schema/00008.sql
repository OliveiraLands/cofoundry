/* 
	#170 IFileStoreService and orphan items
*/

 create table Cofoundry.AssetFileCleanupQueueItem (
	AssetFileCleanupQueueItemId int AUTO_INCREMENT not null,
	EntityDefinitionCode char(6) not null,
	FileNameOnDisk varchar(50) not null,
	FileExtension nvarchar(30) not null,
	CreateDate datetime not null,
	LastAttemptDate datetime null,
	CompletedDate datetime null,
	CanRetry bit not null,
	AttemptPermittedDate datetime not null,

	constraint PK_AssetFileCleanupQueueItem primary key (AssetFileCleanupQueueItemId),
	constraint FK_AssetFileCleanupQueueItem_EntityDefinition foreign key (EntityDefinitionCode) references Cofoundry.EntityDefinition (EntityDefinitionCode)
);

 -- insert soft-deleted items into the queue

 insert into Cofoundry.AssetFileCleanupQueueItem (
	EntityDefinitionCode,
	FileNameOnDisk,
	FileExtension,
	CreateDate,
	CanRetry,
	AttemptPermittedDate
 ) 
 select 'COFIMG', convert(ImageAssetId, char(50)), Extension, now(), 1, Now()
 from Cofoundry.ImageAsset
 where IsDeleted = 1;
 

 insert into Cofoundry.AssetFileCleanupQueueItem (
	EntityDefinitionCode,
	FileNameOnDisk,
	FileExtension,
	CreateDate,
	CanRetry,
	AttemptPermittedDate
 ) 
 select 'COFDOC', convert(DocumentAssetId, char(50)), FileExtension, Now(), 1, Now()
 from Cofoundry.DocumentAsset
 where IsDeleted = 1;


/* 
	#33 Make Image Asset Files Permanently Cachable
*/

-- Also remove soft deletes & tidy naming
delete from Cofoundry.ImageAsset where IsDeleted = 1;
delete from Cofoundry.DocumentAsset where IsDeleted = 1;


alter table Cofoundry.ImageAsset drop column IsDeleted;
-- alter table Cofoundry.DocumentAsset drop constraint DF_DocumentAsset_IsDeleted;
alter table Cofoundry.DocumentAsset drop column IsDeleted;
alter table Cofoundry.ImageAsset modify column FileSize bigint not null;
alter table Cofoundry.ImageAsset modify column FileDescription text not null;
alter table Cofoundry.DocumentAsset modify column Title nvarchar(130) not null;
alter table Cofoundry.DocumentAsset modify column FileName nvarchar(130) not null;
alter table Cofoundry.ImageAsset modify column FileName nvarchar(130) not null;
alter table Cofoundry.ImageAsset add Title nvarchar(130) null;
alter table Cofoundry.ImageAsset modify column Extension nvarchar(30) not null;

alter table Cofoundry.ImageAsset add FileUpdateDate datetime null;
alter table Cofoundry.DocumentAsset add FileUpdateDate datetime null;
alter table Cofoundry.DocumentAsset modify column UpdateDate datetime not null;
alter table Cofoundry.DocumentAsset add FileNameOnDisk varchar(50) null;
alter table Cofoundry.ImageAsset add FileNameOnDisk varchar(50) null;
alter table Cofoundry.ImageAsset add VerificationToken char(6) null;
alter table Cofoundry.DocumentAsset add VerificationToken char(6) null;


update Cofoundry.ImageAsset set 
	-- App command is limited to 120 so there shouldn't be any long data in there
	Title = FileDescription, 
	FileUpdateDate = UpdateDate,
	FileNameOnDisk = ImageAssetId,
	-- Verification token is just to prevent enumeration and so doesn't have to be very unique
	VerificationToken = substring( convert(uuid(), char), 0, 7);

update Cofoundry.DocumentAsset set 
	FileUpdateDate = UpdateDate,
	FileNameOnDisk = DocumentAssetId,
	VerificationToken = substring(convert(uuid(), char), 0, 7);

alter table Cofoundry.ImageAsset drop column FileDescription;
alter table Cofoundry.ImageAsset modify column Title nvarchar(130) not null;
alter table Cofoundry.DocumentAsset modify column FileUpdateDate datetime not null;
alter table Cofoundry.ImageAsset modify column FileUpdateDate datetime not null;
alter table Cofoundry.DocumentAsset modify column FileNameOnDisk varchar(50) not null;
alter table Cofoundry.ImageAsset modify column FileNameOnDisk varchar(50) not null;
alter table Cofoundry.ImageAsset modify column VerificationToken char(6) not null;
alter table Cofoundry.DocumentAsset modify column VerificationToken char(6) not null;


-- Improve naming
alter table Cofoundry.ImageAsset rename column Width to WidthInPixels;

alter table Cofoundry.ImageAsset rename column Height to HeightInPixels;

alter table Cofoundry.ImageAsset rename column Extension to FileExtension;

alter table Cofoundry.ImageAsset rename column FileSize to FileSizeInBytes;


/* 
	#257 Remove soft-deletes 
*/

delete from Cofoundry.Page where IsDeleted = 1;
delete from Cofoundry.PageGroup where IsDeleted = 1;
delete from Cofoundry.ImageAssetGroup where IsDeleted = 1;
delete from Cofoundry.DocumentAssetGroup where IsDeleted = 1;


-- ** Start Delete PageDirectories
-- NB: trigger wont be in place so we need to manually delete dependencies

-- drop table if exists DeletedPageDirectory;

-- declare @DefinitionCode char(6) = 'COFDIR'

-- create temporary table DeletedPageDirectory (PageDirectoryId int);

-- recursively gather inactive directories
/*
insert into DeletedPageDirectory
with cteExpandedDirectories as (
	select PageDirectoryId, IsActive from Cofoundry.PageDirectory
	union all
	select pd.PageDirectoryId, ed.IsActive from cteExpandedDirectories ed
	inner join Cofoundry.PageDirectory pd on pd.ParentPageDirectoryId = ed.PageDirectoryId
)
select distinct ed.PageDirectoryId
from cteExpandedDirectories ed
where ed.IsActive = 0;
*/

-- Dependencies
/*
delete Cofoundry.UnstructuredDataDependency  
inner join @DeletedPageDirectory d 
   on e.RootEntityId = d.PageDirectoryId 
  and RootEntityDefinitionCode = 'COFDIR';
*/
/*
delete Cofoundry.Page
from Cofoundry.Page e
inner join @DeletedPageDirectory d on e.PageDirectoryId = d.PageDirectoryId

delete Cofoundry.PageDirectoryLocale
from Cofoundry.PageDirectoryLocale e
inner join @DeletedPageDirectory d on e.PageDirectoryId = d.PageDirectoryId

delete Cofoundry.PageDirectory 
from Cofoundry.PageDirectory e
inner join @DeletedPageDirectory d on e.PageDirectoryId = d.PageDirectoryId
*/
-- ** End Delete PageDirectories



drop index UIX_Page_Path on Cofoundry.Page;
-- drop index UIX_PageDirectory_UrlPath on Cofoundry.PageDirectory

-- alter table Cofoundry.PageGroup drop constraint DF_PageGroup_IsDeleted;
-- alter table Cofoundry.ImageAssetGroup drop constraint DF_ImageAssetGroup_IsDeleted
-- alter table Cofoundry.DocumentAssetGroup drop constraint DF_DocumentAssetGroup_IsDeleted

alter table Cofoundry.Page drop column IsDeleted;
alter table Cofoundry.PageGroup drop column IsDeleted;
alter table Cofoundry.ImageAssetGroup drop column IsDeleted;
alter table Cofoundry.DocumentAssetGroup drop column IsDeleted;
alter table Cofoundry.PageDirectory drop column IsActive;

-- create unique index UIX_Page_Path on Cofoundry.Page (PageDirectoryId, LocaleId, UrlPath);
-- create unique index UIX_PageDirectory_UrlPath on Cofoundry.PageDirectory (ParentPageDirectoryId, UrlPath);
-- create unique index UIX_PageDirectory_RootDirectory on Cofoundry.PageDirectory (ParentPageDirectoryId);
