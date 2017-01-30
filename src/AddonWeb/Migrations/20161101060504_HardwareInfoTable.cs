using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AddonWeb.Migrations
{
    public partial class HardwareInfoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppHardwareInfo",
                columns: table => new
                {
                    AppHardwareInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivatedDate = table.Column<DateTime>(nullable: false),
                    AppLicenseKey = table.Column<string>(nullable: true),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    HardwareInfo = table.Column<string>(nullable: true),
                    LastLogged = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppHardwareInfo", x => x.AppHardwareInfoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppHardwareInfo");
        }
    }
}
