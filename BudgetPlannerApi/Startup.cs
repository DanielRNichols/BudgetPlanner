using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using BudgetPlanner.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using BudgetPlannerApi.DataTransfer;
using AutoMapper;
using BudgetPlannerApi.Interfaces;
using BudgetPlannerApi.Services;
using BudgetPlannerApi.Services.ControllerHelpers;
using BudgetPlannerApi.Services.Repositories;

namespace BudgetPlanner
{
    public class Startup
    {
        private readonly string CorsPolicyName = "CorsPolicy";
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>() // (options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // AutoMapper
            services.AddAutoMapper(typeof(DataMaps));

            // Cors Policy
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName,
                    policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            // SwaggerGen Swashbuckle
            services.AddSwaggerGen(cfg =>
            {
                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Budget Planner API",
                    Version = "v1",
                    Description = "Budget Planner API"
                });
                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlFullPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);

                cfg.IncludeXmlComments(xmlFullPath);
            });

            services.AddSingleton<ILoggerService, LoggerService>();

            // Repositories Dependency Injection
            services.AddScoped<IBudgetItemTypeRepository, BudgetItemTypeRepository>();
            services.AddScoped<IBudgetItemGroupRepository, BudgetItemGroupRepository>();
            services.AddScoped<IBudgetItemRepository, BudgetItemRepository>();
            services.AddScoped<IMemorizedTransactionRepository, MemorizedTransactionRepository>();
            services.AddScoped<IRegisterRepository, RegisterRepository>();
            services.AddScoped<IRegisterEntryRepository, RegisterEntryRepository>();
            services.AddScoped<IRegisterSplitEntryRepository, RegisterSplitEntryRepository>();
            services.AddScoped<IBudgetCycleRepository, BudgetCycleRepository>();
            services.AddScoped<IBudgetCycleItemRepository, BudgetCycleItemRepository>();

            // Controller Helpers Dependency Injection
            services.AddScoped<IBudgetItemTypesControllerHelper, BudgetItemTypesControllerHelper>();
            services.AddScoped<IBudgetItemGroupsControllerHelper, BudgetItemGroupsControllerHelper>();
            services.AddScoped<IBudgetItemsControllerHelper, BudgetItemsControllerHelper>();
            services.AddScoped<IMemorizedTransactionsControllerHelper, MemorizedTransactionsControllerHelper>();
            services.AddScoped<IRegistersControllerHelper, RegistersControllerHelper>();
            services.AddScoped<IRegisterEntriesControllerHelper, RegisterEntriesControllerHelper>();
            services.AddScoped<IRegisterSplitEntriesControllerHelper, RegisterSplitEntriesControllerHelper>();
            services.AddScoped<IBudgetCyclesControllerHelper, BudgetCyclesControllerHelper>();
            services.AddScoped<IBudgetCycleItemsControllerHelper, BudgetCycleItemsControllerHelper>();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
 
            // Cors Policy
            app.UseCors(CorsPolicyName);

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI(cfg =>
            {
                cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "BudgetPlannerAPI");
                cfg.RoutePrefix = "";
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
