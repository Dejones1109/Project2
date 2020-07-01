using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLySinhVien.Migrations
{
    public partial class Edit5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Sex",
                table: "Students",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<float>(
                name: "MarkAverage",
                table: "Marks",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarkAverage",
                table: "Marks");

            migrationBuilder.AlterColumn<byte>(
                name: "Sex",
                table: "Students",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
