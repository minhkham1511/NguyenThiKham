using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NguyenThiKhambth2.Migrations
{
    public partial class Create_Foreignkey_Student : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacultID",
                table: "Student",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Facult",
                columns: table => new
                {
                    FacultID = table.Column<string>(type: "TEXT", nullable: false),
                    FacultName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facult", x => x.FacultID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_FacultID",
                table: "Student",
                column: "FacultID");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Facult_FacultID",
                table: "Student",
                column: "FacultID",
                principalTable: "Facult",
                principalColumn: "FacultID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Facult_FacultID",
                table: "Student");

            migrationBuilder.DropTable(
                name: "Facult");

            migrationBuilder.DropIndex(
                name: "IX_Student_FacultID",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "FacultID",
                table: "Student");
        }
    }
}
