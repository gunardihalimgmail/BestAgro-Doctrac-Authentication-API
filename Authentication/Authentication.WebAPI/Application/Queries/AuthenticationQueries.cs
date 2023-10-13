using Authentication.Domain.Aggregate.Login;
using Authentication.Domain.DTO;
using Authentication.Domain.DTO.UnitUsaha;
using BestAgroCore.Common.Infrastructure.Data.Contracts;
using BestAgroCore.Infrastructure.Data.DapperRepositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.WebAPI.Application.Queries
{
    public class AuthenticationQueries : IAuthenticationQueries
    {
        private readonly IDbSQLConectionFactory _dbConnectionFactorySQL;

        public AuthenticationQueries(IDbSQLConectionFactory dbConectionFactorySQL)
        {
            _dbConnectionFactorySQL = dbConectionFactorySQL;
        }

        public async Task<List<dataLogin>> Login(string username, string password)
        {
            try
            {   
                var qry = " select login.ID_Ms_Login as id_ms_login, " +
                    "grp.Id as id_ms_group, " +
                    "karyawan.Nama as namakaryawan, " +
                    "grp.NamaGroup as namagroup, " +
                    "bagian.Nama as bagian, " +
                    "karyawan.ID_Ms_Divisi as id_ms_divisi, " +
                    "karyawan.ID_Ms_UnitUsaha as id_ms_unitusaha " +
                    "from Ms_Karyawan karyawan " + 
                    "join Ms_Login login on login.ID_Ms_Karyawan = karyawan.ID_Ms_Karyawan " +
                    "join Ms_Bagian bagian on karyawan.ID_Ms_Bagian = bagian.ID_Ms_Bagian " +
                    "join AppRole2.dbo.Ms_User_Group usrgroup on usrgroup.Id_Ms_Login = login.ID_Ms_Login " +
                    "join AppRole2.dbo.Ms_Group grp on grp.Id = usrgroup.Id_Ms_Group " +
                    "join AppRole2.dbo.Ms_Aplikasi apps on grp.Id_Ms_Aplikasi = apps.Id " +
                    "where login.Username = @p_username " +
                    "and login.Password = @p_password " +
                    "and usrgroup.Status = 1 " +
                    "and apps.Id = 3; ";

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@p_username", username, DbType.String, ParameterDirection.Input);
                parameters.Add("@p_password", password, DbType.String, ParameterDirection.Input);

                var data = await new DapperRepository<dataLogin>(_dbConnectionFactorySQL.GetDbConnection("JVE"))
                    .QueryAsync(qry, parameters);

                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MasterLogin> GetUserById(int Id_Ms_Login)
        {
            try
            {
                var qry = "select * from Ms_Login where ID_Ms_Login = @p_id_ms_login ";

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@p_id_ms_login", Id_Ms_Login, DbType.Int32, ParameterDirection.Input);

                var data = await new DapperRepository<MasterLogin>(_dbConnectionFactorySQL.GetDbConnection("JVE"))
                    .QueryAsync(qry, parameters);

                return data.SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<dataLoginReplacement>> getUserIDReplacement(int id_ms_login)
        {
            try
            {
                var qry = " select DISTINCT " +
                    "repl.Id_Pemilik as idpemilik, " +
                    "usrgroup.Id_Ms_Group as id_ms_group, " +
                    "repl.DateStart as startdate, " +
                    "repl.DateEnd as enddate " +
                    "from AppRole2.dbo.Ms_Aplikasi_User_Replacement repl " +
                    "join AppRole2.dbo.Ms_User_Group usrgroup on repl.Id_Pemilik = usrgroup.Id_Ms_Login " +
                    "join AppRole2.dbo.Ms_Group grp on usrgroup.Id_Ms_Group = grp.Id " +
                    "where Id_Pengganti = @p_id_ms_login " +
                    "and grp.Id_Ms_Aplikasi = 3 " +
                    "and repl.Id_Ms_Aplikasi = 3;";

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@p_id_ms_login", id_ms_login, DbType.Int32, ParameterDirection.Input);

                var data = await new DapperRepository<dataLoginReplacement>(_dbConnectionFactorySQL.GetDbConnection("JVE"))
                    .QueryAsync(qry, parameters);

                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<ListUnitUsaha>> getUnitUsaha(List<int> id_ms_login)
        {
            try
            {
                var qry = " select ID_Ms_UnitUsaha as value, Kode as label from Ms_UnitUsaha where ID_MS_UnitUsaha in " +
                    "(select DISTINCT Id_Ms_UnitUsaha from AppRole2.dbo.Ms_User_UnitUsaha where Id_Ms_Login in @p_id_ms_login); ";


                var data = await new DapperRepository<ListUnitUsaha>(_dbConnectionFactorySQL.GetDbConnection("JVE"))
                    .QueryAsync(qry, new { p_id_ms_login = id_ms_login.ToArray() });

                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<string> IsUserExist(int id_ms_login, string password)
        {
            try
            {
                var qry = " select count(*) from Ms_Login " +
                          " where Password = @p_password and ID_Ms_Login = @p_id_ms_login ";

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@p_id_ms_login", id_ms_login, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@p_password", password, DbType.String, ParameterDirection.Input);

                var data = await new DapperRepository<string>(_dbConnectionFactorySQL.GetDbConnection("JVE"))
                    .QueryAsync(qry, parameters);

                return data.SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
