using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diploma.Migrations
{
    /// <inheritdoc />
    public partial class AddLocal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PreferLocale",
                table: "UserProfiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferLocale",
                table: "UserProfiles");
        }
    }
}
