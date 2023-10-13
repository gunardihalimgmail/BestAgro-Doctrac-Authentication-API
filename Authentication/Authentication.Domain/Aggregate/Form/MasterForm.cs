using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Authentication.Domain.Aggregate.Form
{
    public class MasterForm
    {
        [Key]
        public int id { get; set; }
        public string namaform { get; set; }
        public int id_ms_module { get; set; }
        public int status { get; set; }
    }
}
