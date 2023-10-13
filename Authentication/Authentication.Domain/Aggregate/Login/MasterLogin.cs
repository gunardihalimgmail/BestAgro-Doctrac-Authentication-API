using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Authentication.Domain.Aggregate.Login
{
    public class MasterLogin
    {
        [Key]
        public int ID_Ms_Login { get; set; }
        public int ID_Ms_Karyawan { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ModifyStatus { get; set; }
    }
}
