using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AddonWeb.Migrations
{
    public partial class FullWing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AddonAppLicense",
                table: "AddonAppLicense");

            migrationBuilder.DropColumn(
                name: "AppLicenceMasterId",
                table: "AddonAppLicense");

            migrationBuilder.AlterColumn<string>(
                name: "AppLicenseKey",
                table: "AddonAppLicense",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddonAppLicense",
                table: "AddonAppLicense",
                column: "AppLicenseKey");

            migrationBuilder.AlterColumn<string>(
                name: "AppLicenseKey",
                table: "AppHardwareInfo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppHardwareInfo_AppLicenseKey",
                table: "AppHardwareInfo",
                column: "AppLicenseKey");

            migrationBuilder.AddForeignKey(
                name: "FK_AppHardwareInfo_AddonAppLicense_AppLicenseKey",
                table: "AppHardwareInfo",
                column: "AppLicenseKey",
                principalTable: "AddonAppLicense",
                principalColumn: "AppLicenseKey",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppHardwareInfo_AddonAppLicense_AppLicenseKey",
                table: "AppHardwareInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddonAppLicense",
                table: "AddonAppLicense");

            migrationBuilder.DropIndex(
                name: "IX_AppHardwareInfo_AppLicenseKey",
                table: "AppHardwareInfo");

            migrationBuilder.AddColumn<int>(
                name: "AppLicenceMasterId",
                table: "AddonAppLicense",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "AppLicenseKey",
                table: "AddonAppLicense",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddonAppLicense",
                table: "AddonAppLicense",
                column: "AppLicenceMasterId");

            migrationBuilder.AlterColumn<string>(
                name: "AppLicenseKey",
                table: "AppHardwareInfo",
                nullable: true);
        }
    }
}
