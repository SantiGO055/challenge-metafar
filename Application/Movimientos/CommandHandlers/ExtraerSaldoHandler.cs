using Application.Abstractions;
using Application.Movimiento.Commands;
using Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Movimiento.CommandHandlers
{
    public class ExtraerSaldoHandler : IRequestHandler<ExtraerSaldo, ServiceResult<Saldo>>
    {
        private readonly IATMRepository atmRepository;
        public ExtraerSaldoHandler(IATMRepository atmRepo)
        {
            atmRepository = atmRepo;
            
        }

        public async Task<ServiceResult<Saldo>> Handle(ExtraerSaldo request, CancellationToken cancellationToken)
        {
            var cuenta = await atmRepository.ExtraerSaldo(request.tarjeta, request.saldo);
            return cuenta;
        }
    }
}
