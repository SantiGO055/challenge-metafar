using Application.Abstractions;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movimientos.Queries
{
    public class ObtenerOperaciones : IRequest<ServiceResult<List<Operacion>>>
    {
        public int NroTarjeta { get; set; }
        public int Pagina { get; set; }

    }
}
