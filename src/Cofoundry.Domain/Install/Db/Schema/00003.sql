/* Modernize the page meta data properties */

alter table Cofoundry.PageVersion modify column OpenGraphTitle nvarchar (300) null;
alter table Cofoundry.PageVersion modify column MetaDescription nvarchar (300) null;
alter table Cofoundry.PageVersion modify column Title nvarchar (300) not null;
alter table Cofoundry.PageVersion drop column MetaKeywords;

