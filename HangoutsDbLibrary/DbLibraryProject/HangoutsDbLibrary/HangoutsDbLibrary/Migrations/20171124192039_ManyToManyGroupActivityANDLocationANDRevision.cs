using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HangoutsDbLibrary.Migrations
{
    public partial class ManyToManyGroupActivityANDLocationANDRevision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Group_GroupId",
                table: "Activity");

            migrationBuilder.DropIndex(
                name: "IX_Activity_GroupId",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "Descrition",
                table: "Plan");

            migrationBuilder.DropColumn(
                name: "Descrition",
                table: "Interest");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Activity");

            migrationBuilder.AlterColumn<string>(
                name: "EndTimePlan",
                table: "Plan",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "BeginnigTimePlan",
                table: "Plan",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Plan",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Interest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Activity",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupActivity",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupActivity", x => new { x.ActivityId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_GroupActivity_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupActivity_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_LocationId",
                table: "Activity",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupActivity_GroupId",
                table: "GroupActivity",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Location_LocationId",
                table: "Activity",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Location_LocationId",
                table: "Activity");

            migrationBuilder.DropTable(
                name: "GroupActivity");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Activity_LocationId",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Plan");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Interest");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Activity");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTimePlan",
                table: "Plan",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "BeginnigTimePlan",
                table: "Plan",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descrition",
                table: "Plan",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descrition",
                table: "Interest",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Activity",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activity_GroupId",
                table: "Activity",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Group_GroupId",
                table: "Activity",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
