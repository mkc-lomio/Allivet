using Allivet.WebAPI.Application.Common.ErrorHandlers;
using Allivet.WebAPI.Domain.Entities;
using Allivet.WebAPI.Infrastructure.Common.Interfaces;
using AutoMapper;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Allivet.WebAPI.Application.VeterinaryLocationManagement.Commands
{
    public class CreateVeterinaryLocationCommandHandler : IRequestHandler<CreateVeterinaryLocationCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IVeterinaryLocationRepository _veterinaryLocationRepository;
        public CreateVeterinaryLocationCommandHandler(IMapper mapper,
        IVeterinaryLocationRepository veterinaryLocationRepository)
        {
            _mapper = mapper;
            _veterinaryLocationRepository = veterinaryLocationRepository;
        }

        public async Task<bool> Handle(CreateVeterinaryLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Log.Information("CreateVeterinaryLocationCommand");
                var country = _mapper.Map<CreateVeterinaryLocationCommand, VeterinaryLocation>(request);

                _veterinaryLocationRepository.Create(country);

                return await _veterinaryLocationRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Log.Error("CreateVeterinaryLocationCommand " + ex.Message);
                throw new CustomErrorException("CreateVeterinaryLocationCommand " + ex.Message);
            }
        }
    }
}
