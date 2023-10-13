using System;
using System.Collections.Generic;
using System.Text;
using Authentication.Domain.DTO.UnitUsaha;

namespace Authentication.Domain.DTO
{
    public class LoginInformation
    {
        
        public List<dataLogin> LoginData { get; set; }
        public List<dataLoginReplacement> LoginDataReplacement { get; set; }
        public List<int> Id_Ms_Group { get; set; }
        public List<int> Id_Ms_Login { get; set; }
        public List<ListUnitUsaha> UnitUsahaList { get; set; }
        public string token { get; set; }
    }

    public class dataLogin
    {
        public int id_ms_login { get; set; }
        public int id_ms_group { get; set; }
        public string namagroup { get; set; }
        public string namakaryawan { get; set; }
        public string bagian { get; set; }
        public int id_ms_divisi { get; set; }
        public int id_ms_unitusaha { get; set; }
    }

    public class dataLoginReplacement
    {
        public int idpemilik { get; set; }
        public int id_ms_group { get; set; }
        public DateTime? startdate { get; set; }
        public DateTime? enddate { get; set; }
    }
}
