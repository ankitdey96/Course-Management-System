using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStoreProcedureForCourseEnrollment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"CREATE OR ALTER   PROCEDURE [dbo].[GetCourseEnrollments]
						@OrderBy nvarchar(50),
						@CourseName nvarchar(250) = '%',
						@StudentName nvarchar(250) = '%',
						@EnrollmentDateFrom datetime = null,
						@EnrollmentDateTo datetime = null,
						@TotalEnRollment int output,
						@TotalCourses int output
						
						AS
						BEGIN
							Declare @sql nvarchar(2000);
							Declare @countsql nvarchar(2000);
							Declare @paramList nvarchar(MAX); 
							Declare @countparamList nvarchar(MAX);
							
							SET NOCOUNT ON;

							Select @TotalEnRollment = count(*) from CourseEnrollment;
							Select @TotalCourses = count(*) from Course;

							SET @countsql = 'select @TotalEnRollment = count(*)  from CourseEnrollment cs inner join 
											Course c on cs.CourseId = c.Id inner join
											[User] s on cs.StudentId = s.Id  where 1 = 1 ';
							
							IF @CourseName IS NOT NULL
							SET @countsql = @countsql + ' AND c.Name LIKE ''%'' + @xCourseName + ''%''' 

							IF @StudentName IS NOT NULL
							SET @countsql = @countsql + ' AND s.FullName LIKE ''%'' + @xStudentName + ''%''' 

							IF @EnrollmentDateFrom IS NOT NULL
							SET @countsql = @countsql + ' AND EnrollmentDate >= @xEnrollmentDateFrom'

							IF @EnrollmentDateTo IS NOT NULL
								SET @countsql = @countsql + ' AND EnrollmentDate <= @xEnrollmentDateTo' 


								SET @sql = 'select c.Name as CourseName, s.FullName as StudentName, EnrollmentDate from CourseEnrollment cs inner join 
											Course c on cs.CourseId = c.Id inner join
											[User] s on cs.StudentId = s.Id where 1 = 1 '; 

								IF @CourseName IS NOT NULL
								SET @sql = @sql + ' AND c.[Name] LIKE ''%'' + @xCourseName + ''%''' 

								IF @StudentName IS NOT NULL
								SET @sql = @sql + ' AND s.FullName LIKE ''%'' + @xStudentName + ''%''' 

								IF @EnrollmentDateFrom IS NOT NULL
								SET @sql = @sql + ' AND EnrollmentDate >= @xEnrollmentDateFrom'

								IF @EnrollmentDateTo IS NOT NULL
								SET @sql = @sql + ' AND EnrollmentDate <= @xEnrollmentDateTo' 

								SET @sql = @sql + ' Order by '+@OrderBy;

								SELECT @countparamlist = '@xCourseName nvarchar(250),
									@xStudentName nvarchar(250),
									@xEnrollmentDateFrom datetime,
									@xEnrollmentDateTo datetime,
									@TotalEnRollment int output,
									@TotalCourses int output' ;

								exec sp_executesql @countsql , @countparamlist ,
									@CourseName,
									@StudentName,
									@EnrollmentDateFrom,
									@EnrollmentDateTo,
									@TotalCourses = @TotalCourses output,
									@TotalEnRollment = @TotalEnRollment output;

								SELECT @paramlist = '@xCourseName nvarchar(250),
									@xStudentName nvarchar(250),
									@xEnrollmentDateFrom datetime,
									@xEnrollmentDateTo datetime';

								exec sp_executesql @sql , @paramlist ,
									@CourseName,
									@StudentName,
									@EnrollmentDateFrom,
									@EnrollmentDateTo

								print @countsql;
								print @sql;
	
						END";
            migrationBuilder.Sql(sql);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = @"DROP PROCEDURE [dbo].[GetCourseEnrollments]";
            migrationBuilder.Sql(sql);

        }
    }
}
