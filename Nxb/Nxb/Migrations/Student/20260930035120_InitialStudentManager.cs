using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nxb.Migrations.Student
{
    /// <inheritdoc />
    public partial class InitialStudentManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StdClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StdClass", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StudentEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StudentPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StudentAddress = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StudentAvatar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StudentBirthday = table.Column<DateTime>(type: "date", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Student_StdClass_ClassId",
                        column: x => x.ClassId,
                        principalTable: "StdClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Marks",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marks", x => new { x.SubjectId, x.StudentId });
                    table.CheckConstraint("CK_Marks_Score", "[Score] >= 0 AND [Score] <= 10");
                    table.ForeignKey(
                        name: "FK_Marks_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Marks_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "StdClass",
                columns: new[] { "Id", "ClassName" },
                values: new object[,]
                {
                    { 1, "K24CNT2" },
                    { 2, "K24CNT3" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "SubjectName" },
                values: new object[,]
                {
                    { 1, "Lập trình ASP.NET Core" },
                    { 2, "Cơ sở dữ liệu" },
                    { 3, "Tiếng Anh" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "Id", "ClassId", "StudentAddress", "StudentAvatar", "StudentBirthday", "StudentEmail", "StudentName", "StudentPhone" },
                values: new object[,]
                {
                    { 1, 1, "Hà Nội", "demo.png", new DateTime(2006, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "an@example.com", "Nguyễn An", "0900000001" },
                    { 2, 1, "Hải Phòng", "demo.png", new DateTime(2006, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "binh@example.com", "Trần Bình", "0900000002" },
                    { 3, 2, "Đà Nẵng", "demo.png", new DateTime(2006, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "chi@example.com", "Lê Chi", "0900000003" },
                    { 4, 2, "Hà Nội", "demo.png", new DateTime(2006, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "dung@example.com", "Phạm Dũng", "0900000004" }
                });

            migrationBuilder.InsertData(
                table: "Marks",
                columns: new[] { "StudentId", "SubjectId", "Score" },
                values: new object[,]
                {
                    { 1, 1, 8.5 },
                    { 2, 1, 7.5 },
                    { 1, 2, 9.0 },
                    { 3, 2, 8.0 },
                    { 2, 3, 8.0 },
                    { 4, 3, 7.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Marks_StudentId",
                table: "Marks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_ClassId",
                table: "Student",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Student_StudentEmail",
                table: "Student",
                column: "StudentEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_StudentPhone",
                table: "Student",
                column: "StudentPhone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SubjectName",
                table: "Subjects",
                column: "SubjectName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Marks");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "StdClass");
        }
    }
}
