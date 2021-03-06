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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BudgetPlannerApi.Data;

namespace BudgetPlanner
{
    public class Startup
    {
        private const string CfgVarConnectionString = "BUDGET_PLANNER_API_CONNECTION_STRING";
        private readonly string CorsPolicyName = "CorsPolicy";

        private readonly ILoggerService _loggerService;
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _loggerService = new ConsoleLoggerService();
        }

        private string GetConnectionString()

        {
            string connStr = System.Environment.GetEnvironmentVariable(CfgVarConnectionString);
            if (string.IsNullOrWhiteSpace(connStr))
                connStr = Configuration.GetConnectionString("DefaultConnection");

            _loggerService.LogInfo($"ConnectionString={connStr}");

            return connStr;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(GetConnectionString()));
            services.AddDefaultIdentity<IdentityUser>() // (options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Http Context Accessor to get user in controller
            services.AddHttpContextAccessor();

            // AutoMapper
            services.AddAutoMapper(typeof(DataMaps));

            // JWT Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:issuer"],
                        ValidAudience = Configuration["Jwt:issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:key"]))

                    };

                });


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
                _loggerService.LogInfo($"Swagger xml file = {xmlFullPath}");
                if (System.IO.File.Exists(xmlFullPath))
                    cfg.IncludeXmlComments(xmlFullPath);
                else
                    _loggerService.LogWarn($"Unable to locate {xmlFullPath}");
            });

            // Using ConsoleLoggerService temporarily to until path issues for NLog configuration with Docker 
            services.AddSingleton<ILoggerService, ConsoleLoggerService>(); //LoggerService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            // Repositories Dependency Injection
            services.AddScoped<IBudgetGroupRepository, BudgetGroupRepository>();
            services.AddScoped<IBudgetCategoryRepository, BudgetCategoryRepository>();
            services.AddScoped<IBudgetItemRepository, BudgetItemRepository>();
            services.AddScoped<IMemorizedTransactionRepository, MemorizedTransactionRepository>();
            services.AddScoped<IRegisterRepository, RegisterRepository>();
            services.AddScoped<IRegisterEntryRepository, RegisterEntryRepository>();
            services.AddScoped<IRegisterSplitEntryRepository, RegisterSplitEntryRepository>();
            services.AddScoped<IBudgetCycleRepository, BudgetCycleRepository>();
            services.AddScoped<IBudgetCycleItemRepository, BudgetCycleItemRepository>();

            // Controller Helpers Dependency Injection
            services.AddScoped<IBudgetGroupsControllerHelper, BudgetGroupsControllerHelper>();
            services.AddScoped<IBudgetCategoriesControllerHelper, BudgetCategoriesControllerHelper>();
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
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
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

            SeedData.Seed(userManager, roleManager).Wait();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
