using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blitz.Infrastructure.Migrations
{
    public partial class AddCodeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Code",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_fiscal = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    nume = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    loc = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    str = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    di = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sect = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jud_com = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nr_com = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    an_com = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    act_aut = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tva = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sfarsit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_stare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    judet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imp_100 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imp_120 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imp_200 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imp_410 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imp_416 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imp_420 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imp_423 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imp_430 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imp_439 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imp_500 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imp_602 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imp_701 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imp_710 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imp_755 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imp_756 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bilanturi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imp_412 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imp_480 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imp_432 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    detalii_adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bloc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    scara = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    etaj = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ap = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Code", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Code");
        }
    }
}
