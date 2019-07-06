using Erp.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Erp.BackgroundServices
{
    public class SchemaBuilder : BackgroundService
    {
        private IAuthenticationSchemeProvider _authenticationProvider;
        private ILogger<SchemaBuilder> _logger;
        private IServiceProvider services;

        public SchemaBuilder(IServiceProvider services  ,ILogger<SchemaBuilder> logger ,IAuthenticationSchemeProvider authenticationProvider)
        {

            _authenticationProvider = authenticationProvider;
            _logger = logger;
            this.services = services;
        }
        protected override  async Task ExecuteAsync(CancellationToken stoppingToken)
        { 
            await Task.Run(() =>
            {
                 createSchemas();
            });
            while (!stoppingToken.IsCancellationRequested)
            {

            }
        }

        public void CreateDatabase()
        {
            _logger.LogInformation("Checking The database");
            using (var scope = services.CreateScope())
            {
                var accountDbContext = scope.ServiceProvider.GetRequiredService<AccountDbContext>();
                accountDbContext.Database.EnsureCreated();
            }
        }
        public  void createSchemas()
        {
            using (var scope = services.CreateScope())
            {
                var accountDbContext = scope.ServiceProvider.GetRequiredService<AccountDbContext>();
                var organizations = accountDbContext.Organizations;
                foreach (var organization in organizations)
                {
                    _logger.LogInformation("Creating Authentication Schema for organization : " + organization.Name + "");
                    _authenticationProvider.AddScheme(new AuthenticationScheme(organization.Name, organization.Name, typeof(CookieAuthenticationHandler)));
                       


                    var _sysServices = scope.ServiceProvider.GetRequiredService<IServiceCollection>();
                    var shema = _sysServices.AddAuthentication()
                     .AddCookie(organization.Name, o =>
                     {
                         o.ExpireTimeSpan = TimeSpan.FromHours(1);
                         o.LoginPath = new PathString("/store/{OrganizationName}");
                         o.Cookie.Name = organization.Name + " CustomerCookie";
                         o.SlidingExpiration = true;
                     });

                }
            }
        }
    }
}

