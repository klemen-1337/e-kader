using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace web.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DelovneUre",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(nullable: false),
                    UraZacetka = table.Column<DateTime>(nullable: false),
                    UraKonca = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelovneUre", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Dopusti",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(nullable: false),
                    UraZacetka = table.Column<DateTime>(nullable: false),
                    UraKonca = table.Column<DateTime>(nullable: false),
                    Preostanek = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dopusti", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Izobrazevanja",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(nullable: true),
                    Datum = table.Column<DateTime>(nullable: false),
                    Cena = table.Column<string>(nullable: true),
                    Redno = table.Column<bool>(nullable: false),
                    Specializirano = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izobrazevanja", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Zaposleni",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: true),
                    Priimek = table.Column<string>(nullable: true),
                    Naslov = table.Column<string>(nullable: true),
                    Telefon = table.Column<int>(nullable: false),
                    DatumRojstva = table.Column<DateTime>(nullable: false),
                    DatumZaposlitve = table.Column<DateTime>(nullable: false),
                    Spol = table.Column<string>(nullable: true),
                    PhotoPath = table.Column<string>(nullable: true),
                    DelovnaMestaID = table.Column<int>(nullable: true),
                    DelovneUreID = table.Column<int>(nullable: true),
                    DopustID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposleni", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Zaposleni_DelovneUre_DelovneUreID",
                        column: x => x.DelovneUreID,
                        principalTable: "DelovneUre",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zaposleni_Dopusti_DopustID",
                        column: x => x.DopustID,
                        principalTable: "Dopusti",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DelovnaMesta",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Oddelek = table.Column<string>(nullable: true),
                    Lokacija = table.Column<string>(nullable: true),
                    ZaposlenID = table.Column<int>(nullable: true),
                    NazivDelovnegaMesta = table.Column<string>(nullable: true),
                    IzobrazevanjeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelovnaMesta", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DelovnaMesta_Izobrazevanja_IzobrazevanjeID",
                        column: x => x.IzobrazevanjeID,
                        principalTable: "Izobrazevanja",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DelovnaMesta_Zaposleni_ZaposlenID",
                        column: x => x.ZaposlenID,
                        principalTable: "Zaposleni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DelovnaMesta_IzobrazevanjeID",
                table: "DelovnaMesta",
                column: "IzobrazevanjeID");

            migrationBuilder.CreateIndex(
                name: "IX_DelovnaMesta_ZaposlenID",
                table: "DelovnaMesta",
                column: "ZaposlenID");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposleni_DelovnaMestaID",
                table: "Zaposleni",
                column: "DelovnaMestaID");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposleni_DelovneUreID",
                table: "Zaposleni",
                column: "DelovneUreID");

            migrationBuilder.CreateIndex(
                name: "IX_Zaposleni_DopustID",
                table: "Zaposleni",
                column: "DopustID");

            migrationBuilder.AddForeignKey(
                name: "FK_Zaposleni_DelovnaMesta_DelovnaMestaID",
                table: "Zaposleni",
                column: "DelovnaMestaID",
                principalTable: "DelovnaMesta",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DelovnaMesta_Izobrazevanja_IzobrazevanjeID",
                table: "DelovnaMesta");

            migrationBuilder.DropForeignKey(
                name: "FK_DelovnaMesta_Zaposleni_ZaposlenID",
                table: "DelovnaMesta");

            migrationBuilder.DropTable(
                name: "Izobrazevanja");

            migrationBuilder.DropTable(
                name: "Zaposleni");

            migrationBuilder.DropTable(
                name: "DelovnaMesta");

            migrationBuilder.DropTable(
                name: "DelovneUre");

            migrationBuilder.DropTable(
                name: "Dopusti");
        }
    }
}
