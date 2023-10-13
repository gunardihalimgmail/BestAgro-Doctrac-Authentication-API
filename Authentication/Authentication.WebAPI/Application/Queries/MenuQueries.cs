using Authentication.Domain.DTO;
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
    public class MenuQueries : IMenuQueries
    {
        private readonly IDbSQLConectionFactory _dbConnectionFactorySQL;

        public MenuQueries(IDbSQLConectionFactory dbConnectionFactorySQL)
        {
            _dbConnectionFactorySQL = dbConnectionFactorySQL;
        }

        public async Task<List<form>> GetForm(int id_ms_group)
        {
            try
            {
                var qry = " select frm.Flag as formflag, " +
                    "REPLACE(mod.NamaModule, 'Doctrac - ', '') as namamodul, " +
                    "frm.NamaForm as formname, " +
                    "frm.Path as formpath, " +
                    "frmgrp.[Create] as aksescreate, " +
                    "frmgrp.[Read] as aksesread, " +
                    "frmgrp.[Update] as aksesupdate, " +
                    "frmgrp.[Delete] as aksesdelete, " +
                    "frmgrp.Internal as aksesinternal, " +
                    "frmgrp.[External] as aksesexternal " +
                    "from Ms_Form frm " +
                    "join Ms_Module mod on frm.Id_Ms_Module = mod.Id " +
                    "join Ms_Form_Group frmgrp on frmgrp.Id_Ms_Form = frm.Id " +
                    "where frmgrp.Id_Ms_Group = @p_id_ms_group " +
                    "and (frm.Status = 1 and frmgrp.Status = 1); ";


                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@p_id_ms_group", id_ms_group, DbType.Int32, ParameterDirection.Input);

                var data = await new DapperRepository<form>(_dbConnectionFactorySQL.GetDbConnection("AppRole"))
                    .QueryAsync(qry, parameters);

                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Menu>> GetModul(int id_ms_group)
        {
            try
            {
                var qry = " select DISTINCT frmgrp.Id_Ms_Group as id_ms_group, " +
                    "REPLACE(mod.NamaModule, 'Doctrac - ', '') as modulname, " +
                    "mod.Path as modulpath, " +
                    "mod.Arrow as modularrow, " +
                    "mod.Icon as modulicon " +
                    "from AppRole2.dbo.Ms_Form frm " +
                    "join AppRole2.dbo.Ms_Form_Group frmgrp on frmgrp.Id_Ms_Form = frm.Id " +
                    "join AppRole2.dbo.Ms_Module mod on mod.Id = frm.Id_Ms_Module " +
                    "where frmgrp.Id_Ms_Group = @p_id_ms_group and mod.Status = 1; ";

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@p_id_ms_group", id_ms_group, DbType.Int32, ParameterDirection.Input);

                var data = await new DapperRepository<Menu>(_dbConnectionFactorySQL.GetDbConnection("AppRole"))
                    .QueryAsync(qry, parameters);

                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ListDataCount>> GetListDataCount(List<int> id_ms_login, List<int> id_ms_group)
        {
            try
            {
                var qry = " select " +
                    "usrgroup.Id_Ms_Login, " +
                    "frm.NamaForm, " +
                    "frm.StoredProcedure, " +
                    "frm.Path, " +
                    "log.Username as Karyawan," +
                    "div.Nama as Divisi, " +
                    "bag.Nama as Bagian, " +
                    "usaha.Kode as PT " +
                    "from AppRole2.dbo.Ms_User_Group usrgroup " +
                    "join AppRole2.dbo.Ms_Form_Group frmgrp on usrgroup.Id_Ms_Group = frmgrp.Id_Ms_Group " +
                    "join AppRole2.dbo.Ms_Form frm on frmgrp.Id_Ms_Form = frm.Id " +
                    "join Ms_Login log on usrgroup.ID_Ms_Login = log.ID_Ms_Login " + 
                    "join Ms_Karyawan kar on log.ID_Ms_Karyawan = kar.ID_Ms_Karyawan " + 
                    "join Ms_UnitUsaha usaha on kar.ID_Ms_UnitUsaha = usaha.ID_Ms_UnitUsaha " + 
                    "join Ms_Divisi div on kar.ID_Ms_Divisi = div.ID_Ms_Divisi " + 
                    "join Ms_Bagian bag on kar.ID_Ms_Bagian = bag.ID_Ms_Bagian " + 
                    "where usrgroup.Id_Ms_Login in @p_id_ms_login " +
                    "and usrgroup.Id_Ms_Group in @p_id_ms_group " +
                    "and (frm.Status = 1 and frmgrp.Status = 1); ";


                var data = await new DapperRepository<ListDataCount>(_dbConnectionFactorySQL.GetDbConnection("JVE"))
                    .QueryAsync(qry, new { p_id_ms_login = id_ms_login.ToArray(), p_id_ms_group = id_ms_group.ToArray() });

                return data.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CountedList>> GetListCount(int id_ms_login, string bagian, string divisi, string unitusaha, string storedProcedure)
        {
            try
            {
                var listDoc = new List<CountedList>();
                var qry = "";

                if (storedProcedure.Contains("|"))
                {
                    string[] splitedSP = storedProcedure.Split('|');

                    if (id_ms_login == 2 || id_ms_login == 3 || id_ms_login == 4)
                    {
                        qry = " EXEC " + splitedSP[1] + " @IdLogin = @p_id_ms_login; ";
                    }
                    else if (id_ms_login == 200 || id_ms_login == 201 || id_ms_login == 202)
                    {
                        
                        List<string> ListKodeUnitUsaha = new List<string>();

                        ListKodeUnitUsaha.Add("W1");
                        ListKodeUnitUsaha.Add("W2");
                        ListKodeUnitUsaha.Add("B1");
                        ListKodeUnitUsaha.Add("B2");
                        ListKodeUnitUsaha.Add("B3");
                        ListKodeUnitUsaha.Add("H1");
                        ListKodeUnitUsaha.Add("H2");
                        ListKodeUnitUsaha.Add("T1");
                        ListKodeUnitUsaha.Add("T2");
                        ListKodeUnitUsaha.Add("T3");
                        ListKodeUnitUsaha.Add("SC1");
                        ListKodeUnitUsaha.Add("SC2");
                        ListKodeUnitUsaha.Add("KL");
                        ListKodeUnitUsaha.Add("KL2");
                        ListKodeUnitUsaha.Add("BA");
                        ListKodeUnitUsaha.Add("BE");

                        foreach (var item in ListKodeUnitUsaha.ToList())
                        {
                            var qry_ = "usp_Dt_GetDokumenBelumRequest";

                            DynamicParameters parameters = new DynamicParameters();

                            parameters.Add("@KodeUnitUsaha", item, DbType.String, ParameterDirection.Input);

                            var data_ = await new DapperRepository<CountedList>(_dbConnectionFactorySQL.GetDbConnection("JVE"))
                                .QueryStoredProcedureAsync(qry_, parameters);


                            foreach (var doc in data_.ToList())
                            {
                                listDoc.Add(doc);
                            }
                        }
                    }
                    else if (divisi == "FINE" && (bagian == "EST" || bagian == "FAC"))
                    {
                        qry = "usp_Dt_GetDokumenBelumRequest";

                        DynamicParameters parameters = new DynamicParameters();

                        parameters.Add("@KodeUnitUsaha", unitusaha, DbType.String, ParameterDirection.Input);

                        var data = await new DapperRepository<CountedList>(_dbConnectionFactorySQL.GetDbConnection("JVE"))
                            .QueryStoredProcedureAsync(qry, parameters);

                        return data.ToList();
                    }
                    else
                    {
                        qry = " EXEC " + splitedSP[0] + " @IdLogin = @p_id_ms_login; ";
                    }
                }
                else
                {
                    if (storedProcedure == "usp_Dt_GetPengajuanGradeKaryawan")
                    {
                        qry = " EXEC usp_Dt_GetPengajuanGradeKaryawan; ";
                    }
                    else
                    {
                        qry = " EXEC @p_sp @IdLogin = @p_id_ms_login; ";
                    }
                }

                if (listDoc.Count() != 0)
                {
                    return listDoc;
                }
                else
                {

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@p_id_ms_login", id_ms_login, DbType.Int32, ParameterDirection.Input);
                    parameters.Add("@p_sp", storedProcedure, DbType.String, ParameterDirection.Input);

                    var data = await new DapperRepository<CountedList>(_dbConnectionFactorySQL.GetDbConnection("JVE"))
                    .QueryAsync(qry, parameters);

                    return data.ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
