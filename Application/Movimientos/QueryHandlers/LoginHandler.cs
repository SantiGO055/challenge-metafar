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
    internal class LoginHandler : IRequestHandler<Login, ServiceResult<Tarjeta>>
    {
        private readonly IATMRepository atmRepository;
        public LoginHandler(IATMRepository atmRepo)
        {
            atmRepository = atmRepo;

        }

        public async Task<ServiceResult<Tarjeta>> Handle(Login request, CancellationToken cancellationToken)
        {
            return await atmRepository.Login(request.NroTarjeta, request.Pin);
        }
    }

}
