
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

        public async Task<ICollection<Movimiento>> GetMovimientos(Tarjeta tarjeta)
        {
            return await _db.Movimiento.ToListAsync();
        }

        public async Task<ICollection<Movimiento>> GetSaldo(Tarjeta tarjeta)
        {
            return await _db.Movimiento.ToListAsync();
        }

        public async Task<ICollection<Usuario>> GetUsuarios()
        {
            return await _db.Usuario.ToListAsync();
        }

        public async Task<ICollection<CuentaBancaria>> PostRealizarRetiro(Tarjeta tarjeta, decimal monto)
        {

            return await _db.CuentaBancaria.ToListAsync();
        }
    }
}
