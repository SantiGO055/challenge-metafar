using Application.Abstractions;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movimiento.Commands
{
    public class ExtraerSaldo : IRequest<ServiceResult<Saldo>>
    {
        public decimal saldo { get; set; }
        public int tarjeta { get; set; }
        public string? ResumenCuentaBancaria { get; set; }
    }
}
