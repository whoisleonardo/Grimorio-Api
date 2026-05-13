using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GrimorioDigital.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EscolasDeMagia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Elemento = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscolasDeMagia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Raridade = table.Column<string>(type: "TEXT", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pocoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Efeito = table.Column<string>(type: "TEXT", nullable: false),
                    DuracaoMinutos = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pocoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    SenhaHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Magias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", nullable: false),
                    Nivel = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoAlvo = table.Column<string>(type: "TEXT", nullable: false),
                    EscolaDeMagiaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Magias_EscolasDeMagia_EscolaDeMagiaId",
                        column: x => x.EscolaDeMagiaId,
                        principalTable: "EscolasDeMagia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PocaoIngredientes",
                columns: table => new
                {
                    PocaoId = table.Column<int>(type: "INTEGER", nullable: false),
                    IngredienteId = table.Column<int>(type: "INTEGER", nullable: false),
                    QuantidadeNecessaria = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PocaoIngredientes", x => new { x.PocaoId, x.IngredienteId });
                    table.ForeignKey(
                        name: "FK_PocaoIngredientes_Ingredientes_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "Ingredientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PocaoIngredientes_Pocoes_PocaoId",
                        column: x => x.PocaoId,
                        principalTable: "Pocoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feiticeiros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Nivel = table.Column<int>(type: "INTEGER", nullable: false),
                    EscolaDeMagiaId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feiticeiros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feiticeiros_EscolasDeMagia_EscolaDeMagiaId",
                        column: x => x.EscolaDeMagiaId,
                        principalTable: "EscolasDeMagia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Feiticeiros_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EscolasDeMagia",
                columns: new[] { "Id", "Descricao", "Elemento", "Nome" },
                values: new object[,]
                {
                    { 1, "Magias de invocação de elementos", "Fogo", "Evocação" },
                    { 2, "Magias relacionadas à morte e não-mortos", "Sombra", "Necromancia" },
                    { 3, "Magias de engano e ilusão", "Arcano", "Ilusão" }
                });

            migrationBuilder.InsertData(
                table: "Ingredientes",
                columns: new[] { "Id", "Descricao", "Nome", "Quantidade", "Raridade" },
                values: new object[,]
                {
                    { 1, "Raiz mágica rara", "Raiz de Mandragora", 10, "Raro" },
                    { 2, "Resíduo de dragão ancião", "Pó de Osso de Dragão", 3, "Lendario" },
                    { 3, "Erva comum com propriedades sedativas", "Erva do Sono", 50, "Comum" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feiticeiros_EscolaDeMagiaId",
                table: "Feiticeiros",
                column: "EscolaDeMagiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Feiticeiros_UsuarioId",
                table: "Feiticeiros",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Magias_EscolaDeMagiaId",
                table: "Magias",
                column: "EscolaDeMagiaId");

            migrationBuilder.CreateIndex(
                name: "IX_PocaoIngredientes_IngredienteId",
                table: "PocaoIngredientes",
                column: "IngredienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feiticeiros");

            migrationBuilder.DropTable(
                name: "Magias");

            migrationBuilder.DropTable(
                name: "PocaoIngredientes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "EscolasDeMagia");

            migrationBuilder.DropTable(
                name: "Ingredientes");

            migrationBuilder.DropTable(
                name: "Pocoes");
        }
    }
}
