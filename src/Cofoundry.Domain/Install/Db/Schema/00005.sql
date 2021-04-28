/* Make first and last name optional and add email confirmation */

alter table Cofoundry.User modify column FirstName nvarchar(32) null;
alter table Cofoundry.User modify column LastName nvarchar(32) null;
alter table Cofoundry.User add IsEmailConfirmed bit null;

update Cofoundry.User set IsEmailConfirmed = 0;


alter table Cofoundry.User modify column IsEmailConfirmed bit not null;


/* Role.SpecialistRoleTypeCode > Role.RoleCode */

drop index UIX_Role_SpecialistRoleTypeCode on Cofoundry.Role;

ALTER TABLE Cofoundry.Role
 RENAME COLUMN SpecialistRoleTypeCode TO RoleCode;


create unique index UIX_Role_RoleCode on Cofoundry.Role (RoleCode) ;

/* -------------------------------------------------------------------------------------------------------------------- */

/* Renaming WebDirectory to PageDirectory */

-- WebDirectory -> PageDirectory
rename table Cofoundry.WebDirectory to Cofoundry.PageDirectory;

alter table Cofoundry.PageDirectory rename column WebDirectoryId to PageDirectoryId;

alter table Cofoundry.PageDirectory rename column ParentWebDirectoryId to ParentPageDirectoryId;

/*
	exec sp_rename 'Cofoundry.PK_WebDirectory', 'PK_PageDirectory', 'object'
	exec sp_rename 'Cofoundry.FK_WebDirectory_CreatorUser', 'FK_PageDirectory_CreatorUser', 'object'
	exec sp_rename 'Cofoundry.FK_WebDirectory_ParentWebDirectory', 'FK_PageDirectory_ParentPageDirectory', 'object'
	exec sp_rename 'Cofoundry.PageDirectory.UIX_WebDirectory_UrlPath', 'UIX_PageDirectory_UrlPath', 'index'
*/

-- WebDirectoryLocale -> PageDirectoryLocale
rename table Cofoundry.WebDirectoryLocale to Cofoundry.PageDirectoryLocale;

alter table Cofoundry.PageDirectoryLocale rename column WebDirectoryLocaleId to PageDirectoryLocaleId;

alter table Cofoundry.PageDirectoryLocale rename column WebDirectoryId to PageDirectoryId;

/*
	exec sp_rename 'Cofoundry.PK_WebDirectoryLocale', 'PK_PageDirectoryLocale', 'object'
	exec sp_rename 'Cofoundry.FK_WebDirectoryLocale_CreatorUser', 'FK_PageDirectoryLocale_CreatorUser', 'object'
	exec sp_rename 'Cofoundry.FK_WebDirectoryLocale_Locale', 'FK_PageDirectoryLocale_Locale', 'object'
	exec sp_rename 'Cofoundry.FK_WebDirectoryLocale_WebDirectory', 'FK_PageDirectoryLocale_PageDirectory', 'object'
*/

-- Page.WebDirectoryId

alter table Cofoundry.Page rename column WebDirectoryId to PageDirectoryId;

/*
	exec sp_rename 'Cofoundry.FK_Page_WebDirectory', 'FK_Page_PageDirectory', 'object'
*/

update Cofoundry.EntityDefinition set Name = 'Page Directory' where EntityDefinitionCode = 'COFDIR';



/* Correct PasswordHash terminology */

alter table Cofoundry.User rename column PasswordEncryptionVersion to PasswordHashVersion;


/* Re-naming of template sections to regions and page modules to page blocks */

-- PageTemplateSection to PageTemplateRegion
rename table Cofoundry.PageTemplateSection to Cofoundry.PageTemplateRegion;

alter table Cofoundry.PageTemplateRegion rename column PageTemplateSectionId to PageTemplateRegionId;

alter table Cofoundry.PageTemplateRegion rename column IsCustomEntitySection to IsCustomEntityRegion;

/*
	exec sp_rename 'Cofoundry.PK_PageTemplateSection', 'PK_PageTemplateRegion', 'object'
	exec sp_rename 'Cofoundry.FK_PageTemplateSection_PageTemplate', 'FK_PageTemplateRegion_PageTemplate', 'object'
	exec sp_rename 'Cofoundry.PageTemplateRegion.UIX_PageTemplateSection_Name', 'UIX_PageTemplateRegion_Name', 'index'
*/

-- PageModuleType to PageBlockType

rename table Cofoundry.PageModuleType to Cofoundry.PageBlockType;

alter table Cofoundry.PageBlockType rename column PageModuleTypeId to PageBlockTypeId;

/*
	exec sp_rename 'Cofoundry.PK_PageModuleType', 'PK_PageBlockType', 'object'
	exec sp_rename 'Cofoundry.PageBlockType.UIX_PageModuleType_Name', 'UIX_PageBlockType_Name', 'index'
*/

-- PageModuleTypeTemplate to PageBlockTypeTemplate

rename table Cofoundry.PageModuleTypeTemplate to Cofoundry.PageBlockTypeTemplate;

alter table Cofoundry.PageBlockTypeTemplate rename column PageModuleTypeTemplateId to PageBlockTypeTemplateId;

alter table Cofoundry.PageBlockTypeTemplate rename column PageModuleTypeId to PageBlockTypeId;

/*
	exec sp_rename 'Cofoundry.PK_PageModuleTypeTemplate', 'PK_PageBlockTypeTemplate', 'object'
	exec sp_rename 'Cofoundry.FK_PageModuleTypeTemplate_PageModuleType', 'FK_PageBlockTypeTemplate_PageBlockType', 'object'
*/

-- PageVersionModule to PageVersionBlock

rename table Cofoundry.PageVersionModule to Cofoundry.PageVersionBlock;

alter table Cofoundry.PageVersionBlock rename column PageVersionModuleId to PageVersionBlockId;

alter table Cofoundry.PageVersionBlock  rename column PageTemplateSectionId to PageTemplateRegionId;

alter table Cofoundry.PageVersionBlock rename column PageModuleTypeId to PageBlockTypeId;

alter table Cofoundry.PageVersionBlock rename column PageModuleTypeTemplateId to PageBlockTypeTemplateId;

/*
	exec sp_rename 'Cofoundry.PK_PageVersionModule', 'PK_PageVersionBlock', 'object'
	exec sp_rename 'Cofoundry.FK_PageVersionModule_CreatorUser', 'FK_PageVersionBlock_CreatorUser', 'object'
	exec sp_rename 'Cofoundry.FK_PageVersionModule_PageModuleType', 'FK_PageVersionBlock_PageBlockType', 'object'
	exec sp_rename 'Cofoundry.FK_PageVersionModule_PageModuleTypeTemplate', 'FK_PageVersionBlock_PageBlockTypeTemplate', 'object'
	exec sp_rename 'Cofoundry.FK_PageVersionModule_PageTemplateSection', 'FK_PageVersionBlock_PageTemplateRegion', 'object'
	exec sp_rename 'Cofoundry.FK_PageVersionModule_PageVersion', 'FK_PageVersionBlock_PageVersion', 'object'
*/

-- CustomEntityVersionPageModule to CustomEntityVersionPageBlock

rename table Cofoundry.CustomEntityVersionPageModule to Cofoundry.CustomEntityVersionPageBlock;

alter table Cofoundry.CustomEntityVersionPageBlock rename column CustomEntityVersionPageModuleId to CustomEntityVersionPageBlockId;

alter table Cofoundry.CustomEntityVersionPageBlock rename column PageTemplateSectionId to PageTemplateRegionId;

alter table Cofoundry.CustomEntityVersionPageBlock  rename column PageModuleTypeId to PageBlockTypeId;

alter table Cofoundry.CustomEntityVersionPageBlock rename column PageModuleTypeTemplateId to PageBlockTypeTemplateId;

/*
	exec sp_rename 'Cofoundry.PK_CustomEntityVersionPageModule', 'PK_CustomEntityVersionPageBlock', 'object'
	exec sp_rename 'Cofoundry.FK_CustomEntityVersionPageModule_CustomEntityVersion', 'FK_CustomEntityVersionPageBlock_CustomEntityVersion', 'object'
	exec sp_rename 'Cofoundry.FK_CustomEntityVersionPageModule_PageModuleType', 'FK_CustomEntityVersionPageBlock_PageBlockType', 'object'
	exec sp_rename 'Cofoundry.FK_CustomEntityVersionPageModule_PageModuleTypeTemplate', 'FK_CustomEntityVersionPageBlock_PageBlockTypeTemplate', 'object'
	exec sp_rename 'Cofoundry.FK_CustomEntityVersionPageModule_PageTemplateSection', 'FK_CustomEntityVersionPageBlock_PageTemplateRegion', 'object'
*/

-- remove renamed triggers if this isn't first install

drop trigger if exists Cofoundry.CustomEntityVersionPageModule_CascadeDelete;

drop trigger if exists Cofoundry.PageTemplateSection_CascadeDelete;

drop trigger if exists Cofoundry.PageVersionModule_CascadeDelete;

-- update entity definitions to match the new terms
insert into Cofoundry.EntityDefinition (EntityDefinitionCode, Name) values ('COFPGB', 'Page Version Block');
update Cofoundry.UnstructuredDataDependency set RootEntityDefinitionCode = 'COFPGB' where RootEntityDefinitionCode = 'COFPGM';
update Cofoundry.UnstructuredDataDependency set RelatedEntityDefinitionCode = 'COFPGB' where RelatedEntityDefinitionCode = 'COFPGM';
delete from Cofoundry.EntityDefinition where EntityDefinitionCode = 'COFPGM';

insert into Cofoundry.EntityDefinition (EntityDefinitionCode, Name) values ('COFCEB', 'Custom Entity Version Page Block');
update Cofoundry.UnstructuredDataDependency set RootEntityDefinitionCode = 'COFCEB' where RootEntityDefinitionCode = 'COFCEM';
update Cofoundry.UnstructuredDataDependency set RelatedEntityDefinitionCode = 'COFCEB' where RelatedEntityDefinitionCode = 'COFCEM';
delete from Cofoundry.EntityDefinition where EntityDefinitionCode = 'COFCEM';



/*  Add missing unique indexes, mistakenly created as non-unique */

-- remove duplicates
with pageTemplateDuplicates as (
  select 
	FileName, 
	IsArchived, 
    row_number() over (partition by FileName, IsArchived order by UpdateDate) as RowNumber
  from Cofoundry.PageTemplate
)
delete from Cofoundry.PageTemplate where PageTemplateId in ( Select PageTemplateId from pageTemplateDuplicates where RowNumber > 1); 


with pageBlockDuplicates as (
  select 
	FileName, 
	IsArchived, 
    row_number() over (partition by  FileName, IsArchived order by UpdateDate) as RowNumber
  from Cofoundry.PageBlockType
)
delete from Cofoundry.PageBlockType where PageBlockTypeId in ( Select PageBlockTypeId from pageBlockDuplicates where RowNumber > 1 );



-- recreate indexes

-- drop index UIX_PageTemplate_FullPath on Cofoundry.PageTemplate;
-- drop index UIX_PageTemplate_Name on Cofoundry.PageTemplate;
-- drop index UIX_PageTemplateRegion_Name on Cofoundry.PageTemplateRegion;
-- drop index UIX_PageBlockType_Name on Cofoundry.PageBlockType;
drop index UIX_User_IsSystemAccount on Cofoundry.User;


create unique index UIX_PageTemplate_FullPath on Cofoundry.PageTemplate (FullPath) ;
create unique index UIX_PageTemplate_Name on Cofoundry.PageTemplate (Name) ;
create unique index UIX_PageTemplateRegion_Name on Cofoundry.PageTemplateRegion (PageTemplateId, Name);
create unique index UIX_PageBlockType_Name on Cofoundry.PageBlockType (Name) ;
create unique index UIX_User_IsSystemAccount on Cofoundry.User (IsSystemAccount) ;

