using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Domain.DTO
{
    
    public class Menu
    {
        //public int id_ms_group { get; set; }
        public string modulname { get; set; }
        public string modulpath { get; set; }
        public string modularrow { get; set; }
        public string modulicon { get; set; }
        public List<form> forms { get; set; }

    }
    
    public class form
    {
        public string namamodul { get; set; }
        public string formname { get; set; }
        public string formpath { get; set; }
        public string formflag { get; set; }
        public string aksescreate { get; set; }
        public string aksesread { get; set; }
        public string aksesupdate { get; set; }
        public string aksesdelete { get; set; }
        public string aksesinternal { get; set; }
        public string aksesexternal { get; set; }
        
    }

    public class groupID{
        public List<int> id_ms_group { get; set; }
    }

    public class loginID
    {
        public List<int> id_ms_login { get; set; }
        public List<int> id_ms_group { get; set; }
    }
}
