using Authentication.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.WebAPI.Application.Queries
{
    public interface IMenuQueries
    {
        Task<List<form>> GetForm(int id_ms_group);
        Task<List<Menu>> GetModul(int id_ms_group);
        Task<List<ListDataCount>> GetListDataCount(List<int> id_ms_login, List<int> id_ms_group);
        Task<List<CountedList>> GetListCount(int id_ms_login, string bagian, string unitusaha, string divisi, string storedProcedure);
    }
}
