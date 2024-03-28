using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace challenge_metafar.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tarjeta",
                columns: table => new
                {
                    IDTarjeta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NroTarjeta = table.Column<int>(type: "int", unicode: false, maxLength: 6, nullable: false),
                    Pin = table.Column<int>(type: "int", unicode: false, maxLength: 4, nullable: false),
                    Intentos = table.Column<int>(type: "int", nullable: false),
                    TarjetaBloqueada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarjeta", x => x.IDTarjeta);
                });

            migrationBuilder.CreateTable(
                name: "TipoMovimiento",
                columns: table => new
                {
                    IDTipoMovimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDMovimiento = table.Column<int>(type: "int", nullable: false),
                    DescripcionMovimiento = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMovimiento", x => x.IDTipoMovimiento);
                });

            migrationBuilder.CreateTable(
                name: "CuentaBancaria",
                columns: table => new
                {
                    IDCuentaBancaria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDMovimiento = table.Column<int>(type: "int", nullable: false),
                    IDTarjeta = table.Column<int>(type: "int", nullable: false),
                    NroCuenta = table.Column<int>(type: "int", nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentaBancaria", x => x.IDCuentaBancaria);
                    table.ForeignKey(
                        name: "FK_CuentaBancaria_Tarjeta_IDTarjeta",
                        column: x => x.IDTarjeta,
                        principalTable: "Tarjeta",
                        principalColumn: "IDTarjeta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimiento",
                columns: table => new
                {
                    IDMovimientos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDCuentaBancaria = table.Column<int>(type: "int", nullable: false),
                    IDTipoMovimiento = table.Column<int>(type: "int", nullable: false),
                    FechaMovimiento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimiento", x => x.IDMovimientos);
                    table.ForeignKey(
                        name: "FK_Movimiento_CuentaBancaria_IDCuentaBancaria",
                        column: x => x.IDCuentaBancaria,
                        principalTable: "CuentaBancaria",
                        principalColumn: "IDCuentaBancaria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimiento_TipoMovimiento_IDTipoMovimiento",
                        column: x => x.IDTipoMovimiento,
                        principalTable: "TipoMovimiento",
                        principalColumn: "IDTipoMovimiento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IDUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDCuentaBancaria = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IDUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_CuentaBancaria_IDCuentaBancaria",
                        column: x => x.IDCuentaBancaria,
                        principalTable: "CuentaBancaria",
                        principalColumn: "IDCuentaBancaria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CuentaBancaria_IDTarjeta",
                table: "CuentaBancaria",
                column: "IDTarjeta",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_IDCuentaBancaria",
                table: "Movimiento",
                column: "IDCuentaBancaria");

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_IDTipoMovimiento",
                table: "Movimiento",
                column: "IDTipoMovimiento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_IDCuentaBancaria",
                table: "Usuario",
                column: "IDCuentaBancaria",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimiento");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "TipoMovimiento");

            migrationBuilder.DropTable(
                name: "CuentaBancaria");

            migrationBuilder.DropTable(
                name: "Tarjeta");
        }
    }
}
