using Authentication.Domain.Aggregate.Login;
using Authentication.Domain.DTO;
using Authentication.Domain.DTO.UnitUsaha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.WebAPI.Application.Queries
{
    public interface IAuthenticationQueries
    {
        Task<List<dataLogin>> Login(string username, string password);
        Task<List<dataLoginReplacement>> getUserIDReplacement(int id_ms_login);
        Task<List<ListUnitUsaha>> getUnitUsaha(List<int> id_ms_login);
        Task<MasterLogin> GetUserById(int Id_Ms_Login);
        Task<string> IsUserExist(int id_ms_login, string password);
    }
}
