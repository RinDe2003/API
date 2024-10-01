using Repo_API_1721030646.Helpers;
using Repo_API_1721030646.Models;
using Repo_API_1721030646.Repo.Generic_W1;
using Repo_API_1721030646.Repo.Generic_W2;
using Repo_API_1721030646.Repo.Simple;

namespace Repo_API_1721030646.Helpers
{
    public static class DI_RepositoryService
    {
        // Đăng ký các RepositoryService
        public static IServiceCollection MyConfigureRepositoryService(this IServiceCollection services)
        {            
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            //APITeaching Repository
            //Simple Repository
            services.AddScoped<IAccountService, AccountService>();

            //First Approach (way 1) about Repository Patten in Powerpoint file   
            services.AddScoped<IRepo<Order>, OrderService>();

            //===================================================================
            //Second approach (way 2) about Repository Patten in Powerpoint file
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericAPITeachingRepo<>));

            return services;
        }
    }
}
