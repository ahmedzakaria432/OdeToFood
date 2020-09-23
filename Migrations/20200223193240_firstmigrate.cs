using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace OdeToFood.Migrations
{
    public partial class firstmigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ///var baseDir=AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin",string.Empty)+ "\\Data\\Sql Scripts"
            migrationBuilder.CreateTable(
                name: "Resturants",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: false),
                    Cusine = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resturants", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resturants");
        }
    }
}
