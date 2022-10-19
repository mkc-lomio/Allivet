using Allivet.WebAPI.Application.Common.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Allivet.WebAPI.Application.VeterinaryLocationManagement.Queries
{
    public interface IVeterinaryLocationQueries
    {
        Task<List<VeterinaryLocationDTO>> GetVeterinaryLocations();
        Task<PaginationViewModel<VeterinaryLocationDTO>> GetPaginatedVeterinaryLocations(int pageSize, int pageNumber, string search);
        Task<byte[]> GetVeterinaryLocationsExcelFile();

    }
}
