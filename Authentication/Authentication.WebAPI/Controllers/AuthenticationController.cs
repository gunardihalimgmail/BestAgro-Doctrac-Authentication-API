using Authentication.Domain.DTO;
using Authentication.WebAPI.Application.Commands;
using Authentication.WebAPI.Application.Queries;
using BestAgro.Middleware;
using BestAgroCore.Common.Api;
using BestAgroCore.Common.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationQueries _authenticationQueries;
        private readonly ICommandHandler<CreateChangePasswordCommand> _createChangePasswordCommand;
        private readonly IConfiguration _configuration;
        private readonly IJwtBuilder _jwtBuilder;


        public AuthenticationController(ILogger<AuthenticationController> logger, 
            IAuthenticationQueries authenticationQueries, ICommandHandler<CreateChangePasswordCommand> createChangePasswordCommand,
            IConfiguration configuration,
            IJwtBuilder jwtBuilder)
        {
            _logger = logger;
            _authenticationQueries = authenticationQueries;
            _createChangePasswordCommand = createChangePasswordCommand;
            _configuration = configuration;
            _jwtBuilder = jwtBuilder;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] CreateLoginCommand request)
        {
            LoginInformation result = new LoginInformation(); 
            try
            {

                Sha1Converter convert = new Sha1Converter();
                var convertedPassword = convert.SHA1Encrypt(request.password);

                var data = await _authenticationQueries.Login(request.username, convertedPassword);
                
                
                if (data.Count != 0)
                {
                    var getUserReplacement = await _authenticationQueries.getUserIDReplacement(data[0].id_ms_login);
                    var filteredData = getUserReplacement.Where(x => x.startdate <= DateTime.Now.Date && x.enddate >= DateTime.Now.Date).ToList();

                    result.Id_Ms_Group = new List<int>();
                    result.Id_Ms_Login = new List<int>();
                    var idmsLoginList = new List<int>();
                    var idmsUnitUsaha = new List<string>();

                    var jwtSection = _configuration.GetSection("jwt");
                    var jwtOptions = jwtSection.Get<JwtOptions>();

                    string key = request.username + request.password;

                    AuthenticationToken jwtInformation = _jwtBuilder.GetToken(key);

                    if (data.Count != 0)
                    {
                        
                        foreach (var item in data)
                        {
                            result.Id_Ms_Group.Add(item.id_ms_group);
                            idmsLoginList.Add(item.id_ms_login);
                        }
                    }

                    if (filteredData.Count != 0)
                    {
                        foreach (var item in filteredData)
                        {
                            result.Id_Ms_Group.Add(item.id_ms_group);
                            idmsLoginList.Add(item.idpemilik);
                        }
                    }

                    List<int> distinctidgroup = result.Id_Ms_Group.Distinct().ToList();
                    List<int> distinctidlogin = idmsLoginList.Distinct().ToList();

                    var unitUsahaList = await _authenticationQueries.getUnitUsaha(distinctidlogin);


                    result.Id_Ms_Group = distinctidgroup;
                    result.Id_Ms_Login = distinctidlogin;
                    result.LoginData = data;
                    result.LoginDataReplacement = filteredData;
                    result.UnitUsahaList = unitUsahaList;
                    result.token = jwtInformation.token;


                    return Ok(new ApiOkResponse(result));
                } 
                else
                {
                    return Ok(new ApiResponse(400, "Username or password is incorrect"));
                }
                    
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error on Login [Username, Password ({request.username},{request.password})]");
                return BadRequest(new ApiBadRequestResponse(500, "Something Wrong"));
            }
        }


        [HttpPost]
        [Route("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] CreateChangePasswordCommand request)
        {
            try
            {
                Sha1Converter convert = new Sha1Converter();
                var OldPasswordConverted = convert.SHA1Encrypt(request.Old_Password);
                var NewPasswordConverted = convert.SHA1Encrypt(request.New_Password);

                var isUserExist = await _authenticationQueries.IsUserExist(request.Id_Ms_Login, OldPasswordConverted);
                if (int.Parse(isUserExist) == 1)
                {
                    request.New_Password = NewPasswordConverted;
                    await _createChangePasswordCommand.Handle(request, CancellationToken.None);
                    return Ok(new ApiResponse(200));
                }
                else
                {
                    return Ok(new ApiResponse(400, "Password lama yang dimasukkan salah"));
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error on Change Password [Id, OldPassword, Password ({request.Id_Ms_Login}, {request.Old_Password},{request.New_Password})]");
                return BadRequest(new ApiBadRequestResponse(500, "Something Wrong"));
            }
        }


    }
}
