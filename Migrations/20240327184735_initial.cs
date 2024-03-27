using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace challenge_metafar.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CuentaBancaria",
                columns: table => new
                {
                    IDCuentaBancaria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NroCuenta = table.Column<int>(type: "int", nullable: false),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentaBancaria", x => x.IDCuentaBancaria);
                });

            migrationBuilder.CreateTable(
                name: "Tarjetas",
                columns: table => new
                {
                    IDTarjeta = table.Column<int>(type: "int", nullable: false),
                    NroTarjeta = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    Pin = table.Column<int>(type: "int", unicode: false, maxLength: 4, nullable: false),
                    Intentos = table.Column<int>(type: "int", nullable: false),
                    TarjetaBloqueada = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarjetas", x => x.IDTarjeta);
                    table.ForeignKey(
                        name: "FK_Tarjetas_CuentaBancaria_IDTarjeta",
                        column: x => x.IDTarjeta,
                        principalTable: "CuentaBancaria",
                        principalColumn: "IDCuentaBancaria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IDUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CuentaBancariaIDCuentaBancaria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IDUsuario);
                    table.ForeignKey(
                        name: "FK_Usuario_CuentaBancaria_CuentaBancariaIDCuentaBancaria",
                        column: x => x.CuentaBancariaIDCuentaBancaria,
                        principalTable: "CuentaBancaria",
                        principalColumn: "IDCuentaBancaria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimiento",
                columns: table => new
                {
                    IDMovimientos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDCuentaBancaria = table.Column<int>(type: "int", nullable: false),
                    FechaMovimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CuentaBancariaIDCuentaBancaria = table.Column<int>(type: "int", nullable: false),
                    TarjetaIDTarjeta = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimiento", x => x.IDMovimientos);
                    table.ForeignKey(
                        name: "FK_Movimiento_CuentaBancaria_CuentaBancariaIDCuentaBancaria",
                        column: x => x.CuentaBancariaIDCuentaBancaria,
                        principalTable: "CuentaBancaria",
                        principalColumn: "IDCuentaBancaria",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimiento_Tarjetas_TarjetaIDTarjeta",
                        column: x => x.TarjetaIDTarjeta,
                        principalTable: "Tarjetas",
                        principalColumn: "IDTarjeta");
                });

            migrationBuilder.CreateTable(
                name: "TipoMovimiento",
                columns: table => new
                {
                    IDTipoMovimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescripcionMovimiento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MovimientoIDMovimientos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMovimiento", x => x.IDTipoMovimiento);
                    table.ForeignKey(
                        name: "FK_TipoMovimiento_Movimiento_MovimientoIDMovimientos",
                        column: x => x.MovimientoIDMovimientos,
                        principalTable: "Movimiento",
                        principalColumn: "IDMovimientos",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_CuentaBancariaIDCuentaBancaria",
                table: "Movimiento",
                column: "CuentaBancariaIDCuentaBancaria");

            migrationBuilder.CreateIndex(
                name: "IX_Movimiento_TarjetaIDTarjeta",
                table: "Movimiento",
                column: "TarjetaIDTarjeta");

            migrationBuilder.CreateIndex(
                name: "IX_TipoMovimiento_MovimientoIDMovimientos",
                table: "TipoMovimiento",
                column: "MovimientoIDMovimientos");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_CuentaBancariaIDCuentaBancaria",
                table: "Usuario",
                column: "CuentaBancariaIDCuentaBancaria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoMovimiento");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Movimiento");

            migrationBuilder.DropTable(
                name: "Tarjetas");

            migrationBuilder.DropTable(
                name: "CuentaBancaria");
        }
    }
}
