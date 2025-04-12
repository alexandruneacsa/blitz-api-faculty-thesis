using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blitz.Infrastructure.Migrations
{
    public partial class RefactorCodeAndAddedBorrowerToTheSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Code",
                table: "Code");

            migrationBuilder.DropColumn(
                name: "act_aut",
                table: "Code");

            migrationBuilder.DropColumn(
                name: "an_com",
                table: "Code");

            migrationBuilder.DropColumn(
                name: "ap",
                table: "Code");

            migrationBuilder.DropColumn(
                name: "cod_fiscal",
                table: "Code");

            migrationBuilder.DropColumn(
                name: "loc",
                table: "Code");

            migrationBuilder.RenameTable(
                name: "Code",
                newName: "Codes");

            migrationBuilder.RenameColumn(
                name: "nume",
                table: "Codes",
                newName: "Nume");

            migrationBuilder.RenameColumn(
                name: "judet",
                table: "Codes",
                newName: "Judet");

            migrationBuilder.RenameColumn(
                name: "etaj",
                table: "Codes",
                newName: "Etaj");

            migrationBuilder.RenameColumn(
                name: "bloc",
                table: "Codes",
                newName: "Bloc");

            migrationBuilder.RenameColumn(
                name: "tva",
                table: "Codes",
                newName: "Telefon");

            migrationBuilder.RenameColumn(
                name: "tel",
                table: "Codes",
                newName: "Tara");

            migrationBuilder.RenameColumn(
                name: "str",
                table: "Codes",
                newName: "Strada");

            migrationBuilder.RenameColumn(
                name: "stare",
                table: "Codes",
                newName: "StareFirma");

            migrationBuilder.RenameColumn(
                name: "sfarsit",
                table: "Codes",
                newName: "Sector");

            migrationBuilder.RenameColumn(
                name: "sect",
                table: "Codes",
                newName: "Salariati2021");

            migrationBuilder.RenameColumn(
                name: "scara",
                table: "Codes",
                newName: "Salariati2020");

            migrationBuilder.RenameColumn(
                name: "nr_com",
                table: "Codes",
                newName: "Salariati2019");

            migrationBuilder.RenameColumn(
                name: "nr",
                table: "Codes",
                newName: "Salariati2018");

            migrationBuilder.RenameColumn(
                name: "jud_com",
                table: "Codes",
                newName: "ProfitNetRON2021");

            migrationBuilder.RenameColumn(
                name: "imp_756",
                table: "Codes",
                newName: "ProfitNetRON2020");

            migrationBuilder.RenameColumn(
                name: "imp_755",
                table: "Codes",
                newName: "ProfitNetRON2019");

            migrationBuilder.RenameColumn(
                name: "imp_710",
                table: "Codes",
                newName: "ProfitNetRON2018");

            migrationBuilder.RenameColumn(
                name: "imp_701",
                table: "Codes",
                newName: "NumarRegistrulComertului");

            migrationBuilder.RenameColumn(
                name: "imp_602",
                table: "Codes",
                newName: "Numar");

            migrationBuilder.RenameColumn(
                name: "imp_500",
                table: "Codes",
                newName: "Localitate");

            migrationBuilder.RenameColumn(
                name: "imp_480",
                table: "Codes",
                newName: "DescriereCodCAEN2021");

            migrationBuilder.RenameColumn(
                name: "imp_439",
                table: "Codes",
                newName: "DescriereCodCAEN2020");

            migrationBuilder.RenameColumn(
                name: "imp_432",
                table: "Codes",
                newName: "DescriereCodCAEN2019");

            migrationBuilder.RenameColumn(
                name: "imp_430",
                table: "Codes",
                newName: "DescriereCodCAEN2018");

            migrationBuilder.RenameColumn(
                name: "imp_423",
                table: "Codes",
                newName: "CotaDePiata2021");

            migrationBuilder.RenameColumn(
                name: "imp_420",
                table: "Codes",
                newName: "CotaDePiata2020");

            migrationBuilder.RenameColumn(
                name: "imp_416",
                table: "Codes",
                newName: "CotaDePiata2019");

            migrationBuilder.RenameColumn(
                name: "imp_412",
                table: "Codes",
                newName: "CotaDePiata2018");

            migrationBuilder.RenameColumn(
                name: "imp_410",
                table: "Codes",
                newName: "CodFiscal");

            migrationBuilder.RenameColumn(
                name: "imp_200",
                table: "Codes",
                newName: "CodCAEN2021");

            migrationBuilder.RenameColumn(
                name: "imp_120",
                table: "Codes",
                newName: "CodCAEN2020");

            migrationBuilder.RenameColumn(
                name: "imp_100",
                table: "Codes",
                newName: "CodCAEN2019");

            migrationBuilder.RenameColumn(
                name: "fax",
                table: "Codes",
                newName: "CodCAEN2018");

            migrationBuilder.RenameColumn(
                name: "dp",
                table: "Codes",
                newName: "CifraDeAfaceriNetaRON2021");

            migrationBuilder.RenameColumn(
                name: "di",
                table: "Codes",
                newName: "CifraDeAfaceriNetaRON2020");

            migrationBuilder.RenameColumn(
                name: "detalii_adresa",
                table: "Codes",
                newName: "CifraDeAfaceriNetaRON2019");

            migrationBuilder.RenameColumn(
                name: "data_stare",
                table: "Codes",
                newName: "CifraDeAfaceriNetaRON2018");

            migrationBuilder.RenameColumn(
                name: "cp",
                table: "Codes",
                newName: "Camera");

            migrationBuilder.RenameColumn(
                name: "bilanturi",
                table: "Codes",
                newName: "Apartament");

            migrationBuilder.AlterColumn<string>(
                name: "Nume",
                table: "Codes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Codes",
                table: "Codes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Borrower",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cod_fiscal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nume = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    loc = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Borrower", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Borrower");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Codes",
                table: "Codes");

            migrationBuilder.RenameTable(
                name: "Codes",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "Nume",
                table: "Code",
                newName: "nume");

            migrationBuilder.RenameColumn(
                name: "Judet",
                table: "Code",
                newName: "judet");

            migrationBuilder.RenameColumn(
                name: "Etaj",
                table: "Code",
                newName: "etaj");

            migrationBuilder.RenameColumn(
                name: "Bloc",
                table: "Code",
                newName: "bloc");

            migrationBuilder.RenameColumn(
                name: "Telefon",
                table: "Code",
                newName: "tva");

            migrationBuilder.RenameColumn(
                name: "Tara",
                table: "Code",
                newName: "tel");

            migrationBuilder.RenameColumn(
                name: "Strada",
                table: "Code",
                newName: "str");

            migrationBuilder.RenameColumn(
                name: "StareFirma",
                table: "Code",
                newName: "stare");

            migrationBuilder.RenameColumn(
                name: "Sector",
                table: "Code",
                newName: "sfarsit");

            migrationBuilder.RenameColumn(
                name: "Salariati2021",
                table: "Code",
                newName: "sect");

            migrationBuilder.RenameColumn(
                name: "Salariati2020",
                table: "Code",
                newName: "scara");

            migrationBuilder.RenameColumn(
                name: "Salariati2019",
                table: "Code",
                newName: "nr_com");

            migrationBuilder.RenameColumn(
                name: "Salariati2018",
                table: "Code",
                newName: "nr");

            migrationBuilder.RenameColumn(
                name: "ProfitNetRON2021",
                table: "Code",
                newName: "jud_com");

            migrationBuilder.RenameColumn(
                name: "ProfitNetRON2020",
                table: "Code",
                newName: "imp_756");

            migrationBuilder.RenameColumn(
                name: "ProfitNetRON2019",
                table: "Code",
                newName: "imp_755");

            migrationBuilder.RenameColumn(
                name: "ProfitNetRON2018",
                table: "Code",
                newName: "imp_710");

            migrationBuilder.RenameColumn(
                name: "NumarRegistrulComertului",
                table: "Code",
                newName: "imp_701");

            migrationBuilder.RenameColumn(
                name: "Numar",
                table: "Code",
                newName: "imp_602");

            migrationBuilder.RenameColumn(
                name: "Localitate",
                table: "Code",
                newName: "imp_500");

            migrationBuilder.RenameColumn(
                name: "DescriereCodCAEN2021",
                table: "Code",
                newName: "imp_480");

            migrationBuilder.RenameColumn(
                name: "DescriereCodCAEN2020",
                table: "Code",
                newName: "imp_439");

            migrationBuilder.RenameColumn(
                name: "DescriereCodCAEN2019",
                table: "Code",
                newName: "imp_432");

            migrationBuilder.RenameColumn(
                name: "DescriereCodCAEN2018",
                table: "Code",
                newName: "imp_430");

            migrationBuilder.RenameColumn(
                name: "CotaDePiata2021",
                table: "Code",
                newName: "imp_423");

            migrationBuilder.RenameColumn(
                name: "CotaDePiata2020",
                table: "Code",
                newName: "imp_420");

            migrationBuilder.RenameColumn(
                name: "CotaDePiata2019",
                table: "Code",
                newName: "imp_416");

            migrationBuilder.RenameColumn(
                name: "CotaDePiata2018",
                table: "Code",
                newName: "imp_412");

            migrationBuilder.RenameColumn(
                name: "CodFiscal",
                table: "Code",
                newName: "imp_410");

            migrationBuilder.RenameColumn(
                name: "CodCAEN2021",
                table: "Code",
                newName: "imp_200");

            migrationBuilder.RenameColumn(
                name: "CodCAEN2020",
                table: "Code",
                newName: "imp_120");

            migrationBuilder.RenameColumn(
                name: "CodCAEN2019",
                table: "Code",
                newName: "imp_100");

            migrationBuilder.RenameColumn(
                name: "CodCAEN2018",
                table: "Code",
                newName: "fax");

            migrationBuilder.RenameColumn(
                name: "CifraDeAfaceriNetaRON2021",
                table: "Code",
                newName: "dp");

            migrationBuilder.RenameColumn(
                name: "CifraDeAfaceriNetaRON2020",
                table: "Code",
                newName: "di");

            migrationBuilder.RenameColumn(
                name: "CifraDeAfaceriNetaRON2019",
                table: "Code",
                newName: "detalii_adresa");

            migrationBuilder.RenameColumn(
                name: "CifraDeAfaceriNetaRON2018",
                table: "Code",
                newName: "data_stare");

            migrationBuilder.RenameColumn(
                name: "Camera",
                table: "Code",
                newName: "cp");

            migrationBuilder.RenameColumn(
                name: "Apartament",
                table: "Code",
                newName: "bilanturi");

            migrationBuilder.AlterColumn<string>(
                name: "nume",
                table: "Code",
                type: "nvarchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "act_aut",
                table: "Code",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "an_com",
                table: "Code",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ap",
                table: "Code",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "cod_fiscal",
                table: "Code",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "loc",
                table: "Code",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Code",
                table: "Code",
                column: "Id");
        }
    }
}
