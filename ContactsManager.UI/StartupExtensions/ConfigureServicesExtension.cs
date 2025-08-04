using ContactsManager.Core.Domain.Entities;
using CRUDExample.Filters.ActionFilters;
using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Services;

namespace CRUDExample
{
 public static class ConfigureServicesExtension
 {
  public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
  {
   services.AddTransient<ResponseHeaderActionFilter>();  // need this inject in factory

			//it adds controllers and views as services
			services.AddControllersWithViews(options => {
    //options.Filters.Add<ResponseHeaderActionFilter>(5);

    var logger = services.BuildServiceProvider().GetRequiredService<ILogger<ResponseHeaderActionFilter>>();

    options.Filters.Add(new ResponseHeaderActionFilter(logger)
    {
     Key = "My-Key-From-Global",
     Value = "My-Value-From-Global",
     Order = 2
    }); // We implement iorderdfilter to can add value to order in this global filter
   });  //options.Filters.Add<PersonHeaderActionFilter>(5) if your filter dosn't have additional paramaters and you don't need to implement iorderdfilter

			//add services into IoC container
			services.AddScoped<ICountriesRepository, CountriesRepository>();
   services.AddScoped<IPersonsRepository, PersonsRepository>();

   services.AddScoped<ICountriesGetterService, CountriesGetterService>();
   services.AddScoped<ICountriesAdderService, CountriesAdderService>();
   services.AddScoped<ICountriesUploaderService, CountriesUploaderService>();

   services.AddScoped<IPersonsGetterService, PersonsGetterServiceWithFewExcelFields>();
   services.AddScoped<PersonsGetterService, PersonsGetterService>();

   services.AddScoped<IPersonsAdderService, PersonsAdderService>();
   services.AddScoped<IPersonsDeleterService, PersonsDeleterService>();
   services.AddScoped<IPersonsUpdaterService, PersonsUpdaterService>();
   services.AddScoped<IPersonsSorterService, PersonsSorterService>();

   //Enable Identity in this project
   services.AddIdentity<ApplicationUser, ApplicationRole>(options=>
   {
       options.Password.RequiredLength = 5;
       options.Password.RequireNonAlphanumeric = false;
       options.Password.RequireUppercase = false;
       options.Password.RequireLowercase = true;
       options.Password.RequireDigit = false;
       options.Password.RequiredUniqueChars = 3; //Eg: AB12AB
   })
   .AddEntityFrameworkStores<ApplicationDbContext>()
   .AddDefaultTokenProviders()
   .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
   .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();


	services.AddDbContext<ApplicationDbContext>(options =>
   {
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
   });

   services.AddTransient<PersonsListActionFilter>();

   services.AddHttpLogging(options =>
   {
    options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
   });

   return services;
  }
 }
}
