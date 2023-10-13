using Authentication.Domain.Aggregate.Login;
using Authentication.Infrastructure;
using Authentication.WebAPI.Application.Queries;
using BestAgroCore.Common.Domain;
using BestAgroCore.Infrastructure.Data.EFRepositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.WebAPI.Application.Commands
{
    public class CreateChangePasswordCommandHandler : ICommandHandler<CreateChangePasswordCommand>
    {
        private readonly IUnitOfWork<AuthenticationContext> _uow;
        private readonly IAuthenticationQueries _authenticationQueries;
        private readonly ILoginRepository _loginRepository;
        public CreateChangePasswordCommandHandler(IUnitOfWork<AuthenticationContext> uow,
            ILoginRepository loginRepository,
            IAuthenticationQueries authenticationQueries)
        {
            _uow = uow;
            _authenticationQueries = authenticationQueries;
            _loginRepository = loginRepository;
        }

        public async Task Handle(CreateChangePasswordCommand command, CancellationToken cancellationToken)
        {
            try
            {

                var user = await _authenticationQueries.GetUserById(command.Id_Ms_Login);
                if (user != null)
                {
                    user.Password = command.New_Password;

                    _loginRepository.Update(user);
                }

                await _uow.CommitAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
