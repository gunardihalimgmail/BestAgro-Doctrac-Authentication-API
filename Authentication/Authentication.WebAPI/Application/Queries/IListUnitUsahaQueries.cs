using Authentication.Domain.DTO.UnitUsaha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.WebAPI.Application.Queries
{
    public interface IListUnitUsahaQueries
    {
        Task<List<ListUnitUsaha>> GetUnitUsahaKodeAll();
    }
}
