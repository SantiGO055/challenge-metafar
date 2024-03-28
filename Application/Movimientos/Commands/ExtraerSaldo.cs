using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movimiento.Commands
{
    public class ExtraerSaldo : IRequest<CuentaBancaria>
    {
        public decimal saldo { get; set; }
        public Tarjeta tarjeta { get; set; } = null!;
        public string? ResumenCuentaBancaria { get; set; }
    }
}
