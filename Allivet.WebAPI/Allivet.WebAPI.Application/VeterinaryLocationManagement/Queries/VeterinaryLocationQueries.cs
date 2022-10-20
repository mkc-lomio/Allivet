using Allivet.WebAPI.Application.Common.ErrorHandlers;
using Allivet.WebAPI.Application.Common.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using Serilog;
using Allivet.WebAPI.Infrastructure.Common.Interfaces;
using Allivet.WebAPI.Domain.Entities;

namespace Allivet.WebAPI.Application.VeterinaryLocationManagement.Queries
{
    public class VeterinaryLocationQueries : IVeterinaryLocationQueries
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString = string.Empty;
        private readonly IExcelService _excelService;
        private readonly IWebScrapperService _webScrapperService;
        public VeterinaryLocationQueries(IConfiguration configuration,
             IExcelService excelService,
             IWebScrapperService webScrapperService)
        {
            _configuration = configuration;
            _excelService = excelService;
            _connectionString = _configuration.GetValue<string>("ConnectionStrings:AllivetWebApiDb");
            _webScrapperService = webScrapperService;
        }
        public async Task<PaginationViewModel<VeterinaryLocationDTO>> GetPaginatedVeterinaryLocations(int pageSize, int pageNumber, string search)
        {
            try
            {
                var sql = string.Format(@"DECLARE @PageNumber AS INT
DECLARE @RowsOfPage AS INT

SET @PageNumber={1}
SET @RowsOfPage={0}

SELECT * FROM
VeterinaryLocations vl
WHERE 
vl.[Name] Like '%{2}%'
ORDER BY
vl.[Name]  
OFFSET (@PageNumber-1)*@RowsOfPage ROWS 
FETCH NEXT @RowsOfPage 
ROWS ONLY;

SELECT Count(*) FROM
( SELECT * FROM
VeterinaryLocations vl
WHERE 
vl.[Name] Like '%{2}%'
) A ", pageSize,pageNumber,search);

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var multi = await connection.QueryMultipleAsync(sql))
                    {
                        var data = multi.Read<VeterinaryLocationDTO>().ToList();
                        var dataCount = multi.Read<int>().Single();
                        var dataPaginationViewModel = new PaginationViewModel<VeterinaryLocationDTO>(pageNumber, pageSize, dataCount, data);

                        connection.Close();

                        return dataPaginationViewModel;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetPaginatedCountries " + ex.Message);
                throw new CustomErrorException("GetPaginatedCountries " + ex.Message);
            }
        }

        public async Task<List<VeterinaryLocationDTO>> GetVeterinaryLocations()
        {
            try
            {
                var query = string.Format(@"SELECT * FROM VeterinaryLocations");


                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var multi = await connection.QueryMultipleAsync(query))
                    {
                        var data = multi.Read<VeterinaryLocationDTO>().ToList();

                        connection.Close();

                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetVeterinaryLocations " + ex.Message);
                throw new CustomErrorException("GetVeterinaryLocations " + ex.Message);
            }
        }

        public async Task<byte[]> GetVeterinaryLocationsExcelFile()
        {
            try
            {
                var query = string.Format(@"SELECT * FROM VeterinaryLocations");


                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (var multi = await connection.QueryMultipleAsync(query))
                    {
                        var data = multi.Read<VeterinaryLocationDTO>().ToList();

                        connection.Close();

                        List<string> columns = new List<string>() { "Name", "Address", "ContactNumber" };
                        string tempfileName = "VeterinaryLocation";

                        var fileBytes = _excelService.GetDataExcelFileBytes<VeterinaryLocationDTO>(data, columns, tempfileName);
                        return fileBytes;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("GetVeterinaryLocations " + ex.Message);
                throw new CustomErrorException("GetVeterinaryLocations " + ex.Message);
            }
        }

        public async Task<byte[]> GetVeterinaryLocationsFromPetMed()
        {
            try
            {
                var data = await _webScrapperService.GetDataInPetMedWebApp();
                List<string> columns = new List<string>() { "Name", "Address", "ContactNumber" };
                string tempfileName = "VeterinaryLocation";

                var fileBytes = _excelService.GetDataExcelFileBytes<VeterinaryLocation>(data, columns, tempfileName);
                return fileBytes;
            }
            catch (Exception ex)
            {
                Log.Error("GetVeterinaryLocationsFromPetMed " + ex.Message);
                throw new CustomErrorException("GetVeterinaryLocationsFromPetMed " + ex.Message);
            }
        }
    }
}
