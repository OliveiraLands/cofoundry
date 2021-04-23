
/*
	exec sp_rename 'Cofoundry.CustomEntityVersionPageBlock.PageModuleTypeTemplateId' , 'PageBlockTypeTemplateId', 'column'
*/

use cofoundry;

DROP PROCEDURE IF EXISTS cofoundry.sp_rename;

DELIMITER $$

CREATE PROCEDURE cofoundry.sp_rename (in par1 varchar(250), in par2  varchar(250), in par3  varchar(250) )
BEGIN
	  set @sql = 'SIGNAL SQLSTATE ''45000'' ';
			 -- SET MESSAGE_TEXT = 'Order No not found in orders table';';
             
	  if ( par3 = 'table' or par3 is null ) then
          set @sql = 'rename table ';
	      set @pointFirst = locate('.', @par1);
          set @databaseName = '';
		  set @oldName = @par1;
          
          if ( @pointFirst > 0 )  then
			set @databaseName = substring( @par1, 1, @pointFirst);
            set @oldName = substring( @par1, @pointFirst+1, length(@par1) );
          end if
          
          
          
      end if;
      
      
END $$
