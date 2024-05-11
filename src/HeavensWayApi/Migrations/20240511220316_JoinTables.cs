using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeavensWayApi.Migrations
{
    /// <inheritdoc />
    public partial class JoinTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventoIgreja",
                columns: table => new
                {
                    EventosId = table.Column<int>(type: "int", nullable: false),
                    IgrejasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoIgreja", x => new { x.EventosId, x.IgrejasId });
                    table.ForeignKey(
                        name: "FK_EventoIgreja_Eventos_EventosId",
                        column: x => x.EventosId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventoIgreja_Igrejas_IgrejasId",
                        column: x => x.IgrejasId,
                        principalTable: "Igrejas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventoUsuario",
                columns: table => new
                {
                    EventosId = table.Column<int>(type: "int", nullable: false),
                    UsuariosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoUsuario", x => new { x.EventosId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_EventoUsuario_AspNetUsers_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventoUsuario_Eventos_EventosId",
                        column: x => x.EventosId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventoIgreja_IgrejasId",
                table: "EventoIgreja",
                column: "IgrejasId");

            migrationBuilder.CreateIndex(
                name: "IX_EventoUsuario_UsuariosId",
                table: "EventoUsuario",
                column: "UsuariosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventoIgreja");

            migrationBuilder.DropTable(
                name: "EventoUsuario");
        }
    }
}
