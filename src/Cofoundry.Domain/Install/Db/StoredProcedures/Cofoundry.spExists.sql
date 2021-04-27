Use Cofoundry;

drop procedure if exists spRecordExists;

Delimiter $$
Create Procedure spRecordExists( in Comando text, in ErrorMessage varchar(250) ) 
Begin
	set @comando = Comando;
    
	 prepare stp from @comando;
     execute stp;
     
     if ( Found_Rows() > 0 ) then
		signal sqlstate value '45000'
           set Message_Text = ErrorMessage;
     end if;
     
     DEALLOCATE PREPARE stp; 
end $$

Delimiter ;

-- call  spRecordExists('select * from Cofoundry.User where 1=0', 'error' );

 call Cofoundry.spRecordExists( 'select * from Cofoundry.Page p ' 
      ' left outer join Cofoundry.PageVersion pv on pv.PageId = p.PageId ' 
	  ' where pv.WorkFlowStatusId in (1, 4) and PageVersionId is null ', 
      'Invalid page version data. Pages must have at least one draft or published version');

