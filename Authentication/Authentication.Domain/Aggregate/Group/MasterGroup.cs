using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Authentication.Domain.Aggregate.Group
{
    public class MasterGroup
    {
        [Key]
        public int id { get; set; }
        public string namagroup { get; set; }
        public int id_ms_aplikasi { get; set; }
        public int status { get; set; }
    }
}
