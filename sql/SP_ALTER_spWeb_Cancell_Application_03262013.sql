
/****** Object:  StoredProcedure [dbo].[spWeb_Cancell_Application]    Script Date: 03/26/2013 12:56:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
------------------------------------------------------------------------------------------------------------
--drop procedure [spWeb_Cancell_Application]

ALTER PROCEDURE [dbo].[spWeb_Cancell_Application]
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
	--Declare @ProcessAlready as int
        DECLARE @cutoff AS DATETIME
        DECLARE @person_no VARCHAR(15)
        DECLARE @date_now DATETIME
        DECLARE @status INT
        DECLARE @emp_id VARCHAR(15)
        DECLARE @workdate DATETIME
        DECLARE @half_day BIT
        DECLARE @without_pay BIT
        DECLARE @app VARCHAR(15)
        DECLARE @result BIT
        DECLARE @return_value BIT
        DECLARE @applied_days DECIMAL(3, 2)
        DECLARE @leave_excess DECIMAL(6, 2)
        DECLARE @buffer_id VARCHAR(15) 
        DECLARE @buffer_avail DECIMAL(6, 2)
        DECLARE @union_leave VARCHAR(15)
        DECLARE @payperiod DATETIME
        DECLARE @approval_date DATETIME	
	
        SELECT  @date_now = GETDATE()
        SELECT  @sector_code = sector_code
        FROM    sys_payroll_area (NOLOCK)
        WHERE   payarea_code = ( SELECT payroll_area
                                 FROM   empjob_info (NOLOCK)
                                 WHERE  employee_id = @employee_id
                               )
														
        SELECT  @status = status
        FROM    ( SELECT DISTINCT
                            status
                  FROM      z_ot_header  (NOLOCK)
                  WHERE     ref_no = @ref_no
                  UNION
                  SELECT DISTINCT
                            status
                  FROM      z_ob_header (NOLOCK)
                  WHERE     ref_no = @ref_no
                  UNION
                  SELECT DISTINCT
                            status
                  FROM      z_on_call_header  (NOLOCK)
                  WHERE     ref_no = @ref_no
                  UNION
                  SELECT DISTINCT
                            status
                  FROM      z_leave_header  (NOLOCK)
                  WHERE     ref_no = @ref_no
                  UNION
                  SELECT TOP 1
                            approve_tag
                  FROM      z_shift_header  (NOLOCK)
                  WHERE     ref_no = @ref_no
                ) AS ref_table
								
        IF EXISTS ( SELECT  app_type
                    FROM    z_refno_master (NOLOCK)
                    WHERE   ref_no = @ref_no ) 
            BEGIN
                SELECT  @app_type = app_type
                FROM    z_refno_master (NOLOCK)
                WHERE   ref_no = @ref_no
		--1 start
                IF @app_type = 'LV' 
                    BEGIN
			--Added Code By Andrew 11-06-2011 Start
			--temporary commented by EDB--SELECT @ProcessAlready = processed_tag from z_leave_details where ref_no = @ref_no
                        SELECT  @person_no = employee_id
                        FROM    z_leave_header
                        WHERE   ref_no = @ref_no
                        SELECT  @cutoff = processing_datetime ,
                                @payperiod = payroll_period
                        FROM    payroll_period
                        WHERE   payroll_frequency = ( SELECT TOP 1
                                                              pay_frequency
                                                      FROM    sys_payroll_area a
                                                              INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                      WHERE   employee_id = @person_no
                                                    )
                                AND ( ( ( SELECT    date_from
                                          FROM      z_leave_header
                                          WHERE     ref_no = @ref_no
                                        ) BETWEEN m_from
                                          AND     m_to )
                                      OR ( ( SELECT date_to
                                             FROM   z_leave_header
                                             WHERE  ref_no = @ref_no
                                           ) BETWEEN m_from
                                             AND     m_to )
                                    )
                                AND unit_id = ( SELECT TOP 1
                                                        sector_code
                                                FROM    sys_payroll_area a
                                                        INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                WHERE   employee_id = @person_no
                                              )
			
                        IF @cutoff IS NOT NULL 
                            BEGIN
                                IF EXISTS ( SELECT TOP 1
                                                    approval_date
                                            FROM    z_approvers_approval
                                            WHERE   ref_no = @ref_no
                                                    AND approver_level = 'FINAL'
                                                    AND approval_date IS NOT NULL ) 
                                    BEGIN
                                        SELECT TOP 1
                                                @approval_date = approval_date
                                        FROM    z_approvers_approval
                                        WHERE   ref_no = @ref_no
                                                AND approver_level = 'FINAL'
                                                AND approval_date IS NOT NULL	
							
                                        IF @approval_date > @cutoff 
                                            BEGIN					
                                                SELECT TOP 1
                                                        @cutoff = processing_datetime
                                                FROM    payroll_period
                                                WHERE   payroll_frequency = ( SELECT TOP 1
                                                              pay_frequency
                                                              FROM
                                                              sys_payroll_area a
                                                              INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                              WHERE
                                                              employee_id = @person_no
                                                              )
                                                        AND payroll_period > @payperiod
                                                        AND unit_id = ( SELECT TOP 1
                                                              sector_code
                                                              FROM
                                                              sys_payroll_area a
                                                              INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                              WHERE
                                                              employee_id = @person_no
                                                              )
                                                ORDER BY payroll_period ASC
                                            END
							
                                    END
                            END
                        IF @cutoff < @date_now 
                            BEGIN
                                SET @nOutput = 5
                            END
                        ELSE
			--Added Code By Andrew 11-06-2011 End
                            BEGIN
                                IF EXISTS ( SELECT  a.status
                                            FROM    z_leave_header a ( NOLOCK )
                                                    JOIN empjob_info b ( NOLOCK ) ON a.employee_id = b.employee_id
                                                    JOIN sys_payroll_area c ( NOLOCK ) ON b.payroll_area = c.payarea_code
                                            WHERE   a.ref_no = @ref_no
                                                    AND a.status IN ( 1, 2, 3 ) --AND c.sector_code = @sector_code 
                                                    ) 
                                    BEGIN
                                        DECLARE @l_stat INT
                                        SELECT  @l_stat = status
                                        FROM    z_leave_header (NOLOCK)
                                        WHERE   ref_no = @ref_no
                                        UPDATE  z_leave_header
                                        SET     status = 4 ,
                                                modified_by = @employee_id ,
                                                date_modified = GETDATE()
                                        WHERE   ref_no = @ref_no
                                        SET @nOutput = 1
                                        IF @l_stat = 2 
                                            BEGIN
						--*************************************************************************************************
						--* Modified By: Orlando M. Algentera Jr.
						--* Date Modified: December 19, 2012
						--* Purpose: Return the deducted leave balance to the employee. Compute accordingly
						--*************************************************************************************************
						
						--declare @days money
						--declare @app char(15)
						--declare @emp varchar(15)
						--select @app = application_type from z_leave_header (nolock) where ref_no = @ref_no
						--select @days = convert(money,[days]) from z_leave_header (nolock) where ref_no = @ref_no
						--select @emp = employee_id from z_leave_header (nolock) where ref_no = @ref_no
						--UPDATE leavemaster set leave_avail = leave_avail + @days,leave_taken = leave_taken - @days where employee_id = @emp and leave_type = @app
                                                SELECT TOP 1
                                                        @union_leave = leave_type
                                                FROM    leave_table
                                                WHERE   union_leave = 1
										
                                                DECLARE leave_app_cur CURSOR
                                                FOR
                                                    SELECT  a.employee_id ,
                                                            b.workdate ,
                                                            b.half_day ,
                                                            b.without_pay ,
                                                            a.application_type
                                                    FROM    z_leave_header (NOLOCK) a
                                                            INNER JOIN z_leave_details b ON a.ref_no = b.ref_no
                                                    WHERE   a.ref_no = @ref_no	
						
                                                OPEN leave_app_cur
                                                FETCH NEXT FROM leave_app_cur INTO @emp_id,
                                                    @workdate, @half_day,
                                                    @without_pay, @app
                                                WHILE @@FETCH_STATUS = 0 
                                                    BEGIN
                                                        IF @half_day = 1 
                                                            BEGIN
                                                              SET @applied_days = .5
                                                            END
                                                        ELSE 
                                                            BEGIN
                                                              SET @applied_days = 1
                                                            END
								
                                                        EXEC @return_value = [dbo].[spWeb_Validate_Holiday] @employee_id = @emp_id,
                                                            @work_date = @workdate,
                                                            @result = @result OUTPUT
										
                                                        IF @result <> 1 
                                                            BEGIN
                                                              IF LTRIM(RTRIM(@app)) = '01'
                                                              OR LTRIM(RTRIM(@app)) = '02' 
                                                              BEGIN
											
                                                              IF LTRIM(RTRIM(@app)) = '01' 
                                                              SET @buffer_id = '57'
                                                              IF LTRIM(RTRIM(@app)) = '02' 
                                                              SET @buffer_id = '59'
														
                                                              SELECT
                                                              @leave_excess = leave_taken
                                                              - leave_earned
                                                              FROM
                                                              leavemaster
                                                              WHERE
                                                              employee_id = @emp_id
                                                              AND leave_type = @app
                                                              AND @workdate BETWEEN start_validity
                                                              AND
                                                              DATEADD(ss,
                                                              86399,
                                                              end_validity)
												
                                                              IF @leave_excess > 0
                                                              AND @leave_excess >= @applied_days 
                                                              BEGIN
													
                                                              UPDATE
                                                              leavemaster
                                                              SET
                                                              leave_taken = leave_taken
                                                              - @applied_days ,
                                                              leave_avail = leave_avail
                                                              + @applied_days
                                                              WHERE
                                                              employee_id = @emp_id
                                                              AND leave_type = @app
                                                              AND @workdate BETWEEN start_validity
                                                              AND
                                                              DATEADD(ss,
                                                              86399,
                                                              end_validity)
														
                                                              END
                                                              ELSE 
                                                              IF @leave_excess < @applied_days
                                                              AND @leave_excess >= .5 
                                                              BEGIN
														
                                                              SELECT
                                                              @buffer_avail = leave_taken
                                                              FROM
                                                              leavemaster
                                                              WHERE
                                                              employee_id = @emp_id
                                                              AND leave_type = @buffer_id
                                                              AND @workdate BETWEEN start_validity
                                                              AND
                                                              DATEADD(ss,
                                                              86399,
                                                              end_validity)
														
                                                              IF @buffer_avail > 0
                                                              AND @buffer_avail >= @applied_days
                                                              - .5 
                                                              BEGIN
                                                              UPDATE
                                                              leavemaster
                                                              SET
                                                              leave_taken = leave_taken
                                                              - ( @applied_days
                                                              - .5 ) ,
                                                              leave_avail = leave_avail
                                                              + ( @applied_days
                                                              - .5 )
                                                              WHERE
                                                              employee_id = @emp_id
                                                              AND leave_type = @buffer_id
                                                              AND @workdate BETWEEN start_validity
                                                              AND
                                                              DATEADD(ss,
                                                              86399,
                                                              end_validity)
																
                                                              UPDATE
                                                              leavemaster
                                                              SET
                                                              leave_taken = leave_taken
                                                              - .5 ,
                                                              leave_avail = leave_avail
                                                              + .5
                                                              WHERE
                                                              employee_id = @emp_id
                                                              AND leave_type = @app
                                                              AND @workdate BETWEEN start_validity
                                                              AND
                                                              DATEADD(ss,
                                                              86399,
                                                              end_validity)
														
                                                              END
															
                                                              ELSE 
                                                              IF ISNULL(@buffer_avail,
                                                              0) < .5 
                                                              BEGIN
                                                              UPDATE
                                                              leavemaster
                                                              SET
                                                              leave_taken = leave_taken
                                                              - @applied_days ,
                                                              leave_avail = leave_avail
                                                              + @applied_days
                                                              WHERE
                                                              employee_id = @emp_id
                                                              AND leave_type = @app
                                                              AND @workdate BETWEEN start_validity
                                                              AND
                                                              DATEADD(ss,
                                                              86399,
                                                              end_validity)
                                                              END
                                                              END
                                                              ELSE 
                                                              BEGIN
                                                              SELECT
                                                              @buffer_avail = leave_taken
                                                              FROM
                                                              leavemaster
                                                              WHERE
                                                              employee_id = @emp_id
                                                              AND leave_type = @buffer_id
                                                              AND @workdate BETWEEN start_validity
                                                              AND
                                                              DATEADD(ss,
                                                              86399,
                                                              end_validity)
														
                                                              IF @buffer_avail > 0
                                                              AND @buffer_avail >= @applied_days 
                                                              BEGIN
                                                              UPDATE
                                                              leavemaster
                                                              SET
                                                              leave_taken = leave_taken
                                                              - @applied_days ,
                                                              leave_avail = leave_avail
                                                              + @applied_days
                                                              WHERE
                                                              employee_id = @emp_id
                                                              AND leave_type = @buffer_id
                                                              AND @workdate BETWEEN start_validity
                                                              AND
                                                              DATEADD(ss,
                                                              86399,
                                                              end_validity)
														
                                                              END
                                                              ELSE 
                                                              IF @buffer_avail < @applied_days
                                                              AND @buffer_avail >= .5 
                                                              BEGIN
                                                              UPDATE
                                                              leavemaster
                                                              SET
                                                              leave_taken = leave_taken
                                                              - .5 ,
                                                              leave_avail = leave_avail
                                                              + .5
                                                              WHERE
                                                              employee_id = @emp_id
                                                              AND leave_type = @buffer_id
                                                              AND @workdate BETWEEN start_validity
                                                              AND
                                                              DATEADD(ss,
                                                              86399,
                                                              end_validity)
																
                                                              UPDATE
                                                              leavemaster
                                                              SET
                                                              leave_taken = leave_taken
                                                              - .5 ,
                                                              leave_avail = leave_avail
                                                              + .5
                                                              WHERE
                                                              employee_id = @emp_id
                                                              AND leave_type = @app
                                                              AND @workdate BETWEEN start_validity
                                                              AND
                                                              DATEADD(ss,
                                                              86399,
                                                              end_validity)
                                                              END
                                                              ELSE --If isnull(@buffer_avail,0) <.5 
                                                              BEGIN
                                                              UPDATE
                                                              leavemaster
                                                              SET
                                                              leave_taken = leave_taken
                                                              - @applied_days ,
                                                              leave_avail = leave_avail
                                                              + @applied_days
                                                              WHERE
                                                              employee_id = @emp_id
                                                              AND leave_type = @app
                                                              AND @workdate BETWEEN start_validity
                                                              AND
                                                              DATEADD(ss,
                                                              86399,
                                                              end_validity)
                                                              END
															
                                                              END
												
                                                              END 
                                                              ELSE 
                                                              IF LTRIM(RTRIM(@app)) <> '52'
                                                              OR LTRIM(RTRIM(@app)) <> @union_leave 
                                                              BEGIN
                                                              UPDATE
                                                              leavemaster
                                                              SET
                                                              leave_taken = leave_taken
                                                              - @applied_days ,
                                                              leave_avail = leave_avail
                                                              + @applied_days
                                                              WHERE
                                                              employee_id = @emp_id
                                                              AND leave_type = @app
                                                              AND @workdate BETWEEN start_validity
                                                              AND
                                                              DATEADD(ss,
                                                              86399,
                                                              end_validity)
                                                              END
                                                            END		
                                                        FETCH NEXT FROM
                                                            leave_app_cur INTO @emp_id,
                                                            @workdate,
                                                            @half_day,
                                                            @without_pay, @app	
                                                    END
                                                CLOSE leave_app_cur
                                                DEALLOCATE leave_app_cur
                                            END
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
                    END
		--1 end
		--2 Start
                IF @app_type = 'OB' 
                    BEGIN
			--Added Code By Andrew 11-06-2011 Start
			--temporary commented by EDB--SELECT @ProcessAlready = processed_tag from z_on_call_header where ref_no = @ref_no
                        SELECT  @person_no = employee_id
                        FROM    z_ob_header
                        WHERE   ref_no = @ref_no
                        SELECT  @cutoff = processing_datetime ,
                                @payperiod = payroll_period
                        FROM    payroll_period
                        WHERE   payroll_frequency = ( SELECT TOP 1
                                                              pay_frequency
                                                      FROM    sys_payroll_area a
                                                              INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                      WHERE   employee_id = @person_no
                                                    )
                                AND ( ( ( SELECT    date_from
                                          FROM      z_leave_header
                                          WHERE     ref_no = @ref_no
                                        ) BETWEEN m_from
                                          AND     m_to )
                                      OR ( ( SELECT date_to
                                             FROM   z_leave_header
                                             WHERE  ref_no = @ref_no
                                           ) BETWEEN m_from
                                             AND     m_to )
                                    )
                                AND unit_id = ( SELECT TOP 1
                                                        sector_code
                                                FROM    sys_payroll_area a
                                                        INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                WHERE   employee_id = @person_no
                                              )
			
                        IF @cutoff IS NOT NULL 
                            BEGIN
                                IF EXISTS ( SELECT TOP 1
                                                    approval_date
                                            FROM    z_approvers_approval
                                            WHERE   ref_no = @ref_no
                                                    AND approver_level = 'FINAL'
                                                    AND approval_date IS NOT NULL ) 
                                    BEGIN
                                        SELECT TOP 1
                                                @approval_date = approval_date
                                        FROM    z_approvers_approval
                                        WHERE   ref_no = @ref_no
                                                AND approver_level = 'FINAL'
                                                AND approval_date IS NOT NULL	
							
                                        IF @approval_date > @cutoff 
                                            BEGIN					
                                                SELECT TOP 1
                                                        @cutoff = processing_datetime
                                                FROM    payroll_period
                                                WHERE   payroll_frequency = ( SELECT TOP 1
                                                              pay_frequency
                                                              FROM
                                                              sys_payroll_area a
                                                              INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                              WHERE
                                                              employee_id = @person_no
                                                              )
                                                        AND payroll_period > @payperiod
                                                        AND unit_id = ( SELECT TOP 1
                                                              sector_code
                                                              FROM
                                                              sys_payroll_area a
                                                              INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                              WHERE
                                                              employee_id = @person_no
                                                              )
                                                ORDER BY payroll_period ASC
                                            END
							
                                    END
                            END
                        IF @cutoff < @date_now 
                            BEGIN
                                SET @nOutput = 5
                            END
                        ELSE
			--Added Code by Andrew 11-06-2011 End
                            BEGIN
                                IF EXISTS ( SELECT  a.status
                                            FROM    z_ob_header a ( NOLOCK )
                                                    JOIN empjob_info b ( NOLOCK ) ON a.employee_id = b.employee_id
                                                    JOIN sys_payroll_area c ( NOLOCK ) ON b.payroll_area = c.payarea_code
                                            WHERE   a.ref_no = @ref_no
                                                    AND a.status IN ( 1, 2, 3 ) --AND c.sector_code = @sector_code 
                                                    ) 
                                    BEGIN
                                        UPDATE  z_ob_header
                                        SET     status = 4 ,
                                                modified_by = @employee_id ,
                                                date_modified = GETDATE()
                                        WHERE   ref_no = @ref_no
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
							 
                    END	
		--2 End
		--3 Start
                IF @app_type = 'OT' 
                    BEGIN
			--Added Code by Andrew 11-06-2011 Start
			--temporary commented by EDB--SELECT @ProcessAlready = processed_tag from z_ot_header where ref_no = @ref_no
                        SELECT  @person_no = employee_id
                        FROM    z_ot_header
                        WHERE   ref_no = @ref_no
                        SELECT  @cutoff = processing_datetime ,
                                @payperiod = payroll_period
                        FROM    payroll_period
                        WHERE   payroll_frequency = ( SELECT TOP 1
                                                              pay_frequency
                                                      FROM    sys_payroll_area a
                                                              INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                      WHERE   employee_id = @person_no
                                                    )
                                AND ( ( ( SELECT    date_from
                                          FROM      z_leave_header
                                          WHERE     ref_no = @ref_no
                                        ) BETWEEN m_from
                                          AND     m_to )
                                      OR ( ( SELECT date_to
                                             FROM   z_leave_header
                                             WHERE  ref_no = @ref_no
                                           ) BETWEEN m_from
                                             AND     m_to )
                                    )
                                AND unit_id = ( SELECT TOP 1
                                                        sector_code
                                                FROM    sys_payroll_area a
                                                        INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                WHERE   employee_id = @person_no
                                              )
			
                        IF @cutoff IS NOT NULL 
                            BEGIN
                                IF EXISTS ( SELECT TOP 1
                                                    approval_date
                                            FROM    z_approvers_approval
                                            WHERE   ref_no = @ref_no
                                                    AND approver_level = 'FINAL'
                                                    AND approval_date IS NOT NULL ) 
                                    BEGIN
                                        SELECT TOP 1
                                                @approval_date = approval_date
                                        FROM    z_approvers_approval
                                        WHERE   ref_no = @ref_no
                                                AND approver_level = 'FINAL'
                                                AND approval_date IS NOT NULL	
							
                                        IF @approval_date > @cutoff 
                                            BEGIN					
                                                SELECT TOP 1
                                                        @cutoff = processing_datetime
                                                FROM    payroll_period
                                                WHERE   payroll_frequency = ( SELECT TOP 1
                                                              pay_frequency
                                                              FROM
                                                              sys_payroll_area a
                                                              INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                              WHERE
                                                              employee_id = @person_no
                                                              )
                                                        AND payroll_period > @payperiod
                                                        AND unit_id = ( SELECT TOP 1
                                                              sector_code
                                                              FROM
                                                              sys_payroll_area a
                                                              INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                              WHERE
                                                              employee_id = @person_no
                                                              )
                                                ORDER BY payroll_period ASC
                                            END
							
                                    END
                            END
                        IF @cutoff < @date_now 
                            BEGIN
                                SET @nOutput = 5
                            END
                               
                        ELSE
					    
					    --Added Code by Andrew 11-06-2011 End
                            BEGIN
                                IF EXISTS ( SELECT  a.status
                                            FROM    z_ot_header a ( NOLOCK )
                                                    JOIN empjob_info b ( NOLOCK ) ON a.employee_id = b.employee_id
                                                    JOIN sys_payroll_area c ( NOLOCK ) ON b.payroll_area = c.payarea_code
                                            WHERE   a.ref_no = @ref_no
                                                    AND a.status IN ( 1, 2, 3 ) --AND c.sector_code = @sector_code 
                                                    ) 
                                    BEGIN
                                        UPDATE  z_ot_header
                                        SET     status = 4 ,
                                                modified_by = @employee_id ,
                                                date_modified = GETDATE()
                                        WHERE   ref_no = @ref_no
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
                    END		
                IF @app_type = 'OC' 
                    BEGIN
			--Added Code By Andrew 11-06-2011 Start
			--temporary commented by EDB--SELECT @ProcessAlready = processed_tag from z_on_call_header where ref_no = @ref_no
                        SELECT  @person_no = employee_id
                        FROM    z_on_call_header
                        WHERE   ref_no = @ref_no
                        SELECT  @cutoff = processing_datetime ,
                                @payperiod = payroll_period
                        FROM    payroll_period
                        WHERE   payroll_frequency = ( SELECT TOP 1
                                                              pay_frequency
                                                      FROM    sys_payroll_area a
                                                              INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                      WHERE   employee_id = @person_no
                                                    )
                                AND ( ( ( SELECT    date_from
                                          FROM      z_leave_header
                                          WHERE     ref_no = @ref_no
                                        ) BETWEEN m_from
                                          AND     m_to )
                                      OR ( ( SELECT date_to
                                             FROM   z_leave_header
                                             WHERE  ref_no = @ref_no
                                           ) BETWEEN m_from
                                             AND     m_to )
                                    )
                                AND unit_id = ( SELECT TOP 1
                                                        sector_code
                                                FROM    sys_payroll_area a
                                                        INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                WHERE   employee_id = @person_no
                                              )
			
                        IF @cutoff IS NOT NULL 
                            BEGIN
                                IF EXISTS ( SELECT TOP 1
                                                    approval_date
                                            FROM    z_approvers_approval
                                            WHERE   ref_no = @ref_no
                                                    AND approver_level = 'FINAL'
                                                    AND approval_date IS NOT NULL ) 
                                    BEGIN
                                        SELECT TOP 1
                                                @approval_date = approval_date
                                        FROM    z_approvers_approval
                                        WHERE   ref_no = @ref_no
                                                AND approver_level = 'FINAL'
                                                AND approval_date IS NOT NULL	
							
                                        IF @approval_date > @cutoff 
                                            BEGIN					
                                                SELECT TOP 1
                                                        @cutoff = processing_datetime
                                                FROM    payroll_period
                                                WHERE   payroll_frequency = ( SELECT TOP 1
                                                              pay_frequency
                                                              FROM
                                                              sys_payroll_area a
                                                              INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                              WHERE
                                                              employee_id = @person_no
                                                              )
                                                        AND payroll_period > @payperiod
                                                        AND unit_id = ( SELECT TOP 1
                                                              sector_code
                                                              FROM
                                                              sys_payroll_area a
                                                              INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                              WHERE
                                                              employee_id = @person_no
                                                              )
                                                ORDER BY payroll_period ASC
                                            END
							
                                    END
                            END
                        IF @cutoff < @date_now 
                            BEGIN
                                SET @nOutput = 5
                            END
							 
                        ELSE
						--Added Code By Andrew 11-06-2011 End
                            BEGIN
                                IF EXISTS ( SELECT  a.status
                                            FROM    z_on_call_header a ( NOLOCK )
                                                    JOIN empjob_info b ( NOLOCK ) ON a.employee_id = b.employee_id
                                                    JOIN sys_payroll_area c ( NOLOCK ) ON b.payroll_area = c.payarea_code
                                            WHERE   a.ref_no = @ref_no
                                                    AND a.status IN ( 1, 2, 3 ) --AND c.sector_code = @sector_code 
                                                    ) 
                                    BEGIN
                                        UPDATE  z_on_call_header
                                        SET     status = 4 ,
                                                modified_by = @employee_id ,
                                                date_modified = GETDATE()
                                        WHERE   ref_no = @ref_no
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
                    END
				--3 End
				--4 Start
                IF @app_type = 'SH' 
                    BEGIN
			--Added Code by Andrew 11-06-2011 Start
			--temporary commented by EDB--SELECT @ProcessAlready = processed_tag from z_shift_header where ref_no = @ref_no
                        SELECT  @person_no = employee_id
                        FROM    z_shift_header
                        WHERE   ref_no = @ref_no
                        SELECT  @cutoff = processing_datetime ,
                                @payperiod = payroll_period
                        FROM    payroll_period
                        WHERE   payroll_frequency = ( SELECT TOP 1
                                                              pay_frequency
                                                      FROM    sys_payroll_area a
                                                              INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                      WHERE   employee_id = @person_no
                                                    )
                                AND ( ( ( SELECT    date_from
                                          FROM      z_leave_header
                                          WHERE     ref_no = @ref_no
                                        ) BETWEEN m_from
                                          AND     m_to )
                                      OR ( ( SELECT date_to
                                             FROM   z_leave_header
                                             WHERE  ref_no = @ref_no
                                           ) BETWEEN m_from
                                             AND     m_to )
                                    )
                                AND unit_id = ( SELECT TOP 1
                                                        sector_code
                                                FROM    sys_payroll_area a
                                                        INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                WHERE   employee_id = @person_no
                                              )
			
                        IF @cutoff IS NOT NULL 
                            BEGIN
                                IF EXISTS ( SELECT TOP 1
                                                    approval_date
                                            FROM    z_approvers_approval
                                            WHERE   ref_no = @ref_no
                                                    AND approver_level = 'FINAL'
                                                    AND approval_date IS NOT NULL ) 
                                    BEGIN
                                        SELECT TOP 1
                                                @approval_date = approval_date
                                        FROM    z_approvers_approval
                                        WHERE   ref_no = @ref_no
                                                AND approver_level = 'FINAL'
                                                AND approval_date IS NOT NULL	
							
                                        IF @approval_date > @cutoff 
                                            BEGIN					
                                                SELECT TOP 1
                                                        @cutoff = processing_datetime
                                                FROM    payroll_period
                                                WHERE   payroll_frequency = ( SELECT TOP 1
                                                              pay_frequency
                                                              FROM
                                                              sys_payroll_area a
                                                              INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                              WHERE
                                                              employee_id = @person_no
                                                              )
                                                        AND payroll_period > @payperiod
                                                        AND unit_id = ( SELECT TOP 1
                                                              sector_code
                                                              FROM
                                                              sys_payroll_area a
                                                              INNER JOIN empjob_info b ON a.payarea_code = b.payroll_area
                                                              WHERE
                                                              employee_id = @person_no
                                                              )
                                                ORDER BY payroll_period ASC
                                            END
							
                                    END
                            END
                        IF @cutoff < @date_now 
                            BEGIN
                                SET @nOutput = 5
                            END
								
                        ELSE
						
						--Added Code by Andrew 11-06-2011 End
                            BEGIN
                                IF EXISTS ( SELECT  a.approve_tag
                                            FROM    z_shift_header a ( NOLOCK )
                                                    JOIN empjob_info b ( NOLOCK ) ON a.employee_id = b.employee_id
                                                    JOIN sys_payroll_area c ( NOLOCK ) ON b.payroll_area = c.payarea_code
                                            WHERE   a.ref_no = @ref_no
                                                    AND a.approve_tag IN ( 1,
                                                              2, 3 ) --AND c.sector_code = @sector_code 
                                                    ) 
                                    BEGIN
                                        UPDATE  z_shift_header
                                        SET     approve_tag = 4 ,
                                                modified_by = @employee_id ,
                                                date_modified = GETDATE()
                                        WHERE   ref_no = @ref_no
                                        UPDATE  z_shift_approval
                                        SET     status = 4
                                        WHERE   ref_no = @ref_no
                                        SET @nOutput = 1
                                    END
                                ELSE 
                                    BEGIN
                                        IF EXISTS ( SELECT  approve_tag
                                                    FROM    z_shift_header (NOLOCK)
                                                    WHERE   ref_no = @ref_no
                                                            AND approve_tag = 4 ) 
                                            BEGIN
                                                SET @nOutput = 2				
                                            END
                                        ELSE 
                                            BEGIN
                                                SET @nOutput = 3				
                                            END
                                    END 
                            END				
                    END
				--4 End
            END	
        ELSE 
            BEGIN
				--Don't Exist
                SET @nOutput = 4
            END
			   
        IF @nOutput = 1 
            BEGIN
				
	
                IF @app_type = 'SH' 
                    BEGIN
                        IF @status = 1 
                            BEGIN
                                EXEC [dbo].[spWeb_Insert_Shift_Approvers_v2] @ref_no = @ref_no,
                                    @planner_id = @employee_id, @status = 1
                            END
                        UPDATE  z_shift_approvers
                        SET     status2 = status ,
                                cancelled_by = @employee_id ,
                                cancelled_date = GETDATE() ,
                                status = 4
                        WHERE   ref_no = @ref_no
                    END
				
                ELSE 
                    BEGIN
                        IF @status = 1 
                            BEGIN
                                EXEC dbo.spWeb_Insert_Approver_Approval_v2 @ref_no = @ref_no,
                                    @approver_id = @employee_id,
                                    @approver_level = 'FINAL',
                                    @employee_id = @person_no, @status = 1
                            END
				
                        UPDATE  z_approvers_approval
                        SET     status2 = status ,
                                cancelled_by = @employee_id ,
                                cancelled_date = GETDATE() ,
                                status = 4
                        WHERE   ref_no = @ref_no
                    END	
                INSERT  INTO z_approvers_cancellation
                        ( ref_no ,
                          cancelled_by ,
                          cancelled_date ,
                          approver_level ,
                          tag ,
                          status
                        )
                VALUES  ( @ref_no ,
                          @employee_id ,
                          GETDATE() ,
                          'FINAL' ,
                          1 ,
                          4
                        )	
				
            END
		
        SET @nResult = @nOutput
    END


