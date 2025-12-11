using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalDGC.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TribunalAdjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluacionesMeritos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeritoPostulanteId = table.Column<int>(type: "int", nullable: false),
                    PuntajeAsignado = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaEvaluacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DocumentacionVerificada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionesMeritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluacionesMeritos_MeritosPostulante_MeritoPostulanteId",
                        column: x => x.MeritoPostulanteId,
                        principalTable: "MeritosPostulante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ordenamientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LlamadoId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaGeneracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenamientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordenamientos_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Ordenamientos_Llamados_LlamadoId",
                        column: x => x.LlamadoId,
                        principalTable: "Llamados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pruebas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LlamadoId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PuntajeMaximo = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    FechaProgramada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Lugar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EsObligatoria = table.Column<bool>(type: "bit", nullable: false),
                    OrdenEjecucion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pruebas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pruebas_Llamados_LlamadoId",
                        column: x => x.LlamadoId,
                        principalTable: "Llamados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PosicionesOrdenamiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdenamientoId = table.Column<int>(type: "int", nullable: false),
                    InscripcionId = table.Column<int>(type: "int", nullable: false),
                    Posicion = table.Column<int>(type: "int", nullable: false),
                    PuntajeTotal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    AplicaCuota = table.Column<bool>(type: "bit", nullable: false),
                    TipoCuota = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosicionesOrdenamiento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PosicionesOrdenamiento_Inscripciones_InscripcionId",
                        column: x => x.InscripcionId,
                        principalTable: "Inscripciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PosicionesOrdenamiento_Ordenamientos_OrdenamientoId",
                        column: x => x.OrdenamientoId,
                        principalTable: "Ordenamientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionesPruebas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InscripcionId = table.Column<int>(type: "int", nullable: false),
                    PruebaId = table.Column<int>(type: "int", nullable: false),
                    PuntajeObtenido = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aprobado = table.Column<bool>(type: "bit", nullable: false),
                    FechaEvaluacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Verificado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluacionesPruebas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluacionesPruebas_Inscripciones_InscripcionId",
                        column: x => x.InscripcionId,
                        principalTable: "Inscripciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluacionesPruebas_Pruebas_PruebaId",
                        column: x => x.PruebaId,
                        principalTable: "Pruebas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesMeritos_MeritoPostulanteId",
                table: "EvaluacionesMeritos",
                column: "MeritoPostulanteId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesPruebas_InscripcionId_PruebaId",
                table: "EvaluacionesPruebas",
                columns: new[] { "InscripcionId", "PruebaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EvaluacionesPruebas_PruebaId",
                table: "EvaluacionesPruebas",
                column: "PruebaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenamientos_DepartamentoId",
                table: "Ordenamientos",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenamientos_LlamadoId_Tipo_Estado",
                table: "Ordenamientos",
                columns: new[] { "LlamadoId", "Tipo", "Estado" });

            migrationBuilder.CreateIndex(
                name: "IX_PosicionesOrdenamiento_InscripcionId",
                table: "PosicionesOrdenamiento",
                column: "InscripcionId");

            migrationBuilder.CreateIndex(
                name: "IX_PosicionesOrdenamiento_OrdenamientoId_InscripcionId",
                table: "PosicionesOrdenamiento",
                columns: new[] { "OrdenamientoId", "InscripcionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pruebas_LlamadoId_OrdenEjecucion",
                table: "Pruebas",
                columns: new[] { "LlamadoId", "OrdenEjecucion" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluacionesMeritos");

            migrationBuilder.DropTable(
                name: "EvaluacionesPruebas");

            migrationBuilder.DropTable(
                name: "PosicionesOrdenamiento");

            migrationBuilder.DropTable(
                name: "Pruebas");

            migrationBuilder.DropTable(
                name: "Ordenamientos");
        }
    }
}
