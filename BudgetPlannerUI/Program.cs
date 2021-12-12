using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BudgetPlannerUI.Services;
using BudgetPlannerUI.Interfaces;
using Blazored.Toast;
using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
using BudgetPlannerUI.Providers;
using Microsoft.AspNetCore.Components.Authorization;

namespace BudgetPlannerUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp =>
                new HttpClient
                {
                    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                }
            );

            _ = new JwtHeader();
            _ = new JwtPayload(); 

            builder.Services.AddBlazoredToast();
            builder.Services.AddBlazoredLocalStorage();


            builder.Services.AddScoped<ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
                provider.GetRequiredService<ApiAuthenticationStateProvider>());
            builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();


            builder.Services.AddScoped<JwtSecurityTokenHandler>();

            builder.Services.AddTransient<IBudgetGroupsDataService, BudgetGroupsDataServices>();
            builder.Services.AddTransient<IBudgetCategoriesDataService, BudgetCategoriesDataServices>();
            builder.Services.AddTransient<IBudgetItemsDataService, BudgetItemsDataServices>();
            builder.Services.AddTransient<IBudgetCyclesDataService, BudgetCyclesDataServices>();
            builder.Services.AddTransient<IBudgetCycleItemsDataService, BudgetCycleItemsDataServices>();
            builder.Services.AddTransient<IMemorizedTransactionsDataService, MemorizedTransactionsDataServices>();
            builder.Services.AddTransient<IRegistersDataService, RegistersDataServices>();
            builder.Services.AddTransient<IRegisterEntriesDataService, RegisterEntriesDataServices>();
            builder.Services.AddTransient<BudgetPlannerUI.Interfaces.ILocalStorageService, BudgetPlannerUI.Services.LocalStorageService>();

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            await builder.Build().RunAsync();
        }
    }
}
