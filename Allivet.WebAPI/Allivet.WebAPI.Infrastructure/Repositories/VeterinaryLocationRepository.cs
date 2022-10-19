using Allivet.WebAPI.Domain.Context;
using Allivet.WebAPI.Domain.Entities;
using Allivet.WebAPI.Infrastructure.Common.Interfaces;

namespace Allivet.WebAPI.Infrastructure.Repositories
{
   public class VeterinaryLocationRepository : RepositoryBase<VeterinaryLocation>, IVeterinaryLocationRepository
    {
        public VeterinaryLocationRepository(AllivetWebAPIDbContext context) : base(context)
        {

        }
    }
}
