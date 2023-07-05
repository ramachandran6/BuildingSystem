using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ES.BS.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class FirstMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuildingSystemss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuildingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfFloors = table.Column<int>(type: "int", nullable: true),
                    Generator_Staus = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingSystemss", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "workerDetailss",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    weight = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workerDetailss", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildingSystemss");

            migrationBuilder.DropTable(
                name: "PersonDet");

            migrationBuilder.DropTable(
                name: "workerDetailss");
        }
    }
}
