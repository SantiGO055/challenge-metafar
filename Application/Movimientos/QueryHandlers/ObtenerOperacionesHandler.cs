using Application.Abstractions;
using Application.Movimiento.Queries;
using Application.Movimientos.Queries;
using Domain.Models;
using MediatR;

namespace Application.Movimientos.QueryHandlers
{
    public class ObtenerOperacionesHandler : IRequestHandler<ObtenerOperaciones, ServiceResult<List<Operacion>>>
    {
        private readonly IATMRepository atmRepository;
        public ObtenerOperacionesHandler(IATMRepository atmRepo)
        {
            atmRepository = atmRepo;

        }

        public async Task<ServiceResult<List<Operacion>>> Handle(ObtenerOperaciones request, CancellationToken cancellationToken)
        {
            return await atmRepository.ObtenerHistorialOperaciones(request.NroTarjeta, request.Pagina);
        }
    }
}
