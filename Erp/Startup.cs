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

            services.AddDbContext<AccountsDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<DataDbContext>();
            services.AddScoped<Management>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("CreateUsers", policy => policy.RequireRole("Adminstrator"));
            });
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
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
            app.UseAuthentication();
            app.UseNodeModules(env);
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(cfg =>
            {

                cfg.MapRoute("default", "{controller}/{action}/{id?}", new { controller = "App", action = "index" });

            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }

}
