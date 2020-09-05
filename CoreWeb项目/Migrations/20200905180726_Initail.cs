using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreWeb项目.Migrations
{
    public partial class Initail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    s_number = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    s_name = table.Column<string>(nullable: true),
                    sex = table.Column<bool>(nullable: false),
                    birthday = table.Column<DateTime>(nullable: false),
                    native = table.Column<string>(nullable: true),
                    c_number = table.Column<string>(nullable: true),
                    dep_number = table.Column<string>(nullable: true),
                    tel = table.Column<string>(nullable: true),
                    address = table.Column<string>(nullable: true),
                    zipcode = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student", x => x.s_number);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "student");
        }
    }
}
