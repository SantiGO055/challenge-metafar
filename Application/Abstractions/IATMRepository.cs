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
        Tarjeta ObtenerTarjetaPorNumero(int numeroTarjeta);
        Task<CuentaBancaria> ActualizarSaldo(Tarjeta tarjeta, decimal saldo);
        void GuardarHistorialOperacion(Domain.Models.Movimiento movimiento);
        void AgregarOperacion(Domain.Models.Movimiento movimiento);
        Task<int> ObtenerTotalRegistrosAsync(int numeroTarjeta);
        Task<List<Domain.Models.Movimiento>> ObtenerHistorialOperacionesAsync(int numeroTarjeta, int pagina, int registrosPorPagina);
        Task<Usuario> ObtenerUsuarioPorTarjeta(int numeroTarjeta);
        Task GuardarCambios();
        //public Task<ICollection<Movimiento>> GetSaldo(Tarjeta tarjeta);
        //public Task<ICollection<Movimiento>> GetMovimientos(Tarjeta tarjeta); //nro de tarjeta y pin para traer historial de movimientos
        //public Task<ICollection<Usuario>> GetUsuarios();
        //public Task<ICollection<CuentaBancaria>> PostRealizarRetiro(Tarjeta tarjeta, decimal monto); //retorno el saldo restante y nro de cuenta

    }
}
