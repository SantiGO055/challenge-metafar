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
        Task<ServiceResult<Tarjeta>> Login(int numeroTarjeta, int pin);
        Task<ServiceResult<Tarjeta>> ObtenerTarjeta(int numeroTarjeta);
        Task<ServiceResult<Saldo>> ExtraerSaldo(int tarjeta, decimal saldo);
        Task<ServiceResult<Saldo>> ObtenerSaldoPorNroTarjeta(int numeroTarjeta);
        Task<ServiceResult<List<Operacion>>> ObtenerHistorialOperaciones(int numeroTarjeta, int pagina);
    }
}
