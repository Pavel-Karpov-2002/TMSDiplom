using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostMicroService.Migrations
{
    /// <inheritdoc />
    public partial class IsLocationPostIdNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_LocationPosts_LocationPostId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_LocationPostId",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "LocationPostId",
                table: "Posts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_LocationPostId",
                table: "Posts",
                column: "LocationPostId",
                unique: true,
                filter: "[LocationPostId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_LocationPosts_LocationPostId",
                table: "Posts",
                column: "LocationPostId",
                principalTable: "LocationPosts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_LocationPosts_LocationPostId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_LocationPostId",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "LocationPostId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_LocationPostId",
                table: "Posts",
                column: "LocationPostId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_LocationPosts_LocationPostId",
                table: "Posts",
                column: "LocationPostId",
                principalTable: "LocationPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
