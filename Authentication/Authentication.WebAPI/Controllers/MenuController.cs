using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.Domain.DTO;
using Authentication.WebAPI.Application.Queries;
using BestAgroCore.Common.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Authentication.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly ILogger<MenuController> _logger;
        private readonly IMenuQueries _menuQueries;


        public MenuController(ILogger<MenuController> logger, IMenuQueries menuQueries)
        {
            _logger = logger;
            _menuQueries = menuQueries;
        }


        [HttpPost]
        [Route("menu")]
        public async Task<IActionResult> GetMenu(groupID request)
        {
            try
            {
                var result = new List<Menu>();

                foreach (var item in request.id_ms_group)
                {
                    List<Menu> modulData = await _menuQueries.GetModul(item);

                    //add data to list
                    for (int i = 0; i < modulData.Count; i++)
                    {
                        var singleStringData = modulData[i];
                        result.Add(singleStringData);
                    }

                    foreach (var item_ in modulData)
                    {
                        item_.forms = (List<form>)await _menuQueries.GetForm(item);
                    }
                }


                var distictMenu = result.GroupBy(groupModul => groupModul.modulname)
                    .Select(selectModul =>
                    new Menu
                    {
                        //id_ms_group = groupMenu.Select(selectModul => selectModul.id_ms_group).FirstOrDefault(),
                        modulname = selectModul.Key,
                        modularrow = selectModul.Select(x => x.modularrow).FirstOrDefault(),
                        modulpath = selectModul.Select(x => x.modulpath).FirstOrDefault(),
                        modulicon = selectModul.Select(x => x.modulicon).FirstOrDefault(),
                        forms = selectModul.SelectMany(x => x.forms).GroupBy(groupForm => new
                        {
                            groupForm.formname,
                            groupForm.aksescreate,
                            groupForm.aksesread,
                            groupForm.aksesupdate,
                            groupForm.aksesdelete,
                            groupForm.aksesinternal,
                            groupForm.aksesexternal
                        }).Select(selectForm => new form
                        {
                            formname = selectForm.Key.formname,
                            namamodul = selectForm.Select(x => x.namamodul).FirstOrDefault(),
                            formpath = selectForm.Select(x => x.formpath).FirstOrDefault(),
                            formflag = selectForm.Select(x => x.formflag).FirstOrDefault(),
                            aksescreate = selectForm.Key.aksescreate,
                            aksesread = selectForm.Key.aksesread,
                            aksesupdate = selectForm.Key.aksesupdate,
                            aksesdelete = selectForm.Key.aksesdelete,
                            aksesinternal = selectForm.Key.aksesinternal,
                            aksesexternal = selectForm.Key.aksesexternal
                        }).ToList()
                    }).ToList();


                // new menu after distinct
                List<Menu> NewMenu = new List<Menu>();

                for (int i = 0; i < distictMenu.Count; i++)
                {
                    Menu myMenu = new Menu();

                    myMenu.modulname = distictMenu[i].modulname;
                    myMenu.modularrow = distictMenu[i].modularrow;
                    myMenu.modulpath = distictMenu[i].modulpath;
                    myMenu.modulicon = distictMenu[i].modulicon;
                    myMenu.forms = new List<form>();

                    // Forms
                    var secondDistinct = (from a in distictMenu[i].forms
                                    select new
                                    {
                                        a.namamodul,
                                        a.formname,
                                        a.formpath,
                                        a.formflag
                                    }).Distinct().ToList();


                    for (int j = 0; j < secondDistinct.Count; j++)
                    {
                        string namamodul = secondDistinct[j].namamodul;
                        string formname = secondDistinct[j].formname;
                        string formpath = secondDistinct[j].formpath;
                        string formflag = secondDistinct[j].formflag;


                        form myForm = new form();
                        myForm.namamodul = namamodul;
                        myForm.formname = formname;
                        myForm.formpath = formpath;
                        myForm.formflag = formflag;


                        try
                        {
                            // akses create
                            var getAksesCreate = (from x in distictMenu[i].forms
                                                  where x.aksescreate == "1"
                                                  && x.namamodul == namamodul
                                                  && x.formname == formname
                                                  && x.formpath == formpath
                                                  && x.formflag == formflag
                                                  select x);

                            if (getAksesCreate.Any())
                                myForm.aksescreate = "1";
                            else
                                myForm.aksescreate = "0";


                            // akses read
                            var getAksesRead = (from x in distictMenu[i].forms
                                                  where x.aksesread == "1"
                                                  && x.namamodul == namamodul
                                                  && x.formname == formname
                                                  && x.formpath == formpath
                                                  && x.formflag == formflag
                                                  select x);

                            if (getAksesRead.Any())
                                myForm.aksesread = "1";
                            else
                                myForm.aksesread = "0";


                            // akses update
                            var getAksesUpdate = (from x in distictMenu[i].forms
                                                  where x.aksesupdate == "1"
                                                  && x.namamodul == namamodul
                                                  && x.formname == formname
                                                  && x.formpath == formpath
                                                  && x.formflag == formflag
                                                  select x);

                            if (getAksesUpdate.Any())
                                myForm.aksesupdate = "1";
                            else
                                myForm.aksesupdate = "0";


                            // akses delete
                            var getAksesDelete = (from x in distictMenu[i].forms
                                                  where x.aksesdelete == "1"
                                                  && x.namamodul == namamodul
                                                  && x.formname == formname
                                                  && x.formpath == formpath
                                                  && x.formflag == formflag
                                                  select x);

                            if (getAksesDelete.Any())
                                myForm.aksesdelete = "1";
                            else
                                myForm.aksesdelete = "0";


                            // akses internal
                            var getAksesInternal = (from x in distictMenu[i].forms
                                                  where x.aksesinternal == "1"
                                                  && x.namamodul == namamodul
                                                  && x.formname == formname
                                                  && x.formpath == formpath
                                                  && x.formflag == formflag
                                                  select x);

                            if (getAksesInternal.Any())
                                myForm.aksesinternal = "1";
                            else
                                myForm.aksesinternal = "0";


                            // akses internal
                            var getAksesExternal = (from x in distictMenu[i].forms
                                                    where x.aksesexternal == "1"
                                                    && x.namamodul == namamodul
                                                    && x.formname == formname
                                                    && x.formpath == formpath
                                                    && x.formflag == formflag
                                                    select x);

                            if (getAksesExternal.Any())
                                myForm.aksesexternal = "1";
                            else
                                myForm.aksesexternal = "0";

                        }
                        catch (Exception)
                        {

                            throw;
                        }

                        myMenu.forms.Add(myForm);
                    }

                    NewMenu.Add(myMenu);
                }


                return Ok(new ApiOkResponse(NewMenu));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error on Get Menu [{request.id_ms_group}]");
                return BadRequest(new ApiBadRequestResponse(500, "Something Wrong"));
            }
        }


        [HttpPost]
        [Route("getlistcount")]
        public async Task<IActionResult> GetListCount(loginID request)
        {
            try
            {
                var result = new List<ListCountResult>();

                var data = await _menuQueries.GetListDataCount(request.id_ms_login, request.id_ms_group);


                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].StoredProcedure != null)
                    {
                        ListCountResult single = new ListCountResult();

                        var dataCount = await _menuQueries.GetListCount(data[i].ID_Ms_Login, data[i].Bagian, data[i].Divisi, data[i].PT, data[i].StoredProcedure);
                        var countedData = dataCount.Count();

                        single.id_ms_login = data[i].ID_Ms_Login;
                        single.namaForm = data[i].NamaForm;
                        single.path = data[i].Path;
                        single.listCount = countedData;
                        single.divisi = data[i].Divisi;
                        single.userkaryawan = data[i].Karyawan;
                        result.Add(single);
                    }
                    else
                    {
                        continue;
                    }
                    
                }

                return Ok(new ApiOkResponse(result));
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error on Get List Counted Data");
                return BadRequest(new ApiBadRequestResponse(500, "Something Wrong"));
            }
        }


    }
}
