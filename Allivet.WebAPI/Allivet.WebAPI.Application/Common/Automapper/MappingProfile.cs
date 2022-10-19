using Allivet.WebAPI.Application.VeterinaryLocationManagement.Commands;
using Allivet.WebAPI.Domain.Entities;
using AutoMapper;

namespace Allivet.WebAPI.Application.Common.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Command to Entity
            CreateMap<CreateVeterinaryLocationCommand, VeterinaryLocation>();
            #endregion
        }

    }
}
