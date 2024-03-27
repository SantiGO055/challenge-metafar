using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions
{
    public interface IATMRepository
    {
        
        public Task<ICollection<Movimiento>> GetSaldo(Tarjeta tarjeta);
        public Task<ICollection<Movimiento>> GetMovimientos(Tarjeta tarjeta); //nro de tarjeta y pin para traer historial de movimientos
        public Task<ICollection<Usuario>> GetUsuarios();
        public Task<ICollection<CuentaBancaria>> PostRealizarRetiro(Tarjeta tarjeta, decimal monto); //retorno el saldo restante y nro de cuenta

    }
}
