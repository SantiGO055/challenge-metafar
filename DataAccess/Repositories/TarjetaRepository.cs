
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

        public async Task<CuentaBancaria> ExtraerSaldo(Tarjeta tarjeta, decimal saldo)
        {
            var nuevoMovimiento = new Movimiento
            {
                FechaMovimiento = DateTime.Now,
                IDMovimientos = 1,
                CuentaBancaria = tarjeta.CuentaBancaria

            };
            //agrego nuevo movimiento
            _db.Movimiento.Add(nuevoMovimiento);

            //updatear cuenta bancaria
            var cuenta = await _db.CuentaBancaria.FirstOrDefaultAsync(c => c.IDCuentaBancaria == tarjeta.CuentaBancaria.IDCuentaBancaria);
            cuenta!.Saldo = saldo;

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

        public async Task<ServiceResult<Tarjeta>> Login(int numeroTarjeta, int pin)
        {
            ServiceResult<Tarjeta> result = new();
            try
            {

                var tarjeta = await _db.Tarjeta.FirstOrDefaultAsync(t => t.NroTarjeta == numeroTarjeta);
                if (tarjeta == null)
                {
                    result.Payload = null;
                    result.IsError = true;
                    result.Message = "Usuario Inexistente";
                }
                else
                {
                    if (!(tarjeta!.TarjetaBloqueada))
                    {
                        if (tarjeta?.Pin == pin)
                        {
                            result.Payload = tarjeta;

                        }
                        else
                        {
                            result.IsError = true;
                            result.Message = "Pin Invalido";
                            if (tarjeta?.Intentos > 0)
                            {
                                tarjeta.Intentos--;
                            }
                            else
                            {
                                result.Message = "Tarjeta bloqueada";
                                tarjeta!.TarjetaBloqueada = true;
                            }
                            await _db.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        result.Payload = null;
                        result.IsError = true;
                        result.Message = "Tarjeta bloqueada";
                    }
                }
                
                

                return result;
            }
            catch (Exception e)
            {
                result.IsError = true;
                result.Message = e.Message;
                return result;
            }
            
        }

        public Task<List<Movimiento>> ObtenerHistorialOperacionesAsync(int numeroTarjeta, int pagina, int registrosPorPagina)
        {
            throw new NotImplementedException();
        }

        public async Task<Saldo> ObtenerSaldoPorNroTarjeta(int numeroTarjeta)
        {
            var tarjeta = await _db.Tarjeta.FirstOrDefaultAsync(t => t.NroTarjeta == numeroTarjeta);
            var cuenta = await _db.CuentaBancaria.FirstOrDefaultAsync(c => c.IDTarjeta == tarjeta.IDTarjeta); //nro de cuenta y saldo
            var movimiento = await _db.Movimiento.FirstOrDefaultAsync(m => m.IDCuentaBancaria == cuenta.IDCuentaBancaria);
            var usuario = await _db.Usuario.FirstOrDefaultAsync(u => u.IDCuentaBancaria == cuenta.IDCuentaBancaria);
            var result = new Saldo
            {
                NombreUsuario = usuario.Nombre,
                NroCuenta = cuenta.NroCuenta,
                SaldoActual = cuenta.Saldo,
                FechaExtraccion = movimiento.FechaMovimiento

            };
            return result;
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
