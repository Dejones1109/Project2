using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLySinhVien.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    ParentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FatherName = table.Column<string>(nullable: true),
                    FatherJob = table.Column<string>(nullable: true),
                    FatherPhone = table.Column<string>(nullable: true),
                    FatherBirthYear = table.Column<int>(nullable: true),
                    FatherAddress = table.Column<string>(nullable: true),
                    MotherName = table.Column<string>(nullable: true),
                    MotherJob = table.Column<string>(nullable: true),
                    MotherPhone = table.Column<string>(nullable: true),
                    MotherBirthYear = table.Column<int>(nullable: true),
                    MotherAddress = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.ParentID);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    PerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PerID);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    BeginDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Credits = table.Column<int>(nullable: false),
                    RateMidTerm = table.Column<int>(nullable: false),
                    RateFinalExam = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    RegisterDate = table.Column<DateTime>(nullable: false),
                    PermissionID = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accounts_Permissions_PermissionID",
                        column: x => x.PermissionID,
                        principalTable: "Permissions",
                        principalColumn: "PerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PermisionDetails",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionName = table.Column<string>(nullable: true),
                    ActionCode = table.Column<string>(nullable: true),
                    PermisstionID = table.Column<int>(nullable: false),
                    PesmissionPerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermisionDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PermisionDetails_Permissions_PesmissionPerID",
                        column: x => x.PesmissionPerID,
                        principalTable: "Permissions",
                        principalColumn: "PerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountID = table.Column<int>(nullable: false),
                    StudentCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Nation = table.Column<string>(nullable: true),
                    Religion = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Birthplace = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    CardID = table.Column<string>(nullable: true),
                    DateOfCardID = table.Column<DateTime>(nullable: false),
                    PlaceOfCardID = table.Column<string>(nullable: true),
                    HighShoolPlace = table.Column<string>(nullable: true),
                    GraduationYear = table.Column<int>(nullable: false),
                    PolicyChild = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LastModify = table.Column<DateTime>(nullable: false),
                    Contact = table.Column<string>(nullable: true),
                    ParentID = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Students_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Parents_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Parents",
                        principalColumn: "ParentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(nullable: false),
                    SemesterID = table.Column<int>(nullable: false),
                    SubjectsID = table.Column<int>(nullable: false),
                    MarkMidTerm = table.Column<int>(nullable: false),
                    MarkFinalExam = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Marks_Semesters_SemesterID",
                        column: x => x.SemesterID,
                        principalTable: "Semesters",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Marks_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Marks_Subjects_SubjectsID",
                        column: x => x.SubjectsID,
                        principalTable: "Subjects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PermissionID",
                table: "Accounts",
                column: "PermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_SemesterID",
                table: "Marks",
                column: "SemesterID");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_StudentID",
                table: "Marks",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Marks_SubjectsID",
                table: "Marks",
                column: "SubjectsID");

            migrationBuilder.CreateIndex(
                name: "IX_PermisionDetails_PesmissionPerID",
                table: "PermisionDetails",
                column: "PesmissionPerID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AccountID",
                table: "Students",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ParentID",
                table: "Students",
                column: "ParentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Marks");

            migrationBuilder.DropTable(
                name: "PermisionDetails");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "Permissions");
        }
    }
}
