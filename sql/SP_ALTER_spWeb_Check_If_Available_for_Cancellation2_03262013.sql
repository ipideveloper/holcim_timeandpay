
/****** Object:  StoredProcedure [dbo].[spWeb_Check_If_Available_for_Cancellation2]    Script Date: 03/26/2013 12:56:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------------------------------------------------------------------------------

--drop procedure [spWeb_Check_If_Available_for_Cancellation2]
ALTER PROCEDURE [dbo].[spWeb_Check_If_Available_for_Cancellation2]
    (
      @ref_no VARCHAR(15) ,
      @employee_id VARCHAR(15) ,
      @nResult INT OUTPUT
    )
--WITH ENCRYPTION
AS 
    BEGIN
        DECLARE @nOutput INT
        DECLARE @app_type VARCHAR(10)
        DECLARE @stat INT
        DECLARE @sector_code VARCHAR(15)
        DECLARE @approver_level VARCHAR(15)
        DECLARE @approver_id VARCHAR(15)
		
        SET @approver_id = @employee_id
		
        SELECT  @sector_code = sector_code
        FROM    sys_payroll_area (NOLOCK)
        WHERE   payarea_code = ( SELECT payroll_area
                                 FROM   empjob_info (NOLOCK)
                                 WHERE  employee_id = @employee_id
                               ) 
		
        IF EXISTS ( SELECT  app_type
                    FROM    z_refno_master (NOLOCK)
                    WHERE   ref_no = @ref_no ) 
            BEGIN
                SELECT  @app_type = app_type
                FROM    z_refno_master (NOLOCK)
                WHERE   ref_no = @ref_no
				--Revised Code of Andrew Start 11-17-2011 Start
				--LV Start
                IF @app_type = 'LV' 
                    BEGIN
                        SELECT  @employee_id = employee_id
                        FROM    z_leave_header
                        WHERE   ref_no = @ref_no
                                AND status IN ( 1, 2, 3 )
                        EXEC [dbo].[spWeb_Get_approvers_level] @employee_id = @employee_id,
                            @approver_id = @approver_id,
                            @approver_level = @approver_level OUTPUT
			        
                        IF @approver_level = 'FINAL' 
                            BEGIN
                                IF EXISTS ( SELECT  a.status
                                            FROM    z_leave_header a ( NOLOCK )
                                                    JOIN empjob_info b ( NOLOCK ) ON a.employee_id = b.employee_id
                                                    JOIN sys_payroll_area c ( NOLOCK ) ON b.payroll_area = c.payarea_code
                                            WHERE   a.ref_no = @ref_no
                                                    AND a.status IN ( 1, 2, 3 ) --AND c.sector_code = @sector_code 
                                                    ) 
                                    BEGIN
                                        SET @nOutput = 1
                                    END
                                ELSE 
                                    BEGIN
                                        IF EXISTS ( SELECT  status
                                                    FROM    z_leave_header (NOLOCK)
                                                    WHERE   ref_no = @ref_no
                                                            AND status = 4 ) 
                                            BEGIN
                                                SET @nOutput = 2				
                                            END
                                        ELSE 
                                            BEGIN
                                                SET @nOutput = 3				
                                            END
                                    END 
                            END
                        ELSE 
                            BEGIN
                                SET @nOutput = 4
                            END
					    
                    END
				--LV End
				--OB Start
                IF @app_type = 'OB' 
                    BEGIN
                        SELECT  @employee_id = employee_id
                        FROM    z_ob_header
                        WHERE   ref_no = @ref_no
                                AND status IN ( 1, 2, 3 )
                        EXEC [dbo].[spWeb_Get_approvers_level] @employee_id = @employee_id,
                            @approver_id = @approver_id,
                            @approver_level = @approver_level OUTPUT
				            
                        IF @approver_level = 'FINAL' 
                            BEGIN
                                IF EXISTS ( SELECT  a.status
                                            FROM    z_ob_header a ( NOLOCK )
                                                    JOIN empjob_info b ( NOLOCK ) ON a.employee_id = b.employee_id
                                                    JOIN sys_payroll_area c ( NOLOCK ) ON b.payroll_area = c.payarea_code
                                            WHERE   a.ref_no = @ref_no
                                                    AND a.status IN ( 1, 2, 3 ) --AND c.sector_code = @sector_code 
                                                    ) 
                                    BEGIN
						
                                        SET @nOutput = 1
							              
                                    END
                                ELSE 
                                    BEGIN
                                        IF EXISTS ( SELECT  status
                                                    FROM    z_ob_header (NOLOCK)
                                                    WHERE   ref_no = @ref_no
                                                            AND status = 4 ) 
                                            BEGIN
                                                SET @nOutput = 2				
                                            END
                                        ELSE 
                                            BEGIN
                                                SET @nOutput = 3				
                                            END
                                    END  
                            END
                        ELSE 
                            BEGIN
                                SET @nOutput = 4
                            END
                    END
				--OB End
				--OT Start
                IF @app_type = 'OT' 
                    BEGIN
                        SELECT  @employee_id = employee_id
                        FROM    z_ot_header
                        WHERE   ref_no = @ref_no
                                AND status IN ( 1, 2, 3 )
                        EXEC [dbo].[spWeb_Get_approvers_level] @employee_id = @employee_id,
                            @approver_id = @approver_id,
                            @approver_level = @approver_level OUTPUT
							
                        IF @approver_level = 'FINAL' 
                            BEGIN
                                IF EXISTS ( SELECT  a.status
                                            FROM    z_ot_header a ( NOLOCK )
                                                    JOIN empjob_info b ( NOLOCK ) ON a.employee_id = b.employee_id
                                                    JOIN sys_payroll_area c ( NOLOCK ) ON b.payroll_area = c.payarea_code
                                            WHERE   a.ref_no = @ref_no
                                                    AND a.status IN ( 1, 2, 3 ) --AND c.sector_code = @sector_code 
                                                    ) 
                                    BEGIN
                                        SET @nOutput = 1
                                    END
                                ELSE 
                                    BEGIN
                                        IF EXISTS ( SELECT  status
                                                    FROM    z_ot_header (NOLOCK)
                                                    WHERE   ref_no = @ref_no
                                                            AND status = 4 ) 
                                            BEGIN
                                                SET @nOutput = 2				
                                            END
                                        ELSE 
                                            BEGIN
                                                SET @nOutput = 3				
                                            END
                                    END 
                            END
                        ELSE 
                            BEGIN
                                SET @nOutput = 4
                            END
                    END
				--OT End
				--OC Start
                IF @app_type = 'OC' 
                    BEGIN
                        SELECT  @employee_id = employee_id
                        FROM    z_on_call_header
                        WHERE   ref_no = @ref_no
                                AND status IN ( 1, 2, 3 )
                        EXEC [dbo].[spWeb_Get_approvers_level] @employee_id = @employee_id,
                            @approver_id = @approver_id,
                            @approver_level = @approver_level OUTPUT
								
                        IF @approver_level = 'FINAL' 
                            BEGIN
                                IF EXISTS ( SELECT  a.status
                                            FROM    z_on_call_header a ( NOLOCK )
                                                    JOIN empjob_info b ( NOLOCK ) ON a.employee_id = b.employee_id
                                                    JOIN sys_payroll_area c ( NOLOCK ) ON b.payroll_area = c.payarea_code
                                            WHERE   a.ref_no = @ref_no
                                                    AND a.status IN ( 1, 2, 3 ) --AND c.sector_code = @sector_code 
                                                    ) 
                                    BEGIN
                                        SET @nOutput = 1
                                    END
                                ELSE 
                                    BEGIN
                                        IF EXISTS ( SELECT  status
                                                    FROM    z_on_Call_header (NOLOCK)
                                                    WHERE   ref_no = @ref_no
                                                            AND status = 4 ) 
                                            BEGIN
                                                SET @nOutput = 2				
                                            END
                                        ELSE 
                                            BEGIN
                                                SET @nOutput = 3				
                                            END
                                    END
                            END
                        ELSE 
                            BEGIN
                                SET @nOutput = 4 
                            END
                    END
				--OC End
				--SH Start
                IF @app_type = 'SH' 
                    BEGIN
						--select @employee_id = employee_id from z_shift_header where ref_no = @ref_no and approve_tag in(1,2,3)
						--EXEC	[dbo].[spWeb_Get_approvers_level]
						--		@employee_id = @employee_id,
						--		@approver_id = @approver_id,
						--		@approver_level = @approver_level OUTPUT
				              
						--	  if @approver_level = 'FINAL'
						--	     Begin
						--					IF EXISTS(SELECT a.approve_tag from z_shift_header a (nolock)
						--					   join empjob_info b (nolock)
						--					   on a.employee_id = b.employee_id 
						--					   join sys_payroll_area c (nolock)
						--					   on b.payroll_area = c.payarea_code
						--					   where a.ref_no = @ref_no and a.approve_tag in (1,2,3)
						--					   and c.sector_code = @sector_code )
						--							BEGIN
						--									set @nOutput = 1
						--							END
						--					ELSE
						--						    BEGIN
						--									if exists(select approve_tag from z_shift_header (nolock) where ref_no = @ref_no
						--									   and approve_tag = 4) 
						--										   BEGIN
						--													set @nOutput = 2				
						--										   END
						--									ELSE
						--										   BEGIN
						--												    set @nOutput = 3				
						--										   END
						--							END
						--	     End
						--	  else
						--	     Begin
                        SET @nOutput = 4
							     --End  
                    END
				--SH End
				--Revised Code of Andrew End 11-17-2011 End		
            END
        ELSE 
            BEGIN
				--Don't Exist
                SET @nOutput = 4 
            END
		
		
        SET @nResult = @nOutput
    END
