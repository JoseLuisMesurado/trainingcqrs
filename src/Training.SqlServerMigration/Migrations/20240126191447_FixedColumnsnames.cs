using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Training.SqlServerMigration.Migrations
{
    /// <inheritdoc />
    public partial class FixedColumnsnames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GrantedExpirationDate",
                table: "Permission",
                newName: "ExpirationDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpirationDate",
                table: "Permission",
                newName: "GrantedExpirationDate");
        }
    }
}
