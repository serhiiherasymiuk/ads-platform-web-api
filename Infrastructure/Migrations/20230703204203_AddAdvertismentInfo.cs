using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAdvertismentInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactPerson",
                table: "Advertisments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContactPhoneNumber",
                table: "Advertisments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Advertisments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Advertisments",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisments_AspNetUsers_UserId1",
                table: "Advertisments");

            migrationBuilder.DropIndex(
                name: "IX_Advertisments_UserId1",
                table: "Advertisments");

            migrationBuilder.DropColumn(
                name: "ContactPerson",
                table: "Advertisments");

            migrationBuilder.DropColumn(
                name: "ContactPhoneNumber",
                table: "Advertisments");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Advertisments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Advertisments");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Advertisments");
        }
    }
}
