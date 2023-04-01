using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FAQ.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTheNameOfPropOfLogDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Exception",
                table: "Logs",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Logs",
                newName: "Exception");
        }
    }
}
