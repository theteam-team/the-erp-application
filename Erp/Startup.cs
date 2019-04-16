using Erp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Erp.Interfaces;
using Erp.Models;
using Erp.Repository;

namespace Erp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            
            Configuration = configuration;
        }

        /// <summary>
        /// used to access the configuration of the web host
        /// </summary>
        public IConfiguration Configuration { get; }
      
        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// which is used to implement the dependancy injection pattern
        /// </summary>
        /// <param name="services">The Services container </param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IOpportunityRepository, OpportunityRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            //add the database contexts to the services Containers to use them by the dependency injection
            services.AddDbContext<AccountsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<DataDbContext>();
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
                    .AddEntityFrameworkStores<AccountsDbContext>()
                    .AddDefaultTokenProviders();
            //add the management as a scoped service ,a new object per each new request, to the service Container to use it by the dependency injection 
            services.AddScoped<Management>();
            //enahance the management system by adding the policy-based-authorization
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CreateUsers", policy => policy.RequireRole("Adminstrator"));
            });
            //add the session services to enable the store of the user data in memory beyond the http request life span
            services.AddSession();
            services.AddDistributedMemoryCache();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //Configure the API info and description
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "ERP API",
                    Description = "A public API for the team-team ERP system",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = "https://twitter.com/spboyer"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });


        }

        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AccountsDbContext mcontext)
        {
            
            mcontext.Database.EnsureCreated(); //ensure that the database used to store user accounts is created at the begining
            app.UseAuthentication();//enable the use of the Authentication of the http request
            app.UseNodeModules(env);//include the Node modules File into hte the response
            app.UseStaticFiles();//mark wwwroot Files as servable
            app.UseSession();//enable the use of the session storge 
            app.UseMvc(cfg =>
            {

                cfg.MapRoute("default", "{controller}/{action}/{id?}", new { controller = "App", action = "index" });

            });//enable the use of MVC
            app.UseSwagger(); //enable the use of Swagger To document the api
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });//configure the swagger endpoint 
        }
    }

}
