using Allivet.WebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allivet.WebAPI.Infrastructure.Common.Interfaces
{
    public interface IWebScrapperService
    {
        public Task<List<VeterinaryLocation>> GetDataInPetMedWebApp();
    }
}
