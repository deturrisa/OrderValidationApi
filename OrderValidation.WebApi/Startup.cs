using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;
using OrderValidation.Basket.Validation;
using OrderValidation.ChildOrder.Validation;
using OrderValidation.Client;
using OrderValidation.Client.Clients;
using OrderValidation.Client.Clients.Interfaces;
using OrderValidation.Client.Global;
using OrderValidation.Common.DateTimeWrapper;
using OrderValidation.Currency.Validation;
using OrderValidation.Service;

namespace OrderValidation.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<ICurrencyValidationService, CurrencyValidationService>();
            services.AddScoped<IPortfolioValidationService, PortfolioValidationService>();
            services.AddScoped<IDateTimeWrapper, DateTimeWrapper>();
            services.AddScoped<IStockIdValidationService, StockIdValidationService>();
            services.AddScoped<IClientValidationFactory, ClientValidationFactory>();
            
            RegisterClients(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterClients(IServiceCollection services)
        {
            services.AddScoped<IGlobalValidationService, GlobalValidationService>();
            services.AddScoped<IClientAValidationService, ClientAValidationService>();
            services.AddScoped<IClientBValidationService, ClientBValidationService>();
            services.AddScoped<IClientCValidationService, ClientCValidationService>();
        }
    }
}
