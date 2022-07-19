using EventBus.Base;
using EventBus.Base.Abstrasctions;
using EventBus.Factory;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PaymentService.Api.IntegrationEvents.EventHandler;
using PaymentService.Api.IntegrationEvents.Events;

namespace PaymentService.Api
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaymentService.Api", Version = "v1" });
            });
            services.AddTransient<OrderStatedIntegrationEventHandler>();
            services.AddSingleton<IEventBus>(sp =>
            {
                EventBusConfig conig = new()
                {
                    ConnectionRetryCount = 5,
                    EventNameSuffix = "IntegrationEvent",
                    SubscriptionClinetAppName = "PaymnetService",
                    eventBusType = EventBusType.RabbitMq
                };
                return EventBusFactory.Create(conig, sp);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentService.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.SubScribe<OrderStatedIntegrationEvent, OrderStatedIntegrationEventHandler>();
        }
    }
}