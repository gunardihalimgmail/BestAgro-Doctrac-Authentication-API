using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Authentication.Domain.Aggregate.UnitUsaha
{
    public class MasterUnitUsaha
    {
        [Key]
        public int ID_Ms_UnitUsaha { get; set; }
        public string Kode { get; set; }
        public string Nama { get; set; }
    }
}
