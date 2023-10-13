using Authentication.Domain.DTO.UnitUsaha;
using BestAgroCore.Common.Infrastructure.Data.Contracts;
using BestAgroCore.Infrastructure.Data.DapperRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.WebAPI.Application.Queries
{
    public class ListUnitUsahaQueries : IListUnitUsahaQueries
    {
        private readonly IDbSQLConectionFactory _dbConnectionFactorySQL;

        public ListUnitUsahaQueries(IDbSQLConectionFactory dbSQLConectionFactory)
        {
            _dbConnectionFactorySQL = dbSQLConectionFactory;
        }

        public async Task<List<ListUnitUsaha>> GetUnitUsahaKodeAll()
        {
            try
            {
                var qry = "select uu.ID_Ms_UnitUsaha as value, uu.Kode as label " +
                    "from Ms_UnitUsaha uu with(nolock) " +
                    "where uu.ID_Ms_UnitUsaha in (6, 7, 8, 9, 32, 10, 11, 12, 13, 14, 15, 28, 16, 19, 20)";

                var data = await new DapperRepository<ListUnitUsaha>(_dbConnectionFactorySQL.GetDbConnection("JVE"))
                    .QueryAsync(qry);

                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
