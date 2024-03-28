using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movimiento.Queries
{
    public class ObtenerSaldoPorNroTarjeta : IRequest<Saldo>
    {
        public int tarjeta { get; set; }
    }
}
