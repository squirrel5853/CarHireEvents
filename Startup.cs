using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CarRentalService.Data;
using CarRentalService.Data.Services;
using CarRentalService.Data.Handlers;
using CarRentalService.Data.Events;
using static CarRentalService.Pages.VerficationCodeListener;
using static CarRentalService.Pages.LogListener;

namespace CarRentalService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSingleton<Authentication>();
            services.AddSingleton<CustomerService>();
            services.AddSingleton<CarService>();
            services.AddSingleton<RentalService>();
            services.AddSingleton<CustomerRegistrationService>();

            services.AddSingleton<IEventHandler<NewCustomerSignupEvent>, NewCustomerSignupEventHandler>();
            services.AddSingleton<IEventHandler<LogEvent>, LogEventHandler>();
            services.AddSingleton<IEventHandler<CustomerVerificationGeneratedEvent>, CustomerVerificationGeneratedEventHandler>();

            services.AddSingleton<IEventHandler, NewCustomerSignupEventHandler>();
            services.AddSingleton<IEventHandler, LogEventHandler>();
            services.AddSingleton<IEventHandler, CustomerVerificationGeneratedEventHandler>();
            services.AddSingleton<IEventHandler, VerifiedCustomerEventHandler>();
            services.AddSingleton<IEventHandler, LogEventListener>();

            services.AddSingleton<CommandHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            EventDispatcher.Instance.Configure(app.ApplicationServices);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
