using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ES.ElevatorDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Firstmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonDet",
                columns: table => new
                {
                    personId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    weight = table.Column<int>(type: "int", nullable: true),
                    fromFloor = table.Column<byte>(type: "tinyint", nullable: true),
                    toFloor = table.Column<byte>(type: "tinyint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonDet", x => x.personId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonDet");
        }
    }
}
