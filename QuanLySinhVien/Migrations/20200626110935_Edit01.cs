using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLySinhVien.Migrations
{
    public partial class Edit01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Parents_ParentID",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Parents_ParentID",
                table: "Students",
                column: "ParentID",
                principalTable: "Parents",
                principalColumn: "ParentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Parents_ParentID",
                table: "Students");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Parents_ParentID",
                table: "Students",
                column: "ParentID",
                principalTable: "Parents",
                principalColumn: "ParentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
