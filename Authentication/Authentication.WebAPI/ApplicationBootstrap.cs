using Authentication.Domain.Aggregate.Login;
using Authentication.Infrastructure;
using Authentication.Infrastructure.Repositories;
using Authentication.WebAPI.Application.Commands;
using Authentication.WebAPI.Application.Queries;
using BestAgroCore.Common.Domain;
using BestAgroCore.CrossCutting;
using BestAgroCore.CrossCutting.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.WebAPI
{
    public static class ApplicationBootstrap
    {

        public static IServiceCollection InitBootstraper(this IServiceCollection services, IConfiguration configuration)
        {
            services.InitBestAgroBootstrap_v2()
                .RegisterEf()
                .AddDbContext<AuthenticationContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("JVE")));

            return services;
        }

        public static IServiceCollection InitAppServices(this IServiceCollection services)
        {
            #region Command
            // add scope for command handler
            services.AddScoped<ILoginRepository, LoginRepositories>();
            #endregion

            #region Query
            // add scope for query
            services.AddScoped<IAuthenticationQueries, AuthenticationQueries>();
            services.AddScoped<IMenuQueries, MenuQueries>();
            services.AddScoped<IListUnitUsahaQueries, ListUnitUsahaQueries>();
            #endregion

            return services;
        }


        public static IServiceCollection InitEventHandlers(this IServiceCollection services)
        {
            services.AddTransient<ICommandHandler<CreateChangePasswordCommand>, CreateChangePasswordCommandHandler>();


            return services;
        }


    }
}
