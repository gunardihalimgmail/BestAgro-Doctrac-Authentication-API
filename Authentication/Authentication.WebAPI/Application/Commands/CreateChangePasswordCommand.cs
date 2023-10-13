using BestAgroCore.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.WebAPI.Application.Commands
{
    public class CreateChangePasswordCommand : ICommand
    {
        public string Old_Password { get; set; }
        public string New_Password { get; set; }
        public int Id_Ms_Login { get; set; }
    }
}
