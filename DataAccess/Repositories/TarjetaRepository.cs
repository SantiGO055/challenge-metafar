
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
    internal class TarjetaRepository(AppDbContext db) : IATMRepository
    {
        private readonly AppDbContext _db = db;


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


        public async Task<ServiceResult<Saldo>> ObtenerSaldoPorNroTarjeta(int numeroTarjeta)
        {
            ServiceResult<Saldo> result = new();
            try
            {
                var tarjeta = await ObtenerTarjeta(numeroTarjeta);
                var tarjetaResult = tarjeta.Payload;
                if (tarjetaResult == null)
                {
                    result.Message = "Usuario no encontrado";
                    result.Payload = null;
                    result.IsError = true;
                    return result;
                }
                else
                {
                    var cuenta = await _db.CuentaBancaria.FirstOrDefaultAsync(c => c.IDTarjeta == tarjetaResult.IDTarjeta); //nro de cuenta y saldo
                    var movimiento = await _db.Movimiento.FirstOrDefaultAsync(m => m.IDCuentaBancaria == cuenta.IDCuentaBancaria);
                    var usuario = await _db.Usuario.FirstOrDefaultAsync(u => u.IDCuentaBancaria == cuenta!.IDCuentaBancaria);
                    result.Payload = new Saldo
                    {
                        NombreUsuario = usuario?.Nombre,
                        NroCuenta = cuenta!.NroCuenta,
                        SaldoActual = cuenta!.Saldo,
                        FechaExtraccion = movimiento?.FechaMovimiento

                    };
                }
            }
            catch (Exception e)
            {
                result.IsError = true;
                result.Message = e.Message;
            }
            return result;
        }


        public async Task<ServiceResult<Saldo>> ExtraerSaldo(int numeroTarjeta, decimal saldo)
        {
            ServiceResult<Saldo> result = new();
            

            try
            {
                var tarjeta = await ObtenerTarjeta(numeroTarjeta);
                var tarjetaResult = tarjeta.Payload;
                if (tarjetaResult == null)
                {
                    result.Payload = null;
                    result.IsError = true;
                    result.Message = "Usuario Inexistente";
                }
                else
                {
                    var cuenta = await _db.CuentaBancaria.FirstOrDefaultAsync(c => c.IDTarjeta == tarjetaResult.IDTarjeta);
                    if(cuenta == null)
                    {
                        result.Payload = null;
                        result.IsError = true;
                        result.Message = "La cuenta para realizar la extracción no existe.";
                        return result;
                    }
                    else
                    {
                        if (saldo > cuenta?.Saldo)
                        {
                            result.Payload = null;
                            result.IsError = true;
                            result.Message = "Saldo insuficiente para realizar la operación.";
                            return result;
                        }
                        else
                        {
                            cuenta!.Saldo -= saldo;
                            var nuevoMovimiento = new Movimiento
                            {
                                FechaMovimiento = DateTime.Now,
                                IDTipoMovimiento= 1,// extraccion
                                CuentaBancaria = tarjetaResult.CuentaBancaria,
                                Saldo = cuenta.Saldo
                            };
                            _db.Movimiento.Add(nuevoMovimiento);

                            var usuario = await _db.Usuario.FirstOrDefaultAsync(c => c.IDCuentaBancaria == cuenta.IDCuentaBancaria);

                            result.Payload = new Saldo
                            {
                                NombreUsuario = usuario?.Nombre,
                                NroCuenta = cuenta.NroCuenta,
                                SaldoActual = cuenta.Saldo,
                                FechaExtraccion = nuevoMovimiento.FechaMovimiento

                            };
                        }

                    }
                }
            }
            catch (Exception e)
            {
                result.IsError = true;
                result.Message = e.Message;
                return result;
            }
            await _db.SaveChangesAsync();
            return result;
        }


        
        public async Task<ServiceResult<List<Operacion>>> ObtenerHistorialOperaciones(int numeroTarjeta, int pagina)
        {
            ServiceResult<List<Operacion>> result = new();
            Task<List<Operacion>> operacion;

            try
            {
                var tarjeta = await ObtenerTarjeta(numeroTarjeta);
                var tarjetaResult = tarjeta.Payload;
                
                if (tarjeta.IsError)
                {
                    
                    result.IsError = true;

                    return result;
                }
                else
                {
                    operacion = (from mov in _db.Movimiento
                                  join cue in _db.CuentaBancaria on mov.IDCuentaBancaria equals cue.IDCuentaBancaria
                                  join usu in _db.Usuario on mov.IDCuentaBancaria equals usu.IDCuentaBancaria
                                  join tmov in _db.TipoMovimiento on mov.IDTipoMovimiento equals tmov.IDTipoMovimiento
                                  select new Operacion
                                  {
                                      Resumen = new Saldo { NombreUsuario = usu.Nombre, NroCuenta = cue.NroCuenta, SaldoActual = mov.Saldo, FechaExtraccion = mov.FechaMovimiento },
                                      TipoMovimiento = new TipoMovimiento { IDTipoMovimiento = mov.IDTipoMovimiento, DescripcionMovimiento = tmov.DescripcionMovimiento }

                                  }
                        )
                        .Skip((pagina - 1) * 10)
                        .Take(10)
                        .ToListAsync();

                    result.Payload = await operacion;

                }

            }
            catch (Exception e)
            {
                result.IsError = true;
                result.Message = e.Message;
                return result;
            }
            

            result.IsError = result.Payload.Count > 0 ? false : true;
            result.Message = result.IsError ? "No se encontraron resultados" : 
                result.Payload.Count < 10 ? $"Se encontraron {result.Payload.Count} resultados en la pagina {pagina}." 
                : $"Se muestran {result.Payload.Count} registros de la pagina {pagina}";
            return result;
        }

        public async Task<ServiceResult<Tarjeta>> ObtenerTarjeta(int numeroTarjeta)
        {
            ServiceResult<Tarjeta> result = new();
            try
            {
                var tarjetaResult = await _db.Tarjeta.FirstOrDefaultAsync(t => t.NroTarjeta == numeroTarjeta);
                if(tarjetaResult == null)
                {
                    result.IsError = true;
                    result.Message = "Numero de tarjeta inválido.";
                    return result;
                }
                else
                {
                    result.Payload = tarjetaResult;
                }
            }
            catch (Exception e)
            {
                result.IsError = true;
                result.Message = e.Message;
                return result;
            }
            return result;
        }
    }
}
