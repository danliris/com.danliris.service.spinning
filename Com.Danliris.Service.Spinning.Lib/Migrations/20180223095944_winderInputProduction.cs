using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Spinning.Lib.Migrations
{
    public partial class winderInputProduction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SpinningInputProductions",
                table: "SpinningInputProductions");

            migrationBuilder.RenameTable(
                name: "SpinningInputProductions",
                newName: "WinderInputProductions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WinderInputProductions",
                table: "WinderInputProductions",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WinderInputProductions",
                table: "WinderInputProductions");

            migrationBuilder.RenameTable(
                name: "WinderInputProductions",
                newName: "SpinningInputProductions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpinningInputProductions",
                table: "SpinningInputProductions",
                column: "Id");
        }
    }
}
