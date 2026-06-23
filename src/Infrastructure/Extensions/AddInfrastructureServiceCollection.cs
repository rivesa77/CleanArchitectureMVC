// <copyright file="AddInfrastructureServiceCollection.cs" company="Ricardo">
// Copyright (c) Ricardo. All rights reserved.
// </copyright>

namespace Ricardo.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Ricardo.Application.Repositories;
    using Ricardo.Application.UserInfo;
    using Ricardo.Infrastructure.Data;
    using Ricardo.Infrastructure.Data.Repositories;
    using Ricardo.Infrastructure.UserInfo;

    public static class AddInfrastructureServiceCollection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddHttpContextAccessor();

            services
                .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services
                .AddScoped<IPersonRepository, PersonRepository>()
                .AddScoped<IPersonUserDetails, PersonUserDetails>();

            return services;
        }
    }
}
