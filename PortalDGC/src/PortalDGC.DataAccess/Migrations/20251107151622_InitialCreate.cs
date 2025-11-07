using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalDGC.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Llamados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bases = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaApertura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaCierre = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CantidadPuestos = table.Column<int>(type: "int", nullable: false),
                    PorcentajeAfrodescendiente = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    PorcentajeTrans = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    PorcentajeDiscapacidad = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Llamados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Postulantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CedulaIdentidad = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneroOtro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domicilio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentoCedula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postulantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApoyosNecesarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LlamadoId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApoyosNecesarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApoyosNecesarios_Llamados_LlamadoId",
                        column: x => x.LlamadoId,
                        principalTable: "Llamados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsPuntuables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LlamadoId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PuntajeMaximo = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsPuntuables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsPuntuables_Llamados_LlamadoId",
                        column: x => x.LlamadoId,
                        principalTable: "Llamados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LlamadoDepartamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LlamadoId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    CantidadPuestos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LlamadoDepartamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LlamadoDepartamentos_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LlamadoDepartamentos_Llamados_LlamadoId",
                        column: x => x.LlamadoId,
                        principalTable: "Llamados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequisitosExcluyentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LlamadoId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Obligatorio = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequisitosExcluyentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequisitosExcluyentes_Llamados_LlamadoId",
                        column: x => x.LlamadoId,
                        principalTable: "Llamados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Constancias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostulanteId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Archivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Validado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constancias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Constancias_Postulantes_PostulanteId",
                        column: x => x.PostulanteId,
                        principalTable: "Postulantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inscripciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostulanteId = table.Column<int>(type: "int", nullable: false),
                    LlamadoId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    FechaInscripcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PuntajeTotal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    PosicionOrdenamiento = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscripciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Llamados_LlamadoId",
                        column: x => x.LlamadoId,
                        principalTable: "Llamados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inscripciones_Postulantes_PostulanteId",
                        column: x => x.PostulanteId,
                        principalTable: "Postulantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApoyosSolicitados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InscripcionId = table.Column<int>(type: "int", nullable: false),
                    ApoyoId = table.Column<int>(type: "int", nullable: false),
                    Justificacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApoyosSolicitados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApoyosSolicitados_ApoyosNecesarios_ApoyoId",
                        column: x => x.ApoyoId,
                        principalTable: "ApoyosNecesarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApoyosSolicitados_Inscripciones_InscripcionId",
                        column: x => x.InscripcionId,
                        principalTable: "Inscripciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AutodefinicionesLey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InscripcionId = table.Column<int>(type: "int", nullable: false),
                    EsAfrodescendiente = table.Column<bool>(type: "bit", nullable: false),
                    EsTrans = table.Column<bool>(type: "bit", nullable: false),
                    TieneDiscapacidad = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutodefinicionesLey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutodefinicionesLey_Inscripciones_InscripcionId",
                        column: x => x.InscripcionId,
                        principalTable: "Inscripciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeritosPostulante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InscripcionId = table.Column<int>(type: "int", nullable: false),
                    ItemPuntuableId = table.Column<int>(type: "int", nullable: false),
                    DocumentoRespaldo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PuntajeObtenido = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Verificado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeritosPostulante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeritosPostulante_Inscripciones_InscripcionId",
                        column: x => x.InscripcionId,
                        principalTable: "Inscripciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeritosPostulante_ItemsPuntuables_ItemPuntuableId",
                        column: x => x.ItemPuntuableId,
                        principalTable: "ItemsPuntuables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequisitosPostulante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InscripcionId = table.Column<int>(type: "int", nullable: false),
                    RequisitoId = table.Column<int>(type: "int", nullable: false),
                    Cumple = table.Column<bool>(type: "bit", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequisitosPostulante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequisitosPostulante_Inscripciones_InscripcionId",
                        column: x => x.InscripcionId,
                        principalTable: "Inscripciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequisitosPostulante_RequisitosExcluyentes_RequisitoId",
                        column: x => x.RequisitoId,
                        principalTable: "RequisitosExcluyentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApoyosNecesarios_LlamadoId",
                table: "ApoyosNecesarios",
                column: "LlamadoId");

            migrationBuilder.CreateIndex(
                name: "IX_ApoyosSolicitados_ApoyoId",
                table: "ApoyosSolicitados",
                column: "ApoyoId");

            migrationBuilder.CreateIndex(
                name: "IX_ApoyosSolicitados_InscripcionId",
                table: "ApoyosSolicitados",
                column: "InscripcionId");

            migrationBuilder.CreateIndex(
                name: "IX_AutodefinicionesLey_InscripcionId",
                table: "AutodefinicionesLey",
                column: "InscripcionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Constancias_PostulanteId",
                table: "Constancias",
                column: "PostulanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_Codigo",
                table: "Departamentos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_DepartamentoId",
                table: "Inscripciones",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_LlamadoId",
                table: "Inscripciones",
                column: "LlamadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripciones_PostulanteId_LlamadoId",
                table: "Inscripciones",
                columns: new[] { "PostulanteId", "LlamadoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemsPuntuables_LlamadoId",
                table: "ItemsPuntuables",
                column: "LlamadoId");

            migrationBuilder.CreateIndex(
                name: "IX_LlamadoDepartamentos_DepartamentoId",
                table: "LlamadoDepartamentos",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_LlamadoDepartamentos_LlamadoId",
                table: "LlamadoDepartamentos",
                column: "LlamadoId");

            migrationBuilder.CreateIndex(
                name: "IX_MeritosPostulante_InscripcionId",
                table: "MeritosPostulante",
                column: "InscripcionId");

            migrationBuilder.CreateIndex(
                name: "IX_MeritosPostulante_ItemPuntuableId",
                table: "MeritosPostulante",
                column: "ItemPuntuableId");

            migrationBuilder.CreateIndex(
                name: "IX_Postulantes_CedulaIdentidad",
                table: "Postulantes",
                column: "CedulaIdentidad",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Postulantes_Email",
                table: "Postulantes",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequisitosExcluyentes_LlamadoId",
                table: "RequisitosExcluyentes",
                column: "LlamadoId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisitosPostulante_InscripcionId",
                table: "RequisitosPostulante",
                column: "InscripcionId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisitosPostulante_RequisitoId",
                table: "RequisitosPostulante",
                column: "RequisitoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApoyosSolicitados");

            migrationBuilder.DropTable(
                name: "AutodefinicionesLey");

            migrationBuilder.DropTable(
                name: "Constancias");

            migrationBuilder.DropTable(
                name: "LlamadoDepartamentos");

            migrationBuilder.DropTable(
                name: "MeritosPostulante");

            migrationBuilder.DropTable(
                name: "RequisitosPostulante");

            migrationBuilder.DropTable(
                name: "ApoyosNecesarios");

            migrationBuilder.DropTable(
                name: "ItemsPuntuables");

            migrationBuilder.DropTable(
                name: "Inscripciones");

            migrationBuilder.DropTable(
                name: "RequisitosExcluyentes");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "Postulantes");

            migrationBuilder.DropTable(
                name: "Llamados");
        }
    }
}
