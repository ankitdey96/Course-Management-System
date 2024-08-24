using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseTopicDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TopicParentID",
                table: "CourseTopic");

            migrationBuilder.CreateTable(
                name: "CourseTopicDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TopicId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TopicName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseTopicDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseTopicDetail_CourseTopic_TopicId",
                        column: x => x.TopicId,
                        principalTable: "CourseTopic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });


            migrationBuilder.CreateIndex(
                name: "IX_CourseTopicDetail_TopicId",
                table: "CourseTopicDetail",
                column: "TopicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseTopicDetail");

            migrationBuilder.AddColumn<Guid>(
                name: "TopicParentID",
                table: "CourseTopic",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        }
    }
}
