using Allivet.WebAPI.Application.Common.ViewModels;
using Allivet.WebAPI.Application.VeterinaryLocationManagement.Commands;
using Allivet.WebAPI.Application.VeterinaryLocationManagement.Queries;
using Allivet.WebAPI.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Allivet.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VeterinaryLocationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<VeterinaryLocationController> _logger;
        private readonly IVeterinaryLocationQueries _veterinaryLocationQueries;
        public VeterinaryLocationController(ILogger<VeterinaryLocationController> logger,
          IMediator mediator,
          IVeterinaryLocationQueries veterinaryLocationQueries)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _veterinaryLocationQueries = veterinaryLocationQueries;
        }

        [HttpPost("", Name = "createVeterinaryLocation")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> CreateVeterinaryLocationAsync([FromBody] CreateVeterinaryLocationCommand command)
        {
            try
            {
                var commandResult = await _mediator.Send(command);

                if (!commandResult)
                {
                    return StatusCode(400, "Unable to process the request");
                }

                return Ok(commandResult);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("veterinarylocations", Name = "getVeterinaryLocations")]
        [ProducesResponseType(typeof(List<VeterinaryLocationDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetVeterinaryLocations()
        {
            try
            {
                var result = await _veterinaryLocationQueries.GetVeterinaryLocations();

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpGet("paginatedveterinarylocation", Name = "getPaginatedVeterinaryLocations")]
        [ProducesResponseType(typeof(PaginationViewModel<VeterinaryLocationDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetPaginatedVeterinaryLocations([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1, [FromQuery] string search = "")
        {
            try
            {
                var result = await _veterinaryLocationQueries.GetPaginatedVeterinaryLocations(pageSize, pageNumber, search);

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("excelfile", Name = "getVeterinaryLocationsExcelFile")]
        [ProducesResponseType(typeof(FileResultFromStream), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> GetVeterinaryLocationsExcelFile()
        {
            try
            {
                var result = await _veterinaryLocationQueries.GetVeterinaryLocationsExcelFile();

                var file = new FileResultFromStream(
                    $"VeterinaryLocation.xlsx",
                    new MemoryStream(result),
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                );

                return file;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
