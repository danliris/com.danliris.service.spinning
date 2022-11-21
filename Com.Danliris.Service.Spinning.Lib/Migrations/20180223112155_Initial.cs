using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Spinning.Lib.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "LotYarns",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Active = table.Column<bool>(nullable: false),
            //        Code = table.Column<string>(maxLength: 100, nullable: true),
            //        Date = table.Column<DateTime>(nullable: false),
            //        Lot = table.Column<string>(maxLength: 500, nullable: true),
            //        MachineCode = table.Column<string>(maxLength: 100, nullable: true),
            //        MachineId = table.Column<string>(maxLength: 100, nullable: true),
            //        MachineName = table.Column<string>(maxLength: 100, nullable: true),
            //        Remark = table.Column<string>(nullable: true),
            //        UnitCode = table.Column<string>(maxLength: 100, nullable: true),
            //        UnitId = table.Column<string>(maxLength: 100, nullable: true),
            //        UnitName = table.Column<string>(maxLength: 100, nullable: true),
            //        YarnCode = table.Column<string>(maxLength: 100, nullable: true),
            //        YarnId = table.Column<int>(nullable: false),
            //        YarnName = table.Column<string>(maxLength: 100, nullable: true),
            //        _CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
            //        _CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
            //        _CreatedUtc = table.Column<DateTime>(nullable: false),
            //        _DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
            //        _DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
            //        _DeletedUtc = table.Column<DateTime>(nullable: false),
            //        _IsDeleted = table.Column<bool>(nullable: false),
            //        _LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
            //        _LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
            //        _LastModifiedUtc = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_LotYarns", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WinderInputProductions",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Active = table.Column<bool>(nullable: false),
            //        Bale = table.Column<double>(nullable: false),
            //        Counter = table.Column<double>(nullable: false),
            //        Date = table.Column<DateTime>(nullable: false),
            //        Hank = table.Column<double>(nullable: false),
            //        Lot = table.Column<string>(nullable: true),
            //        MachineId = table.Column<string>(maxLength: 500, nullable: true),
            //        MachineName = table.Column<string>(maxLength: 500, nullable: true),
            //        Ne = table.Column<double>(nullable: false),
            //        NomorInputProduksi = table.Column<string>(maxLength: 100, nullable: true),
            //        Shift = table.Column<string>(maxLength: 500, nullable: true),
            //        UnitId = table.Column<string>(maxLength: 500, nullable: true),
            //        UnitName = table.Column<string>(maxLength: 500, nullable: true),
            //        YarnId = table.Column<int>(maxLength: 100, nullable: false),
            //        YarnName = table.Column<string>(maxLength: 500, nullable: true),
            //        _CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
            //        _CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
            //        _CreatedUtc = table.Column<DateTime>(nullable: false),
            //        _DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
            //        _DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
            //        _DeletedUtc = table.Column<DateTime>(nullable: false),
            //        _IsDeleted = table.Column<bool>(nullable: false),
            //        _LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
            //        _LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
            //        _LastModifiedUtc = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WinderInputProductions", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "WinderOutputProductions",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Active = table.Column<bool>(nullable: false),
            //        BadOutput = table.Column<double>(nullable: false),
            //        Code = table.Column<string>(maxLength: 100, nullable: true),
            //        Date = table.Column<DateTime>(nullable: false),
            //        DrumTotal = table.Column<double>(nullable: false),
            //        GoodOutput = table.Column<double>(nullable: false),
            //        LotYarnCode = table.Column<string>(maxLength: 100, nullable: true),
            //        LotYarnId = table.Column<int>(nullable: false),
            //        LotYarnName = table.Column<string>(maxLength: 500, nullable: true),
            //        MachineCode = table.Column<string>(maxLength: 500, nullable: true),
            //        MachineId = table.Column<string>(maxLength: 500, nullable: true),
            //        MachineName = table.Column<string>(maxLength: 500, nullable: true),
            //        Shift = table.Column<string>(maxLength: 500, nullable: true),
            //        SpinningCode = table.Column<string>(maxLength: 500, nullable: true),
            //        SpinningId = table.Column<string>(maxLength: 500, nullable: true),
            //        SpinningName = table.Column<string>(maxLength: 500, nullable: true),
            //        YarnCode = table.Column<string>(maxLength: 100, nullable: true),
            //        YarnId = table.Column<int>(nullable: false),
            //        YarnName = table.Column<string>(maxLength: 500, nullable: true),
            //        YarnWeightPerCone = table.Column<double>(nullable: false),
            //        _CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
            //        _CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
            //        _CreatedUtc = table.Column<DateTime>(nullable: false),
            //        _DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
            //        _DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
            //        _DeletedUtc = table.Column<DateTime>(nullable: false),
            //        _IsDeleted = table.Column<bool>(nullable: false),
            //        _LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
            //        _LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
            //        _LastModifiedUtc = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_WinderOutputProductions", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Yarns",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Active = table.Column<bool>(nullable: false),
            //        Code = table.Column<string>(maxLength: 100, nullable: true),
            //        Name = table.Column<string>(maxLength: 500, nullable: true),
            //        Ne = table.Column<double>(nullable: false),
            //        Remark = table.Column<string>(maxLength: 500, nullable: true),
            //        _CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
            //        _CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
            //        _CreatedUtc = table.Column<DateTime>(nullable: false),
            //        _DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
            //        _DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
            //        _DeletedUtc = table.Column<DateTime>(nullable: false),
            //        _IsDeleted = table.Column<bool>(nullable: false),
            //        _LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
            //        _LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
            //        _LastModifiedUtc = table.Column<DateTime>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Yarns", x => x.Id);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "LotYarns");

            //migrationBuilder.DropTable(
            //    name: "WinderInputProductions");

            //migrationBuilder.DropTable(
            //    name: "WinderOutputProductions");

            //migrationBuilder.DropTable(
            //    name: "Yarns");
        }
    }
}
