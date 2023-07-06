using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAdvertismentUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisments_AspNetUsers_UserId1",
                table: "Advertisments");

            migrationBuilder.DropIndex(
                name: "IX_Advertisments_UserId1",
                table: "Advertisments");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Advertisments");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Advertisments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisments_UserId",
                table: "Advertisments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisments_AspNetUsers_UserId",
                table: "Advertisments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisments_AspNetUsers_UserId",
                table: "Advertisments");

            migrationBuilder.DropIndex(
                name: "IX_Advertisments_UserId",
                table: "Advertisments");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Advertisments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Advertisments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Advertisments_UserId1",
                table: "Advertisments",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisments_AspNetUsers_UserId1",
                table: "Advertisments",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
