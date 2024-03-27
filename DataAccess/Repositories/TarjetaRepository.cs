
using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class TarjetaRepository : IATMRepository
    {
        private readonly AppDbContext _db;
        public TarjetaRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<CuentaBancaria> ActualizarSaldo(Tarjeta tarjeta, decimal saldo)
        {
            var nuevoMovimiento = new Movimiento
            {
                FechaMovimiento = DateTime.Now,
                IDMovimientos = 1,
                IDCuentaBancaria = tarjeta.CuentaBancaria.IDCuentaBancaria

            };
            //agrego nuevo movimiento
            _db.Movimiento.Add(nuevoMovimiento);

            //updatear cuenta bancaria
            var cuenta = await _db.CuentaBancaria.FirstOrDefaultAsync(c => c.IDCuentaBancaria == tarjeta.CuentaBancaria.IDCuentaBancaria);
            cuenta.Saldo = saldo;

            await _db.SaveChangesAsync();

            return cuenta;
        }

        public void AgregarOperacion(Movimiento movimiento)
        {
            throw new NotImplementedException();
        }

        public Task GuardarCambios()
        {
            throw new NotImplementedException();
        }

        public void GuardarHistorialOperacion(Movimiento movimiento)
        {
            throw new NotImplementedException();
        }

        public Task<List<Movimiento>> ObtenerHistorialOperacionesAsync(int numeroTarjeta, int pagina, int registrosPorPagina)
        {
            throw new NotImplementedException();
        }

        public Tarjeta ObtenerTarjetaPorNumero(int numeroTarjeta)
        {
            throw new NotImplementedException();
        }

        public Task<int> ObtenerTotalRegistrosAsync(int numeroTarjeta)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> ObtenerUsuarioPorTarjeta(int numeroTarjeta)
        {
            throw new NotImplementedException();
        }
    }
}
