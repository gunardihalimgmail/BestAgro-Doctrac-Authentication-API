using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.WebAPI.Application.Commands
{
    public class CreateLoginCommand
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
