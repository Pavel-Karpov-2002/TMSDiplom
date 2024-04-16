using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostMicroService.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationPostId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LocationPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationPosts", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_LocationPosts_LocationPostId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "LocationPosts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_LocationPostId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "LocationPostId",
                table: "Posts");
        }
    }
}
