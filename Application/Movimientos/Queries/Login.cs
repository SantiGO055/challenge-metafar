using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movimiento.Queries
{
    public class Login : IRequest<Tarjeta>
    {
        public int NroTarjeta { get; set; }
        public int Pin { get; set; }

    }
}
