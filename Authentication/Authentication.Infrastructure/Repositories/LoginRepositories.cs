using Authentication.Domain.Aggregate.Login;
using BestAgroCore.Infrastructure.Data.EFRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Infrastructure.Repositories
{
    public class LoginRepositories : EfRepository<MasterLogin>, ILoginRepository
    {
        private readonly AuthenticationContext _context;

        public LoginRepositories(AuthenticationContext context ) : base(context)
        {
            _context = context;
        }
    }
}
