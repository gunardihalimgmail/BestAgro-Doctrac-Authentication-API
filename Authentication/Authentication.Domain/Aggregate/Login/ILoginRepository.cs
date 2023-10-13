using BestAgroCore.Common.Infrastructure.Data.Contracts;

namespace Authentication.Domain.Aggregate.Login
{
    public interface ILoginRepository : IRepository<MasterLogin>
    {
    }
}
