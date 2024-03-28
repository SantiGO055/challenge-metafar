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
        Task<Tarjeta> Login(int numeroTarjeta, int pin);
        Task<CuentaBancaria> ActualizarSaldo(Tarjeta tarjeta, decimal saldo);
        void GuardarHistorialOperacion(Domain.Models.Movimiento movimiento);
        void AgregarOperacion(Domain.Models.Movimiento movimiento);
        Task<int> ObtenerTotalRegistrosAsync(int numeroTarjeta);
        Task<List<Domain.Models.Movimiento>> ObtenerHistorialOperacionesAsync(int numeroTarjeta, int pagina, int registrosPorPagina);
        Task<Saldo> ObtenerSaldoPorNroTarjeta(int numeroTarjeta);
        Task GuardarCambios();
    }
}
