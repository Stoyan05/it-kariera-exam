using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class remove : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Bars_BarId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_BarId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b5c02fd7-f423-470b-b364-38edded91fc6", "AQAAAAIAAYagAAAAEBcoMMDsJABUSsCwp/yo76OA76VfmPxNdSM9EnHSjO17KItgk/DkKob3PDRe9MnfBg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3592dc3a-f74a-496b-bf03-4dc0e3634698", "AQAAAAIAAYagAAAAEDF4/iUikjHpsN3oFBwJoT60cCfP8TQiQixAYgqPDK1uHk+fFxuWdKyILhwl1qH60g==" });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BarId",
                table: "Reviews",
                column: "BarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Bars_BarId",
                table: "Reviews",
                column: "BarId",
                principalTable: "Bars",
                principalColumn: "BarId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
