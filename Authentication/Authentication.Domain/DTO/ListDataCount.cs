using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Domain.DTO
{
    public class ListDataCount
    {
        public int ID_Ms_Login { get; set; }
        public string NamaForm { get; set; }
        public string StoredProcedure { get; set; }
        public string Path { get; set; }
        public string Karyawan { get; set; }
        public string Divisi { get; set; }
        public string Bagian { get; set; }
        public string PT { get; set; }
    }
}
