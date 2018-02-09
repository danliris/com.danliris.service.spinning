using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Spinning.Lib.Migrations
{
    public partial class spinninginput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpinningInputProductions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Lot = table.Column<string>(nullable: true),
                    NomorInputProduksi = table.Column<string>(maxLength: 100, nullable: true),
                    Shift = table.Column<string>(nullable: true),
                    UnitId = table.Column<int>(nullable: false),
                    UnitName = table.Column<string>(nullable: true),
                    YarnId = table.Column<int>(nullable: false),
                    YarnName = table.Column<string>(nullable: true),
                    _CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedUtc = table.Column<DateTime>(nullable: false),
                    _DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedUtc = table.Column<DateTime>(nullable: false),
                    _IsDeleted = table.Column<bool>(nullable: false),
                    _LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    tanggal = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpinningInputProductions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YarnOutputProductions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    BadOutput = table.Column<double>(nullable: false),
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    DrumTotal = table.Column<double>(nullable: false),
                    GoodOutput = table.Column<double>(nullable: false),
                    LotYarnCode = table.Column<string>(maxLength: 10, nullable: true),
                    LotYarnId = table.Column<int>(maxLength: 500, nullable: false),
                    LotYarnName = table.Column<string>(maxLength: 500, nullable: true),
                    MachineId = table.Column<string>(maxLength: 500, nullable: true),
                    MachineName = table.Column<string>(maxLength: 500, nullable: true),
                    Shift = table.Column<string>(maxLength: 500, nullable: true),
                    Spinning = table.Column<string>(maxLength: 500, nullable: true),
                    SpinningId = table.Column<string>(nullable: true),
                    YarnCode = table.Column<string>(maxLength: 100, nullable: true),
                    YarnId = table.Column<int>(maxLength: 500, nullable: false),
                    YarnName = table.Column<string>(maxLength: 500, nullable: true),
                    YarnWeightPerCone = table.Column<double>(nullable: false),
                    _CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedUtc = table.Column<DateTime>(nullable: false),
                    _DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedUtc = table.Column<DateTime>(nullable: false),
                    _IsDeleted = table.Column<bool>(nullable: false),
                    _LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YarnOutputProductions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Yarns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    Ne = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    _CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedUtc = table.Column<DateTime>(nullable: false),
                    _DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedUtc = table.Column<DateTime>(nullable: false),
                    _IsDeleted = table.Column<bool>(nullable: false),
                    _LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yarns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpinningInputProduction_InputDetails",
                columns: table => new
                {
                    Id = table.Column<int>(maxLength: 100, nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Counter = table.Column<int>(nullable: false),
                    Hash = table.Column<int>(nullable: false),
                    SpinningInputProductionId = table.Column<int>(nullable: false),
                    _CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedUtc = table.Column<DateTime>(nullable: false),
                    _DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedUtc = table.Column<DateTime>(nullable: false),
                    _IsDeleted = table.Column<bool>(nullable: false),
                    _LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    test = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpinningInputProduction_InputDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpinningInputProduction_InputDetails_SpinningInputProductions_SpinningInputProductionId",
                        column: x => x.SpinningInputProductionId,
                        principalTable: "SpinningInputProductions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LotYarns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    Lot = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    UnitId = table.Column<int>(maxLength: 100, nullable: false),
                    YarnId = table.Column<int>(nullable: false),
                    _CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _CreatedUtc = table.Column<DateTime>(nullable: false),
                    _DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _DeletedUtc = table.Column<DateTime>(nullable: false),
                    _IsDeleted = table.Column<bool>(nullable: false),
                    _LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    _LastModifiedUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotYarns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotYarns_Yarns_YarnId",
                        column: x => x.YarnId,
                        principalTable: "Yarns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LotYarns_YarnId",
                table: "LotYarns",
                column: "YarnId");

            migrationBuilder.CreateIndex(
                name: "IX_SpinningInputProduction_InputDetails_SpinningInputProductionId",
                table: "SpinningInputProduction_InputDetails",
                column: "SpinningInputProductionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LotYarns");

            migrationBuilder.DropTable(
                name: "SpinningInputProduction_InputDetails");

            migrationBuilder.DropTable(
                name: "YarnOutputProductions");

            migrationBuilder.DropTable(
                name: "Yarns");

            migrationBuilder.DropTable(
                name: "SpinningInputProductions");
        }
    }
}
