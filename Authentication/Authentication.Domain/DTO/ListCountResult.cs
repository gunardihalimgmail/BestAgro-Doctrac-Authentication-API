using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Domain.DTO
{
    public class ListCountResult
    {
        public int id_ms_login { get; set; }
        public int listCount { get; set; }
        public string namaForm { get; set; }
        public string path { get; set; }
        public string userkaryawan { get; set; }
        public string divisi { get; set; }
    }
}
