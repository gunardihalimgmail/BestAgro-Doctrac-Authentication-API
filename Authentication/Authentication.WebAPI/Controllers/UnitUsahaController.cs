using Authentication.Domain.DTO.UnitUsaha;
using Authentication.WebAPI.Application.Queries;
using BestAgro.Middleware;
using BestAgroCore.Common.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UnitUsahaController : ControllerBase
    {
        private readonly ILogger<UnitUsahaController> _logger;
        private readonly IListUnitUsahaQueries _listUnitUsahaQueries;
        private readonly IConfiguration _configuration;
        private readonly IJwtBuilder _jwtBuilder;

        public UnitUsahaController(ILogger<UnitUsahaController> logger,
            IListUnitUsahaQueries listUnitUsahaQueries,
            IConfiguration configuration,
            IJwtBuilder jwtBuilder)
        {
            _logger = logger;
            _listUnitUsahaQueries = listUnitUsahaQueries;
            _configuration = configuration;
            _jwtBuilder = jwtBuilder;
        }

        [HttpGet]
        [Route("getunitusahakodeall")]
        public async Task<IActionResult> GetUnitUsahaKodeAll()
        {
            try
            {
                var data = await _listUnitUsahaQueries.GetUnitUsahaKodeAll();

                return Ok(new ApiOkResponse(data));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error on Get Unit Usaha Kode All");
                return BadRequest(new ApiBadRequestResponse(500, "Something Wrong"));
            }
        }
    }
}
