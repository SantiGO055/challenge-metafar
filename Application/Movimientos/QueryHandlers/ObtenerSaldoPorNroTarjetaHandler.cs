using Application.Abstractions;
using Application.Movimiento.Queries;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movimiento.QueryHandlers
{
    public class ObtenerSaldoPorNroTarjetaHandler : IRequestHandler<ObtenerSaldoPorNroTarjeta, ServiceResult<Saldo>>
    {
        private readonly IATMRepository atmRepository;
        public ObtenerSaldoPorNroTarjetaHandler(IATMRepository atmRepo)
        {
            atmRepository = atmRepo;

        }


        public async Task<ServiceResult<Saldo>> Handle(ObtenerSaldoPorNroTarjeta request, CancellationToken cancellationToken)
        {
            return await atmRepository.ObtenerSaldoPorNroTarjeta(request.tarjeta);
        }
    }
}
